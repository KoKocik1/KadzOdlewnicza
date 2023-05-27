using KadzOdlewnicza.Gas;
using KadzOdlewnicza.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadzOdlewnicza.Calculations
{
    public class GasCalculate
    {
        private GasProperties gasProperties;
        private GasResults gasResults;
        private MainWindow gui;
        public bool Error { get; set; } =true;

        public GasCalculate(GasProperties gasProperties, GasResults gasResults, MainWindow gui)
        {
            this.gasProperties = gasProperties;
            this.gasResults = gasResults;
            this.gui = gui;
        }

        public void getGasModel()
        {
            if (gasProperties.isAll())
            {
                if (gasProperties.PrzeplywGazuQ > 0)
                {
                    gui.setErrorGas("");
                    gasResults.WspolczynnikCprzemysl = gasProperties.MasaMolowaArgonu / (gasProperties.GestoscArgonu * gasProperties.GestoscStali);
                    gasResults.WspolczynnikCmodel = (gasProperties.MasaMolowaPowietrza / (gasProperties.GestoscPowietrza * gasProperties.GestoscWody)) * (gasProperties.Powietrze / 100.0) +
                        (gasProperties.MasaMolowaArgonu / (gasProperties.GestoscArgonu * gasProperties.GestoscWody)) * (gasProperties.Argon / 100.0) +
                        (gasProperties.MasaMolowaAzotu / (gasProperties.GestoscAzotu * gasProperties.GestoscWody)) * (gasProperties.Azot / 100.0) +
                        (gasProperties.MasaMolowaTlenu / (gasProperties.GestoscTlenu * gasProperties.GestoscWody)) * (gasProperties.Tlen / 100.0);
                    gasResults.StosunekC = Math.Pow(gasResults.WspolczynnikCmodel / gasResults.WspolczynnikCprzemysl, -0.5);

                    if (gasProperties.SkalaLiniowaModeluSl != 0)
                    {
                        gasResults.SkalaPrzeplywuGazu = Math.Pow(gasProperties.SkalaLiniowaModeluSl, 2.5);

                        gasResults.PrzeplywGazuModel = gasProperties.PrzeplywGazuQ * gasResults.SkalaPrzeplywuGazu * gasResults.StosunekC;

                        Error = false;
                        gui.setGas();
                    }
                }
                else
                {
                    Error = true;
                    gui.setErrorGas("Brak przeplywu z huty");
                }
            }
            else
            {
                gui.setErrorGas("Brak 100%");
            }
        }
        
    }
}
