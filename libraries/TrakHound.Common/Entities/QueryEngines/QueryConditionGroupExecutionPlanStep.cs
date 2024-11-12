// Copyright (c) 2024 TrakHound Inc., All Rights Reserved.
// TrakHound Inc. licenses this file to you under the MIT license.

using System.Collections.Generic;

namespace TrakHound.Entities.QueryEngines
{
    public class QueryConditionGroupExecutionPlanStep : QueryExecutionPlanStep
    {
        public TrakHoundConditionGroupOperator GroupOperator { get; set; }

        public List<QueryCondition> Conditions { get; set; }
    }
}
