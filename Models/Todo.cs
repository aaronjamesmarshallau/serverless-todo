using System.Text.Json.Serialization;

namespace ServerlessTodo.Models
{
	public class Todo 
	{
		[JsonPropertyName("summary")]
		public string Summary { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }

		[JsonPropertyName("tags")]
		public string[] Tags { get; set; }

		[JsonPropertyName("isComplete")]
		public bool IsComplete { get; set; }
	}
}