﻿// Copyright (c) 2024 TrakHound Inc., All Rights Reserved.
// TrakHound Inc. licenses this file to you under the MIT license.

using System;
using System.Text.Json.Serialization;

namespace TrakHound.Functions
{
	public class FunctionLog
	{
		[JsonPropertyName("level")]
		[JsonConverter(typeof(JsonStringEnumConverter))]
		[JsonIgnore(Condition = JsonIgnoreCondition.Never)]
		public TrakHoundLogLevel Level { get; set; }

		[JsonPropertyName("message")]
		public string Message { get; set; }

		[JsonPropertyName("timestamp")]
		public DateTime Timestamp { get; set; }
	}
}