﻿// Copyright (c) 2024 TrakHound Inc., All Rights Reserved.
// TrakHound Inc. licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Threading.Tasks;
using TrakHound.Messages;

namespace TrakHound.Drivers
{
    public interface IMessageSubscribeDriver : ITrakHoundDriver
    {
        Task<TrakHoundResponse<ITrakHoundConsumer<TrakHoundMessageResponse>>> Subscribe(string brokerId, string clientId, IEnumerable<string> topics, int qos);
    }
}