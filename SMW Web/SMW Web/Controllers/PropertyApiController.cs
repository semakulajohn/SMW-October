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
    public class PropertyApiController : ApiController
    {
        private IPropertyService _propertyService;
       
        ILog logger = log4net.LogManager.GetLogger(typeof(PropertyApiController));
        private string userId = string.Empty;

        public PropertyApiController()
        {
        }

        public PropertyApiController(IPropertyService propertyService)
        {
            this._propertyService = propertyService;
         
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
        [ActionName("GetAllPropertyTypes")]
        public IEnumerable<PropertyType> GetAllPropertyTypes()
        {
            return _propertyService.GetAllPropertyTypes();
        }

        [HttpGet]
        [ActionName("GetLatestFourProperties")]
        public IEnumerable<Property> GetLatestFourProperties()
        {
            return _propertyService.GetLatestProperties();
        }

        [HttpGet]
        [ActionName("GetFeaturedProperties")]
        public IEnumerable<Property> GetFeaturedProperties()
        {
            return _propertyService.GetFeaturedProperties();
        }

        [HttpGet]
        [ActionName("GetPropertiesForAParticularPropertyType")]
        public IEnumerable<Property> GetAllPropertiesForAParticularPropertyType(long propertyTypeId)
        {
            return _propertyService.GetAllPropertiesForAParticularPropertyType(propertyTypeId);
        }
    }
}
