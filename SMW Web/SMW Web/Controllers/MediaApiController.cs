using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;
using log4net;
using SMW.BAL.Interface;
using SMW.Models;

namespace SMW_Web.Controllers
{
    public class MediaApiController : ApiController
    {
       
        private IMediaService _mediaService;

        public MediaApiController(IMediaService mediaService)
        {
            this._mediaService = mediaService; 
        }

        [HttpGet]
        [System.Web.Http.ActionName("GetFilesInFolder")]
        public IEnumerable<Media> GetFilesInFolder(long folderId, string mediaTypes)
        {
            if (string.IsNullOrEmpty(mediaTypes))
                return _mediaService.GetFilesInFolder(folderId);
            else
                return _mediaService.GetFilesInFolder(folderId, mediaTypes);
        }
    }
}
