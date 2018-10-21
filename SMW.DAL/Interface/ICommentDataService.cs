using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.DTO;
using SMW.EF.Models;

namespace SMW.DAL.Interface
{
  public  interface ICommentDataService
    {
      IEnumerable<Comment> GetAllComments();
      Comment GetComment(long CommentId);
      Comment SaveComment(CommentDTO comment, string userId);
      void MarkAsDeleted(long commentId, string userId);
      IEnumerable<Comment> GetMediaComments(int mediaId);
    }
}
