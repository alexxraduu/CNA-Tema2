using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace ZodiacClient
{
    class Program
    {
        private static bool isNumber(string s)
        {
            foreach (var c in s)
            {
                if (Char.IsDigit(c) != true)
                {
                    return false;
                }
            }
            return true;
        }
        private static bool IsDateValid(string date)
        {

            foreach (var c in date)
            {
                if (Char.IsDigit(c) != true && c.Equals('/') != true)
                {
                    return false;
                }
            }
            var aux = date.Split("/");
            if (aux.Length != 3)
            {
                return false;
            }
            foreach (var c in aux)
            {
                if (!isNumber(c) || c == "")
                {
                    return false;
                }
            }
            var month = Int32.Parse(aux[0]);
            var day = Int32.Parse(aux[1]);
            var year = Int32.Parse(aux[2]);
            var leapYear = false;
            if (year < 1900)
            {
                return false;
            }
            if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
            {
                leapYear = true;
            }


            if (month < 1 || month > 12)
            {
                return false;
            }
            if (day < 1)
            {
                return false;
            }

            if (month == 2)
            {
                if (!leapYear)
                {
                    if (day > 28)
                    {
                        return false;
                    }
                }
                else
                {
                    if (day > 29)
                    {
                        return false;
                    }
                }

            }
            else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                if (day > 31)
                {
                    return false;
                }
            }
            else
            {
                if (day > 30)
                {
                    return false;
                }
            }

            return true;
        }
        private static string ReadInput()
        {

            Console.WriteLine("Enter a date (month/day/year): ");
            var date = Console.ReadLine();
            while (!IsDateValid(date))
            {
                Console.WriteLine("Retry: ");
                date = Console.ReadLine();
            }
            return date;
        }
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new ZodiacSign.ZodiacSignClient(channel);
            Console.WriteLine("Do you want to enter a date? Y/N");
            var option = Console.ReadLine();
            while (option.ToLower().Equals("Y".ToLower()))
            {
                var reply = await client.GetZodiacSignAsync(
                                              new GetZodiacSignRequest { Date = ReadInput() });
                Console.WriteLine("Sign: " + reply.Sign);
                Console.WriteLine("\nDo you want to enter another date? Y/N");
                option = Console.ReadLine();
            }
        }
    }
}
