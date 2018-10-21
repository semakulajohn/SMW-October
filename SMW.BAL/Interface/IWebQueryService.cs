using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.Models;

namespace SMW.BAL.Interface
{
 public   interface IWebQueryService
    {
        IEnumerable<WebQuery> GetAllWebQueries();
        long SaveWebQuery(WebQuery webQuery);
        IEnumerable<WebQuery> GetAllRespondedToWebQueries();
        IEnumerable<WebQuery> GetAllUnRespondedToWebQueries();
    }
}
