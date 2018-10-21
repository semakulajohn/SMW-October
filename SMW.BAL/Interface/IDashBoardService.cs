using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.Models;

namespace SMW.BAL.Interface
{
  public  interface IDashBoardService
    {

         int GetNumberOfPropertiesForSale();
       
         int GetNumberOfPropertiesForRent();
       
        int GetNumberOfOnGoingProjects();
       
         int GetNumberOfClients();
      
        int GetNumberOfCompletedProjects();

        int GetNumberOfExternalUsers();
        int GetNumberOfUnRespondedToWebQueries();
        DashBoard GetDashBoardData();
    }
}
