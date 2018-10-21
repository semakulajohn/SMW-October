using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.EF.Models;
using SMW.DAL.Concrete;
using SMW.DAL.Interface;
using SMW.EF.UnitOfWork;
using SMW.DTO;
using log4net;
using System.Data;


namespace SMW.DAL.Concrete
{
  public  class ProjectDataService : DataServiceBase,IProjectDataService
    {
      ILog logger = log4net.LogManager.GetLogger(typeof(ProjectDataService));

       public ProjectDataService(IUnitOfWork<SMWEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

      
        public IEnumerable<Project> GetAllProjects()
        {
            return this.UnitOfWork.Get<Project>().AsQueryable().Where(e => e.Deleted == false); 
        }

        public IEnumerable<Project> GetAllCompletedProjects(long statusId)
        {
            return this.UnitOfWork.Get<Project>().AsQueryable().Where(e => e.Deleted == false && e.StatusId == statusId);
        }

        public IEnumerable<Project> GetAllOnGoingProjects(long statusId)
        {
            return this.UnitOfWork.Get<Project>().AsQueryable().Where(e => e.Deleted == false && e.StatusId == statusId);
        }


        public IEnumerable<Project> GetAllClientProjects(string clientId)
        {
            return this.UnitOfWork.Get<Project>().AsQueryable().Where(e => e.Deleted == false && e.ClientId == clientId );
        }

        public Project GetProject(long projectId)
        {
            return this.UnitOfWork.Get<Project>().AsQueryable()
                 .FirstOrDefault(c =>
                    c.ProjectId == projectId &&
                    c.Deleted == false
                );
        }


        public IEnumerable<Status> GetAllStatuses()
        {
            return this.UnitOfWork.Get<Status>().AsQueryable();
        }

        /// <summary>
        /// Saves a new project or updates an already existing project.
        /// </summary>
        /// <param name="project">project to be saved or updated.</param>
        /// <param name="projectId">projectId of the project creating or updating</param>
        /// <returns>projectId</returns>
        public long SaveProject(ProjectDTO projectDTO, string userId)
        {
            long projectId = 0;
            
            if (projectDTO.ProjectId == 0)
            {
                long mediaFolderId = 0;

                var media = new Media
                {
                    MediaGuid = Guid.NewGuid(),
                    //ParentId = rootGalleryId,
                    Name = projectDTO.Title,
                    MediaTypeId = (long)SMW.Library.EnumTypes.MediaType.Folder,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    Deleted = false
                };

                this.UnitOfWork.Get<Media>().AddNew(media);
                this.UnitOfWork.SaveChanges();
                mediaFolderId = media.MediaId;
           
                var project = new Project()
                {
                    
                    Title = projectDTO.Title, 
                    MediaFolderId = mediaFolderId,
                    ClientId = projectDTO.ClientId,
                    StatusId = projectDTO.StatusId,
                    Description = projectDTO.Description,
                    CreatedOn = DateTime.Now,
                    Timestamp = DateTime.Now,
                    CreatedBy = userId,
                    Deleted = false,
 

                };

                this.UnitOfWork.Get<Project>().AddNew(project);
                this.UnitOfWork.SaveChanges();
                projectId = project.ProjectId;
                return projectId;
            }

            else
            {
                var result = this.UnitOfWork.Get<Project>().AsQueryable()
                    .FirstOrDefault(e => e.ProjectId == projectDTO.ProjectId);
                if (result != null)
                {
                    result.Title = projectDTO.Title; 
                    result.ClientId = projectDTO.ClientId;
                    result.Description = projectDTO.Description;
                    result.MediaFolderId = projectDTO.MediaFolderId;
                    result.StatusId = projectDTO.StatusId;
                    result.Timestamp = DateTime.Now;
                    result.Deleted = projectDTO.Deleted;
                    result.DeletedBy = projectDTO.DeletedBy;
                    result.DeletedOn = projectDTO.DeletedOn;
                    result.UpdatedBy = userId;
                    
 
                    this.UnitOfWork.Get<Project>().Update(result);
                    this.UnitOfWork.SaveChanges();
                }
                return projectDTO.ProjectId;
            }
            return projectId;
        }

        public void MarkAsDeleted(long projectId,string userId)
        {
            var result = this.UnitOfWork.Get<Project>().AsQueryable().Where(e => e.Deleted == false && e.ProjectId == projectId);
            if (result != null)
            {
                using (var dbContext = new SMWEntities())
                {
                    dbContext.Mark_Project_And_RelatedData_AsDeleted(projectId, userId);

                }
            }
        }
    }
}
