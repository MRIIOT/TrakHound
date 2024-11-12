// Copyright (c) 2024 TrakHound Inc., All Rights Reserved.
// TrakHound Inc. licenses this file to you under the MIT license.

namespace TrakHound.Routing
{
    interface IRouteRouterTarget : IRouteTarget
    {
        TrakHoundRouter Router { get; set; }
    }
}
