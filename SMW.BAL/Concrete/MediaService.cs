using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMW.BAL.Interface;
using SMW.DAL.Interface;
using SMW.Models;
using System.Configuration;
using SMW.Helpers;
using SMW.Library;
using System.IO;
using AutoMapper;

namespace SMW.BAL.Concrete
{
public    class MediaService : IMediaService
    {
        private IMediaDataService _dataService;
        private ICommentService _commentService;

        public MediaService(IMediaDataService dataService,ICommentService commentService)
        {
            this._dataService = dataService;
            this._commentService = commentService;
        }

        public SMW.Models.Media GetMedia(long mediaId)
        {
            var m = _dataService.GetMedia(mediaId);
            return MapEFToModel(m);
        }



        private Media FilterByMediaTypes(Media media, string filterMediaTypes)
        {
            if (!string.IsNullOrEmpty(filterMediaTypes))
            {
                var allowedMediaTypesIds = filterMediaTypes.Split(',').Select(Int64.Parse).ToList();
                if (allowedMediaTypesIds.Any(x => x == media.MediaTypeId))
                {
                    var filteredChildren = new List<SMW.Models.Media>();
                    foreach (var child in media.Children)
                    {
                        if (allowedMediaTypesIds.Any(x => x == child.MediaTypeId))
                        {
                            filteredChildren.Add(child);
                        }
                    }
                    media.Children = filteredChildren;
                }
                else
                {
                    return null;
                }
            }
            return media;
        }

        private Media MapEFToModel(EF.Models.Media m)
        {
            if (m != null)
            {
                var children = _dataService.GetChildren(m.MediaId);
                var comments = new List<Comment>();
                if (m.Comment != null)
                {
                    comments = _commentService.MapEFToModel(m.Comment).ToList();
                }
                var media = new Models.Media
                {
                    MediaId = m.MediaId,
                    MediaGuid = m.MediaGuid,
                    Filesize = m.Filesize,
                    Name = m.Name,
                    CreatedOn = m.CreatedOn,
                    TimeStamp = m.TimeStamp,
                    MediaTypeId = m.MediaTypeId,
                    ExtensionTypeId = m.ExtensionTypeId,
                    Path = StringHelper.ReversePath(m.Path, '\\'),
                    Comments = comments
                };

               
                if (children != null)
                {
                    var list = new List<SMW.Models.Media>();
                    media.Children = list;
                    foreach (var child in children)
                    {
                        media.Children.Add(
                            new SMW.Models.Media
                            {
                                MediaId = child.MediaId,
                                ParentId = child.ParentId,
                                MediaGuid = child.MediaGuid,
                                Name = child.Name,
                                Filesize = child.Filesize,
                                CreatedOn = child.CreatedOn,
                                TimeStamp = child.TimeStamp,
                                MediaTypeId = child.MediaTypeId,
                                ExtensionTypeId = child.ExtensionTypeId,
                                Path = StringHelper.ReversePath(child.Path, '\\')
                            });
                    }
                }
                return media;
            }
            else
                return null;
        }

        public IEnumerable<MediaFolder> GetFolder(long mediaId)
        {
            var media = GetMedia(mediaId);
            var mediaFolder = new MediaFolder();
            if (media != null)
            {
                mediaFolder = new MediaFolder
                {
                    id = media.MediaId,
                    MediaId = media.MediaId,
                    ParentId = media.ParentId,
                    MediaGuid = media.MediaGuid,
                    label = media.Name,
                };
            }
            List<MediaFolder> list = new List<MediaFolder>();
            list.Add(mediaFolder);
            return list;
        }

        public SMW.Models.MediaFolder GetFolderAndChildFolders(long parentFolderId)
        {
            var m = _dataService.GetMedia(parentFolderId);
            var children = _dataService.GetChildFolders(parentFolderId);
            var media = new SMW.Models.MediaFolder
            {
                id = m.MediaId,
                MediaId = m.MediaId,
                MediaGuid = m.MediaGuid,
                label = m.Name,
            };

            if (children != null)
            {
                var list = new List<SMW.Models.MediaFolder>();
                media.children = list;
                foreach (var child in children)
                {
                    media.children.Add(
                        new SMW.Models.MediaFolder
                        {
                            id = child.MediaId,
                            MediaId = child.MediaId,
                            ParentId = child.ParentId,
                            MediaGuid = child.MediaGuid,
                            label = child.Name,
                        });
                }
            }
            return media;
        }

        public IEnumerable<SMW.Models.MediaFolder> GetChildFolders(long parentId)
        {
            var children = _dataService.GetChildFolders(parentId);
            var list = new List<SMW.Models.MediaFolder>();
            foreach (var child in children)
            {

                list.Add(
                    new SMW.Models.MediaFolder
                    {
                        id = child.MediaId,
                        MediaId = child.MediaId,
                        ParentId = child.ParentId,
                        MediaGuid = child.MediaGuid,
                        label = child.Name,
                    });

            }
            return list;
        }

        public IEnumerable<SMW.Models.Media> GetFilesInFolder(long folderId, string filterMediaTypes)
        {
            var allowedMediaTypesIds = filterMediaTypes.Split(',').Select(Int64.Parse).ToList();
            var children = _dataService.GetFilesInFolder(folderId);
            var list = new List<SMW.Models.Media>();
            foreach (var m in children)
            {
                if (allowedMediaTypesIds.Any(x => x == m.MediaTypeId))
                {
                    list.Add(
                        new SMW.Models.Media
                        {
                            MediaId = m.MediaId,
                            MediaGuid = m.MediaGuid,
                            Filesize = m.Filesize,
                            Name = m.Name,
                            CreatedOn = m.CreatedOn,
                            TimeStamp = m.TimeStamp,
                            MediaTypeId = m.MediaTypeId,
                            ExtensionType = m.ExtensionType != null ? m.ExtensionType.Ext : string.Empty,
                            Path = StringHelper.ReversePath(m.Path, '\\')
                        });
                }
            }
            return list;
        }

        public IEnumerable<SMW.Models.Media> GetFilesInFolder(long folderId)
        {
            var children = _dataService.GetFilesInFolder(folderId);
            var list = new List<SMW.Models.Media>();
            foreach (var m in children)
            {
                list.Add(
                    new SMW.Models.Media
                    {
                        MediaId = m.MediaId,
                        MediaGuid = m.MediaGuid,
                        Filesize = m.Filesize,
                        Name = m.Name,
                        CreatedOn = m.CreatedOn,
                        TimeStamp = m.TimeStamp,
                        MediaTypeId = m.MediaTypeId,
                        ExtensionType = m.ExtensionType != null ? m.ExtensionType.Ext : string.Empty,
                        Path = StringHelper.ReversePath(m.Path, '\\')
                    });

            }
            return list;
        }


        public long SaveFolder(MediaFolder folder)
        {
            return _dataService.SaveFolder(folder.MediaId, folder.ParentId, folder.label);
        }

        public long SaveFile(long parentId, string fileName, int contentLength, long extensionTypeId, long mediaTypeId)
        {
            return _dataService.SaveFile(parentId, fileName, contentLength, extensionTypeId, mediaTypeId);
        }

        /// <summary>
        ///   Mark media and Descendants as deleted (Soft Deleted)
        ///   1) MarkAsDeleted in DB
        /// </summary>
        /// <param name="text">The media to delete.</param>
        /// <returns></returns>
        /// 
        public void MarkAsDeleted(long mediaId)
        {
            var descendants = _dataService.GetDescendants(mediaId);
            foreach (var media in descendants)
            {
                //Mark media as deleted
                _dataService.MarkAsDeleted(media.MediaId);

            }

            //Remove all references
        }

        /// <summary>
        ///   Delete media from all cms & web servers and DB
        /// </summary>
        /// <param name="text">The media to delete.</param>
        /// <returns></returns>
        /// 
        public void Delete(long mediaId)
        {

            string _mediaFolder = ConfigurationManager.AppSettings["MediaFolder"];
            var descendants = _dataService.GetDescendants(mediaId);
            foreach (var media in descendants)
            {
                if (media.MediaTypeId != (long)EnumTypes.MediaType.Folder)
                {

                    var _mediaFolderPath = _mediaFolder + "\\" + media.MediaGuid.ToString();
                    if (Directory.Exists(_mediaFolderPath))
                    {
                        System.IO.Directory.Delete(_mediaFolderPath, true);
                    }
                }
                _dataService.Delete(media.MediaId);
            }
        }


        public IEnumerable<SMW.Models.ExtensionType> GetAllExtentionTypes()
        {
            var results = _dataService.GetAllExtentionTypes();
            Mapper.CreateMap<EF.Models.ExtensionType, Models.ExtensionType>();
            return Mapper.Map<IEnumerable<EF.Models.ExtensionType>, IEnumerable<Models.ExtensionType>>(results);
        }


        public long AddExtention(string ext)
        {
            return _dataService.AddExtention(ext);
        }

        public bool HasFolderOrFiles(long folderId)
        {
            return _dataService.HasFolderOrFiles(folderId);
        }

        public string GetPathCsvMediaId(long mediaId)
        {
            return _dataService.GetPathCsvMediaId(mediaId);
        }


        public long[] GetPathMediaIds(long mediaId)
        {
            var results = _dataService.GetPathCsvMediaId(mediaId);

            return results.Split(',').Select(Int64.Parse).ToList().ToArray();
        }

        public IEnumerable<MediaFolder> DrillDownToMediaId(long mediaId)
        {
            var rootvFolderMediaId = ConfigurationManager.AppSettings["RootvFolderMediaId"];
            var results = _dataService.GetPathCsvMediaId(mediaId);
            var ids = results.Split(',').Select(Int64.Parse).Reverse().ToList();
            var itemToRemove = ids.Single(r => r == Convert.ToInt64(rootvFolderMediaId));
            ids.Remove(itemToRemove);

            ids.Add(mediaId);


            var rootFolder = new List<SMW.Models.MediaFolder>();

            if (ids.Count > 0)
            {
                rootFolder = GetFolder(ids[0]).ToList();
                var childFolders = getChildFoldersToAdd(ref ids, ids[0]);
                rootFolder[0].children = childFolders;
            }
            return rootFolder;
        }

        private List<SMW.Models.MediaFolder> getChildFoldersToAdd(ref List<long> ids, long parentMediaId)
        {
            var allChildren = new List<MediaFolder>();
            var childFolders = GetChildFolders(parentMediaId);

            if (!childFolders.Any())
                return null;

            foreach (var mediaFolder in childFolders)
            {
                var children = GetChildFolders(mediaFolder.MediaId);
                if (children.Any())
                {
                    if (ids.Contains(mediaFolder.MediaId))
                    {
                        var childNodes = getChildFoldersToAdd(ref ids, mediaFolder.MediaId);
                        mediaFolder.children = childNodes;
                    }
                }

                //if (mediaFolder.MediaId == ids[ids.Length - 1])
                // {
                //     mediaFolder.selected = true;
                // }
                allChildren.Add(mediaFolder);
            }

            return allChildren;
        }
    }
}
