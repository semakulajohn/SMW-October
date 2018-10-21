function configState($stateProvider, $urlRouterProvider, $compileProvider) {

    // Optimize load start with remove binding information inside the DOM element
    $compileProvider.debugInfoEnabled(false);
    
    // Set default state
    $urlRouterProvider

        .otherwise("/dashboard");  

    $stateProvider
        .state('sample', {
            url: "/sample",
            templateUrl: "/app/views/sample.html",
            data: {
                pageTitle: 'sample',

            }
        })
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

    .state('project', {
        url: "/projects",
        templateUrl: "/app/views/project/list.html",
        data: {
            pageTitle: 'Projects',
        },
        controller: function ($scope, $stateParams) {

        }
    })

         .state('projectdetail', {
             url: "/projects/view/:projectId/:mediaFolderId",
             templateUrl: "/app/views/project/details.html",
             data: {
                 pageTitle: 'Project Details',
                 pageDesc: ''
             },
             controller: function ($scope, $stateParams) {
                 $scope.action = 'view';
                 $scope.projectId = $stateParams.projectId;
                 $scope.mediaFolderId = $stateParams.mediaFolderId;
                 $scope.defaultTab = 'edit';
             }
         })
   
      .state('ongoingprojects', {
          url: "/ongoingprojects",
          templateUrl: "/app/views/project/ongoinglist.html",
          data: {
              pageTitle: 'OnGoing Projects',
          },
          controller: function ($scope, $stateParams) {

          }
      })

         .state('completedprojects', {
             url: "/completedprojects",
             templateUrl: "/app/views/project/completedlist.html",
             data: {
                 pageTitle: 'Completed Projects',
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

    .state('rentalproperties', {
        url: "/rentalproperties",
        templateUrl: "/app/views/property/rentallist.html",
        data: {
            pageTitle: 'Rental Properties',
        },
        controller: function ($scope, $stateParams) {

        }
    })

         .state('saleproperties', {
             url: "/saleproperties",
             templateUrl: "/app/views/property/salelist.html",
             data: {
                 pageTitle: 'Sale Properties',
             },
             controller: function ($scope, $stateParams) {

             }
         })
          
    .state('propertyEdit', {
        url: "/properties/view/:propertyId/:mediaFolderId",
        templateUrl: "/app/views/property/details.html",
        data: {
            pageTitle: 'Property Details',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = 'view';
            $scope.propertyId = $stateParams.propertyId;
            $scope.mediaFolderId = $stateParams.mediaFolderId;
            $scope.defaultTab = 'edit';
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
        templateUrl: "/app/views/service/details.html",
        data: {
            pageTitle: 'Service Details',
            pageDesc: ''
        },
        controller: function ($scope, $stateParams) {
            $scope.action = $stateParams.action;
            $scope.serviceId = $stateParams.serviceId;
            $scope.mediaFolderId = $stateParams.mediaFolderId;
            $scope.defaultTab = 'edit';
        }
    })
         
    //contact
    $stateProvider
    .state('contact', {
        url: "/contactus",
        templateUrl: "/app/views/contactus/contactus.html",
        data: {
            pageTitle: 'Contact us'
        }
    })

    //about
    $stateProvider
    .state('about', {
        url: "/aboutus",
        templateUrl: "/app/views/aboutus/aboutus.html",
        data: {
            pageTitle: ' About Us'
        }
    })

      //facilities
     .state('facility', {
        // abstract: true,
         url: "/facilities",
         templateUrl: "/app/views/facility/view.html",
         data: {
             pageTitle: 'Facilities'
         }
     })
          .state('facilitydetail', {
              url: "/facilities/view/:facilityId/:mediaFolderId",
              templateUrl: "/app/views/facility/details.html",
              data: {
                  pageTitle: 'Facility Details',
                  pageDesc: ''
              },
              controller: function ($scope, $stateParams) {
                  $scope.action = 'view';
                  $scope.facilityId = $stateParams.facilityId;
                  $scope.mediaFolderId = $stateParams.mediaFolderId;
                  $scope.defaultTab = 'edit';
              }
          })

     //designs
     .state('design', {
         // abstract: true,
         url: "/designs",
         templateUrl: "/app/views/design/view.html",
         data: {
             pageTitle: 'Design'
         }
     })

  //team
     .state('team', {
        
         url: "/ourteam",
         templateUrl: "/app/views/team/ourteam.html",
         data: {
             pageTitle: 'Our Team'
         }
     })

        //land
     .state('land', {

         url: "/land",
         templateUrl: "/app/views/land/view.html",
         data: {
             pageTitle: 'Land Processing'
         }
     })

     //partner
     .state('partner', {

         url: "/partner",
         templateUrl: "/app/views/partner/view.html",
         data: {
             pageTitle: 'partners'
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
  