using System;
using System.Collections.Generic;
using System.Text;

namespace EsolApp.Data.Common
{
    public class Auditable : IAuditable
    {
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyBy { get; set; }
    }
}
