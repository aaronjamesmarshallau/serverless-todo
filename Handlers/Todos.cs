
using System;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
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
	}
}