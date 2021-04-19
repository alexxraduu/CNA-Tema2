using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZodiacServer.Helper;

namespace ZodiacServer
{
    public class ZodiacService : ZodiacSign.ZodiacSignBase
    {
        private Operations operations;
        public override Task<GetZodiacSignResponse> GetZodiacSign(GetZodiacSignRequest request, ServerCallContext context)
        {
            operations = new Operations();
            using var channel = GrpcChannel.ForAddress(Constants.Constants.CHANNEL_ADDRESS);
            var date = request.Date;
            string sign = "";
            var season = operations.GetSeason(date);
            if (season.Equals("INVALID"))
            {
                Console.WriteLine("INVALID DATE");
                return Task.FromResult(new GetZodiacSignResponse
                {
                    Sign = "INVALID"
                });
            }

            switch (season)
            {
                case "Spring":
                    var springClient = new SpringSign.SpringSignClient(channel);
                    var springResponse = springClient.GetSpringSign(new GetSpringSignRequest
                    {
                        Date = date
                    });
                    sign = springResponse.Sign;
                    break;
                case "Summer":
                    var summerClient = new SummerSign.SummerSignClient(channel);
                    var summerResponse = summerClient.GetSummerSign(new GetSummerSignRequest
                    {
                        Date = date
                    });
                    sign = summerResponse.Sign;
                    break;
                case "Autumn":
                    var autumnClient = new AutumnSign.AutumnSignClient(channel);
                    var autumnResponse = autumnClient.GetAutumnSign(new GetAutumnSignRequest
                    {
                        Date = date
                    });
                    sign = autumnResponse.Sign;
                    break;
                case "Winter":
                    var winterClient = new WinterSign.WinterSignClient(channel);
                    var winterResponse = winterClient.GetWinterSign(new GetWinterSignRequest
                    {
                        Date = date
                    });
                    sign = winterResponse.Sign;
                    break;
                default:
                    break;
            }
            return Task.FromResult(new GetZodiacSignResponse
            {
                Sign = sign
            });
        }
    }
}
