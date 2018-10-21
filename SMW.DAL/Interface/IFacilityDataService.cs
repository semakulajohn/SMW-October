using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.DTO;
using SMW.EF.Models;

namespace SMW.DAL.Interface
{
 public   interface IFacilityDataService
    {
        IEnumerable<Facility> GetAllFacilities();
        Facility GetFacility(long facilityId);
        long SaveFacility(FacilityDTO facility, string userId);
        void MarkAsDeleted(long facilityId, string userId);
        IEnumerable<FacilityType> GetAllFacilityTypes();
        IEnumerable<Facility> GetAllFacilitiesForAParticularFacilityType(long facilityTypeId);
        IEnumerable<RentalPeriod> GetAllRentalPeriods();
                
    }
}
