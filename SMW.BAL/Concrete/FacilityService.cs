using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.DTO;
using SMW.BAL.Interface;
using SMW.DAL.Interface;
using SMW.Models;
using SMW.Helpers;
using log4net;

namespace SMW.BAL.Concrete
{
 public   class FacilityService : IFacilityService
    {
        
        ILog logger = log4net.LogManager.GetLogger(typeof(FacilityService));
        private IFacilityDataService _dataService;
        private IUserService _userService;
        private IMediaService _mediaService;
       
        

        public FacilityService(IFacilityDataService dataService,IUserService userService,IMediaService mediaService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._mediaService = mediaService;
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FacilityId"></param>
        /// <returns></returns>
        public Facility GetFacility(long facilityId)
        {
            var result = this._dataService.GetFacility(facilityId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Facility> GetAllFacilities()
        {
            var results = this._dataService.GetAllFacilities();
            return MapEFToModel(results);
        }

        public IEnumerable<Facility> GetLatestFacilities()
        {
            var results = GetAllFacilities().OrderByDescending(p => p.CreatedOn).Take(6);
            return results;
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Facility> GetAllFacilitiesForAParticularFacilityType(long facilityTypeId)
        {
            var results = this._dataService.GetAllFacilitiesForAParticularFacilityType(facilityTypeId);
            return MapEFToModel(results);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FacilityType> GetAllFacilityTypes()
        {
            var results = this._dataService.GetAllFacilityTypes();
            return MapEFToModel(results);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RentalPeriod> GetAllRentalPeriods()
        {
            var results = this._dataService.GetAllRentalPeriods();
            return MapEFToModel(results);
        }

        public long SaveFacility(Facility facility, string userId)
        {
            var facilityDTO = new DTO.FacilityDTO()
            {
                FacilityId = facility.FacilityId,
                Location = facility.Location,
                Dimensions = facility.Dimensions,
                RentalPeriodId = facility.RentalPeriodId,
                MediaFolderId = facility.MediaFolderId,
                Description = facility.Description,
                FacilityTypeId = facility.FacilityTypeId,
                
                    

            };

           var facilityId = this._dataService.SaveFacility(facilityDTO, userId);

           return facilityId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FacilityId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long facilityId, string userId)
        {
            _dataService.MarkAsDeleted(facilityId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<Facility> MapEFToModel(IEnumerable<EF.Models.Facility> data)
        {
            var list = new List<Facility>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }


        public FacilityType MapEFToModel(EF.Models.FacilityType data)
        {


            var facilityType = new FacilityType()
            {
                FacilityTypeId = data.FacilityTypeId,
                Name = data.Name,


            };
            return facilityType;
        }


        public RentalPeriod MapEFToModel(EF.Models.RentalPeriod data)
        {


            var rentalPeriod = new RentalPeriod()
            {
                RentalPeriodId = data.RentalPeriodId,
                Name = data.Name,


            };
            return rentalPeriod;
        }

        private IEnumerable<RentalPeriod> MapEFToModel(IEnumerable<EF.Models.RentalPeriod> data)
        {
            var list = new List<RentalPeriod>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }
        private IEnumerable<FacilityType> MapEFToModel(IEnumerable<EF.Models.FacilityType> data)
        {
            var list = new List<FacilityType>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }
        /// <summary>
        /// Maps Facility EF object to Facility Model Object and
        /// returns the Facility model object.
        /// </summary>
        /// <param name="result">EF Facility object to be mapped.</param>
        /// <returns>Facility Model Object.</returns>
        public Facility MapEFToModel(EF.Models.Facility data)
        {
            var facilityTypeName = string.Empty;
            var rentalPeriodName = string.Empty;

            if (data.FacilityId != 0)
            {

               
                if (data.FacilityType != null)
                {
                    facilityTypeName = data.FacilityType.Name;
                }
                

            }
            if (data.RentalPeriodId != 0)
            {


                if (data.RentalPeriod != null)
                {
                    rentalPeriodName = data.RentalPeriod.Name;
                }


            }
            var facility = new Facility()
            {
                FacilityId = data.FacilityId,
                Location = data.Location,
                Description = data.Description,
                Dimensions = data.Dimensions,
                RentalPeriodId = data.RentalPeriodId,
                FacilityTypeId = data.FacilityTypeId,
                MediaFolderId = data.MediaFolderId,
                CreatedOn = data.CreatedOn,
                Timestamp = data.Timestamp,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),
                FacilityTypeName = facilityTypeName,
                RentalPeriodName = rentalPeriodName,

            };
            if (data.Medium != null)
            {
                var media = new Media()
                {
                    MediaId = data.Medium.MediaId,
                    MediaGuid = data.Medium.MediaGuid,
                    MediaTypeId = data.Medium.MediaTypeId,
                    Name = data.Medium.Name
                };
                facility.Media = media;
            }
            facility.MediaForView = GetFirstImageForFacility(data.MediaFolderId);
            return facility;
        }



       #endregion

        private Media GetFirstImageForFacility(long parentId)
        {
            Media mediaObject = new Media();
            var mediaRecords = _mediaService.GetFilesInFolder(parentId);
            if (mediaRecords.Any())
            {
                mediaObject = mediaRecords.FirstOrDefault();
                var media = new Media()
                {
                    MediaId = mediaObject.MediaId,
                    MediaGuid = mediaObject.MediaGuid,
                    MediaTypeId = mediaObject.MediaTypeId,
                    Name = mediaObject.Name
                };
                return media;
            }
            return mediaObject;
        }

    }
}
