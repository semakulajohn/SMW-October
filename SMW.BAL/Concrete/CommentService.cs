using System;
using System.Web;
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
using System.IO;
using System.Configuration;

namespace SMW.BAL.Concrete
{
 public   class CommentService : ICommentService
    {
         ILog logger = log4net.LogManager.GetLogger(typeof(CommentService));
        private ICommentDataService _dataService;
        private IUserService _userService;
       
        

        public CommentService(ICommentDataService dataService,IUserService userService)
        {
            this._dataService = dataService;
            this._userService = userService;
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CommentId"></param>
        /// <returns></returns>
        public Comment GetComment(long commentId)
        {
            var result = this._dataService.GetComment(commentId);
            return MapEFToModel(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Comment> GetAllComments()
        {
            var results = this._dataService.GetAllComments();
            return MapEFToModel(results);
        } 

       
        public Comment SaveComment(Comment comment, string userId)
        {
            var commentDTO = new DTO.CommentDTO()
            {
                CommentId = comment.CommentId,
                Body = comment.Body,
                MediaId = comment.MediaId,               

            };

           var returnedComment = this._dataService.SaveComment(commentDTO, userId);

           var user = _userService.GetAspNetUser(userId);
           if (user != null)
           {
               var roleName = user.RoleName;
               if(roleName =="client"){
                   // send email to admin
                   string userName = user.FirstName + ' '+user.LastName;
                   SendCommentEmailToAdmin(commentDTO, userName);
               }
               //else if (roleName == "admin")
               //{
               //    //send email to owner of project
               //    SendCommentEmailToClient(commentDTO, user);
               //}
           }
           return MapEFToModel(returnedComment);
                      
        }


        //Send off comment as email 
        public void SendCommentEmailToAdmin(CommentDTO query,string userName)
        {

            StringBuilder sb = new StringBuilder();
            string strNewPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["CommentAsEmail"]);
            using (StreamReader sr = new StreamReader(strNewPath))
            {
                while (!sr.EndOfStream)
                {
                    sb.Append(sr.ReadLine());
                }
            }

            string body = sb.ToString().Replace("#MEDIAID#", Convert.ToString(query.MediaId));
          
            body = body.Replace("#BODY#", query.Body);
            body = body.Replace("#USER#",userName);
            
            Helpers.Email email = new Helpers.Email();
            email.MailBodyHtml = body;

            email.MailToAddress = ConfigurationManager.AppSettings["EmailAddressTo"];


            email.MailFromAddress = ConfigurationManager.AppSettings["no-reply-email"];
            email.Subject = ConfigurationManager.AppSettings["comment_email_subject"];

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



        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CommentId"></param>
        /// <param name="userId"></param>
        public void MarkAsDeleted(long commentId, string userId)
        {
            _dataService.MarkAsDeleted(commentId, userId);
        }

        public IEnumerable<Comment> GetMediaComments(int mediaId)
        {
            var results = _dataService.GetMediaComments(mediaId);
            return MapEFToModel(results);
        }

      
        #region Mapping Methods

        public IEnumerable<Comment> MapEFToModel(IEnumerable<EF.Models.Comment> data)
        {
            var list = new List<Comment>();
            foreach (var result in data)
            {
                list.Add(MapEFToModel(result));
            }
            return list;
        }

        /// <summary>
        /// Maps Comment EF object to Comment Model Object and
        /// returns the Comment model object.
        /// </summary>
        /// <param name="result">EF Comment object to be mapped.</param>
        /// <returns>Comment Model Object.</returns>
        public Comment MapEFToModel(EF.Models.Comment data)
        {
          
            var Comment = new Comment()
            {
                CommentId = data.CommentId,
                Body = data.Body,
                MediaId = data.MediaId,
                CreatedOn = data.CreatedOn,
                Timestamp = data.Timestamp,
                CreatedBy = _userService.GetUserFullName(data.AspNetUser),
                UpdatedBy = _userService.GetUserFullName(data.AspNetUser1),
               

            };
            return Comment;
        }



       #endregion
    }
}
