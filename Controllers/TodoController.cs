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
	}
}
