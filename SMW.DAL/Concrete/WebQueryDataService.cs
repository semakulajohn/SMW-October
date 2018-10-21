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

namespace SMW.DAL.Concrete
{
    public class WebQueryDataService : DataServiceBase, IWebQueryDataService
    {
        ILog logger = log4net.LogManager.GetLogger(typeof(WebQueryDataService));

        public WebQueryDataService(IUnitOfWork<SMWEntities> unitOfWork)
            : base(unitOfWork)
        {

        }


        public IEnumerable<WebQuery> GetAllWebQueries()
        {
            return this.UnitOfWork.Get<WebQuery>().AsQueryable();
        }

        public IEnumerable<WebQuery> GetAllRespondedToWebQueries()
        {
            return this.UnitOfWork.Get<WebQuery>().AsQueryable().
                Where(m=>m.RespondedTo == true);
        }

      public  IEnumerable<WebQuery> GetAllUnRespondedToWebQueries()
        {
            return this.UnitOfWork.Get<WebQuery>().AsQueryable().
               Where(m => m.RespondedTo == false);
        }

        /// <summary>
        /// Saves a new WebQuery or updates an already existing WebQuery.
        /// </summary>
        /// <param name="WebQuery">WebQuery to be saved.</param>
        /// <param name="WebQueryId">WebQueryId of the WebQuery creating</param>
        /// <returns>WebQueryId</returns>
        public long SaveWebQuery(WebQueryDTO webQueryDTO)
        {
            long webQueryId = 0;

            if (webQueryDTO.WebQueryId == 0)
            {


                var webQuery = new WebQuery()
                {

                    WebQueryId = webQueryDTO.WebQueryId,
                    Body = webQueryDTO.Body,
                    PhoneNumber = webQueryDTO.PhoneNumber,
                    CreatedOn = DateTime.Now,
                    Name = webQueryDTO.Name,
                    EmailAddress = webQueryDTO.EmailAddress,
                    RespondedTo = webQueryDTO.RespondedTo,


                };

                this.UnitOfWork.Get<WebQuery>().AddNew(webQuery);
                this.UnitOfWork.SaveChanges();
                webQueryId = webQuery.WebQueryId;
                return webQueryId;
            }


            return webQueryId;
        }

    }             
}
