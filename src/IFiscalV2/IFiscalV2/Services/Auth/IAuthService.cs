using IFiscalV2.Models.Auth;
using System.Threading.Tasks;

namespace IFiscalV2.Services.Auth
{
    public interface IAuthService
    {
        Common_ES CES { get; }
        bool IsLoggedIn { get; set; }
        JwtAuthToken Token { get; set; }

        Task<LoginResult> CheckPreviusLoginAsync();
        Task<LoginResult> LoginAsync(UserLogin userLogin, bool isRenew = false);
        void StartRefreshToken();
        void StopRefreshToken();

        Task LogoutAsync();
    }
}