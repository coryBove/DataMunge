using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMunge
{
    class Day : IComparable
    {

        public int DayOfMonth { get; set; }
        public string DayOfWeek { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public int ChanceOfPrecipitation { get; set; }

        public int CompareTo(Object o)
        {
            Day d = o as Day;
            return DayOfMonth.CompareTo(d.DayOfMonth);
        }

        public Day(string dayOfMonth, string dayOfWeek, string high, string low, string chanceOfPrecipitation)
        {
            DayOfMonth = int.Parse(dayOfMonth);
            DayOfWeek = ConvertLetterToWord(dayOfMonth);
            High = CelsiusToFarenheit(high);
            Low = CelsiusToFarenheit(low);
            ChanceOfPrecipitation = int.Parse(chanceOfPrecipitation);
        }

        private double CelsiusToFarenheit(string celsius)
        {
            return double.Parse(celsius) * 9.0 / 5.0 + 32.0;
        }        

        private string ConvertLetterToWord(string dayOfMonth)
        {
            //TODO: Make this work for every month
            string dayOfWeek = string.Empty;
            switch(int.Parse(dayOfMonth) % 7)
            {
                case 1:
                    dayOfWeek = "Wednesday";
                    break;
                case 2:
                    dayOfWeek = "Thursday";
                    break;
                case 3:
                    dayOfWeek = "Friday";
                    break;
                case 4:
                    dayOfWeek = "Saturday";
                    break;
                case 5:
                    dayOfWeek = "Sunday";
                    break;
                case 6:
                    dayOfWeek = "Monday";
                    break;
                case 0:
                    dayOfWeek = "Tuesday";
                    break;
            }
            return dayOfWeek;
        }

        public void Print()
        {
            Console.Write(DayOfMonth + ", ");
            Console.Write(DayOfWeek + ", ");
            Console.Write(High + ", ");
            Console.Write(Low + ", ");
            Console.Write(ChanceOfPrecipitation + "\n");            
        }
    }
}
