using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMunge
{
    class Program
    {
        public static void Main()
        {
            string line = string.Empty;
            List<Day> days = new List<Day>();
            StreamReader weatherFile = new StreamReader(@"C:\Users\cbovein1\Desktop\weather.txt");
            string headerLine = weatherFile.ReadLine();
            while ((line = weatherFile.ReadLine()) != null)
            {
                string[] spl = line.Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
                days.Add(new Day(spl[0], spl[1], spl[2], spl[3], spl[4]));
            }

            days = days.OrderBy(p => p.ChanceOfPrecipitation).Where(t => t.High <= 85 && t.Low >= 70).ToList();
            //PrintAllDays(days);

            PrintTheBestDay(days);
        }

        private static void PrintTheBestDay(List<Day> days)
        {
            Day day = days.FirstOrDefault();
            Console.WriteLine(string.Format("{0} the {1} day of the month is the best day for a picnic.", day.DayOfWeek, day.DayOfMonth));
        }

        private static void PrintAllDays(List<Day> days)
        {
            foreach (var d in days)
            {
                d.Print();
            }
        }
    }
}
