// Copyright (c) 2024 TrakHound Inc., All Rights Reserved.
// TrakHound Inc. licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Threading.Tasks;
using TrakHound.Entities;

namespace TrakHound.Clients
{
    public partial interface ITrakHoundSystemObjectHashClient : ITrakHoundEntityClient<ITrakHoundObjectHashEntity>
    {
        Task<IEnumerable<ITrakHoundObjectHashEntity>> QueryByObject(
            string path,
            long skip = 0,
            long take = 1000,
            SortOrder sortOrder = TrakHound.SortOrder.Ascending,
            string routerId = null);

        Task<IEnumerable<ITrakHoundObjectHashEntity>> QueryByObject(
            IEnumerable<string> paths,
            long skip = 0,
            long take = 1000,
            SortOrder sortOrder = TrakHound.SortOrder.Ascending,
            string routerId = null);

        Task<IEnumerable<ITrakHoundObjectHashEntity>> QueryByObjectUuid(
            string objectUuid,
            long skip = 0,
            long take = 1000,
            SortOrder sortOrder = TrakHound.SortOrder.Ascending,
            string routerId = null);

        Task<IEnumerable<ITrakHoundObjectHashEntity>> QueryByObjectUuid(
            IEnumerable<string> objectUuids,
            long skip = 0,
            long take = 1000,
            SortOrder sortOrder = TrakHound.SortOrder.Ascending,
            string routerId = null);

        Task<ITrakHoundConsumer<IEnumerable<ITrakHoundObjectHashEntity>>> SubscribeByObject(
            string path,
            int interval = 0,
            int take = 1000,
            string consumerId = null,
            string routerId = null);

        Task<ITrakHoundConsumer<IEnumerable<ITrakHoundObjectHashEntity>>> SubscribeByObject(
            IEnumerable<string> paths,
            int interval = 0,
            int take = 1000,
            string consumerId = null,
            string routerId = null);

        Task<ITrakHoundConsumer<IEnumerable<ITrakHoundObjectHashEntity>>> SubscribeByObjectUuid(
            string objectUuid,
            int interval = 0,
            int take = 1000,
            string consumerId = null,
            string routerId = null);

        Task<ITrakHoundConsumer<IEnumerable<ITrakHoundObjectHashEntity>>> SubscribeByObjectUuid(
            IEnumerable<string> objectUuids,
            int interval = 0,
            int take = 1000,
            string consumerId = null,
            string routerId = null);

        Task<bool> DeleteByObjectUuid(
            string objectUuid,
            string routerId = null);

        Task<bool> DeleteByObjectUuid(
            IEnumerable<string> objectUuids,
            string routerId = null);

    }
}