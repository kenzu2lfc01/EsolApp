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
        private readonly IImageService _imageService;
        public TodoController(ITodoService todoService, IImageService imageService)
        {
            _todoService = todoService;
            _imageService = imageService;
        }

        // GET: api/Todo
        [HttpGet]
        public ActionResult<IEnumerable<TodoViewModel>> GetTodos()
        {
            List<TodoViewModel> lstTodoOutput = new List<TodoViewModel>();
            var todos = _todoService.GetAllTodos();
            foreach (var item in todos)
            {
                TodoViewModel todoOutput = new TodoViewModel()
                {
                    Id = item.Id,
                    TodoName = item.TodoName,
                    Description = item.Description,
                    Status = item.Status,
                    ImageViewModels = _imageService.GetImageByTodoId(item.Id)
                };
                lstTodoOutput.Add(todoOutput);
            }
            return Ok(lstTodoOutput);
        }
        [HttpPost]
        [Route("update")]
        public void UpdateStatus()
        {
            int Id = Convert.ToInt32(Request.Form["Id"]);
            _todoService.UpdateStatus(Id);
        }
        // POST: api/Todo
        [HttpPost]
        [Route("create")]
        public ActionResult<Todos> PostTodos([FromBody]TodoViewModel todo)
        {
            Todos todos = new Todos()
            {
                TodoName = todo.TodoName,
                Description = todo.Description,
                Status = false,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.Now
            };
            _todoService.AddTodo(todos);
            //return HttpResponse();
            return CreatedAtAction("GetTodos", new { id = todos.Id }, todo);
        }

        // DELETE: api/Todo/5
        [HttpDelete]
        [Route("delete/{id}")]
        public void DeleteTodos(int id)
        {
            _todoService.DeleteTodo(id);
        }
    }
}
