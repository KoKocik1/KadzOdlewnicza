using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadzOdlewnicza.Calculations
{
    public class VatResults
    {
        public double Height_metal { set; get; } = 0;
        public double Volume_vat { set; get; } = 0;
        public double Volume_metal { set; get; } = 0;
        public bool Error { set; get; }

        public void clear()
        {
            Height_metal = 0;
            Volume_vat = 0;
            Volume_metal = 0;
        }
        public bool resultsExists()
        {
            return (Height_metal!=0&&Volume_metal!=0&&Volume_vat!=0);
        }
        public override string ToString()
        {
            return $"Obliczenia dla kadzi hutniczej:\n" +
                $"\t-Wysokość wlanego metalu: {Height_metal:F2} m\n\t" +
                $"-Objętość wlanego metalu: {Volume_metal:F3} m³\n\t" +
                $"-Objętość kotła: {Volume_vat:F3} m³\n";
        }
        public List<Tuple<string, string>> toList()
        {
            var variables = new List<Tuple<string, string>>();
            variables.Add(new Tuple<string, string>("", ""));
            variables.Add(new Tuple<string, string>("Obliczenia dla kadzi hutniczej", ""));
            variables.Add(new Tuple<string, string>("Wysokość wlanego metalu [m]", $"{Height_metal:F2}"));
            variables.Add(new Tuple<string, string>("Objętość wlanego metalu [m³]", $"{Volume_metal:F3}"));
            variables.Add(new Tuple<string, string>("Objętość kotła [m³]", $"{Volume_vat:F3}"));

            return variables;
        }
    }
}
