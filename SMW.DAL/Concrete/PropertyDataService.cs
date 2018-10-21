using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.EF.Models;
using SMW.DAL.Concrete;
using SMW.DAL.Interface;
using SMW.EF.UnitOfWork;
using SMW.DTO;
using log4net;

namespace SMW.DAL.Concrete
{
  public  class PropertyDataService : DataServiceBase,IPropertyDataService
    {
      ILog logger = log4net.LogManager.GetLogger(typeof(PropertyDataService));

       public PropertyDataService(IUnitOfWork<SMWEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<Property> GetAllDraftProperties()
        {
            return this.UnitOfWork.Get<Property>().AsQueryable().Where(e => e.Deleted == false && e.Published == false); 
        }

        public IEnumerable<Property> GetAllPublishedProperties()
        {
            return this.UnitOfWork.Get<Property>().AsQueryable().Where(e => e.Deleted == false && e.Published == true);
        }
        public IEnumerable<Property> GetAllPropertiesForAParticularUser(string frontUserId)
        {
            return this.UnitOfWork.Get<Property>().AsQueryable().Where(e => e.Deleted == false && e.CreatedBy == frontUserId);
        }
        public IEnumerable<PropertyType> GetAllPropertyTypes()
        {
            return this.UnitOfWork.Get<PropertyType>().AsQueryable();
        }

        public IEnumerable<Property> GetAllPropertiesForAParticularPropertyType(long propertyTypeId)
        {
            return this.UnitOfWork.Get<Property>().AsQueryable().Where(e => e.Deleted == false && e.PropertyTypeId == propertyTypeId && e.Published == true);
        }
        public Property GetProperty(long propertyId)
        {
            return this.UnitOfWork.Get<Property>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.PropertyId == propertyId &&
                    c.Deleted == false
                );
        }

      
      
        public Property GetDraftProperty(long draftPropertyId)
        {
            return this.UnitOfWork.Get<Property>().AsQueryable()
                .FirstOrDefault(m =>
                    m.PropertyId == draftPropertyId &&
                    m.Deleted == false
                );
        }


        public long SaveProperty(PropertyDTO propertyDTO, string userId)
        {
            long propertyId = 0;

            if (propertyDTO.PropertyId == 0)
            {
                long mediaFolderId = 0;

                var media = new Media
                {
                    MediaGuid = Guid.NewGuid(),
                    //ParentId = rootGalleryId,
                    Name = propertyDTO.Description,
                    MediaTypeId = (long)SMW.Library.EnumTypes.MediaType.Folder,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    Deleted = false
                };

                this.UnitOfWork.Get<Media>().AddNew(media);
                this.UnitOfWork.SaveChanges();
                mediaFolderId = media.MediaId;

                var property = new Property()
                {

                    PropertyTypeId = propertyDTO.PropertyTypeId,
                    MediaFolderId = mediaFolderId,
                    Location = propertyDTO.Location,
                    PropertyFee = propertyDTO.PropertyFee,
                    Description = propertyDTO.Description,
                    Published = false,
                    UnPublishedBy = propertyDTO.UnPublishedBy,
                    PublishedBy = propertyDTO.PublishedBy,
                    UnPublishedOn = propertyDTO.UnPublishedOn,
                    PublishedOn = propertyDTO.PublishedOn,
                    PropertyGuid = Guid.NewGuid(),
                    VersionGuid = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                    Timestamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,


                };

                this.UnitOfWork.Get<Property>().AddNew(property);
                this.UnitOfWork.SaveChanges();
                propertyId = property.PropertyId;
                return propertyId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Property>().AsQueryable()
                    .FirstOrDefault(e => e.PropertyId == propertyDTO.PropertyId);
                if (result != null)
                {
                    result.PropertyTypeId = propertyDTO.PropertyTypeId;
                    result.Location = propertyDTO.Location;
                    result.PropertyFee = propertyDTO.PropertyFee;
                    result.Description = propertyDTO.Description;
                    result.Timestamp = DateTime.Now;
                    result.MediaFolderId = propertyDTO.MediaFolderId;
                    result.Deleted = propertyDTO.Deleted;
                    result.VersionGuid = propertyDTO.VersionGuid;
                    result.PropertyGuid = propertyDTO.PropertyGuid;
                    result.DeletedBy = propertyDTO.DeletedBy;
                    result.DeletedOn = propertyDTO.DeletedOn;
                    result.UpdatedBy = userId;
                    result.Published = propertyDTO.Published;
                    result.PropertyGuid = propertyDTO.PropertyGuid;
                    result.VersionGuid = propertyDTO.VersionGuid;
                    result.PublishedBy = propertyDTO.PublishedBy;
                    result.UnPublishedBy = propertyDTO.UnPublishedBy;
                    result.PublishedOn = propertyDTO.PublishedOn;
                    result.UnPublishedOn = propertyDTO.UnPublishedOn;

                    this.UnitOfWork.Get<Property>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return propertyDTO.PropertyId;
            }

           
           
        }

       
        public void UnPublish(long draftPropertyId, string userId)
        {
            
            using (var dbContext = new SMWEntities())
            {
                dbContext.Property_UnPublish(draftPropertyId);
            }
        }

     

        public void MarkAsDeleted(long draftPropertyId, string userId)
        {
            //First unpublish Property
            UnPublish(draftPropertyId, userId);

            var property = (from n in this.UnitOfWork.Get<Property>().AsQueryable()
                         where n.PropertyId == draftPropertyId
                         select n
                            ).FirstOrDefault();
            if (property != null)
            {
                property.DeletedOn = DateTime.Now;
                property.Deleted = true;
                property.DeletedBy = userId;
                this.UnitOfWork.Get<Property>().Update(property);
                this.UnitOfWork.SaveChanges();
            }
        }

      
    
    }
}
