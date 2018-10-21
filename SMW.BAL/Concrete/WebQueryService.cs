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
using System.Net.Mail;
using System.Web;
using System.IO;
using System.Configuration;

namespace SMW.BAL.Concrete
{
 public   class WebQueryService : IWebQueryService
    {
      ILog logger = log4net.LogManager.GetLogger(typeof(WebQueryService));
        private IWebQueryDataService _dataService;
       
       
        

        public WebQueryService(IWebQueryDataService dataService)
        {
            this._dataService = dataService;
                       
        }

    

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WebQuery> GetAllWebQueries()
        {
            var results = this._dataService.GetAllWebQueries();
            return MapEFToModel(results);
        }

        public IEnumerable<WebQuery> GetAllRespondedToWebQueries()
        {
            var results = this._dataService.GetAllRespondedToWebQueries();
            return MapEFToModel(results);
        }

        public IEnumerable<WebQuery> GetAllUnRespondedToWebQueries()
        {
            var results = this._dataService.GetAllUnRespondedToWebQueries();
            return MapEFToModel(results);
        }
        public long SaveWebQuery(WebQuery webQuery)
        {
            var webQueryDTO = new DTO.WebQueryDTO()
            {
                WebQueryId = webQuery.WebQueryId,
                Name = webQuery.Name,
                PhoneNumber = webQuery.PhoneNumber,
                EmailAddress = webQuery.EmailAddress,
                Body = webQuery.Body,
                CreatedOn = webQuery.CreatedOn,
                RespondedTo = webQuery.RespondedTo,
                    

            };

           var webQueryId = this._dataService.SaveWebQuery(webQueryDTO);

           SendEmail(webQueryDTO);
           return webQueryId;
                      
        }



        //Send off query as email 
        public void SendEmail(WebQueryDTO query)
        {

            StringBuilder sb = new StringBuilder();
            string strNewPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["QueryAsEmail"]);
            using (StreamReader sr = new StreamReader(strNewPath))
            {
                while (!sr.EndOfStream)
                {
                    sb.Append(sr.ReadLine());
                }
            }

            string body = sb.ToString().Replace("#NAME#", query.Name);
            body = body.Replace("#PHONE#", query.PhoneNumber);
            body = body.Replace("#QUERY#", query.Body);
            body = body.Replace("#EMAIL#", query.EmailAddress);

            Helpers.Email email = new Helpers.Email();
            email.MailBodyHtml = body;
           
            email.MailToAddress = ConfigurationManager.AppSettings["EmailAddressTo"];
                
               
            email.MailFromAddress = ConfigurationManager.AppSettings["no-reply-email"];
            email.Subject = ConfigurationManager.AppSettings["query_email_subject"];

            try
            {
                email.SendMail();
                logger.Debug("Email sent");
            }
            catch (Exception ex)
            {

                logger.Debug("email Not Sent: " + ex.Message);
            }



        }

         
      
        #region Mapping Methods

        private IEnumerable<WebQuery> MapEFToModel(IEnumerable<EF.Models.WebQuery> data)
        {
            var list = new List<WebQuery>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps WebQuery EF object to WebQuery Model Object and
        /// returns the WebQuery model object.
        /// </summary>
        /// <param name="result">EF WebQuery object to be mapped.</param>
        /// <returns>WebQuery Model Object.</returns>
        public WebQuery MapEFToModel(EF.Models.WebQuery data)
        {
                       
          
            var webQuery = new WebQuery()
            {
                WebQueryId = data.WebQueryId,
                Name = data.Name,
                EmailAddress = data.EmailAddress,
                Body = data.Body,
                CreatedOn = data.CreatedOn,
                RespondedTo = data.RespondedTo,

            };
            return webQuery;
        }



       #endregion
    }
}
