using EsolApp.Data.Model;
using EsolApp.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsolApp.Data.Repositories.Todo
{
    public class TodoRepository : Repository<Todos, int>, ITodoRepository
    {
        private readonly EsolAppDbContext _context;
        public TodoRepository(EsolAppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
