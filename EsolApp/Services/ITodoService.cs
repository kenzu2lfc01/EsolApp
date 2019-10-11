using EsolApp.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsolApp.Services
{
    public interface ITodoService
    {
        List<Todos> GetAllTodos();
        void AddTodo(Todos todos);
        void DeleteTodo(int Id);
        void UpdateStatus(int Id);
    }
}
