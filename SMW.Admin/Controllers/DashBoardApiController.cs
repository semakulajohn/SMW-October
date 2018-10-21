using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMW.BAL.Interface;
using log4net;
using SMW.Models;

namespace SMW.Admin.Controllers
{
    public class DashBoardApiController : ApiController
    {
         private IDashBoardService _dashBoardService;
            private IUserService _userService;
            ILog logger = log4net.LogManager.GetLogger(typeof(DashBoardApiController));
            private string userId = string.Empty;

            public DashBoardApiController()
            {
            }

            public DashBoardApiController(IDashBoardService dashBoardService,IUserService userService)
            {
                this._dashBoardService = dashBoardService;
                this._userService = userService;
                userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(RequestContext.Principal.Identity);
            }

            [HttpGet]
            [ActionName("GetDashBoardData")]
            public DashBoard GetDashBoardData()
            {
                return _dashBoardService.GetDashBoardData();
            }

           
    }
}
