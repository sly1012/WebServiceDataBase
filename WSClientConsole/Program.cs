using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebServiceDemo;

namespace WSClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const string serverUrl = "http://localhost:61735";
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var responce = client.GetAsync("Api/DemoHotels").Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        var hotels = responce.Content.ReadAsAsync<IEnumerable<DemoHotel>>().Result;
                        foreach (var hotel in hotels)
                        {
                            Console.WriteLine(hotel);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }
            new DBHotelClient().Start();
        }
    }
}
