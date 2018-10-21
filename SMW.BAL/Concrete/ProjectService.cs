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
using System.Configuration;

namespace SMW.BAL.Concrete
{
 public   class ProjectService : IProjectService
    {
     private string onGoingStatusId = ConfigurationManager.AppSettings["OnGoingStatusId"];
     private string CompletedStatusId = ConfigurationManager.AppSettings["CompletedStatusId"];
      ILog logger = log4net.LogManager.GetLogger(typeof(ProjectService));
        private IProjectDataService _dataService;
        private IUserService _userService;
        private IMediaService _mediaService;
       
        

        public ProjectService(IProjectDataService dataService,IUserService userService, IMediaService mediaService)
        {
            this._dataService = dataService;
            this._userService = userService;
            this._mediaService = mediaService;
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public Project GetProject(long projectId)
        {
            var result = this._dataService.GetProject(projectId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Project> GetAllProjects()
        {
            var results = this._dataService.GetAllProjects();
            return MapEFToModel(results);
        }

        public IEnumerable<Project> GetAllOnGoingProjects()
        {
            long statusId = Convert.ToInt64(onGoingStatusId);
            var results = this._dataService.GetAllOnGoingProjects(statusId);
            return MapEFToModel(results);
        }

        public IEnumerable<Project> GetAllCompletedProjects()
        {
            long statusId = Convert.ToInt64(CompletedStatusId);
            var results = this._dataService.GetAllCompletedProjects(statusId);
            return MapEFToModel(results);
        }
        public IEnumerable<Project> GetLatestProjects()
        {
            var results = GetAllProjects().OrderByDescending(p => p.CreatedOn).Take(4);
            return results;
        }

        public IEnumerable<Project> GetLatestEightProjects()
        {
            var results = GetAllProjects().OrderByDescending(p => p.CreatedOn).Take(8);
            return results;
        }

        public IEnumerable<Status> GetAllStatuses()
        {
            var results = this._dataService.GetAllStatuses();
            return MapEFToModel(results);
        }
        public IEnumerable<Project> GetAllClientProjects(string clientId)
        {
            var results = this._dataService.GetAllClientProjects(clientId);
            return MapEFToModel(results);
        } 
        public long SaveProject(Project project, string userId)
        {
            var projectDTO = new DTO.ProjectDTO()
            {
                ProjectId = project.ProjectId,
                Title = project.Title,
                Description = project.Description,
                ClientId = project.ClientId,
                MediaFolderId = project.MediaFolderId,
                StatusId = project.StatusId,
                    

            };

           var projectId = this._dataService.SaveProject(projectDTO, userId);

           return projectId;
                      
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long projectId, string userId)
        {
            _dataService.MarkAsDeleted(projectId, userId);
        }

      
        #region Mapping Methods

        private IEnumerable<Status> MapEFToModel(IEnumerable<EF.Models.Status> data)
        {
            var list = new List<Status>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

       
        public Status MapEFToModel(EF.Models.Status data)
        {
          
          
            var status = new Status()
            {
                StatusId = data.StatusId,
                Name = data.Name,
               
           
            };
           
            return status;
        }

        private IEnumerable<Project> MapEFToModel(IEnumerable<EF.Models.Project> data)
        {
            var list = new List<Project>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Project EF object to Project Model Object and
        /// returns the Project model object.
        /// </summary>
        /// <param name="result">EF Project object to be mapped.</param>
        /// <returns>Project Model Object.</returns>
        public Project MapEFToModel(EF.Models.Project data)
        {
            var clientFullName = string.Empty;
            var client = _userService.GetAspNetUser(data.ClientId);
            if (client != null)
            {
                clientFullName = client.FirstName + ' ' + client.LastName;
            }


            var project = new Project()
            {
                ProjectId = data.ProjectId,
                Title = data.Title,
                Description = data.Description,
                ClientId = data.ClientId,
                CreatedOn = data.CreatedOn,
                MediaFolderId = data.MediaFolderId,
                Timestamp = data.Timestamp,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),
                ClientName = clientFullName,
                StatusId = data.StatusId,
                StatusName = data.Status != null ? data.Status.Name : "",


            };
            if (data.Medium != null)
            {
                var media = new Media()
                {
                    MediaId = data.Medium.MediaId,
                    MediaGuid = data.Medium.MediaGuid,
                    MediaTypeId = data.Medium.MediaTypeId,
                    Name = data.Medium.Name
                };
                project.Media = media;

            }
          
            project.MediaForView = GetFirstImageForProperty(data.MediaFolderId);

            return project;
        }

        private Media GetFirstImageForProperty(long parentId)
        {
            Media mediaObject = new Media();
            var mediaRecords = _mediaService.GetFilesInFolder(parentId);
            if (mediaRecords.Any())
            {
                mediaObject = mediaRecords.FirstOrDefault();
                var media = new Media()
                {
                    MediaId = mediaObject.MediaId,
                    MediaGuid = mediaObject.MediaGuid,
                    MediaTypeId = mediaObject.MediaTypeId,
                    Name = mediaObject.Name
                };
                return media;
            }
            return mediaObject;
        }

       #endregion
    }
}
