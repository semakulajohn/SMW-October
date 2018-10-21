using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.DTO;
using SMW.EF.Models;

namespace SMW.DAL.Interface
{
 public   interface IWebQueryDataService
    {
        IEnumerable<WebQuery> GetAllWebQueries();
        long SaveWebQuery(WebQueryDTO webQueryDTO);
        IEnumerable<WebQuery> GetAllRespondedToWebQueries();
        IEnumerable<WebQuery> GetAllUnRespondedToWebQueries();
    }
}
