using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PlauralsightTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var Sales = ProcessFile($"/Users/hamidram/Downloads/Salesjan2009.csv");
            var query = from sale in Sales
                        group sale by sale.Country into countryGroup
                        select new
                        {
                            Name = countryGroup.Key,
                            Max = countryGroup.Max(c => c.Price)

                        };
            foreach (var group in query) 
            {
                Console.WriteLine($"Name >{group.Name }");

                Console.WriteLine($"\t Max>{group.Max }");
            } 
            
        }

        private static IEnumerable<Sale> ProcessFile(string path)
        {
            return File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .Select(Sale.ParseFromCSV).ToList();
        }

       
    }
    public class Sale
    {
        public object Transaction_date { get; set; }
        public string Product { get; set; }
        public float Price { get; set; }
        public string Payment_Type { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Account_Created { get; set; }
        public string Last_Login { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        internal static Sale ParseFromCSV(string line)
        {
            try { 
            var columns = line.Split(',');
            return new Sale
            {
                Transaction_date = columns[0],
                Product = columns[1],
                Price = float.Parse(columns[2]),
                Payment_Type = columns[3],
                Name = columns[4],
                City = columns[5],
                State = columns[6],
                Country = columns[7],
                Account_Created = columns[8],
                Last_Login = columns[9],
                Latitude = columns[10],
                Longitude = columns[11]
            };
            }
            catch
            {
                var columns = line.Split(',');

                return new Sale
                {

                    Transaction_date = columns[0],
                    Product = columns[1],
                    Price = 1000,
                    Payment_Type = columns[3],
                    Name = columns[4],
                    City = columns[5],
                    State = columns[6],
                    Country = columns[7],
                    Account_Created = columns[8],
                    Last_Login = columns[9],
                    Latitude = columns[10],
                    Longitude = columns[11]
                };
            }
        }
    }

}