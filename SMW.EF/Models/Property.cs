//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMW.EF.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Property
    {
        public long PropertyId { get; set; }
        public System.Guid PropertyGuid { get; set; }
        public System.Guid VersionGuid { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public long PropertyTypeId { get; set; }
        public long DraftId_ { get; set; }
        public long MediaFolderId { get; set; }
        public double PropertyFee { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime Timestamp { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public string PublishedBy { get; set; }
        public Nullable<System.DateTime> PublishedOn { get; set; }
        public string UnPublishedBy { get; set; }
        public Nullable<System.DateTime> UnPublishedOn { get; set; }
        public bool Published { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        public virtual AspNetUser AspNetUser2 { get; set; }
        public virtual AspNetUser AspNetUser3 { get; set; }
        public virtual AspNetUser AspNetUser4 { get; set; }
        public virtual Media Medium { get; set; }
        public virtual PropertyType PropertyType { get; set; }
    }
}
