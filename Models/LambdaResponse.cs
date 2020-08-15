using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.Lambda.Core;

[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace ServerlessTodo.Models
{
	[DataContract]
	public class LambdaResponse<T>
	{
		[JsonPropertyName("isBase64Encoded")]
		public bool IsBase64Encoded { get; set; }

		[JsonPropertyName("statusCode")]
		public HttpStatusCode StatusCode { get; set; }

		[JsonPropertyName("headers")]
		public Dictionary<string, string> Headers { get; set; }

		[JsonIgnore]
		public T Body { get; set; }

		[JsonPropertyName("body")]
		public string BodyContent { 
			get 
			{
				return JsonSerializer.Serialize(Body);
			} 
		}

		private LambdaResponse(T body) 
		{
			Body = body;
			StatusCode = HttpStatusCode.OK;
			IsBase64Encoded = false;
			Headers = new Dictionary<string, string>
			{
				{ "Access-Control-Allow-Origin", "*" },
				{ "Access-Control-Allow-Credentials", "true" },
			};
		}

		public static LambdaResponse<T> Ok(T body) 
		{
			var response = new LambdaResponse<T>(body);
			return response;
		}

		public static LambdaResponse<T> InternalServerError(T body) 
		{
			var response = new LambdaResponse<T>(body)
			{
				StatusCode = HttpStatusCode.InternalServerError,
			};
			return response;
		}

		public static LambdaResponse<T> BadRequest(T body) 
		{
			var response = new LambdaResponse<T>(body)
			{
				StatusCode = HttpStatusCode.BadRequest,
			};
			return response;
		}
	}
}