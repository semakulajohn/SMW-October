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
   public class ServiceDataService : DataServiceBase,IServiceDataService
    {
         ILog logger = log4net.LogManager.GetLogger(typeof(ServiceDataService));

       public ServiceDataService(IUnitOfWork<SMWEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<Service> GetAllServices()
        {
            return this.UnitOfWork.Get<Service>().AsQueryable().Where(e => e.Deleted == false); 
        }




        public Service GetService(long serviceId)
        {
            return this.UnitOfWork.Get<Service>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.ServiceId == serviceId &&
                    c.Deleted == false
                );
        }

        /// <summary>
        /// Saves a new Service or updates an already existing Service.
        /// </summary>
        /// <param name="Service">Service to be saved or updated.</param>
        /// <param name="ServiceId">ServiceId of the Service creating or updating</param>
        /// <returns>ServiceId</returns>
        public long SaveService(ServiceDTO serviceDTO, string userId)
        {
            long serviceId = 0;
            
            if (serviceDTO.ServiceId == 0)
            {
                long mediaFolderId = 0;

                var media = new Media
                {
                    MediaGuid = Guid.NewGuid(),
                    //ParentId = rootGalleryId,
                    Name = serviceDTO.Title,
                    MediaTypeId = (long)SMW.Library.EnumTypes.MediaType.Folder,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    Deleted = false
                };

                this.UnitOfWork.Get<Media>().AddNew(media);
                this.UnitOfWork.SaveChanges();
                mediaFolderId = media.MediaId;
           
                var service = new Service()
                {
                    
                    Title = serviceDTO.Title, 
                    MediaFolderId = mediaFolderId,
                    Description = serviceDTO.Description,
                    CreatedOn = DateTime.Now,
                    Timestamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,
 

                };

                this.UnitOfWork.Get<Service>().AddNew(service);
                this.UnitOfWork.SaveChanges();
                serviceId = service.ServiceId;
                return serviceId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Service>().AsQueryable()
                    .FirstOrDefault(e => e.ServiceId == serviceDTO.ServiceId);
                if (result != null)
                {
                    result.Title = serviceDTO.Title; 
                    result.Description = serviceDTO.Description;
                    result.MediaFolderId = serviceDTO.MediaFolderId;
                    result.Timestamp = DateTime.Now;
                    result.Deleted = serviceDTO.Deleted;
                    result.DeletedBy = serviceDTO.DeletedBy;
                    result.DeletedOn = serviceDTO.DeletedOn;
                    result.UpdatedBy = userId;
                    result.CreatedBy = serviceDTO.CreatedBy;
 
                    this.UnitOfWork.Get<Service>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return serviceDTO.ServiceId;
            }
            return serviceId;
        }

        public void MarkAsDeleted(long serviceId,string userId)
        {
            var result = this.UnitOfWork.Get<Service>().AsQueryable().Where(e => e.Deleted == false && e.ServiceId == serviceId);
            if (result != null)
            {

                using (var dbContext = new SMWEntities())
                {
                    dbContext.Mark_Service_And_RelatedData_AsDeleted(serviceId, userId);
                }
            }
        }
    }
}
