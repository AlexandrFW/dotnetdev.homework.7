using System;
using System.Collections.Generic;
using System.Text.Json;

namespace WebClient
{
    public class CustomerCreateRequest
    {
        private List<string> _firstNames = new List<string>
        {
           "Андрей",
           "Дмитрий",
           "Александр",
           "Роман",
           "Данила"
        };

        private List<string> _lastNames = new List<string>
        {
            "Васин",
            "Жданов",
            "Антонов",
            "Петров",
            "Смирнов"
        };

        private Random random = new Random();

        public CustomerCreateRequest() { }

        public CustomerCreateRequest(
            string firstName,
            string lastName)
        {
            Firstname = firstName;
            Lastname = lastName;
        }

        public string GetCustomerRequestString()
        {
            Customer customer;

            if (string.IsNullOrEmpty(Firstname) && string.IsNullOrEmpty(Lastname))
            {
                customer = new Customer()
                {
                    Id = random.Next(1, 6),
                    Firstname = Firstname,
                    Lastname = Lastname
                };
            }
            else
            {
                customer = new Customer()
                {
                    Id = random.Next(1, 6),
                    Firstname = _firstNames[random.Next(1, 6)],
                    Lastname = _firstNames[random.Next(1, 6)]
                };
            }

            return JsonSerializer.Serialize(customer);
        }


        public string Firstname { get; init; }

        public string Lastname { get; init; }
    }
}