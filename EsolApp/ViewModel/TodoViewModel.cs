using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsolApp.ViewModel
{
    public class TodoViewModel
    {
        public int Id { get; set; }
        public string TodoName { get; set; }
        public string Description { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool Status { get; set; }
        public Guid UserId { get; set; }
        public List<ImageViewModel> ImageViewModels { get; set; }
    }
}
