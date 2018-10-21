angular
    .module('homer')
    .controller('PropertyEditController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval', 'resizeService', 'FileUploader', 'appSettingsService', 'usSpinnerService',
    function ($scope, ngTableParams, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval,resizeService, FileUploader, appSettingsService,usSpinnerService) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        var propertyId = $scope.propertyId;
        var action = $scope.action;
        var uploader = $scope.uploader = new FileUploader({
            url: '/upload/?parentId=' + $scope.mediaFolderId
        });
        $http.get('/webapi/PropertyApi/GetAllPropertyTypes').success(function (data, status) {
            $scope.propertyTypes = data;
        });

        $scope.mediaTypes = 2;

        if (action == 'create') {
            propertyId = 0;

            var promise = $http.get('/webapi/UserApi/GetLoggedInUser', {});
            promise.then(
                function (payload) {
                    var c = payload.data;

                    $scope.user = {
                        UserName: c.UserName,
                        Id: c.Id,
                        FirstName: c.FirstName,
                        LastName: c.LastName,
                        UserRoles: c.UserRoles,
                    };
                }

            );
        }

        if (action == 'edit') {

            var promise = $http.get('/webapi/PropertyApi/GetProperty?propertyId=' + propertyId, {});
            promise.then(
                function (payload) {
                    var m = payload.data;

                    $scope.property = {
                        PropertyId: m.PropertyId,
                        Location: m.Location,
                        Description: m.Description,
                        PropertyTypeId : m.PropertyTypeId,
                        PropertyFee: m.PropertyFee,
                        MediaFolderId :m.MediaFolderId,
                        CreatedOn: m.CreatedOn,
                        Timestamp: m.Timestamp,
                        VersionGuid: m.VersionGuid,
                        PropertyGuid: m.PropertyGuid,
                        CreatedBy: m.CreatedBy,
                        UpdatedBy: m.UpdatedBy,
                        UnPublishedBy: m.UnPublishedBy,
                        PublishedBy: m.PublishedBy,
                        Published: m.Published,
                        PublishedOn : m.PublishedOn,


                    };

                });


        }

        $scope.Save = function (property,publish) {

            $scope.showMessageSave = false;
            usSpinnerService.spin('global-spinner');
            if ($scope.form.$valid) {
                var promise = $http.post('/webapi/PropertyApi/Save', {
                    PropertyId: propertyId,
                    Location : property.Location,
                    Description : property.Description,
                    PropertyFee: property.PropertyFee,
                    PropertyTypeId : property.PropertyTypeId,
                    CreatedOn: property.CreatedOn,
                    MediaFolderId: property.MediaFolderId,
                    Timestamp : property.Timestamp,
                    CreatedBy: property.CreatedBy,
                    UpdatedBy: property.UpdatedBy,
                    UnPublishedBy: property.UnPublishedBy,
                    PublishedBy: property.PublishedBy,
                    Published : publish

                });

                promise.then(
                    function (payload) {

                        PropertyId = payload.data;
                        usSpinnerService.stop('global-spinner');

                        if (publish) {
                            $scope.showMessagePropertyPublished = true;
                        }
                        else {
                            $scope.showMessageSave = true;
                        }
                       
                        $timeout(function () {
                            $scope.showMessageSave = false;
                            $scope.showMessagePropertyPublished = false;

                            if (action == "create") {
                                $state.go('property-edit', { 'action': 'edit', 'propertyId': PropertyId });
                            }

                        }, 1500);


                    });
            }

        }



        $scope.Cancel = function () {
            $state.go('properties.list');

        };

        $scope.UnPublish = function (property) {
            var promise = $http.get('/webapi/PropertyApi/UnPublish?draftPropertyId=' + property.PropertyId, {});
            promise.then(
                function (payload) {
                    $scope.showMessagePropertyUnPublished = true;
                    $timeout(function () {
                        $scope.showMessagePropertyUnPublished = false;
                    }, 2000);
                }
            );
        };


        $scope.Delete = function (propertyId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/PropertyApi/Delete?propertyId=' + propertyId, {});
            promise.then(
                function (payload) {
                    $scope.showMessageDeleted = true;
                    $timeout(function () {
                        $scope.showMessageDeleted = false;
                        $scope.Cancel();
                    }, 2500);
                    $scope.showMessageDeleteFailed = false;
                },
                function (errorPayload) {
                    $scope.showMessageDeleteFailed = true;
                    $timeout(function () {
                        $scope.showMessageDeleteFailed = false;
                        $scope.Cancel();
                    }, 1500);
                });
        }

        //Images section
        $scope.showUploadFiles = true;

        //Load files and display in table
        var promise = $http.get('/webapi/MediaApi/GetFilesInFolder?folderId=' + $scope.mediaFolderId + '&mediaTypes=' + $scope.mediaTypes, {});
        promise.then(
            function (payload) {
                //$scope.folder.Files = payload.data;
                $scope.showFileProperties = false;
                $scope.data = payload.data;

                $scope.tableParams = new ngTableParams({
                    page: 1,            // show first page
                    count: 20,          // count per page
                    sorting: {
                        Name: 'asc'     // initial sorting
                    }
                }, {
                    getData: function ($defer, params) {
                        var filteredData = $filter('filter')($scope.data, $scope.filter);
                        var orderedData = params.sorting() ?
                                            $filter('orderBy')(filteredData, params.orderBy()) :
                                            filteredData;
                        if (orderedData != null) {
                            params.total(orderedData.length);
                            $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                        }
                    }
                });

            }
        );


        $scope.Preview = function (media) {
            appSettingsService.getAppSettingsAsync(function (data) {
                $window.open(data.SharedMediaFolder + '\\' + media.MediaId + '\\' + media.Name);
            });
        };

        $scope.basicResize = function (mediaId, src) {
            resizeService.resizeImage(src, { width: 100 }, function (err, image) {
                if (err) {
                    return;
                }
                var basicImgResizedWidth = document.createElement('img');
                basicImgResizedWidth.src = image;
                document.getElementById('img' + mediaId).appendChild(basicImgResizedWidth);
            });
        };

        $scope.CancelFileUpload = function () {
            $scope.showUploadFiles = false;
            uploader.clearQueue();
        }

        $scope.selectMedia = function (media) {
            $scope.showUploadFiles = false;
            $scope.showEditFolder = false;
            $scope.showFolderCreated = false;
            $scope.showFolderUpdated = false;
            $scope.selectedMedia = media;

            $scope.showFileProperties = true;
            $scope.showWarningDeleteFile = false;
        }

        $scope.WarningDeleteFile = function () {
            $scope.showWarningDeleteFile = true;
        }

        $scope.DeleteFile = function (media) {
            var promise = $http.get('/webapi/MediaApi/MarkAsDeleted?mediaId=' + media.MediaId + '&rnd=' + new Date().getTime(), {});
            promise.then(
                function (payload) {

                    var index = $scope.data.indexOf(media)
                    $scope.data.splice(index, 1);
                    $scope.tableParams.reload();

                    $scope.showFileDeleted = true;
                    $scope.showWarningDeleteFile = false;
                    $scope.showFileProperties = false;

                    setTimeout(function () {
                        $scope.$apply(function () {
                            $scope.showFileDeleted = false;
                        });
                    }, 3000);


                }
            );
        }

        $scope.UploadFiles = function () {
            $scope.showUploadFiles = true;
        }

        var validFileTypes = '|jpg|jpeg|png|gif|bmp|';
        var validFileExtentions = ["bmp", "gif", "png", "jpg", "jpeg"];
        uploader.filters.push({
            name: 'imageFilter',
            fn: function (item /*{File|FileLikeObject}*/, options) {
                var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
                var ext = item.name.substring(item.name.lastIndexOf(".") + 1, item.name.length).toLowerCase();
                if (validFileTypes.indexOf(type) !== -1 && validFileExtentions.indexOf(ext) !== -1)
                    return true;
                else
                    return false
            }
        });
        uploader.filters.push({
            name: 'customFilter',
            fn: function (item /*{File|FileLikeObject}*/, options) {
                return this.queue.length < 10;
            }
        });
        uploader.onCompleteAll = function () {

            //Load files and display in table
            var promise = $http.get('/webapi/MediaApi/GetFilesInFolder?folderId=' + $scope.mediaFolderId + '&mediaTypes=' + $scope.mediaTypes, {});
            promise.then(
                function (payload) {

                    $scope.data = payload.data; //$scope.data
                    $scope.showFileProperties = false;
                    $scope.tableParams.reload();
                }
            );
        };

    }
    ]);


angular
    .module('homer').controller('PropertyController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/PropertyApi/GetAllProperties');
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );

            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: true
            };

            $scope.gridData.multiSelect = true;

            $scope.gridData.columnDefs = [

                {
                    name: 'Description', field:'Description',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },

                { name: 'Location', field: 'Location' },

                 { name: 'PropertyFee', field: 'PropertyFee' },

                    { name: 'Property Type', field: 'PropertyTypeName' },

                    
            {
            name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/properties/edit/{{row.entity.PropertyId}}/{{row.entity.MediaFolderId}}">Edit</a> </div>',
           
        },
            ];       
        }]);


angular
    .module('homer')
    .controller('MediaDetailController', ['$scope', '$http', '$filter', '$location', '$log', '$timeout', '$state', '$interval', 'resizeService',
    function ($scope, $http, $filter, $location, $log, $timeout, $state, $interval, resizeService) {

        var mediaId = $scope.mediaId;
        var action = $scope.action;        
        $scope.mediaTypes = 2;
        $scope.showComments = true;

        var promise = $http.get('/webapi/MediaApi/GetMedia?mediaId=' + mediaId, {});
        promise.then(
            function (payload) {
                var media = payload.data;
                $scope.selectedMedia = {
                    MediaId: media.MediaId,
                    CreatedOn: media.CreatedOn,
                    ParentId: media.ParentId,
                    TimeStamp: media.TimeStamp,
                    Name: media.Name,
                    Path: media.Path,
                    Filesize: media.Filesize,
                    MediaTypeId: media.MediaTypeId,
                    Comments: media.Comments,
                };
                $scope.mediaComments = $scope.selectedMedia.Comments;
            });

        $scope.addComment = function (comment) {
            $scope.showCommentMessageSave = false;
            var promise = $http.post('/webapi/MediaApi/SaveComment', { MediaId: mediaId, Body: comment.Body });
            promise.then(
            function (payload) {
                $scope.savedComment = payload.data;
                $scope.comment.commentId = $scope.savedComment.CommentId;

                $scope.mediaComments.push({ CommentId: $scope.savedComment.CommentId, MediaId: mediaId, Body: comment.Body, CreatedBy: comment.CreatedBy, CreatedOn: $scope.savedComment.CreatedOn });
                comment.Body = "";
                
                $scope.showCommentMessageSave = true;
                $scope.commentform.$setPristine();
                setTimeout(function () {
                    $scope.$apply(function () {
                        $scope.showCommentMessageSave = false;
                    });
                }, 2000);
            });
        }

        $scope.DeleteComment = function (comment) {


            $scope.showCommentMessageDelete = false;
            $scope.showCommentMessageDeleteFailed = false;

            var promise = $http.get('/webapi/MediaApi/deleteComment/?commentId=' + comment.CommentId, {});
            promise.then(
                function (payload) {
                    $scope.showCommentMessageDelete = true;

                    var index = $scope.contentComments.indexOf(comment)
                    $scope.contentComments.splice(index, 1);

                    setTimeout(function () {
                        $scope.$apply(function () {
                            $scope.showCommentMessageDelete = false;
                        });
                    }, 2000);
                },
                function (errorPayload) {
                    $log.error('faliled to delete comment with id' + comment.CommentId, errorPayload);
                    $scope.showCommentMessageDeleteFailed = true;
                });

        }

    }]);


angular
    .module('homer').controller('UserPropertyController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var frontUserId = $scope.frontUserId;
            var promise = $http.get('/webapi/PropertyApi/GetAllPropertiesForAParticularUser?frontUserId='+ frontUserId,{});
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );

            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: true
            };

            $scope.gridData.multiSelect = false;

            $scope.gridData.columnDefs = [

                {
                    name: 'Description', field:'Description',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },

                { name: 'Location', field: 'Location' },

                 { name: 'PropertyFee', field: 'PropertyFee' },

                    { name: 'Property Type', field: 'PropertyTypeName' },
                     {
                         name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/properties/edit/{{row.entity.PropertyId}}/{{row.entity.MediaFolderId}}">Edit</a> </div>',

                     },
            ];
        }]);



angular
    .module('homer').controller('PublishedPropertyController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/PropertyApi/GetAllPublishedProperties');
            promise.then(
                function (payload) {
                    $scope.gridData.data = payload.data;
                    $scope.loadingSpinner = false;
                }
            );

            $scope.gridData = {
                enableFiltering: true,
                columnDefs: $scope.columns,
                enableRowSelection: true
            };

            $scope.gridData.multiSelect = true;

            $scope.gridData.columnDefs = [

                {
                    name: 'Description', field: 'Description',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },

                { name: 'Location', field: 'Location' },

                 { name: 'PropertyFee', field: 'PropertyFee' },

                    { name: 'Property Type', field: 'PropertyTypeName' },


            {
                name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/properties/edit/{{row.entity.PropertyId}}/{{row.entity.MediaFolderId}}">Edit</a> </div>',

            },
            ];
        }]);
