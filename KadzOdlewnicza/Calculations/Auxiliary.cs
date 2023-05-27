using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadzOdlewnicza.Calculations
{
    public static class Auxiliary
    {
        /********************** ZAMIANY JEDNOSTEK ***********************/
        public static double getUnitKg(int index, string data)
        {
            double value = double.Parse(data);
            return ((index == 1) ? 1000 : 1 )* value;
        }

        public static double getUnitMeters(int index, string data)
        {
            double value = double.Parse(data);
            return ((index == 0) ? 1 : ((index == 1) ? 0.01: 0.001))* value;
        }

        public static string getUnitKgBack(int index, double value)
        {
            return checkZero(value / ((index == 1) ? 1000.0 : 1));
        }

        public static string getUnitMetersBack(int index, double value)
        {
            return checkZero(value/((index == 0) ? 1 : ((index == 1) ? 0.01 : 0.001)));
        }
        /********************** USUWANIE ZER NA KOŃCU LICZB ***********************/
        public static String checkZero(double value)
        {
            if (value == (int)value)
            {
                return value.ToString("0");
            }
            else
            {
                if ((value * 10) == (int)(10 * value))
                {
                    return value.ToString("0.0");
                }
                else
                {
                    return value.ToString("0.00");
                }
            }
        }
        public static bool IsNumericAndGreaterThanZero(string text)
        {
            if (double.TryParse(text, out double value))
            {
                return value >= 0;
            }
            return false;
        }
    }
}
