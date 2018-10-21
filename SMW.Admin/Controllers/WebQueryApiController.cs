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
    public class WebQueryApiController : ApiController
    {
         private IWebQueryService _webQueryService;
            ILog logger = log4net.LogManager.GetLogger(typeof(WebQueryApiController));
            private string userId = string.Empty;

            public WebQueryApiController()
            {
            }

            public WebQueryApiController(IWebQueryService webQueryService)
            {
                this._webQueryService = webQueryService;
                              
            }

           
            [HttpGet]
            [ActionName("GetAllWebQueries")]
            public IEnumerable<WebQuery> GetAllWebQueries()
            {
                return _webQueryService.GetAllWebQueries();
            }

         
            [HttpPost]
            [ActionName("Save")]
            public long Save(WebQuery model)
            {

                var webQueryId = _webQueryService.SaveWebQuery(model);
                return webQueryId;
            }
    }
}
