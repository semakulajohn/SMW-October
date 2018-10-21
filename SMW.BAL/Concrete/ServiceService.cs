using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.DTO;
using SMW.BAL.Interface;
using SMW.DAL.Interface;
using SMW.Models;
using SMW.Helpers;
using log4net;

namespace SMW.BAL.Concrete
{
 public   class ServiceService : IServiceService
    {
         ILog logger = log4net.LogManager.GetLogger(typeof(ServiceService));
        private IServiceDataService _dataService;
        private IUserService _userService;
       
        

        public ServiceService(IServiceDataService dataService,IUserService userService)
        {
            this._dataService = dataService;
            this._userService = userService;
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ServiceId"></param>
        /// <returns></returns>
        public Service GetService(long serviceId)
        {
            var result = this._dataService.GetService(serviceId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Service> GetAllServices()
        {
            var results = this._dataService.GetAllServices();
            return MapEFToModel(results);
        }

       
        public long SaveService(Service service, string userId)
        {
            var serviceDTO = new DTO.ServiceDTO()
            {
                ServiceId = service.ServiceId,
                Title = service.Title,
                Description = service.Description,
                MediaFolderId = service.MediaFolderId,
                    

            };

           var serviceId = this._dataService.SaveService(serviceDTO, userId);

           return serviceId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ServiceId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long serviceId, string userId)
        {
            _dataService.MarkAsDeleted(serviceId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<Service> MapEFToModel(IEnumerable<EF.Models.Service> data)
        {
            var list = new List<Service>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Service EF object to Service Model Object and
        /// returns the Service model object.
        /// </summary>
        /// <param name="result">EF Service object to be mapped.</param>
        /// <returns>Service Model Object.</returns>
        public Service MapEFToModel(EF.Models.Service data)
        {
                      
          
            var service = new Service()
            {
                ServiceId = data.ServiceId,
                Title = data.Title,
                Description = data.Description,
                CreatedOn = data.CreatedOn,
                MediaFolderId = data.MediaFolderId,
                Timestamp = data.Timestamp,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),
               
               

            };
            return service;
        }



       #endregion
    }
}
