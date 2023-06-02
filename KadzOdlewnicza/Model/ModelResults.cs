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

        public List<Tuple<string, string>> toList()
        {
            var variables = new List<Tuple<string, string>>();
            variables.Add(new Tuple<string, string>("", ""));
            variables.Add(new Tuple<string, string>("Obliczenia modelu kadzi", ""));
            variables.Add(new Tuple<string, string>("Wysokość wlanej wody [m]", $"{Height_water:F2}"));
            variables.Add(new Tuple<string, string>("Objętość wlanej wody [m³]", $"{Volume_water:F3}"));
            variables.Add(new Tuple<string, string>("Liczba litrów wody [l]",  $"{WaterLiter:F3}"));
            variables.Add(new Tuple<string, string>("Objętość modelu [m³]", $"{Volume_model:F3}"));

            return variables;
        }
        public override string ToString()
        {
            return $"Obliczenia modelu kadzi:\n" +
                $"\t-Wysokość wlanej wody: {Height_water:F2} m\n\t" +
                $"-Objętość wlanej wody: {Volume_water:F3} m³\n\t" +
                $"-Liczba litrów wody: {WaterLiter:F3} l\n\t" +
                $"-Objętość modelu: {Volume_model:F3} m³\n";
        }
    }
}
