// Copyright (c) 2024 TrakHound Inc., All Rights Reserved.
// TrakHound Inc. licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Threading.Tasks;
using TrakHound.Entities;

namespace TrakHound.Clients
{
    public partial interface ITrakHoundSystemDefinitionWikiClient : ITrakHoundEntityClient<ITrakHoundDefinitionWikiEntity>
    {
        Task<IEnumerable<ITrakHoundDefinitionWikiEntity>> QueryByDefinitionUuid(
            string definitionUuid,
            string routerId = null);

        Task<IEnumerable<ITrakHoundDefinitionWikiEntity>> QueryByDefinitionUuid(
            IEnumerable<string> definitionUuids,
            string routerId = null);

    }
}