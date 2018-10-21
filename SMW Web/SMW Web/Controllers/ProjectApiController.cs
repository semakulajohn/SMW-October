using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMW.BAL.Interface;
using log4net;
using SMW.Models;

namespace SMW_Web.Controllers
{
    public class ProjectApiController : ApiController
    {
    private IProjectService _projectService;
       
        ILog logger = log4net.LogManager.GetLogger(typeof(ProjectApiController));
        private string userId = string.Empty;

        public ProjectApiController()
        {
        }

        public ProjectApiController(IProjectService projectService)
        {
            this._projectService = projectService;
         
        }

        [HttpGet]
        [ActionName("GetProject")]
        public Project GetProject(long projectId)
        {
            return _projectService.GetProject(projectId);
        }

        [HttpGet]
        [ActionName("GetAllProjects")]
        public IEnumerable<Project> GetAllProjects()
        {
            return _projectService.GetAllProjects();
        }


        [HttpGet]
        [ActionName("GetLatestEightProjects")]
        public IEnumerable<Project> GetLatestEightProjects()
        {
            return _projectService.GetLatestEightProjects();
        }

        [HttpGet]
        [ActionName("GetAllOnGoingProjects")]
        public IEnumerable<Project> GetAllOnGoingProjects()
        {
            return _projectService.GetAllOnGoingProjects();
        }
       
        [HttpGet]
        [ActionName("GetAllCompletedProjects")]
        public IEnumerable<Project> GetAllCompletedProjects()
        {
            return _projectService.GetAllCompletedProjects();
        }
    }
}

