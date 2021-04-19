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
    }
}
