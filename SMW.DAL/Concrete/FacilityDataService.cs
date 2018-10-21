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
  public  class FacilityDataService : DataServiceBase,IFacilityDataService
    {
        
      ILog logger = log4net.LogManager.GetLogger(typeof(FacilityDataService));

       public FacilityDataService(IUnitOfWork<SMWEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<Facility> GetAllFacilities()
        {
            return this.UnitOfWork.Get<Facility>().AsQueryable().Where(e => e.Deleted == false); 
        }


       
        public IEnumerable<FacilityType> GetAllFacilityTypes()
        {
            return this.UnitOfWork.Get<FacilityType>().AsQueryable();
        }
        public IEnumerable<RentalPeriod> GetAllRentalPeriods()
        {
            return this.UnitOfWork.Get<RentalPeriod>().AsQueryable();
        }

        public IEnumerable<Facility> GetAllFacilitiesForAParticularFacilityType(long facilityTypeId)
        {
            return this.UnitOfWork.Get<Facility>().AsQueryable().Where(e => e.Deleted == false && e.FacilityTypeId == facilityTypeId);
        }
        public Facility GetFacility(long facilityId)
        {
            return this.UnitOfWork.Get<Facility>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.FacilityId == facilityId &&
                    c.Deleted == false
                );
        }

        /// <summary>
        /// Saves a new Facility or updates an already existing Facility.
        /// </summary>
        /// <param name="Facility">Facility to be saved or updated.</param>
        /// <param name="FacilityId">FacilityId of the Facility creating or updating</param>
        /// <returns>FacilityId</returns>
        public long SaveFacility(FacilityDTO facilityDTO, string userId)
        {
            long facilityId = 0;
            
            if (facilityDTO.FacilityId == 0)
            {
                long mediaFolderId = 0;

                var media = new Media
                {
                    MediaGuid = Guid.NewGuid(),
                    //ParentId = rootGalleryId,
                    Name = facilityDTO.Description,
                    
                    MediaTypeId = (long)SMW.Library.EnumTypes.MediaType.Folder,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    Deleted = false
                };

                this.UnitOfWork.Get<Media>().AddNew(media);
                this.UnitOfWork.SaveChanges();
                mediaFolderId = media.MediaId;
           
                var facility = new Facility()
                {
                   
                    FacilityTypeId = facilityDTO.FacilityTypeId,
                    MediaFolderId = mediaFolderId,
                    Location = facilityDTO.Location,
                    Dimensions = facilityDTO.Dimensions,
                    RentalPeriodId = facilityDTO.RentalPeriodId,
                    
                    Description = facilityDTO.Description,
                    CreatedOn = DateTime.Now,
                    Timestamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,
 

                };

                this.UnitOfWork.Get<Facility>().AddNew(facility);
                this.UnitOfWork.SaveChanges();
                facilityId = facility.FacilityId;
                return facilityId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Facility>().AsQueryable()
                    .FirstOrDefault(e => e.FacilityId == facilityDTO.FacilityId);
                if (result != null)
                {
                    result.FacilityTypeId = facilityDTO.FacilityTypeId; 
                    result.Location = facilityDTO.Location;
                    result.RentalPeriodId = facilityDTO.RentalPeriodId;
                    result.Dimensions = facilityDTO.Dimensions;
                    result.Description = facilityDTO.Description;
                    result.Timestamp = DateTime.Now;
                    result.MediaFolderId = facilityDTO.MediaFolderId;
                    result.Deleted = facilityDTO.Deleted;
                    result.DeletedBy = facilityDTO.DeletedBy;
                    result.DeletedOn = facilityDTO.DeletedOn;
                    result.UpdatedBy = userId;
                    
 
                    this.UnitOfWork.Get<Facility>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return facilityDTO.FacilityId;
            }
        }

        public void MarkAsDeleted(long facilityId,string userId)
        {
            var result = this.UnitOfWork.Get<Facility>().AsQueryable().Where(e => e.Deleted == false && e.FacilityId == facilityId);
            if (result != null)
            {

                //using (var dbContext = new SMWEntities())
                //{
                //    dbContext.Mark_Facility_And_Related_DataAs_Deleted(FacilityId, userId);
                //}
            }
        }
    }
}
