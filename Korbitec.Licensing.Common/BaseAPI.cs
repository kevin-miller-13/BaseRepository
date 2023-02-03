using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Korbitec.Licensing.Common
{
    public static class BaseAPI
    {
        public static async Task<T> GetApiResponseFromBody<T>(this HttpRequest request)
        {
            var content = await new StreamReader(request.Body).ReadToEndAsync();

            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch
            {
                throw new DeserializeObjectException();
            }
        }
    }
}
