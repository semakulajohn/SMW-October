using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.Models;

namespace SMW.BAL.Interface
{
 public   interface IPropertyService
    {
        IEnumerable<Property> GetAllProperties();
        Property GetProperty(long propertyId);
        long SaveProperty(Property property, string userId);
        void MarkAsDeleted(long propertyId, string userId);
        IEnumerable<PropertyType> GetAllPropertyTypes();
        IEnumerable<Property> GetFeaturedProperties();
        IEnumerable<Property> GetLatestProperties();
        IEnumerable<Property> GetAllPropertiesForAParticularUser(string frontUserId);
        IEnumerable<Property> GetAllPropertiesForAParticularPropertyType(long propertyTypeId);
        IEnumerable<Property> GetAllPublishedProperties();
       // long Publish(long draftPropertyId, string userId);
        void UnPublish(long draftPropertyId, string userId);
    }
}
