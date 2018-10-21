using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.EF.Models;
using SMW.DTO;

namespace SMW.DAL.Interface
{
 public   interface IServiceDataService
    {
        IEnumerable<Service> GetAllServices();
        Service GetService(long serviceId);
        long SaveService(ServiceDTO service, string userId);
        void MarkAsDeleted(long serviceId, string userId);
       
    }
}
