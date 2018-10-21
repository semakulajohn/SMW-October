using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMW.BAL.Interface;
using log4net;
using SMW.Models;

namespace SMW.Admin.Controllers
{
    public class FacilityApiController : ApiController
    {
        
         private IFacilityService _facilityService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(FacilityApiController));
            private string userId = string.Empty;

            public FacilityApiController()
            {
            }

            public FacilityApiController(IFacilityService facilityService,IUserService userService)
            {
                this._facilityService = facilityService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetFacility")]
            public Facility GetFacility(long facilityId)
            {
                return _facilityService.GetFacility(facilityId);
            }

            [HttpGet]
            [ActionName("GetAllFacilities")]
            public IEnumerable<Facility> GetAllFacilities()
            {
                return _facilityService.GetAllFacilities();
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
            [HttpGet]
            [ActionName("Delete")]
            public void DeleteFacility(long facilityId)
            {
                _facilityService.MarkAsDeleted(facilityId, userId);
            }

            [HttpPost]
            [ActionName("Save")]
            public long Save(Facility model)
            {

                var facilityId = _facilityService.SaveFacility(model, userId);
                return facilityId;
            }
    }
}
