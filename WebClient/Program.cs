using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebClient
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            await RunApiCommunicationProcess();
        }

        private async static Task RunApiCommunicationProcess()
        {
            bool IsExit = false;

            while (IsExit == false)
            {

                var customer = new Customer()
                {
                    Id = 1,
                    Firstname = "Пётр",
                    Lastname = "Васильев"
                };

                var serializedCustomer = JsonSerializer.Serialize(customer);

                Console.WriteLine($"Responce {serializedCustomer}");

                var url = @"http://localhost:5000/api/customer/";
                var httpClient = new HttpClient();

                var scontent = new StringContent(serializedCustomer, Encoding.UTF8, "application/json");

                Console.WriteLine($"Content: {scontent}");

                var responce = await httpClient.PostAsync(url, scontent);

                Console.WriteLine($"Status code: {responce.StatusCode}");

                if (responce.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var readBytes = Array.Empty<byte>();

                    var responceString = await responce!.Content.ReadAsStringAsync();
                    Console.WriteLine($"Responce: {responceString}");

                }

                var consoleKey = Console.ReadKey();
            }
        }

        private static CustomerCreateRequest RandomCustomer()
        {
            throw new NotImplementedException();

        }
    }
}