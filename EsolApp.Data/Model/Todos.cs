using EsolApp.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace EsolApp.Data.Model
{
    public class Todos : DomainEntity<int>
    {
        public string TodoName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public Guid UserId { get; set; }
        public virtual IEnumerable<TodoShare> TodoShares { get; set; }
        public virtual IEnumerable<Images> Images { get; set; } 
    }
}
