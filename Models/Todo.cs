using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Amazon.DynamoDBv2.Model;

namespace ServerlessTodo.Models
{
	public class Todo 
	{
		private const string IdAttributeKey = "id";
		private const string SummaryAttributeKey = "summary";
		private const string DescriptionAttributeKey = "description";
		private const string IsCompleteAttributeKey = "isComplete";

		[JsonPropertyName(IdAttributeKey)]
		public string IdRaw { get; set; }

		public Guid Id 
		{ 
			get 
			{
				return Guid.TryParse(IdRaw, out var guid) ? guid : Guid.Empty;
			} 
		}

		[JsonPropertyName(SummaryAttributeKey)]
		public string Summary { get; set; }

		[JsonPropertyName(DescriptionAttributeKey)]
		public string Description { get; set; }

		[JsonPropertyName(IsCompleteAttributeKey)]
		public bool IsComplete { get; set; }

		internal static Todo FromDynamo(Dictionary<string, AttributeValue> entity)
		{
			return new Todo
			{
				IdRaw = entity.GetValueOrDefault(IdAttributeKey)?.S,
				Summary = entity.GetValueOrDefault(SummaryAttributeKey)?.S,
				Description = entity.GetValueOrDefault(DescriptionAttributeKey)?.S,
				IsComplete = entity.GetValueOrDefault(IsCompleteAttributeKey)?.BOOL ?? false,
			};
		}

		internal static Dictionary<string, AttributeValue> ToDynamo(Todo entity)
		{
			return new Dictionary<string, AttributeValue>
			{
				{IdAttributeKey, new AttributeValue { S = entity.IdRaw }},
				{SummaryAttributeKey, new AttributeValue { S = entity.Summary }},
				{DescriptionAttributeKey, new AttributeValue { S = entity.Description }},
				{IsCompleteAttributeKey, new AttributeValue { BOOL = entity.IsComplete }},
			};
		}
	}
}