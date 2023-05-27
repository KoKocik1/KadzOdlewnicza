﻿using KadzOdlewnicza.Calculations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadzOdlewnicza.Status
{
    public class VatProperties
    {
        public double Mass { get; set; } = 0;
        public double Density { get; set; } = 0;
        public double Height { get; set; } = 0;
        public double BaseDiameter { get; set; } = 0;
        public double TopDiameter  { get; set; } = 0;

        private MainWindow GUI { get; set; }
        private VatResults Results { get; set; }

        public VatProperties(MainWindow gui, VatResults results)
        {
            GUI = gui;
            Results = results;
        }

        public bool checkValue()
        {
            return (Mass>0&& Density>0 && Height>0 && BaseDiameter>0&& TopDiameter>0);
        }

        public void calculateParameters()
        {
            try
            {

                // Obliczanie promieni podstawy i górnej części kadzi
                double baseRadius = BaseDiameter / 2.0;
                double topRadius = TopDiameter / 2.0;

                // Obliczanie objętości metalu wlanej do kadzi
                double metalVolume = (Mass / Density);
                double vatVolume = ((1 / 3.0) * Math.PI * Height * (baseRadius * baseRadius + baseRadius * topRadius + topRadius * topRadius));

                if (metalVolume > vatVolume)
                {
                    //zwróć informacje o zamałej kadzi
                    GUI.setError("Za mała kadź");
                    Results.Error = true;
                    //return;
                }
                else
                {
                    Results.Error = false;
                    GUI.setError("");
                }

                // Obliczanie stosunku wysokości metalu do wysokości kadzi
                double ratio = metalVolume / vatVolume;

                // Obliczanie wysokości metalu
                double metalHeight = ratio * Height;

                Results.Height_metal = metalHeight;
                Results.Volume_vat = vatVolume;
                Results.Volume_metal = metalVolume;
                //Zwracanie wyniku
                GUI.setVatResults();
            }
            catch (Exception ex)
            {
            }
        }
        public override string ToString()
        {
            return $"Właściwości kadzi hutniczej:\n" +
                $"\t-Masa stali: {Mass:F2} kg\n\t" +
                $"-Gęstość stali: {Density:F2} kg/m³\n\t" +
                $"-Wysokość kotła: {Height:F2} m\n\t" +
                $"-Średnica dolna kotła: {BaseDiameter:F2} m\n\t" +
                $"-Średnica górna kotła: {TopDiameter:F2} m\n";
        }
    }
}