using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMW.Models.ViewModels
{
  public  class ServiceViewModel
    {
        public long ServiceId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public long MediaFolderId { get; set; }
        
    }
}
