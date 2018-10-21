using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.Models;

namespace SMW.BAL.Interface
{
  public  interface IServiceService
    {

        IEnumerable<Service> GetAllServices();
        Service GetService(long serviceId);
        long SaveService(Service service, string userId);
        void MarkAsDeleted(long serviceId, string userId);
       
    }
}
