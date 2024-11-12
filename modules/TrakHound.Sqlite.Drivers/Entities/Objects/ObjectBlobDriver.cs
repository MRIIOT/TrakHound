﻿// Copyright (c) 2024 TrakHound Inc., All Rights Reserved.
// TrakHound Inc. licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrakHound.Drivers;
using TrakHound.Drivers.Entities;
using TrakHound.Entities;
using TrakHound.Sqlite.Drivers.Models;

namespace TrakHound.Sqlite.Drivers
{
    public class ObjectBlobDriver : SqliteEntityDriver<ITrakHoundObjectBlobEntity, DatabaseObjectBlob>, IObjectBlobQueryDriver
    {
        public ObjectBlobDriver() { }

        public ObjectBlobDriver(ITrakHoundDriverConfiguration configuration) : base(configuration) 
        {
            EntityName = "trakhound_objects_blob";
            TableColumnList = new List<string> {
                "[uuid]",
                "[object_uuid]",
                "[blob_id]",
                "[content_type]",
                "[size]",
                "[filename]",
                "[source_uuid]",
                "[created]"
            };
        }

        class PublishItem
        {
            public string Uuid { get; set; }
            public string ObjectUuid { get; set; }
            public string BlobId { get; set; }
            public string ContentType { get; set; }
            public long Size { get; set; }
            public string Filename { get; set; }
            public string SourceUuid { get; set; }
            public long Created { get; set; }      
        }


        protected async override Task<bool> OnPublish(IEnumerable<ITrakHoundObjectBlobEntity> entities)
        {
            var items = new List<PublishItem>();
            foreach (var entity in entities)
            {
                var item = new PublishItem();
                item.Uuid = entity.Uuid;
                item.ObjectUuid = entity.ObjectUuid;
                item.BlobId = entity.BlobId;
                item.ContentType = entity.ContentType;
                item.Size = entity.Size;
                item.Filename = entity.Filename;
                item.SourceUuid = entity.SourceUuid;
                item.Created = entity.Created;
                items.Add(item);
            }

            _client.Insert(items, TableName, new string[] { "object_uuid" });

            return true;
        }

        public async Task<TrakHoundResponse<ITrakHoundObjectBlobEntity>> Query(IEnumerable<string> objectUuids)
        {
            Func<IEnumerable<string>, Task<IEnumerable<DatabaseObjectBlob>>> readFunction = async (ids) =>
            {
                var dbEntities = ExecuteConditionQuery<DatabaseObjectBlob>($"select {TableColumns} from {TableName} where", "object_uuid", objectUuids);
                if (!dbEntities.IsNullOrEmpty())
                {
                    foreach (var dbEntity in dbEntities)
                    {
                        dbEntity.RequestedId = dbEntity.ObjectUuid;
                    }
                }

                return dbEntities;
            };

            return await ProcessResponse(this, objectUuids, readFunction, QueryType.Object);
        }

        private IEnumerable<T> ExecuteConditionQuery<T>(string baseQuery, string key, IEnumerable<string> values, int chunkSize = 50)
        {
            var results = new List<T>();

            if (!string.IsNullOrEmpty(baseQuery) && !string.IsNullOrEmpty(key) && !values.IsNullOrEmpty())
            {
                var count = 0;
                var limit = values.Count();

                while (count < limit)
                {
                    var chunkValues = values.Skip(count).Take(chunkSize);

                    var conditions = new List<string>();
                    foreach (var value in chunkValues)
                    {
                        conditions.Add($"[{key}] = '{value}'");
                    }
                    var condition = string.Join(" or ", conditions);

                    var query = $"{baseQuery} {condition};";
                    var queryResults = _client.ReadList<T>(query);
                    if (!queryResults.IsNullOrEmpty())
                    {
                        results.AddRange(queryResults);
                    }

                    count += chunkSize;
                }
            }

            return results;
        }
    }
}