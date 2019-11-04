using EsolApp.Data.Model;
using EsolApp.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsolApp.Data.Repositories.Todo.ShareTodo
{
    public class ShareTodoRepository: Repository<TodoShare,int>, IShareTodoRepository
    {
        public EsolAppDbContext _context;
        public ShareTodoRepository(EsolAppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
