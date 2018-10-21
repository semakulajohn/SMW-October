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
    [Authorize]
    public class PropertyApiController : ApiController
    {
         private IPropertyService _propertyService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(PropertyApiController));
            private string userId = string.Empty;

            public PropertyApiController()
            {
            }

            public PropertyApiController(IPropertyService propertyService,IUserService userService)
            {
                this._propertyService = propertyService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetProperty")]
            public Property GetProperty(long propertyId)
            {
                return _propertyService.GetProperty(propertyId);
            }

            [HttpGet]
            [ActionName("GetAllProperties")]
            public IEnumerable<Property> GetAllProperties()
            {
                return _propertyService.GetAllProperties();
            }

            [HttpGet]
            [ActionName("GetAllPublishedProperties")]
            public IEnumerable<Property> GetAllPublishedProperties()
            {
                return _propertyService.GetAllPublishedProperties();
            }

        [HttpGet]
        [ActionName("GetAllPropertiesForAParticularUser")]
            public IEnumerable<Property> GetAllPropertiesForAParticularUser(string frontUserId)
            {
                return _propertyService.GetAllPropertiesForAParticularUser(frontUserId);
            }

            [HttpGet]
            [ActionName("GetAllPropertyTypes")]
            public IEnumerable<PropertyType> GetAllPropertyTypes()
            {
                return _propertyService.GetAllPropertyTypes();
            }

            [HttpGet]
            [ActionName("Delete")]
            public void DeleteProperty(long propertyId)
            {
                _propertyService.MarkAsDeleted(propertyId, userId);
            }

            [HttpPost]
            [ActionName("Save")]
            public long Save(Property model)
            {

                var draftPropertyId = _propertyService.SaveProperty(model, userId);
                //if (Convert.ToBoolean(model.Published))
                //{
                //    _propertyService.Publish(draftPropertyId,userId);
                //}
                return draftPropertyId;
            }

          

            [HttpGet]
            [ActionName("UnPublish")]
            public void UnPublish(long draftPropertyId)
            {
                _propertyService.UnPublish(draftPropertyId, userId);
            }

    }
}
