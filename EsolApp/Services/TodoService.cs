using EsolApp.Data.Model;
using EsolApp.Data.Repositories.Todo;
using EsolApp.Data.Repositories.Todo.ShareTodo;
using EsolApp.Data.Repository;
using EsolApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsolApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IShareTodoRepository _shareTodoRepository;
        private readonly IImageService _imageService;

        public TodoService(ITodoRepository todoRepository, IShareTodoRepository shareTodoRepository, IImageService imageService)
        {
            _todoRepository = todoRepository;
            _shareTodoRepository = shareTodoRepository;
            _imageService = imageService;
        }
        public List<Todos> GetAllTodos()
        {
            return _todoRepository.FindAll().ToList();
        }
        public void AddTodo(Todos todos)
        {
             _todoRepository.Add(todos);
            _todoRepository.Commit();
        }
        public void DeleteTodo(int Id)
        {
            _todoRepository.Remove(Id);
            _todoRepository.Commit();
        }
        public void UpdateStatus(int Id)
        {
            var todo = _todoRepository.FindById(Id);
            todo.Status = true;
            _todoRepository.Commit();
        }
        public bool UpdateTodo(TodoPatchViewModel todoPatchViewModel)
        {
            var todo = _todoRepository.FindById(todoPatchViewModel.Id);
            if (!String.IsNullOrEmpty(todoPatchViewModel.TodoName))
            {
                todo.TodoName = todoPatchViewModel.TodoName;
            }
            if (!String.IsNullOrEmpty(todoPatchViewModel.Description))
            {
                todo.Description = todoPatchViewModel.Description;
            }
            else
            {
                return false ;
            }
            _todoRepository.Commit();
            return true;
        }
        public void ShareTodo(ShareTodoViewModel shareTodo)
        {
            Todos todos = _todoRepository.FindById(shareTodo.TodoId);
            foreach (var item in shareTodo.UserId)
            {
                TodoShare todoShare = new TodoShare()
                {
                    Todos = todos,
                    UserId = Guid.Parse(item)
                };
                _shareTodoRepository.Add(todoShare);
            }
            _shareTodoRepository.Commit();
        }
        public List<TodoViewModel> GetTodoShare(Guid UserId)
        {
            List<TodoShare> todoShare = _shareTodoRepository.FindAll().Where(x => x.UserId == UserId).ToList();
            List<Todos> todos = _todoRepository.FindAll().ToList();
            List<TodoViewModel> todoViewModels = new List<TodoViewModel>();
            foreach (var item in todoShare)
            {
                Todos todo = todos.Find(x => x.Id == item.Todos.Id);
                TodoViewModel todoViewModel = new TodoViewModel()
                {
                    Id = todo.Id,
                    TodoName = todo.TodoName,
                    Description = todo.Description,
                    ModifyDate = todo.ModifyDate,
                    Status = todo.Status,
                    ImageViewModels = _imageService.GetImageByTodoId(todo.Id)
                };
                todoViewModels.Add(todoViewModel);
            }
            return todoViewModels;
        }

    }
}
