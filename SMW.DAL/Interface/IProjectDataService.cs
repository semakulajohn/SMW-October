using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.DTO;
using SMW.EF.Models;

namespace SMW.DAL.Interface
{
  public  interface IProjectDataService
    {
        IEnumerable<Project> GetAllProjects();
        Project GetProject(long projectId);
        long SaveProject(ProjectDTO project, string userId);
        void MarkAsDeleted(long projectId, string userId);
        IEnumerable<Project> GetAllClientProjects(string clientId);
        IEnumerable<Project> GetAllCompletedProjects(long statusId);
        IEnumerable<Project> GetAllOnGoingProjects(long statusId);
        IEnumerable<Status> GetAllStatuses();
    }
}
