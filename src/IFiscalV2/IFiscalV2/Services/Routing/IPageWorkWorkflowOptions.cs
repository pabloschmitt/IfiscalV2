using System;
using System.Collections.Generic;
using System.Text;

namespace IFiscalV2.Services.Routing
{
    public interface IPageWorkflowServiceOptions
    {
        bool BackButtonNoAllowed { get; set; }
        bool InEleccion { get; set; }
        bool InGlobal { get; set; }
        bool IsVisible_EleccionChange { get; set; }
        bool IsVisible_Mesas { get; set; }
        bool IsVisible_Resultados { get; set; }
        bool IsVisible_SiteChange { get; set; }
        string Route { get; set; }
        string SelectedEleccionId { get; set; }
        string SelectedEleccionName { get; set; }
        string SelectedSiteId { get; set; }
        string SelectedSiteName { get; set; }
    }


}
