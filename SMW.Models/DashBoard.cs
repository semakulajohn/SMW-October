using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMW.Models
{
 public   class DashBoard
    {
        public int NumberOfCompletedProjects { get; set; }
        public int NumberOfOnGoingProjects { get; set; }
        public int NumberOfPropertiesForSale { get; set; }
        public int NumberOfPropertiesForRent { get; set; }
        public int NumberOfClients { get; set; }
        public int NumberOfExternalUsers { get; set; }
        public int NumberOfUnRespondedToWebQueries { get; set; }
        

    }
}
