using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Amazon.DynamoDBv2.Model;

namespace ServerlessTodo.Models
{
	public class Todo 
	{
		[JsonPropertyName("summary")]
		public string Summary { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }

		[JsonPropertyName("isComplete")]
		public bool IsComplete { get; set; }

		internal static Todo FromDynamo(Dictionary<string, AttributeValue> items)
		{
			return new Todo
			{
				Summary = items.GetValueOrDefault("summary")?.S,
				Description = items.GetValueOrDefault("description")?.S,
				IsComplete = items.GetValueOrDefault("isComplete")?.BOOL ?? false,
			};
		}
	}
}