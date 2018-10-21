using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMW.Models.ViewModels
{
   public class PropertyViewModel
    {
        public long PropertyId { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string TypeName { get; set; }
        public long MediaFolderId { get; set; }
        public double PropertyFee { get; set; }
        
    }
}
