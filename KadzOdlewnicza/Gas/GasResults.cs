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
                $"-Skala przepływu gazu: {SkalaPrzeplywuGazu:F3}\n\t" +
                $"-Przepływ gazu dla modelu: {PrzeplywGazuModel:F3} l/min\n";
        }
        public List<Tuple<string, string>> toList()
        {
            var variables = new List<Tuple<string, string>>();
            variables.Add(new Tuple<string, string>("", ""));
            variables.Add(new Tuple<string, string>("Obliczenia przepływu gazu", ""));
            variables.Add(new Tuple<string, string>("Współczynnik C przemysłu", $"{WspolczynnikCprzemysl}"));
            variables.Add(new Tuple<string, string>("Współczynnik C modelu [m³]", $"{WspolczynnikCmodel}"));
            variables.Add(new Tuple<string, string>("Stosunek C", $"{StosunekC}"));
            variables.Add(new Tuple<string, string>("Skala przepływu gazu", $"{SkalaPrzeplywuGazu:F3}"));
            variables.Add(new Tuple<string, string>("Przepływ gazu dla modelu [l/min]", $"{PrzeplywGazuModel:F3}"));

            return variables;
        }
    }
}
