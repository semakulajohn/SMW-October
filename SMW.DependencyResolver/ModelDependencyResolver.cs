 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.EF.UnitOfWork;
using Ninject.Modules;

namespace SMW.DependencyResolver
{
  public  class ModelDependencyResolver : NinjectModule
    {
     
        public override void Load()
        {
            Bind(typeof(IUnitOfWork<>)).To(typeof(UnitOfWork<>));
        }
    }
}

    

