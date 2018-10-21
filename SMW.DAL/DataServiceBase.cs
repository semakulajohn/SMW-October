using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.EF.UnitOfWork;
using SMW.EF.Context;
using SMW.EF.Models;

namespace SMW.DAL
{
  public  class DataServiceBase
    {
        private IUnitOfWork<SMWEntities> _unitOfwork;

        protected IUnitOfWork<SMWEntities> UnitOfWork { get { return this._unitOfwork; } }

        public DataServiceBase(IUnitOfWork<SMWEntities> unitOfWork)
        {
            this._unitOfwork = unitOfWork;
        }
    }
}
