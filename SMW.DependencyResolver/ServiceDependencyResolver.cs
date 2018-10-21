using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using SMW.BAL.Interface;
using SMW.BAL.Concrete;
using SMW.DAL.Interface;
using SMW.DAL.Concrete;

namespace SMW.DependencyResolver
{
 public   class ServiceDependencyResolver : NinjectModule
    {
     public override void Load()
     {
         //BAL
         Bind(typeof(IUserService)).To(typeof(UserService));
         Bind(typeof(IPropertyService)).To(typeof(PropertyService));
         Bind(typeof(IProjectService)).To(typeof(ProjectService));
         Bind(typeof(ICommentService)).To(typeof(CommentService));
         Bind(typeof(IMediaService)).To(typeof(MediaService));
         Bind(typeof(IServiceService)).To(typeof(ServiceService));
         Bind(typeof(IWebQueryService)).To(typeof(WebQueryService));
         Bind(typeof(IFacilityService)).To(typeof(FacilityService));
         Bind(typeof(IDashBoardService)).To(typeof(DashBoardService));



         //DAL
         Bind(typeof(IUserDataService)).To(typeof(UserDataService));
         Bind(typeof(IPropertyDataService)).To(typeof(PropertyDataService));
         Bind(typeof(IProjectDataService)).To(typeof(ProjectDataService));
         Bind(typeof(ICommentDataService)).To(typeof(CommentDataService));
         Bind(typeof(IMediaDataService)).To(typeof(MediaDataService));
         Bind(typeof(IServiceDataService)).To(typeof(ServiceDataService));
         Bind(typeof(IWebQueryDataService)).To(typeof(WebQueryDataService));
         Bind(typeof(IFacilityDataService)).To(typeof(FacilityDataService));
     }   
    }
}
