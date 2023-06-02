using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadzOdlewnicza.Status
{
    public class ModelProperties
    {
        public double Height { get; set; } = 0;
        public double BaseDiameter { get; set; } = 0;
        public double TopDiameter { get; set; } = 0;

        public override string ToString()
        {
            return $"Właściwości dla modelu kadzi:\n" +
                $"\t-Wysokość: {Height:F2} m\n\t" +
                $"-Górna średnica: {TopDiameter:F2} m\n\t" +
                $"-Dolna średnia: {BaseDiameter:F2} m\n";
        }
        public List<Tuple<string, string>> toList()
        {
            var variables = new List<Tuple<string, string>>();
            variables.Add(new Tuple<string, string>("", ""));
            variables.Add(new Tuple<string, string>("Właściwości dla modelu kadzi", ""));
            variables.Add(new Tuple<string, string>("Wysokość [m]", $"{Height:F2}"));
            variables.Add(new Tuple<string, string>("Górna średnica [m]", $"{TopDiameter:F2}"));
            variables.Add(new Tuple<string, string>("Dolna średnia [m]", $"{BaseDiameter:F2}"));

            return variables;
        }
    }
}
