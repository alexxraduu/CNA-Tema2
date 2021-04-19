using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZodiacServer.Helper;

namespace ZodiacServer
{
    public class SummerSignsService : SummerSign.SummerSignBase
    {
        private Operations operations;

        public override Task<GetSummerSignResponse> GetSummerSign(GetSummerSignRequest request, ServerCallContext context)
        {
            operations = new Operations();
            var date = request.Date;
            Console.WriteLine("Date: " + date);
            var sign = operations.GetSign(date);

            if (sign.Equals("INVALID"))
            {
                Console.Write("INVALID DATE\n");
                return Task.FromResult(new GetSummerSignResponse
                {
                    Sign = sign
                });
            }
            else
            {
                Console.WriteLine("Sign: " + sign);
                Console.WriteLine("Season: " + operations.GetSeason(date));
                return Task.FromResult(new GetSummerSignResponse
                {
                    Sign = sign
                });
            }
        }
    }
}
