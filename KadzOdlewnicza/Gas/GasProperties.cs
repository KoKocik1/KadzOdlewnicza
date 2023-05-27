using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadzOdlewnicza.Status
{
    public class GasProperties
    {
        // %
        public int Argon { get; set; } = 0;
        public int Tlen { get; set; } = 0;
        public int Azot { get; set; } = 0;
        public int Powietrze { get; set; } = 0;

        //stałe masy
        public double MasaMolowaPowietrza { get; set; }=28.96;
        public double MasaMolowaArgonu { get; set; } = 39.948;
        public double MasaMolowaAzotu { get; set; } = 14;
        public double MasaMolowaTlenu { get; set; } = 31.999;

        //stałe gestosci
        public double GestoscArgonu { get; set; } = 1.784;
        public double GestoscPowietrza { get; set; } = 1.293;
        public double GestoscTlenu { get; set; } = 1.43;
        public double GestoscAzotu{ get; set; } = 1.251;

        public double GestoscStali { get; set; } = 6800;
        public double GestoscWody { get; set; } = 1000;


        //zmienne z huty
        public double PrzeplywGazuQ { get; set; } = 0;
        public double SkalaLiniowaModeluSl { get; set; } = 0;


        public bool isAll()
        {
            return (Argon + Azot + Powietrze + Tlen) == 100;
        }
        public override string ToString()
        {
            return $"Właściwości dla obliczeń przepływu gazu:\n\t" +
                $"-Skład argonu: {Argon}%\n\t" +
                $"-Skład tlenu: {Tlen}%\n\t" +
                $"-Skład azotu: {Azot}%\n\t" +
                $"-Skład powietrza: {Powietrze}%\n\n\t" +
                $"-Masa molowa argonu: {MasaMolowaArgonu} g/mol\n\t" +
                $"-Masa molowa tlenu: {MasaMolowaTlenu} g/mol\n\t" +
                $"-Masa molowa azotu: {MasaMolowaAzotu} g/mol\n\t" +
                $"-Masa molowa powietrza: {MasaMolowaPowietrza} g/mol\n\n\t" +
                $"-Gęstość argonu: {GestoscArgonu} g/dm³\n\t" +
                $"-Gęstość tlenu: {GestoscTlenu} g/dm³\n\t" +
                $"-Gęstość azotu: {GestoscAzotu} g/dm³\n\t" +
                $"-Gęstość powietrza: {GestoscPowietrza} g/dm³\n\n\t" +
                $"-Gęstość stali: {GestoscStali} kg/m³\n\t" +
                $"-Gęstość wody: {GestoscWody} kg/m³\n\n\t" +
                $"-Przepływ gazu Q: {PrzeplywGazuQ} l/min\n\t" +
                $"-Skala modelu: {SkalaLiniowaModeluSl}\n";
        }
    }
}
