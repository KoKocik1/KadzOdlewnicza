using KadzOdlewnicza.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadzOdlewnicza.Calculations
{
    public class Scale
    {
        public double LeftScale { get; set; } = 1;
        public double RightScale { get; set; } = 1;

        public double Scale_ { get; set; }


        private VatProperties VatProperties;
        private VatResults VatResults;
        private ModelProperties ModelProperties;
        private ModelResults ModelResults;
        private MainWindow GUI;

        public Scale(VatResults vatRsults, VatProperties vatProperties, ModelResults modelResults,ModelProperties modelProperties, MainWindow gUI)
        {
            VatResults = vatRsults;
            VatProperties = vatProperties;
            ModelProperties = modelProperties;
            ModelResults = modelResults;
            GUI = gUI;
        }
        private void update()
        {
            ModelProperties.TopDiameter = VatProperties.TopDiameter * Scale_;
            ModelProperties.BaseDiameter = VatProperties.BaseDiameter * Scale_;
            ModelProperties.Height = VatProperties.Height * Scale_;

            ModelResults.Height_water = VatResults.Height_metal * Scale_;
            
            //promienie modelu
            double x = ModelProperties.BaseDiameter / 2.0;
            double y = ModelProperties.TopDiameter / 2.0;  

            ModelResults.Volume_model = (1 / 3.0) * Math.PI * ModelProperties.Height * (x * x + x * y + y * y);

            ModelResults.Volume_water = (1 / 3.0) * Math.PI * ModelResults.Height_water * (x * x + x * y + y * y);
            
            ModelResults.WaterLiter = ModelResults.Volume_water * 1000.0;
        }

        /********************** OBLICZENIA DLA PODANEJ SKALI MODELU ***********************/
        public void calculateScale()
        {
            try
            {
                
                Scale_ = LeftScale / RightScale;

                if (VatResults.resultsExists())
                {
                    update();
                    GUI.setModel();
                }
            }
            catch (Exception ex)
            {
            }
        }
        /********************** OBLICZENIA DLA PODANEJ WYSOKOŚCI MODELU ***********************/
        public void calculateScaleFromOwnModel()
        {
            try
            {

                Scale_ = ModelProperties.Height / VatProperties.Height;

                if (Scale_ < 1)
                {
                    double multiply = 1 / Scale_;
                    LeftScale = 1;
                    RightScale = (1.0 * multiply);
                }
                else
                {
                    LeftScale = Scale_;
                    RightScale = 1;
                }

                update();
                GUI.setModel();

            }
            catch (Exception ex) { }
        }

    }
}
