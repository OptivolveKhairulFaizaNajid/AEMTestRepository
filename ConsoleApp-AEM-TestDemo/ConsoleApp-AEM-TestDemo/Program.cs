using System.Threading.Tasks;

namespace ConsoleApp_AEM_TestDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApiService webApiService = new WebApiService();

            await webApiService.Login();

            await webApiService.GetPlatformWellActual();
        }

    }
}
