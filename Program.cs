using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StubHub
{
    class Program
    {
        static void Main(string[] args)
        {
            int eventCount = 1;

            var events = new List<Event>{
                new Event{ Name = "Phantom of the Opera", City = "New York"},
                new Event{ Name = "Metallica", City = "Los Angeles"},
                new Event{ Name = "Metallica", City = "New York"},
                new Event{ Name = "Metallica", City = "Boston"},
                new Event{ Name = "LadyGaGa", City = "New York"},
                new Event{ Name = "LadyGaGa", City = "Boston"},
                new Event{ Name = "LadyGaGa", City = "Chicago"},
                new Event{ Name = "LadyGaGa", City = "San Francisco"},
                new Event{ Name = "LadyGaGa", City = "Washington"}
                };

            //1. find out all events that arein cities of customer
            // then add to email.
            var customer = new Customer { Name = "Mr. Fake", City = "New York" };

            var allEventsWithinCustomerCity = events.Where(x => x.City == customer.City);

            // 1. TASK
            if (allEventsWithinCustomerCity != null && allEventsWithinCustomerCity.Any())
            {
                //Events in customers city
                foreach (var item in allEventsWithinCustomerCity)
                {
                    eventCount = eventCount++;

                    if (eventCount <= 5)
                    {
                        AddToEmail(customer, item);
                    }
                }

                //Get events outside custmers city
                if (eventCount <= 5)
                {
                    foreach (var item in events)
                    {
                        AddToEmail(customer, item);
                    }
                }
            }
            else
            {
                //Get events outside custmers city
                foreach (var item in events)
                {
                    if (eventCount <= 5)
                    {
                        AddToEmail(customer, item);
                    }
                }
            }

            /*
            * We want you to send an email to this customer with all events in their city
            * Just call AddToEmail(customer, event) for each event you think they should get
            */
        }

        // You do not need to know how these methods work
        static void AddToEmail(Customer c, Event e, int? price = null)
        {
            int customerCity = GetTotalPricePerCity(c.City);
            int eventCity = GetTotalPricePerCity(c.City);
            price = GetPrice(e);
            // int concertPrice = customerCity - eventCity;

            var distance = GetDistance(c.City, e.City);

            if (distance == 0 || (distance>0  && distance <=2))
            {
                Console.Out.WriteLine($"{c.Name}: {e.Name} in {e.City}"
                    + (distance > 0 ? $" ({distance} miles away)" : "")
                    + (price.HasValue ? $" for ${price}" : ""));
            }

        }

        public static int GetTotalPricePerCity(string s)
        {
            int totalPrice = 0;

            foreach (char c in s)
            {
                int index = char.ToUpper(c) - 64;

                totalPrice += totalPrice + index;               
            }

            return totalPrice;
        }

        static int GetPrice(Event e)
        {
            return (AlphebiticalDistance(e.City, "") + AlphebiticalDistance(e.Name, "")) / 10;
        }

        static int GetDistance(string fromCity, string toCity)
        {
            return AlphebiticalDistance(fromCity, toCity);
        }

        private static int AlphebiticalDistance(string s, string t)
        {
            var result = 0;
            var i = 0;
            for (i = 0; i < Math.Min(s.Length, t.Length); i++)
            {
                // Console.Out.WriteLine($"loop 1 i={i} {s.Length} {t.Length}");
                result += Math.Abs(s[i] - t[i]);
            }
            for (; i < Math.Max(s.Length, t.Length); i++)
            {
                // Console.Out.WriteLine($"loop 2 i={i} {s.Length} {t.Length}");
                result += s.Length > t.Length ? s[i] : t[i];
            }

            return result;
        }
    }


    public class Event
    {
        public string Name { get; set; }
        public string City { get; set; }
    }
    public class Customer
    {
        public string Name { get; set; }
        public string City { get; set; }
    }


    /*
    var customers = new List<Customer>{
    new Customer{ Name = "Nathan", City = "New York"},
    new Customer{ Name = "Bob", City = "Boston"},
    new Customer{ Name = "Cindy", City = "Chicago"},
    new Customer{ Name = "Lisa", City = "Los Angeles"}
    };
    */

}