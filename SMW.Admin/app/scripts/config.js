function configState($stateProvider, $urlRouterProvider, $compileProvider) {

    // Optimize load start with remove binding information inside the DOM element
    $compileProvider.debugInfoEnabled(false);
    
    // Set default state
    $urlRouterProvider

        .otherwise("/dashboard");  

    $stateProvider
          // Dashboard - Main page
        .state('dashboard', {
            url: "/dashboard",
            templateUrl: "/app/views/dashboard.html",
            data: {
                pageTitle: 'Dashboard',
               
            }
        })
   
         .state('login', {
             url: "/login",
             templateUrl: "/app/views/adminAccount/login/login.html",
             data: {
                 //pageTitle: 'Profile'
             }
         })
              // User Profile page
    .state('profile', {
        url: "/profile",
        templateUrl: "/app/views/_common/profile.html",
        data: {
            pageTitle: 'Profile'
        }
    })
     

    

          // Modules section 


   //projects
     .state('projects', {
         abstract: true,
         url: "/projects",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Projects'
         }
     })

    .state('projects.list', {
        url: "/projects",
        templateUrl: "/app/views/project/list.html",
        data: {
            pageTitle: 'Projects',
        },
        controller: function ($scope, $stateParams) {

        }
    })
        .state('media-detail', {
            url: "/media/:action/:mediaId",
            templateUrl: "/app/views/project/media-detail.html",
            data: {
                pageTitle: 'Media Detail',
                pageDesc: ''
            },
            controller: function ($scope, $stateParams) {
                $scope.action = $stateParams.action;
                $scope.mediaId = $stateParams.mediaId;             
            }
        })

         .state('project-edit', {
             url: "/projects/:action/:projectId/:mediaFolderId",
             templateUrl: "/app/views/project/edit.html",
             data: {
                 pageTitle: 'Project edit',
                 pageDesc: ''
             },
             controller: function ($scope, $stateParams) {
                 $scope.action = $stateParams.action;
                 $scope.projectId = $stateParams.projectId;
                 $scope.mediaFolderId = $stateParams.mediaFolderId;
                 $scope.defaultTab = 'edit';
             }
         })
   
          .state('client-Project-edit', {
              url: "/project/:action/:projectId/:mediaFolderId/:clientId",
              templateUrl: "/app/views/project/edit.html",
              data: {
                  pageTitle: 'Client Project edit',
                  pageDesc: ''
              },
              controller: function ($scope, $stateParams) {
                  $scope.action = $stateParams.action;
                  $scope.projectId = $stateParams.projectId;
                  $scope.mediaFolderId = $stateParams.mediaFolderId;
                  $scope.clientId = $stateParams.clientId;
                  $scope.defaultTab = 'edit';
              }
          })

          .state('client-projects', {
              url: "/projects/:clientId",
              templateUrl: "/app/views/client/client-project.html",
              data: {
                  pageTitle: 'Client Projects',
                  pageDesc: ''
              },
              controller: function ($scope, $stateParams) {
                  $scope.clientId = $stateParams.clientId;

              }
          })

        .state('user-projects', {
            url: "/projects/:clientId",
            templateUrl: "/app/views/client/client-project.html",
            data: {
                pageTitle: 'Projects',
                pageDesc: ''
            },
            controller: function ($scope, $stateParams) {
                $scope.clientId = $stateParams.clientId;
            }
        })
          //webqueries
     .state('webqueries', {
         abstract: true,
         url: "/webqueries",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'WebQueries'
         }
     })

    .state('webqueries.list', {
        url: "/webqueries",
        templateUrl: "/app/views/webquery/list.html",
        data: {
            pageTitle: 'WebQueries',
        },
        controller: function ($scope, $stateParams) {

        }
    })

         //properties
     .state('properties', {
         abstract: true,
         url: "/properties",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Properties'
         }
     })

    .state('properties.list', {
        url: "/properties",
        templateUrl: "/app/views/property/list.html",
        data: {
            pageTitle: 'Properties',
        },
        controller: function ($scope, $stateParams) {

        }
    })

        .state('properties.publishedlist', {
            url: "/properties/published",
            templateUrl: "/app/views/property/publishedlist.html",
            data: {
                pageTitle: 'Published Properties',
            },
            controller: function ($scope, $stateParams) {

            }
        })
    .state('property-edit', {
        url: "/properties/:action/:propertyId/:mediaFolderId",
        templateUrl: "/app/views/property/edit.html",
        data: {
            pageTitle: 'Property edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.propertyId = $stateParams.propertyId;
            $scope.mediaFolderId = $stateParams.mediaFolderId;
            $scope.defaultTab = 'edit';
        }
    })

         .state('user-properties', {
             url: "/properties/:frontUserId",
             templateUrl: "/app/views/property/user-property.html",
             data: {
                 pageTitle: 'Properties',
                 pageDesc: ''
             },
             controller: function ($scope, $stateParams) {
                 $scope.frontUserId = $stateParams.frontUserId;
             }
         })

          //services
     .state('services', {
         abstract: true,
         url: "/services",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Services'
         }
     })

    .state('services.list', {
        url: "/services",
        templateUrl: "/app/views/service/list.html",
        data: {
            pageTitle: 'Services',
        },
        controller: function ($scope, $stateParams) {

        }
    })


    .state('service-edit', {
        url: "/services/:action/:serviceId/:mediaFolderId",
        templateUrl: "/app/views/service/edit.html",
        data: {
            pageTitle: 'Service edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.serviceId = $stateParams.serviceId;
            $scope.mediaFolderId = $stateParams.mediaFolderId;
            $scope.defaultTab = 'edit';
        }
    })

         //facilities
     .state('facilities', {
         abstract: true,
         url: "/facilities",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Facilities'
         }
     })

    .state('facilities.list', {
        url: "/facilities",
        templateUrl: "/app/views/facility/list.html",
        data: {
            pageTitle: 'Facilities',
        },
        controller: function ($scope, $stateParams) {

        }
    })


    .state('facility-edit', {
        url: "/facilities/:action/:facilityId/:mediaFolderId",
        templateUrl: "/app/views/facility/edit.html",
        data: {
            pageTitle: 'Facility edit',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.facilityId = $stateParams.facilityId;
            $scope.mediaFolderId = $stateParams.mediaFolderId;
            $scope.defaultTab = 'edit';
        }
    })

           //clients
     .state('clients', {
         abstract: true,
         url: "/clients",
         templateUrl: "/app/views/_common/content_empty.html",
         data: {
             pageTitle: 'Clients'
         }
     })

     .state('clients.list', {
         url: "/clients",
         templateUrl: "/app/views/client/list.html",
         data: {
             pageTitle: 'Cleints',
         },
         controller: function ($scope, $stateParams) {

         }
     })

         
    //Search
    $stateProvider
    .state('search', {
        url: "/search/:q",
        templateUrl: "/app/views/search/index.html",
        data: {
            pageTitle: 'Search'
        },
        controller: function ($scope, $stateParams) {
            $scope.q = $stateParams.q;
        }
    })

}

angular
    .module('homer')
    .config(configState).run(function ($rootScope, $state) {
        $rootScope.$state = $state;
      
        $rootScope.$on("$locationChangeStart", function (event, next, current) {
            if (next.match("/UsersAdmin/")) {
                var parts = next.split('#');
                if (parts.length > 1) {
                    if (!next.match('#/dashboard')) {
                        window.location = '/#' + parts[1];
                    }
                }
            }
        });

    })
  