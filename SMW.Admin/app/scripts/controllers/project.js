angular
    .module('homer')
    .controller('ProjectEditController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', '$log', '$timeout', '$modal', '$state', 'uiGridConstants', '$interval', 'selectClientService', 'resizeService', 'FileUploader', 'appSettingsService', 'usSpinnerService',
    function ($scope, ngTableParams, $http, $filter, $location, $log, $timeout, $modal, $state, uiGridConstants, $interval, selectClientService,resizeService, FileUploader, appSettingsService,usSpinnerService) {

        $scope.tab = {};
        if ($scope.defaultTab == 'dashboard') {
            $scope.tab.dashboard = true;
        }

        $scope.showMessageClientSelected = false;
        var projectId = $scope.projectId;
        var action = $scope.action;
        var selectedClientId = "";
        var uploader = $scope.uploader = new FileUploader({
            url: '/upload/?parentId=' + $scope.mediaFolderId
        });

        $scope.mediaTypes = 2;



        $http.get('/webapi/ProjectApi/GetAllStatuses').success(function (data, status) {
            $scope.statuses = data;
        });

        if (action == 'create') {
            projectId = 0;

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

            var promise = $http.get('/webapi/ProjectApi/GetProject?projectId=' + projectId, {});
            promise.then(
                function (payload) {
                    var m = payload.data;

                    $scope.project = {
                        ProjectId : m.ProjectId,
                        Title : m.Title,
                        Description: m.Description,
                        StatusId : m.StatusId,
                        ClientId :m.ClientId,
                        CreatedOn: m.CreatedOn,
                        MediaFolderId: m.MediaFolderId,
                        Timestamp :m.Timestamp,
                        CreatedBy :m.CreatedBy,
                        UpdatedBy :m.UpdatedBy,
                        

                    };
                    selectedClientId = m.ClientId;

                });


        }
        $scope.selectClient = function () {
            $scope.modalInstance = $modal.open({
                templateUrl: '/app/views/modal/modalClient.html' + CACHE_BUST_SUFFIX,
                size: 'large',
                controller: 'ModalInstanceCtrl',
                resolve: {
                    items: function () {
                        $scope.dataItems = {

                        }
                        return $scope.dataItems;
                    },
                },
                windowClass: 'contentModalBox'
            });
        };

        $scope.$on('selectClientServiceBroadcastHandler', function () {


            $scope.selectedClients = [];
            $scope.filteredClients = [];
            var currentDate = new Date();

            selectClientService.item.forEach(function (client) {
                var clientIds = {
                    Id: client.Id,

                };

                $scope.selectedClients.push(clientIds);
                selectedClientId = clientIds.Id;

            });
            $scope.showMessageClientSelected = true;
            $timeout(function () {
                $scope.showMessageSave = false;
               // $scope.showMessageClientSelected = false;
                if ($scope.modalInstance != null)
                    $scope.modalInstance.close();
            }, 1500);

        });

        $scope.Save = function (project) {
            if ($scope.user.UserRoles == 'client') {
                selectedClientId = $scope.user.Id;
            }
            if (($scope.selectedClients == null || $scope.selectedClients == undefined) && selectedClientId == "") {
                $scope.showMessageClientNotSelected = true;
                $timeout(function () {
                    $scope.showMessageClientNotSelected = false;

                }, 2000);

            }
            else {
                $scope.showMessageSave = false;
                usSpinnerService.spin('global-spinner');
                if ($scope.form.$valid) {
                    var promise = $http.post('/webapi/ProjectApi/Save', {
                        ProjectId: projectId,

                        ProjectId: project.ProjectId,
                        Title: project.Title,
                        Description: project.Description,
                        MediaFolderId: project.MediaFolderId,
                        StatusId : project.StatusId,
                        ClientId: selectedClientId,
                        CreatedOn: project.CreatedOn,
                        Timestamp: project.Timestamp,
                        CreatedBy: project.CreatedBy,
                        UpdatedBy: project.UpdatedBy,


                    });

                    promise.then(
                        function (payload) {

                            ProjectId = payload.data;
                            usSpinnerService.stop('global-spinner');
                            $scope.showMessageSave = true;

                            $timeout(function () {
                                $scope.showMessageSave = false;
                                $scope.showMessageClientSelected = false;
                                if ($scope.modalInstance != null)
                                    $scope.modalInstance.close();


                                if (action == "create") {
                                    $state.go('project-edit', { 'action': 'edit', 'projectId': ProjectId });
                                }

                            }, 1500);


                        });
                }

            }

            
        }



        $scope.Cancel = function () {
            $state.go('projects.list');

        };

        $scope.Delete = function (projectId) {
            $scope.showMessageDeleted = false;
            var promise = $http.get('/webapi/ProjectApi/Delete?projectId=' + projectId, {});
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
        //var validFileTypes = '|jpg|jpeg|png|gif|bmp|mp4|mkv|';

        var validFileExtentions = ["bmp", "gif", "png", "jpg", "jpeg"];
        //var validFileExtentions = ["bmp", "gif", "png", "jpg", "jpeg","mp4","mkv"];
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
    .module('homer').controller('ProjectController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var promise = $http.get('/webapi/ProjectApi/GetAllProjects');
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
                    name: 'Title', field :'Title',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },

                { name: 'Description', field: 'Description' },

                 { name: 'Client Name', field: 'ClientName' },
                 {name : 'Status',field:'StatusName'},

                 {
                     name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/projects/edit/{{row.entity.ProjectId}}/{{row.entity.MediaFolderId}}">Edit</a> </div>',
   
                 },
               
            ];




        }]);


angular
    .module('homer')
    .controller("ClientsPickerController", ['$scope', '$modal', '$http', '$timeout', '$filter', 'Utils', '$window', 'selectClientService', '$sessionStorage', 'uiGridConstants',
function ($scope, $modal, $http, $timeout, $filter, Utils, $window, selectClientService, $sessionStorage, uiGridConstants) {


    $scope.$sessionStorage = $sessionStorage;
    $scope.error = false;
    $scope.gridData = {
        enableFiltering: true,
        columnDefs: $scope.columns,
        enableRowSelection: true
    };

    $scope.gridData.multiSelect = false;

    $scope.gridData.columnDefs = [

        { name: 'ClientId', field: 'Id', width: '15%', cellTemplate: '<div class="ui-grid-cell-contents"><a href="#/client/edit/{{row.entity.Id}}">{{row.entity.Id}}</a></div>' },
        {
            name: 'First Name', field: 'FirstName',
            sort: {
                direction: uiGridConstants.ASC,
                priority: 1
            }
        },

        { name: 'LastName', field: 'LastName', width: '15%', },

        { name: 'Email', field: 'Email' },


    ];


    $scope.gridData.onRegisterApi = function (gridApi) {
        $scope.gridApi = gridApi;
    };




    $scope.loadingSpinner = true;
    var promise = $http.get('/webapi/UserApi/GetAllClients');
    promise.then(
        function (payload) {
            $scope.gridData.data = payload.data;
            $scope.loadingSpinner = false;
        }
    );

    $scope.selectProjectClient = function () {
        $scope.selectedClients = $scope.gridApi.selection.getSelectedRows($scope.gridData);
        if ($scope.selectedClients != null) {
            selectClientService.prepForBroadcast($scope.selectedClients);

        }
    }


}]);



angular
    .module('homer').controller('ClientProjectController', ['$scope', 'ngTableParams', '$http', '$filter', '$location', 'Utils', 'uiGridConstants',
        function ($scope, ngTableParams, $http, $filter, $location, Utils, uiGridConstants) {
            $scope.loadingSpinner = true;
            var clientId = $scope.clientId;
            var promise = $http.get('/webapi/ProjectApi/GetAllClientProjects?clientId=' + clientId, {});
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
                    name: 'Title',field:'Title',
                    sort: {
                        direction: uiGridConstants.ASC,
                        priority: 1
                    }
                },

                { name: 'Description', field: 'Description' },

                 { name: 'Client Name', field: 'ClientName' },

                  {
                      name: 'Action', cellTemplate: '<div class="ui-grid-cell-contents"> <a href="#/projects/edit/{{row.entity.ProjectId}}/{{row.entity.MediaFolderId}}">Edit</a> </div>',
                     
                  },

            ];




        }]);
