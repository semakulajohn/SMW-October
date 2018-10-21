using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.Models;

namespace SMW.BAL.Interface
{
 public   interface IFacilityService
    {
        IEnumerable<Facility> GetAllFacilities();
        Facility GetFacility(long facilityId);
        long SaveFacility(Facility facility, string userId);
        void MarkAsDeleted(long facilityId, string userId);
        IEnumerable<FacilityType> GetAllFacilityTypes();
        IEnumerable<Facility> GetAllFacilitiesForAParticularFacilityType(long FacilityTypeId);
        IEnumerable<RentalPeriod> GetAllRentalPeriods();
        IEnumerable<Facility> GetLatestFacilities();
    }
}
