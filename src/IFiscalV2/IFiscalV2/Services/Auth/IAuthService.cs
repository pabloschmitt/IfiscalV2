using IFiscalV2.Models.Auth;
using System.Threading.Tasks;

namespace IFiscalV2.Services.Auth
{
    public interface IAuthService
    {
        Common_ES CES { get; }
        bool HasRole_Colador { get; set; }
        bool HasRole_ConfigAdmin { get; set; }
        bool HasRole_Fiscal { get; set; }
        bool HasRole_GlobalSiteAdmin { get; set; }
        bool HasRole_Intendente { get; set; }
        bool HasRole_ResponsableDeEscuela { get; set; }
        bool HasRole_Resultados { get; set; }
        bool HasRole_SiteAdmin { get; set; }
        bool HasRole_SoloVer { get; set; }
        bool HasRole_UserAdmin { get; set; }
        bool HasRole_Verificador { get; set; }
        bool IsLoggedIn { get; set; }
        JwtAuthToken Token { get; set; }

        Task<LoginResult> CheckPreviusLoginAsync();
        Task<LoginResult> LoginAsync(UserLogin userLogin);
        Task LogoutAsync();
        void StartRefreshToken();
        void StopRefreshToken();
    }
}