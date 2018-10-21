using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.DTO;
using SMW.EF.Models;

namespace SMW.DAL.Interface
{
   public interface IUserDataService
    {
        AspNetUser GetLoggedInUser(string userId);
        bool UserExists(string finder);
        AspNetUser SaveUser(AspNetUserDTO user, string userId);
        bool MarkAsDeleted(string Id);
        AspNetUser GetAspNetUser(string Id);
        IEnumerable<AspNetRole> GetAllRoles();
        AspNetRole GetAspNetRole(string roleId);
        IEnumerable<AspNetUser> GetAspNetUsers();
        void CreateAspNetUserRolesRecord(string userId, string roleId);
    }
}
