using System.Threading.Tasks;

namespace IFiscalV2.Services.Routing
{
    public interface IRoutingService
    {
        Task GoBackAsync();
        Task GoBackModalAsync();
        Task NavigateToAsync(string route);
    }
}
