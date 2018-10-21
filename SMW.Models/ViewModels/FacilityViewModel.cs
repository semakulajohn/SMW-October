using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMW.Models.ViewModels
{
 public   class FacilityViewModel
    {
        public long FacilityId { get; set; }
        public string Description { get; set; }
        public string Dimensions { get; set; }
        public string Location { get; set; }
        public long MediaFolderId { get; set; }

        public string FacilityTypeName { get; set; }
        public string RentalPeriodName { get; set; }
    }
}
