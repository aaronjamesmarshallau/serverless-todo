using System.Text.Json.Serialization;

namespace ServerlessTodo.Models
{
	class CreateResponse
	{
		[JsonPropertyName("success")]
		public bool Success { get; set; }

		[JsonPropertyName("message")]
		public string Message { get; set; }
	}
}