using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.DAL.Interface;
using SMW.DTO;
using SMW.EF.UnitOfWork;
using SMW.EF.Models;
using AutoMapper;

namespace SMW.DAL.Concrete
{
  public  class MediaDataService : DataServiceBase,IMediaDataService
    {
        public MediaDataService(IUnitOfWork<SMWEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

        public Media GetMedia(long mediaId)
        {
            return (from n in this.UnitOfWork.Get<Media>().AsQueryable()
                    where n.MediaId == mediaId && (n.Deleted == null || !n.Deleted)
                    select n).FirstOrDefault();
        }



        public IEnumerable<Media> GetChildren(long parentId)
        {
            return (from n in this.UnitOfWork.Get<Media>().AsQueryable()
                    where n.ParentId == parentId && (n.Deleted == null || !n.Deleted)
                    select n);
        }

        public IEnumerable<Media> GetChildFolders(long parentId)
        {
            return (from n in this.UnitOfWork.Get<Media>().AsQueryable()
                    where n.ParentId == parentId && n.MediaTypeId == (long)SMW.Library.EnumTypes.MediaType.Folder && (n.Deleted == null || !n.Deleted)
                    select n);
        }

        public IEnumerable<Media> GetFilesInFolder(long folderId)
        {
            return (from n in this.UnitOfWork.Get<Media>().AsQueryable()
                    where n.ParentId == folderId && n.MediaTypeId != (long)SMW.Library.EnumTypes.MediaType.Folder && (n.Deleted == null || !n.Deleted)
                    select n);
        }

        public long SaveFolder(long mediaId, long? parentId, string name)
        {
            if (mediaId == 0)
            {
                var media = new Media
                {
                    MediaGuid = Guid.NewGuid(),
                    ParentId = parentId,
                    Name = name,
                    MediaTypeId = (long)SMW.Library.EnumTypes.MediaType.Folder,
                    CreatedOn = DateTime.Now,
                    TimeStamp = DateTime.Now,
                    Deleted = false
                };
                this.UnitOfWork.Get<Media>().AddNew(media);
                this.UnitOfWork.SaveChanges();
                return media.MediaId;
            }
            else
            {
                var media = (from n in this.UnitOfWork.Get<Media>().AsQueryable()
                             where n.MediaId == mediaId
                             select n
                            ).FirstOrDefault();
                if (media != null)
                {
                    media.Name = name;
                    media.TimeStamp = DateTime.Now;
                    this.UnitOfWork.Get<Media>().Update(media);
                    this.UnitOfWork.SaveChanges();
                }
                return mediaId;
            }
        }

        public long SaveFile(long parentId, string fileName, int contentLength, long extensionTypeId, long mediaTypeId)
        {
            var media = new Media
            {
                MediaGuid = Guid.NewGuid(),
                ParentId = parentId,
                Name = fileName,
                MediaTypeId = mediaTypeId,
                ExtensionTypeId = extensionTypeId,
                Filesize = contentLength,
                CreatedOn = DateTime.Now,
                TimeStamp = DateTime.Now,
                Deleted = false
            };
            this.UnitOfWork.Get<Media>().AddNew(media);
            this.UnitOfWork.SaveChanges();

            //Update path
            using (var dbContext = new SMWEntities())
            {
                var results = dbContext.Media_SetPath(media.MediaId);
            }

            return media.MediaId;
        }

        public IEnumerable<MediaDTO> GetDescendants(long mediaId)
        {
            using (var dbContext = new SMWEntities())
            {
                var results = dbContext.Media_GetDescendants(mediaId).ToList();
                Mapper.CreateMap<SMW.EF.Models.Media_GetDescendants_Result, SMW.DTO.MediaDTO>();
                return Mapper.Map<List<SMW.EF.Models.Media_GetDescendants_Result>, List<SMW.DTO.MediaDTO>>(results);
            }
        }

        public string GetPathCsvMediaId(long mediaId)
        {
            var data = string.Empty;

            using (var dbContext = new SMWEntities())
            {
                data = dbContext.Media_GetPathCsvMediaId(mediaId).FirstOrDefault();
            }

            return data;
        }

        public void MarkAsDeleted(long mediaId)
        {
            var media = (from n in this.UnitOfWork.Get<Media>().AsQueryable()
                         where n.MediaId == mediaId
                         select n
                            ).FirstOrDefault();
            if (media != null)
            {
                media.Deleted = true;
                media.DeletedOn = DateTime.Now;
                this.UnitOfWork.Get<Media>().Update(media);
                this.UnitOfWork.SaveChanges();
            }
        }

        public void Delete(long mediaId)
        {
            var media = (from n in this.UnitOfWork.Get<Media>().AsQueryable()
                         where n.MediaId == mediaId
                         select n
                            ).FirstOrDefault();
            if (media != null)
            {
                this.UnitOfWork.Get<Media>().Delete(media);
                this.UnitOfWork.SaveChanges();
            }
        }

        public IEnumerable<ExtensionType> GetAllExtentionTypes()
        {
            return this.UnitOfWork.Get<ExtensionType>().AsQueryable();
        }

        public long AddExtention(string ext)
        {
            var extention = new ExtensionType()
            {
                Ext = ext
            };
            this.UnitOfWork.Get<ExtensionType>().AddNew(extention);
            this.UnitOfWork.SaveChanges();
            return extention.ExtensionTypeId;
        }

        public bool HasFolderOrFiles(long folderId)
        {
            return (from n in this.UnitOfWork.Get<Media>().AsQueryable()
                    where n.ParentId == folderId && (n.Deleted == null || !n.Deleted)
                    select n).Any();
        }


        public long CreateFolder(long parentId, string name)
        {
            //Add entry to Media 
            var media = new Media
            {
                MediaGuid = Guid.NewGuid(),
                ParentId = parentId,
                Name = name,
                MediaTypeId = (long)SMW.Library.EnumTypes.MediaType.Folder,
                CreatedOn = DateTime.Now,
                TimeStamp = DateTime.Now,
                Deleted = false
            };
            this.UnitOfWork.Get<Media>().AddNew(media);
            this.UnitOfWork.SaveChanges();

            return media.MediaId;
        }

    }
}
