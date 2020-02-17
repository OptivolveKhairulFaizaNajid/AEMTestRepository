using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp_AEM_TestDemo.Models;
using Newtonsoft.Json;

namespace ConsoleApp_AEM_TestDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
        
            IWebApiService webApiService = new WebApiService();

            await webApiService.Login();

            await webApiService.GetPlatformWellActual();
        }

    }
}
