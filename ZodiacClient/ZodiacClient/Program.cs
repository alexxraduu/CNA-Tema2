using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace ZodiacClient
{
    class Program
    {
        private static string ReadInput()
        {

            Console.WriteLine("Enter a date (month/day/year): ");
            var date = Console.ReadLine();
          
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
