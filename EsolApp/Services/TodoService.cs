using EsolApp.Data.Model;
using EsolApp.Data.Repositories.Todo;
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

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
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
    }
}
