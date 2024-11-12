// Copyright (c) 2024 TrakHound Inc., All Rights Reserved.
// TrakHound Inc. licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrakHound.Clients;

namespace TrakHound.Entities.QueryEngines.Evaluators.Entities
{
    public class ObjectAssignmentEvaluator : EntityEvaluator, IEvaluator
    {
        public ObjectAssignmentEvaluator(ConditionObjectQuery query, ITrakHoundObjectEntityModel conditionObject)
        {
            ContentType = conditionObject.ContentType.ConvertEnum<TrakHoundObjectContentType>();
            TargetObject = query.TargetObject;
            ConditionObject = conditionObject;
            ConditionGroup = query.ConditionGroup;
            Condition = query.Condition;
        }


        public bool Evaluate()
        {
            return false;
        }


        public static async Task<IEnumerable<IEvaluatorResult>> Evaluate(QueryStatement statement, ITrakHoundSystemEntitiesClient client, IEnumerable<ObjectAssignmentEvaluator> evaluators)
        {
            var results = new List<IEvaluatorResult>();

            if (statement != null && !statement.Select.IsNullOrEmpty() && client != null && !evaluators.IsNullOrEmpty())
            {
                IEnumerable<ITrakHoundObjectAssignmentEntityModel> entities;

                if (statement.Start > 0 || statement.Stop > 0)
                {
                    var startUnix = statement.Start ?? 0;
                    if (startUnix < 1) startUnix = 0;

                    var stopUnix = statement.Stop ?? 0;
                    if (stopUnix < 1) stopUnix = long.MaxValue;

                    entities = await client.Objects.Assignment.QueryModelsByAssigneeUuid(evaluators.Select(o => o.ConditionObject.Uuid), startUnix, stopUnix);
                }
                else
                {
                    entities = await client.Objects.Assignment.CurrentModelsByAssigneeUuid(evaluators.Select(o => o.ConditionObject.Uuid));
                }

                if (!entities.IsNullOrEmpty())
                {
                    foreach (var entity in entities)
                    {
                        var entityEvaluators = evaluators.Where(o => o.ConditionObject.Uuid == entity.AssigneeUuid);
                        if (!entityEvaluators.IsNullOrEmpty())
                        {
                            foreach (var entityEvaluator in entityEvaluators)
                            {
                                var value = statement.Scope.GetVariable(entityEvaluator.Condition.Value)?.Value;

                                if (ProcessValue(entityEvaluator.Condition.Operator, entity.Member, value))
                                {
                                    foreach (var select in statement.Select)
                                    {
                                        var selectQuery = statement.Scope.GetVariable(select.Expression);
                                        if (selectQuery != null)
                                        {
                                            var result = new EntityEvaluatorResult();
                                            result.Id = Guid.NewGuid().ToString();
                                            result.ConditionId = entityEvaluator.Condition.Id;
                                            result.GroupId = entityEvaluator.ConditionObject?.Uuid;
                                            result.Success = true;
                                            result.Evaluator = entityEvaluator;
                                            result.Entity = entity;
                                            results.Add(result);

                                            //var selectTarget = TrakQL.Match(selectQuery.Value, entityEvaluator.ConditionObject)?.FirstOrDefault();
                                            //if (selectTarget == null) selectTarget = TrakQL.Match(selectQuery.Value, entity.Object)?.FirstOrDefault();
                                            //if (selectTarget != null)
                                            //{
                                            //    var result = new EntityEvaluatorResult();
                                            //    result.Id = Guid.NewGuid().ToString();
                                            //    result.ConditionId = entityEvaluator.Condition.Id;
                                            //    result.ValueId = selectTarget.Uuid;
                                            //    result.Success = true;
                                            //    result.Evaluator = entityEvaluator;
                                            //    result.Entity = entity;
                                            //    results.Add(result);
                                            //}
                                        }
                                    }
                                }
                                else
                                {
                                    var result = new EntityEvaluatorResult();
                                    result.Id = Guid.NewGuid().ToString();
                                    result.ConditionId = entityEvaluator.Condition.Id;
                                    result.Success = false;
                                    results.Add(result);
                                }
                            }
                        }
                    }
                }
                else
                {
                    //foreach (var entityQuery in observationQueries)
                    //{
                    //    entityResults.Add(new EntityResult(entityQuery, null));
                    //}
                }
            }

            return results;
        }

        private static bool ProcessValue(TrakHoundConditionOperator operatorType, ITrakHoundObjectEntityModel target, string pattern)
        {
            if (target != null)
            {
                var match = TrakHoundObjectExpression.IsMatch(pattern, target);               

                switch (operatorType)
                {
                    case TrakHoundConditionOperator.EQUALS: return match;
                    case TrakHoundConditionOperator.NOT_EQUALS: return !match;
                }
            }

            return false;
        }
    }
}
