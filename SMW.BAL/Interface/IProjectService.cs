using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.Models;

namespace SMW.BAL.Interface
{
  public  interface IProjectService
    {
        IEnumerable<Project> GetAllProjects();
        Project GetProject(long projectId);
        long SaveProject(Project project, string userId);
        void MarkAsDeleted(long projectId, string userId);
        IEnumerable<Project> GetAllClientProjects(string clientId);
        IEnumerable<Project> GetLatestProjects();
        IEnumerable<Project> GetLatestEightProjects();
        IEnumerable<Project> GetAllOnGoingProjects();
        IEnumerable<Project> GetAllCompletedProjects();
        IEnumerable<Status> GetAllStatuses();
      
    }
}
