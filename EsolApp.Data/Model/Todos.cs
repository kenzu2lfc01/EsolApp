using EsolApp.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EsolApp.Data.Model
{
    public class Todos : DomainEntity<int>
    {
        public string TodoName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
