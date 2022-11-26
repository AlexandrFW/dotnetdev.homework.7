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

            var url = @"http://localhost:5000/api/customer/";
            var httpClient = new HttpClient();

            while (IsExit == false)
            {
                PrintMenu();
                var consoleKey = Console.ReadKey().Key;

                switch(consoleKey)
                {
                    case ConsoleKey.N:
                        await CreateNewCustomer(httpClient, url);
                        break;

                    case ConsoleKey.G:
                        await GetCustomerById(httpClient, url);
                        break;

                    case ConsoleKey.Escape:
                        IsExit = true;
                        break;
                }
            }

            httpClient.Dispose();
        }

        private async static Task CreateNewCustomer(HttpClient httpClient, string url)
        {
            Console.Clear();

            var randomCustomerGenerator = RandomCustomer();
            var customerJson = randomCustomerGenerator.GetCustomerRequestString();

            Console.WriteLine($"Request: {customerJson}");
            var scontent = new StringContent(customerJson, Encoding.UTF8, "application/json");

            Console.WriteLine($"Content: {scontent}");

            var responce = await httpClient.PostAsync(url, scontent);

            Console.WriteLine($"Status code: {responce.StatusCode}");

            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responceString = await responce!.Content.ReadAsStringAsync();
                Console.WriteLine($"Responce: New customer Id = {responceString}");
            }

            ShowHowToProceedMessage();
        }

        private async static Task GetCustomerById(HttpClient httpClient, string url)
        {
            Console.Clear();
            Console.WriteLine($"Запрос клиента по Id");
            Console.WriteLine($"Введите Id сейчас (Id = [1..5]): ");

            var customerId = Console.ReadLine();

            var reauest = string.Concat(url, $"{customerId}");

            var responce = await httpClient.GetAsync(reauest);

            Console.WriteLine($"Status code: {responce.StatusCode}");

            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responceBytes = await responce!.Content.ReadAsByteArrayAsync();
                var responceString = Encoding.UTF8.GetString(responceBytes);

                Console.WriteLine($"Responce JSON: Customer = {responceString}");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var customer = JsonSerializer.Deserialize<Customer>(responceString, options);

                Console.WriteLine($"Responce Deserialized: Customer: Id = {customer.Id}, FirstName = {customer.Firstname}, LastName = {customer.Lastname}");
            }

            ShowHowToProceedMessage();
        }

        private static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("[Тестирование WebApi]");
            Console.WriteLine("");
            Console.WriteLine("Для выхода нажмите 'ESC'");
            Console.WriteLine("Нажмите 'N' для создания нового клиента");
            Console.WriteLine("Нажмите 'G' для получения клиента по Id (Id = [1..5])");
        }

        private static void ShowHowToProceedMessage()
        {
            Console.WriteLine("");
            Console.WriteLine("Нажмите любую клавишу для выхода в меню");
            Console.ReadKey();
        }

        private static CustomerCreateRequest RandomCustomer()
        {
            return new CustomerCreateRequest();
        }
    }
}