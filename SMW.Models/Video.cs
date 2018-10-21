using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMW.Models
{
 public   class Video
    {
        public long VideoId { get; set; }
        public System.Guid VideoGuid { get; set; }
        public System.Guid VersionGuid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public Nullable<long> ProjectId { get; set; }
        public string ClientId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> TimeStamp { get; set; }
        public bool Deleted { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }
        public string UnPublishedBy { get; set; }
        public Nullable<System.DateTime> PublishedOn { get; set; }
        public bool Published { get; set; }
        public string PublishedBy { get; set; }
        public Nullable<System.DateTime> ScheduledPublishOn { get; set; }
        public Nullable<System.DateTime> UnPublishOn { get; set; }
        public Nullable<System.DateTime> ScheduledUnPublishOn { get; set; }
    }
}
