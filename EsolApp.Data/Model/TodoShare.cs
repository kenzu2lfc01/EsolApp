using EsolApp.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsolApp.Data.Model
{
    public class TodoShare : DomainEntity<int>
    {
        public Todos Todos { get; set; }
        public Guid UserId { get; set; }
    }
}
