using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Test_ASP.NETCore_WebAPI_EF.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TodoController : ControllerBase
	{
		// GET api/todo
		[HttpGet]
		public ActionResult<IEnumerable<string>> GetTodoList()
		{
			return new string[] { "todo 1", "todo 2" };
		}
	}
}
