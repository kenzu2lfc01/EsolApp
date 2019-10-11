using EsolApp.Data.Model;
using EsolApp.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsolApp.Data.Repositories.Todo
{
    public interface ITodoRepository : IRepository<Todos,int>
    {
    }
}
