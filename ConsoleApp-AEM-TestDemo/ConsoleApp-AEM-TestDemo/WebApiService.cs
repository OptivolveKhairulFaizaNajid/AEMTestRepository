using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp_AEM_TestDemo.Models;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;

namespace ConsoleApp_AEM_TestDemo
{

    public interface IWebApiService
    {
        Task Login();
        Task GetPlatformWellActual();
    }
    public class WebApiService: IWebApiService
    {
        static string Baseurl = "http://test-demo.aem-enersol.com/";
        static string username = "user@aemenersol.com";
        static string password = "Test@123";
        static string accessToken;

        public WebApiService()
        {
        }


            public async Task Login()
            {

                Console.WriteLine("Sending login request...");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Baseurl + "api/Account/Login/");
                    request.Content = new StringContent("{\"username\":\"" + username + "\",\"password\":\"" + password + "\"}",
                                        Encoding.UTF8,
                                        "application/json");

                    HttpResponseMessage Res = await client.SendAsync(request);

                     if (Res.IsSuccessStatusCode)
                    {
                        accessToken = Res.Content.ReadAsAsync<string>().Result;
                        accessToken.Trim(new char[] { '"' });
                        Console.WriteLine("Token string: " + accessToken);
                    }
                };
            }

            public async Task GetPlatformWellActual()                                                                                                         
            {

                Console.WriteLine("Sending GetPlatformWellActual request...");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    HttpResponseMessage Res = await client.GetAsync("api/PlatformWell/GetPlatformWellActual/");

                    if (Res.IsSuccessStatusCode)
                    {
                    var json = Res.Content.ReadAsStringAsync().Result.ToString();
                    List<PlatformDto> platformDto = JsonConvert.DeserializeObject<List<PlatformDto>>(json);

                    Console.WriteLine("Platform Count : " + platformDto.Count);
                    AddOrUpdateDatabase(platformDto);
                    }
                };
                Console.ReadKey();
            }

            static void AddOrUpdateDatabase(List<PlatformDto> platformDto)
            {
            AemTestContext aemTestContext = new AemTestContext();
            List<PlatformDto> newData = new List<PlatformDto>();
            List<PlatformDto> existingData = new List<PlatformDto>();
            foreach (var platform in platformDto)
                {
                    var data = aemTestContext.PlatformDto.Find(platform.Id);
                    if (data == null)
                        newData.Add(platform);
                    else
                        existingData.Add(platform);
                }

            Add(newData);
            Update(existingData);
            }

        private static void Update(List<PlatformDto> existingData)
        {
            AemTestContext aemTestContext = new AemTestContext();
            aemTestContext.UpdateRange(existingData);
            aemTestContext.SaveChangesAsync();
        }

        private static void Add(List<PlatformDto> newData)
        {
            AemTestContext aemTestContext = new AemTestContext();
            aemTestContext.AddRange(newData);
            aemTestContext.SaveChangesAsync();
        }

    }

    }

