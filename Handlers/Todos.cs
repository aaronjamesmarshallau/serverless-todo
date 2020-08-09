
using ServerlessTodo.Models;

namespace ServerlessTodo.Handlers 
{
	class Todos 
	{
		public LambdaResponse<Todo[]> Get() 
		{
			return LambdaResponse<Todo[]>.Ok(new [] 
			{
				new Todo
				{
					Summary = "Make to-do app",
					Description = "Build a to-do app using the serverless/serverless framework",
					Tags = new[] {
						"spike",
						"difficult"
					},
					IsComplete = false,
				},
				new Todo
				{
					Summary = "Cook dinner",
					Description = "Cook some spaghetti bolognese",
					Tags = new[] {
						"life",
						"easy"
					},
					IsComplete = false,
				},
				new Todo
				{
					Summary = "Do groceries",
					Description = "Go to woolies and get:\n- Carrots\n- Potatoes\n- Mince Meat",
					Tags = new[] {
						"groceries",
						"easy"
					},
					IsComplete = false,
				}
			});
		}
	}
}