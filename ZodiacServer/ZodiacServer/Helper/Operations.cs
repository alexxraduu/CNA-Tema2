using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ZodiacServer.Helper
{
    public class Operations
    {
        public List<Tuple<string, string, string>> GetZodiacSigns()
        {
            var signList = new List<Tuple<string, string, string>>();

            try
            {
                var streamReader = new StreamReader(Constants.Constants.ZODIAC_PATH);
                var line = streamReader.ReadLine();
                while (line != null)
                {
                    var elements = line.Split("|");
                    signList.Add(new Tuple<string, string, string>(
                        elements[0],
                        elements[1],
                        elements[2]));
                    line = streamReader.ReadLine();
                }
                streamReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return signList;
        }
        public string GetSign(string date)
        {
            var elem = date.Split("/");
            var currentDate = new Tuple<int, int>(Int32.Parse(elem[0]), Int32.Parse(elem[1]));

            foreach (var sign in GetZodiacSigns())
            {
                var auxBegin = sign.Item2.Split("/");
                var beginDate = new Tuple<int, int>(Int32.Parse(auxBegin[0]), Int32.Parse(auxBegin[1]));
                var auxEnd = sign.Item3.Split("/");
                var endDate = new Tuple<int, int>(Int32.Parse(auxEnd[0]), Int32.Parse(auxEnd[1])); ;

                if ((currentDate.Item1 == beginDate.Item1 && currentDate.Item2 >= beginDate.Item2) || (currentDate.Item1 == endDate.Item1 && currentDate.Item2 <= endDate.Item2))
                {
                    return sign.Item1;
                }

            }
            return "INVALID";
        }

        public string GetSeason(string date)
        {
            var aux = date.Split("/");
            try
            {
                var month = Int32.Parse(aux[0]);
                switch (month)
                {
                    case 1:
                        return "Winter";
                    case 2:
                        return "Winter";
                    case 3:
                        return "Spring";
                    case 4:
                        return "Spring";
                    case 5:
                        return "Spring";
                    case 6:
                        return "Summer";
                    case 7:
                        return "Summer";
                    case 8:
                        return "Summer";
                    case 9:
                        return "Autumn";
                    case 10:
                        return "Autumn";
                    case 11:
                        return "Autumn";
                    case 12:
                        return "Winter";
                    default:
                        return "INVALID";
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return "INVALID";
        }
    }
}
