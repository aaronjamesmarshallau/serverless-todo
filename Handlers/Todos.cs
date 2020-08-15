
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.APIGatewayEvents;
using ServerlessTodo.Models;

namespace ServerlessTodo.Handlers 
{
	class Todos 
	{
		private static AmazonDynamoDBClient GetDynamoClient()
		{
			var awsRegionRaw = Environment.GetEnvironmentVariable("AWS_REGION");
			var dynamoConfig = new AmazonDynamoDBConfig()
			{
				RegionEndpoint = RegionEndpoint.GetBySystemName(awsRegionRaw),
			};
			
			var client = new AmazonDynamoDBClient(dynamoConfig);
			return client;
		}

		private static string GetTodoTableName() 
		{
			return Environment.GetEnvironmentVariable("TODO_TABLE_NAME");
		}

		private static Table GetTodoTable(AmazonDynamoDBClient client)
		{
			var tableNameRaw = GetTodoTableName();
			var todoTable = Table.LoadTable(client, tableNameRaw);

			return todoTable;
		}

		public async Task<LambdaResponse<Todo[]>> Get() 
		{
			var client = GetDynamoClient();
			var todoTableName = GetTodoTableName();

			var request = new ScanRequest
			{
				TableName = todoTableName,
				Limit = 10,
			};

			var response = await client.ScanAsync(request);
			var results = response.Items.Select(Todo.FromDynamo).ToArray();

			return LambdaResponse<Todo[]>.Ok(results);
		}

		public async Task<LambdaResponse<CreateResponse>> Create(APIGatewayProxyRequest request) 
		{
			var inboundObject = JsonSerializer.Deserialize<Todo>(request?.Body);

			inboundObject.IdRaw = Guid.NewGuid().ToString();

			if (string.IsNullOrWhiteSpace(inboundObject.Summary)) 
			{
				return LambdaResponse<CreateResponse>.BadRequest(new CreateResponse 
				{
					Success = false,
					Message = "Failed to create todo item - summary is required."
				});
			}

			if (string.IsNullOrWhiteSpace(inboundObject.Description))
			{
				inboundObject.Description = inboundObject.Summary;
			}
			
			var insertableObject = Todo.ToDynamo(inboundObject);
			var client = GetDynamoClient();
			var todoTableName = GetTodoTableName();

			var dynamoRequest = new PutItemRequest(todoTableName, insertableObject);
			var putResult = await client.PutItemAsync(dynamoRequest);

			if (putResult.HttpStatusCode != HttpStatusCode.OK) 
			{
				return LambdaResponse<CreateResponse>.InternalServerError(new CreateResponse
				{
					Success = false,
					Message = $"Failed to create todo: {putResult.HttpStatusCode.ToString()}"
				});
			}

			return LambdaResponse<CreateResponse>.Ok(new CreateResponse
			{
				Success = true,
				Message = "Successfully created item."
			});
		}
	}
}