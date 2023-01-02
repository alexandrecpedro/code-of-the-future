using System.Threading.Tasks;

namespace MVC
{
    public interface ISessionHelper
    {
        Task<string> GetAccessToken(string scope);
        int? GetOrderId();
        void SetAccessToken(string accessToken);
        void SetOrderId(int orderId);
    }
}