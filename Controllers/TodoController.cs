using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TodoController : ControllerBase
	{
		private readonly TodoDbContext _dbContext;
		public TodoController(TodoDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		// GET api/todo
		[HttpGet]
		public ActionResult<string> Welcome()
		{
			return @"Welcome to the Todo API. Operations:
- GET /todo/list (list all todo's)
- GET /todo/id (get a todo item)
- POST /todo (add a new todo item)
- PUT /todo/id (update a todo item)
- PATCH /todo/id (set the IsComplete property of a todo item)
- DELETE /todo/id (delete a todo item)";
		}

		// GET api/todo/list
		[HttpGet("list")]
		public ActionResult<IEnumerable<TodoItem>> GetTodoList()
		{
			return _dbContext.TodoItems.ToList();
		}

		// GET api/todo/{id}
		[HttpGet("{id}")]
		public ActionResult<TodoItem> GetTodoItem(long id)
		{
			var todoItem = _dbContext.TodoItems.Find(id);

			if (todoItem == null)
				return NotFound();

			return todoItem;
		}

		// POST api/todo
		[HttpPost]
		public ActionResult<TodoItem> AddTodoItem(TodoItem item)
		{
			_dbContext.TodoItems.Add(item);
			_dbContext.SaveChanges();

			return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
		}

		// PUT api/todo/{id}
		[HttpPut("{id}")]
		public ActionResult UpdateTodoItem(long id, TodoItem item)
		{
			if (id != item.Id)
				return BadRequest();

			_dbContext.Entry(item).State = EntityState.Modified;
			_dbContext.SaveChanges();

			return NoContent();
		}

		// PATCH api/todo/{id}?isComplete=bool
		[HttpPatch("{id}")]
		public ActionResult PatchTodoItem(long id, [FromQuery] bool isComplete)
		{
			var todoItem = _dbContext.TodoItems.Find(id);

			if (todoItem == null)
				return NotFound();

			todoItem.IsComplete = isComplete;
			_dbContext.SaveChanges();

			return NoContent();
		}

		// DELETE api/todo/{id}
		[HttpDelete("{id}")]
		public ActionResult DeleteTodoItem(long id)
		{
			var todoItem = _dbContext.TodoItems.Find(id);

			if (todoItem == null)
				return NotFound();

			_dbContext.TodoItems.Remove(todoItem);
			_dbContext.SaveChanges();

			return NoContent();
		}
	}
}
