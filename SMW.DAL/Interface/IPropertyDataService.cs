using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.DTO;
using SMW.EF.Models;

namespace SMW.DAL.Interface
{
   public interface IPropertyDataService
    {
        
       IEnumerable<Property> GetAllDraftProperties();
       IEnumerable<Property> GetAllPublishedProperties();
       IEnumerable<Property> GetAllPropertiesForAParticularUser(string frontUserId);
       IEnumerable<PropertyType> GetAllPropertyTypes();
       IEnumerable<Property> GetAllPropertiesForAParticularPropertyType(long propertyTypeId);
       Property GetProperty(long propertyId);    
       Property GetDraftProperty(long draftPropertyId);
       long SaveProperty(PropertyDTO propertyDTO, string userId);
      //long Publish(long draftPropertyId, string userId);
      void UnPublish(long draftPropertyId, string userId);
      void MarkAsDeleted(long draftPropertyId, string userId);
     
       

    }
}
