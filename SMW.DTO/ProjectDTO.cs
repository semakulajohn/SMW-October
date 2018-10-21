﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMW.DTO
{
  public  class ProjectDTO
    {
        public long ProjectId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string ClientId { get; set; }
        public long MediaFolderId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public long StatusId { get; set; }
        public System.DateTime Timestamp { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
      
    
    }
}
