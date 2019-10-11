using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EsolApp.Data;
using EsolApp.Data.Model;
using EsolApp.Services;
using EsolApp.ViewModel;

namespace EsolApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: api/Todo
        [HttpGet]
        public ActionResult<IEnumerable<Todos>> GetTodos()
        {
            return  Ok(_todoService.GetAllTodos());
        }
    
        // POST: api/Todo
        [HttpPost]
        public ActionResult<Todos> PostTodos([FromBody]TodoViewModel todo)
        {
            Todos todos = new Todos()
            {
                TodoName = todo.TodoName,
                Description = todo.Description,
                Status = todo.Status,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.Now
            };
            _todoService.AddTodo(todos);
            return CreatedAtAction("GetTodos", new { id = todos.Id }, todo);
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public void DeleteTodos(int id)
        {
            _todoService.DeleteTodo(id);
        }
    }
}
