// Copyright (c) 2024 TrakHound Inc., All Rights Reserved.
// TrakHound Inc. licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Threading.Tasks;
using TrakHound.Entities;

namespace TrakHound.Drivers.Entities
{
    /// <summary>
    /// Entity Driver specifically for reading the Current TrakHound State Entities for the specified Object UUID.
    /// </summary>
    public interface IObjectStateLatestDriver : ITrakHoundDriver
    {
        Task<TrakHoundResponse<ITrakHoundObjectStateEntity>> Latest(IEnumerable<string> objectUuids);
    }
}
