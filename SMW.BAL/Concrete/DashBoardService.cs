using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.BAL.Interface;
using SMW.DAL.Interface;
using SMW.Models;
using SMW.Helpers;
using log4net;

namespace SMW.BAL.Concrete
{
public    class DashBoardService : IDashBoardService
    {
      ILog logger = log4net.LogManager.GetLogger(typeof(DashBoardService));
        private IPropertyService _propertyService;
        private IUserService _userService;
    private IWebQueryService _webQueryService;
    private IProjectService _projectService;
       
        

        public DashBoardService(IPropertyService propertyService,IUserService userService,
           IWebQueryService webQueryService,IProjectService projectService )
        {
            this._propertyService = propertyService;
            this._userService = userService;
            this._webQueryService = webQueryService;
            this._projectService = projectService;
           
        }


        public int GetNumberOfPropertiesForSale()
        {
            int number = 0;
            long propertyTypeId=1;
            var saleProperties = _propertyService.GetAllPropertiesForAParticularPropertyType(propertyTypeId);
            if (saleProperties.Any())
            {
              number =  saleProperties.Count();
            }
            return number;
        }
        public int GetNumberOfPropertiesForRent()
        {
            int number = 0;
            long propertyTypeId = 2;
            var rentalProperties = _propertyService.GetAllPropertiesForAParticularPropertyType(propertyTypeId);
            if (rentalProperties.Any())
            {
                number = rentalProperties.Count();
            }
            return number;
        }
        public int GetNumberOfOnGoingProjects()
        {
            int numberProjects = 0;
            var onGoingProjects = _projectService.GetAllOnGoingProjects();
            if (onGoingProjects.Any())
            {
                numberProjects = onGoingProjects.Count();
            }
            return numberProjects;
        }
        public int GetNumberOfClients()
        {
            int numberClients = 0;
            var clients = _userService.GetAllClients();
            if (clients.Any())
            {
                numberClients = clients.Count();
            }
            return numberClients;
        }
        public int GetNumberOfCompletedProjects()
        {
            int numberProjects = 0;
            var completedProjects = _projectService.GetAllCompletedProjects();
            if (completedProjects.Any())
            {
                numberProjects = completedProjects.Count();
            }
            return numberProjects;
        }
        public int GetNumberOfExternalUsers()
        {
            int numberUsers = 0;
            var externalUsers = _userService.GetAllUsers();
            if (externalUsers.Any())
            {
                numberUsers = externalUsers.Count();
            }
            return numberUsers;
        }

        public int GetNumberOfUnRespondedToWebQueries()
        {
            int number = 0;
            var unRespondedWebQueries = _webQueryService.GetAllUnRespondedToWebQueries();
            if (unRespondedWebQueries.Any())
            {
                number = unRespondedWebQueries.Count();
            }
            return number;
        }

        public DashBoard GetDashBoardData()
        {
            var dashBoard = new DashBoard()
            {
                NumberOfClients = GetNumberOfClients(),
                NumberOfCompletedProjects = GetNumberOfCompletedProjects(),
                NumberOfExternalUsers = GetNumberOfExternalUsers(),
                NumberOfOnGoingProjects = GetNumberOfOnGoingProjects(),
                NumberOfPropertiesForRent = GetNumberOfPropertiesForRent(),
                NumberOfPropertiesForSale = GetNumberOfPropertiesForSale(),
                NumberOfUnRespondedToWebQueries = GetNumberOfUnRespondedToWebQueries(),

            };
            return dashBoard;
        }
    }
}
