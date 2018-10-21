using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.Models;

namespace SMW.BAL.Interface
{
   public interface ICommentService
    {
        IEnumerable<Comment> GetAllComments();
        Comment GetComment(long commentId);
        Comment SaveComment(Comment comment, string userId);
        void MarkAsDeleted(long commentId, string userId);
        IEnumerable<Comment> GetMediaComments(int mediaId);
        IEnumerable<Comment> MapEFToModel(IEnumerable<EF.Models.Comment> data);
    }
}
