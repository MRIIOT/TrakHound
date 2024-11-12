// Copyright (c) 2024 TrakHound Inc., All Rights Reserved.
// TrakHound Inc. licenses this file to you under the MIT license.

namespace TrakHound.Routing
{
    public interface ITrakHoundRouterInformation
    {
        string Id { get; }

        string Name { get; }

        string Description { get; }
    }
}