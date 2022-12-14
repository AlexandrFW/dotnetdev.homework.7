using System;
using System.Collections.Generic;
using System.Text.Json;

namespace WebClient
{
    public class CustomerCreateRequest
    {
        private List<string> _firstNames = new List<string>
        {
           "??????",
           "???????",
           "?????????",
           "?????",
           "??????"
        };

        private List<string> _lastNames = new List<string>
        {
            "?????",
            "??????",
            "???????",
            "??????",
            "???????"
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
                    Id = random.Next(1, 5),
                    Firstname = _firstNames[random.Next(1, 5)],
                    Lastname = _lastNames[random.Next(1, 5)]
                };
            }
            else
            {
                customer = new Customer()
                {
                    Id = random.Next(1, 5),
                    Firstname = Firstname,
                    Lastname = Lastname
                };
            }

            return JsonSerializer.Serialize(customer);
        }


        public string Firstname { get; init; }

        public string Lastname { get; init; }
    }
}