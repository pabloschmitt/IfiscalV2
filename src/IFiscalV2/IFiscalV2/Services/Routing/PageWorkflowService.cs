using IFiscalV2.Common;
using IFiscalV2.Services.Auth;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFiscalV2.Services.Routing
{


    public class PageWorkflowService : IPageWorkflowServiceOptions, IPageWorkflowService
    {
        private readonly IRoutingService _routingService;
        private readonly IAuthService _authService;

        public PageWorkflowService(IRoutingService routingService = null, IAuthService authService = null)
        {
            _instance = this;

            _routingService = routingService ?? Locator.Current.GetService<IRoutingService>();
            _authService = authService ?? Locator.Current.GetService<IAuthService>();
        }

        #region Singleton
        private static PageWorkflowService _instance;
        public static PageWorkflowService Instance => _instance is null ? new PageWorkflowService() : _instance;
        #endregion


        #region IPageWorkflowService - GET / SET
        public string Route { get; set; } = string.Empty;

        public bool InGlobal { get; set; }
        public string SelectedSiteId { get; set; }
        public string SelectedSiteName { get; set; }
        public bool InEleccion { get; set; }
        public string SelectedEleccionId { get; set; }
        public string SelectedEleccionName { get; set; }

        public bool BackButtonNoAllowed { get; set; }
        public bool IsVisible_SiteChange { get; set; }
        public bool IsVisible_EleccionChange { get; set; }
        public bool IsVisible_Resultados { get; set; }
        public bool IsVisible_Mesas { get; set; }
        #endregion

        public void BuildShellVisibleItems()
        {
            #region Reset 
            IsVisible_SiteChange = false;
            IsVisible_EleccionChange = false;
            IsVisible_Resultados = false;
            IsVisible_Mesas = false;
            #endregion

            if (_authService.IsLoggedIn)
            {
                if (_authService.HasRole_GlobalSiteAdmin || _authService.HasRole_SiteAdmin)
                {
                    SelectedSiteName = InGlobal ? "GLOBAL" : $"{SelectedSiteName}";
                    IsVisible_SiteChange = 
                        (_authService.HasRole_GlobalSiteAdmin && _authService.HasRole_GlobalSiteAdmin && InGlobal);
                    IsVisible_EleccionChange = (_authService.HasRole_GlobalSiteAdmin && !InGlobal && !InEleccion);

                } // HasRole_GlobalSiteAdmin

                IsVisible_Resultados =
                    (_authService.HasRole_GlobalSiteAdmin || _authService.HasRole_SiteAdmin
                    || _authService.HasRole_Resultados || _authService.HasRole_Intendente) && InEleccion;

                IsVisible_Mesas =
                    (_authService.HasRole_GlobalSiteAdmin || _authService.HasRole_SiteAdmin
                    || _authService.HasRole_Resultados || _authService.HasRole_ResponsableDeEscuela
                    || _authService.HasRole_Fiscal) && InEleccion;

            } // if (_authService.IsLoggedIn)
        }

        public string GetNextRoute()
        {
            #region Armado de la Ruta a la cual dirigirse
            if (_authService.IsLoggedIn)
            {
                if (_authService.HasRole_GlobalSiteAdmin || _authService.HasRole_SiteAdmin)
                {
                    if (InGlobal)
                    {
                        Route = "//main/site_change";
                    }
                    else
                    {
                        if (!InEleccion)
                        {
                            Route = "//main/eleccion_change";
                        }
                        else
                        {
                            Route = "//main/sel_mesas";
                        }
                    }
                }
                else if (_authService.HasRole_Resultados && InEleccion)
                {
                    Route = "//main";
                }
                else if ((_authService.HasRole_Fiscal || _authService.HasRole_ResponsableDeEscuela) && InEleccion)
                {
                    Route = "//main";
                }

                ApplicationSettings.LastRoute = Route;

            }
            else
            {
                ApplicationSettings.LastRoute = string.Empty;
                Route = "//main/login";
            }
            #endregion
            return Route;
        }

        public void SincFromSettings()
        {
            Route = ApplicationSettings.LastRoute;
            InGlobal = ApplicationSettings.InGlobal;
            InEleccion = ApplicationSettings.InEleccion;
            SelectedSiteId = ApplicationSettings.SelectedSiteId;
            SelectedSiteName = ApplicationSettings.SelectedSiteName;
            SelectedEleccionId = ApplicationSettings.SelectedEleccionId;
            SelectedEleccionName = ApplicationSettings.SelectedEleccionName;
        }

        public void Save()
        {
            ApplicationSettings.LastRoute = Route;
            ApplicationSettings.InGlobal = InGlobal;
            ApplicationSettings.InEleccion = InEleccion;
            ApplicationSettings.SelectedSiteId = SelectedSiteId;
            ApplicationSettings.SelectedSiteName = SelectedSiteName;
            ApplicationSettings.SelectedEleccionId = SelectedEleccionId;
            ApplicationSettings.SelectedEleccionName = SelectedEleccionName;
        }

    } // PageWorkflowService

}
