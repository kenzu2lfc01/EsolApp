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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace EsolApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly IImageService _imageService;
        private readonly IJwtService _jwtService;

        public TodoController(ITodoService todoService, IImageService imageService, IJwtService jwtService)
        {
            _todoService = todoService;
            _imageService = imageService;
            _jwtService = jwtService;

        }
        [HttpGet]
        public ActionResult<IEnumerable<TodoViewModel>> GetAllTodos()
        {
           return Ok(_todoService.GetAllTodos().ToList());
        }

        [HttpGet]
        [Route("get")]
        public ActionResult<IEnumerable<TodoViewModel>> GetTodos()
        {
            string token = this.HttpContext.Request.Headers["Authorization"];
            IEnumerable<Claim> claims = _jwtService.GetClaim(token);
            Guid UserID = Guid.Parse(claims.FirstOrDefault().Value);
            List<TodoViewModel> lstTodoOutput = new List<TodoViewModel>();
            var todos = _todoService.GetAllTodos().Where(x=> x.UserId == UserID).ToList();
            foreach (var item in todos)
            {
                TodoViewModel todoOutput = new TodoViewModel()
                {
                    Id = item.Id,
                    TodoName = item.TodoName,
                    Description = item.Description,
                    ModifyDate = item.ModifyDate,
                    Status = item.Status,
                    ImageViewModels = _imageService.GetImageByTodoId(item.Id)
                };
                lstTodoOutput.Add(todoOutput);
            }
            return Ok(lstTodoOutput);
        }
        [HttpGet]
        [Route("getshare")]
        public ActionResult<IEnumerable<TodoViewModel>> GetTodoShares()
        {
            string token = this.HttpContext.Request.Headers["Authorization"];
            IEnumerable<Claim> claims = _jwtService.GetClaim(token);
            Guid UserID = Guid.Parse(claims.FirstOrDefault().Value);
            return _todoService.GetTodoShare(UserID);
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
            string token = this.HttpContext.Request.Headers["Authorization"];
            IEnumerable<Claim> claims = _jwtService.GetClaim(token);
            Guid UserID = Guid.Parse(claims.FirstOrDefault().Value);
            Todos todos = new Todos()
            {
                TodoName = todo.TodoName,
                Description = todo.Description,
                Status = false,
                UserId = UserID,
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.Now
            };
            _todoService.AddTodo(todos);
            //return HttpResponse();
            return CreatedAtAction("GetTodos", new { id = todos.Id }, todo);
        }
        [HttpPut]
        public ActionResult PatchTodo([FromBody]TodoPatchViewModel todoPatchViewModel)
        {
            bool check = _todoService.UpdateTodo(todoPatchViewModel);
            if (!check)
            {
                return BadRequest("Update fail");
            }
            return Ok("Update success");
        }
        
        [HttpPost]
        [Route("share")]
        public ActionResult ShareTodo(ShareTodoViewModel shareTodo)
        {
            if(shareTodo == null)
            {
                return BadRequest();
            }
            _todoService.ShareTodo(shareTodo);
            return Ok();
        }
        // DELETE: api/Todo/5
        [HttpDelete]
        [Route("delete/{id}")]
        public void DeleteTodos(int id)
        {
            _todoService.DeleteTodo(id);
            _imageService.DeleteImageTodo(id);
        }
    }
}
