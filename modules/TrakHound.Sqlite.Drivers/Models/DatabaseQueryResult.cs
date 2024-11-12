﻿// Copyright (c) 2024 TrakHound Inc., All Rights Reserved.
// TrakHound Inc. licenses this file to you under the MIT license.

namespace TrakHound.Sqlite.Drivers.Models
{
    public class DatabaseQueryResult
    {
        public string Query { get; set; }

        public string Uuid { get; set; }

        public string ParentUuid { get; set; }

        public string RequestedParentUuid { get; set; }
    }
}
