using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadzOdlewnicza.Calculations
{
    public class ModelResults
    {
        public double Height_water { set; get; } = 0;
        public double Volume_model { set; get; } = 0;
        public double Volume_water { set; get; } = 0;

        public double WaterLiter { set; get; } = 0;

        public override string ToString()
        {
            return $"Obliczenia modelu kadzi:\n" +
                $"\t-Wysokość wlanej wody: {Height_water:F2} m\n\t" +
                $"-Objętość wlanej wody: {Volume_water:F2} m³\n\t" +
                $"-Liczba litrów wody: {WaterLiter:F2} l\n\t" +
                $"-Objętość modelu: {Volume_model:F2} m³\n";
        }
    }
}
