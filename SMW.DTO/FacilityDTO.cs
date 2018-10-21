using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMW.DTO
{
 public   class FacilityDTO
    {
        public long FacilityId { get; set; }
        public string Description { get; set; }
        public string Dimensions { get; set; }
        public string Location { get; set; }
        public long FacilityTypeId { get; set; }
        public Nullable<long> RentalPeriodId { get; set; }
        public long MediaFolderId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime Timestamp { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
    }
}
