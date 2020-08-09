namespace ServerlessTodo.Models
{
	public class Todo 
	{
		public string Summary { get; set; }
		public string Description { get; set; }
		public string[] Tags { get; set; }
		public bool IsComplete { get; set; }
	}
}