using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadzOdlewnicza.Gas
{
    public class GasResults
    {
        public double WspolczynnikCprzemysl { get; set; }
        public double WspolczynnikCmodel { get; set; }
        public double StosunekC { get; set; }
        public double SkalaPrzeplywuGazu { get; set; }
        public double PrzeplywGazuModel { get; set; } //l/min

        public override string ToString()
        {
            return $"Obliczenia przepływu gazu:\n" +
                $"\t-Współczynnik C przemysłu: {WspolczynnikCprzemysl}\n\t" +
                $"-Współczynnik C modelu: {WspolczynnikCmodel}\n\t" +
                $"-Stosunek C: {StosunekC}\n\t" +
                $"-Skala przepływu gazu: {SkalaPrzeplywuGazu:F2}\n\t" +
                $"-Przepływ gazu dla modelu: {PrzeplywGazuModel:F2} l/min\n";
        }
    }
}
