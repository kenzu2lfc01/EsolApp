using EsolApp.Data.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EsolApp.Data.Model
{
    public class Images : DomainEntity<int>
    {
         public string Base64Image { get; set; }

        public Todos Todo { get; set; }

    }
}
