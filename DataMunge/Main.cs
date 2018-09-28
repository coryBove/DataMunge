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
        public static void Main(string[] args)
        {            
            string line = string.Empty;
            List<Day> days = new List<Day>();
            StreamReader weatherFile = new StreamReader(@"C:\Users\mini-veg\source\repos\DataMunge\DataMunge\weather.txt");
            string headerLine = weatherFile.ReadLine();
            while ((line = weatherFile.ReadLine()) != null)
            {
                string[] spl = line.Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
                days.Add(new Day(spl[0], spl[1], spl[2], spl[3], spl[4]));
            }

            days = days.OrderBy(p => p.ChanceOfPrecipitation).Where(t => t.High <= 85 && t.Low >= 70).ToList();            
            PrintAllDays(days);                       

            if (args.Length == 1)
            {
                days = days.OrderBy(d => d.DayOfMonth).ToList();
                List<Day> longestListOfVacationDays = LongestConsecutiveSubsequence(days);
                PrintKCombs(GetConsecutiveIntegers(days), int.Parse(args[0]));
            }
            else
            {
                PrintTheBestDay(days);
            }
        }

        private static void PrintKCombs(IEnumerable<List<Day>> consecutiveDays, int length)
        {            
            Console.WriteLine("Vacation combinations:");                        
            foreach (var comb in consecutiveDays)
            {                
                var kCombs = GetKCombs(comb, length);                
                foreach (var d in kCombs.SelectMany(l => l.Select(o => o)))
                {
                    Console.WriteLine(string.Format("{0} the {1} day of the month.", d.DayOfWeek, ConvertNumToOrdinal(d.DayOfMonth)));
                }
            }            
        }

        private static IEnumerable<IEnumerable<T>> GetKCombs<T>(List<T> list, int length) where T : IComparable
        {
            if (length == 1) return list.Select(d => new T[] { d });
            return GetKCombs(list, length - 1).SelectMany(d => list.Where(o => o.CompareTo(d.Last()) > 0), (d1, d2) => d1.Concat(new T[] { d2 }));
        }

        private static IEnumerable<List<Day>> GetConsecutiveIntegers(List<Day> days)
        {
            return days.Select((value, index) => new { value, index }).GroupBy(x => x.value.DayOfMonth - x.index, x => x.value).Select(g => g.ToList());
        }

        private static List<Day> LongestConsecutiveSubsequence(List<Day> days)
        {
            int currentPosition = 0;
            int currentLongestRun = 0;
            int startOfLongestRun = 0;
            while(currentPosition < days.Count)
            {
                int startOfRun = currentPosition;
                while(currentPosition < days.Count - 1 && days[currentPosition].DayOfMonth + 1 == days[currentPosition + 1].DayOfMonth)
                {
                    currentPosition++;
                }

                if (currentPosition - startOfRun > currentLongestRun)
                {
                    startOfLongestRun = startOfRun;
                    currentLongestRun = currentPosition - startOfRun;
                }
                currentPosition++;
            }

            Day[] longestSequence = new Day[currentLongestRun + 1];
            Array.Copy(days.ToArray(), startOfLongestRun, longestSequence, 0, currentLongestRun + 1);

            Console.WriteLine(string.Format("The longest sequence of days is the {0} following:", longestSequence.Count()));
            foreach(var d in longestSequence)
            {
                Console.WriteLine(string.Format("{0} the {1} day of the month.", d.DayOfWeek, ConvertNumToOrdinal(d.DayOfMonth)));
            }

            return longestSequence.ToList();
        }

        private static void PrintTheBestDay(List<Day> days)
        {
            Day day = days.FirstOrDefault();
            Console.WriteLine(string.Format("{0} the {1} day of the month is the best day for a picnic.", day.DayOfWeek, ConvertNumToOrdinal(day.DayOfMonth)));
        }

        private static void PrintAllDays(List<Day> days)
        {
            foreach (var d in days)
            {
                d.Print();
            }
        }

        private static string ConvertNumToOrdinal(int dayOfMonth)
        {
            string day = dayOfMonth.ToString();
            if (day.EndsWith("11")) day = dayOfMonth + "th";
            else if (day.EndsWith("12")) day = dayOfMonth + "th";
            else if (day.EndsWith("11")) day = dayOfMonth + "th";
            else if (day.EndsWith("1")) day = dayOfMonth + "st";
            else if (day.EndsWith("2")) day = dayOfMonth + "nd";
            else if (day.EndsWith("3")) day = dayOfMonth + "rd";
            else day = dayOfMonth + "th";

            return day;
        }
    }
}
