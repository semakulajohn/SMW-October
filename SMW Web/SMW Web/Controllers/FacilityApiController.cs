using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMW.BAL.Interface;
using log4net;
using SMW.Models;

namespace SMW_Web.Controllers
{
    public class FacilityApiController : ApiController
    {

        private IFacilityService _facilityService;
        
        ILog logger = log4net.LogManager.GetLogger(typeof(FacilityApiController));
       
        public FacilityApiController()
        {
        }

        public FacilityApiController(IFacilityService facilityService)
        {
            this._facilityService = facilityService;
           
           
        }

        [HttpGet]
        [ActionName("GetFacility")]
        public Facility GetFacility(long facilityId)
        {
            return _facilityService.GetFacility(facilityId);
        }

        [HttpGet]
        [ActionName("GetLatestFacilities")]
        public IEnumerable<Facility> GetLatestFacilities()
        {
            return _facilityService.GetLatestFacilities();
        }

        [HttpGet]
        [ActionName("GetAllFacilityTypes")]
        public IEnumerable<FacilityType> GetAllFacilityTypes()
        {
            return _facilityService.GetAllFacilityTypes();
        }

        [HttpGet]
        [ActionName("GetAllRentalPeriods")]
        public IEnumerable<RentalPeriod> GetAllRentalPeriods()
        {
            return _facilityService.GetAllRentalPeriods();
        }
       
    }
}
