// Copyright (c) 2024 TrakHound Inc., All Rights Reserved.
// TrakHound Inc. licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Threading.Tasks;
using TrakHound.Entities;

namespace TrakHound.Drivers.Entities
{
    /// <summary>
    /// Driver used to read TrakHound Boolean Entities.
    /// </summary>
    public interface IObjectBooleanQueryDriver : ITrakHoundDriver
    {
        /// <summary>
        /// Query the Boolean Entities with the specified Object UUID's
        /// </summary>
        Task<TrakHoundResponse<ITrakHoundObjectBooleanEntity>> Query(IEnumerable<string> objectUuids);
    }
}
