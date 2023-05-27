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
                $"-Objętość wlanego metalu: {Volume_metal:F2} m³\n\t" +
                $"-Objętość kotła: {Volume_vat:F2} m³\n";
        }
    }
}
