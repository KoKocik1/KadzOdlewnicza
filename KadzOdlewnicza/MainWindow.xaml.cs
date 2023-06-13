using KadzOdlewnicza.Calculations;
using KadzOdlewnicza.Gas;
using KadzOdlewnicza.Status;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using OfficeOpenXml;

//TODO: Obliczanie ilości gazu

namespace KadzOdlewnicza
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        private bool ready = false;

        private ModelResults modelResults;
        private Scale scale;
        private VatResults vatResults;

        private GasProperties gasProperties;
        private ModelProperties modelProperties;
        private VatProperties vatProperties;
        private GasResults gasResults;
        private GasCalculate gasCalculate;
        public MainWindow()
        {
            InitializeComponent();

            modelResults = new ModelResults();
            gasProperties = new GasProperties();
            modelProperties = new ModelProperties();
            vatResults = new VatResults();
            gasResults = new GasResults();
            vatProperties = new VatProperties(this, vatResults);
            scale = new Scale(vatResults,vatProperties, modelResults,modelProperties, this);
            gasCalculate = new GasCalculate(gasProperties, gasResults, this);
            ready = true;
        }
        /************************** VAT CHANGE ********************/
        // Zmiany parametrów wejściowych kadzi dla comboBox lub textBox
        private void Vat_Changed(String index)
        {
            if (ready)
            {
                switch (index)
                {
                    case "MassTextBox":
                    case "MassComboBox":
                        vatPropertiesSetGet(MassComboBox, MassTextBox, true, "Mass");
                        break;
                    case "DensityTextBox":
                    case "DensityComboBox":
                        vatPropertiesSetGet(DensityComboBox, DensityTextBox, true, "Density");
                        break;
                    case "HeightTextBox":
                    case "HeightComboBox":
                        vatPropertiesSetGet(HeightComboBox, HeightTextBox, false, "Height");           
                        break;
                    case "BaseDiameterTextBox":
                    case "BaseDiameterComboBox":
                        vatPropertiesSetGet(BaseDiameterComboBox, BaseDiameterTextBox, false, "BaseDiameter");        
                        break;
                    case "TopDiameterTextBox":
                    case "TopDiameterComboBox":
                        vatPropertiesSetGet(TopDiameterComboBox, TopDiameterTextBox, false, "TopDiameter");
                        break;
                    case "OxygenPersent":
                        checkPersent(OxygenPersent, "Tlen");
                        break;
                    case "NitrogenPersent":
                        checkPersent(NitrogenPersent, "Azot");
                        break;
                    case "ArgonPersent":
                        checkPersent(ArgonPersent, "Argon");
                        break;
                    case "PowietrzePersent":
                        checkPersent(PowietrzePersent, "Powietrze");
                        break;
                    case "PrzeplywGazuHuta":
                        if ((Auxiliary.IsNumericAndGreaterThanZero(PrzeplywGazuHuta.Text)))
                        {
                            gasProperties.PrzeplywGazuQ = double.Parse(PrzeplywGazuHuta.Text);
                            runGasCalculations();
                        }
                        else
                        {
                            if (Auxiliary.IsEmpty(PrzeplywGazuHuta.Text))
                            {
                                gasProperties.PrzeplywGazuQ = 0;
                            }
                            else
                            {
                                PrzeplywGazuHuta.Text = gasProperties.PrzeplywGazuQ.ToString();
                            }
                        }
                        break;
                    case "SkalaLiniowa":
                        if ((Auxiliary.IsNumericAndGreaterThanZero(SkalaLiniowa.Text)))
                        {
                            gasProperties.SkalaLiniowaModeluSl = double.Parse(SkalaLiniowa.Text);
                            runGasCalculations();
                        }
                        else
                        {
                            SkalaLiniowa.Text = gasProperties.SkalaLiniowaModeluSl.ToString();
                        }
                        break;
                    case "SkalaPrzeplywGazu":
                        if ((Auxiliary.IsNumericAndGreaterThanZero(SkalaPrzeplywGazu.Text)))
                        {
                            gasResults.SkalaPrzeplywuGazu = double.Parse(SkalaPrzeplywGazu.Text);
                            runGasCalculations();
                        }
                        else
                        {
                            if (Auxiliary.IsEmpty(SkalaPrzeplywGazu.Text))
                            {
                                gasResults.SkalaPrzeplywuGazu = 0;
                            }
                            else
                            {
                                SkalaPrzeplywGazu.Text = gasProperties.PrzeplywGazuQ.ToString();
                            }
                        }
                        break;
                    default:
                        break;
                }
                
            }
        }

        private void runGasCalculations()
        {
            if (ready&&!vatResults.Error && vatProperties.checkValue())
            {
                gasCalculate.getGasModel();
            }
        }

        private void checkPersent(TextBox textBox, String properties)
        {
            //pobranie pola klasy
            PropertyInfo property = gasProperties.GetType().GetProperty(properties);
            // Sprawdź czy wpisane dane są poprawne
            if (Auxiliary.IsNumericAndGreaterThanZero(textBox.Text))
            {
                property.SetValue(gasProperties, int.Parse(textBox.Text));
                //jeżeli brak błędów (np. za mały kocioł względem wpisanej stali)
                runGasCalculations();
            }
            else
            {
                textBox.Text = "";
                property.SetValue(gasProperties, 0);
            }
        }
        private void vatPropertiesSetGet(ComboBox comboBox, TextBox textBox, bool kg, String properties)
        {
            //pobranie pola klasy
            PropertyInfo property = vatProperties.GetType().GetProperty(properties);

            // Sprawdź czy wpisane dane są poprawne
            if (Auxiliary.IsNumericAndGreaterThanZero(textBox.Text))
            {
                if (kg)
                {
                    property.SetValue(vatProperties, Auxiliary.getUnitKg(comboBox.SelectedIndex, textBox.Text));
                }
                else
                {
                    property.SetValue(vatProperties, Auxiliary.getUnitMeters(comboBox.SelectedIndex, textBox.Text));
                }
                update();
            }
            // Jeżeli nie to zwróć poprzednią wartość
            else
            {
                if (Auxiliary.IsEmpty(textBox.Text))
                {
                    property.SetValue(vatProperties, null);
                }
                else
                {
                    if (kg)
                    {
                        textBox.Text = Auxiliary.getUnitKgBack(comboBox.SelectedIndex, (double)property.GetValue(vatProperties));
                    }
                    else
                    {
                        textBox.Text = Auxiliary.getUnitMetersBack(comboBox.SelectedIndex, (double)property.GetValue(vatProperties));
                    }
                }
                
            }
        }


        /**************************** COMBO BOX ************************************/
        // Wywoływane podczas zmiany jednostek

        private void ComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            Vat_Changed((sender as FrameworkElement).Name);
        }

        /************************* TEXT BOX ********************************/

        private void TextBoxChanged(object sender, TextChangedEventArgs e)
        {
            Vat_Changed((sender as FrameworkElement).Name);
        }


        /************************* SCALE TEXT BOX ********************************/
        // Ręczna zmiana skali dla lewej i prawej strony
        private void ScaleLCalculate_Changed(object sender, TextChangedEventArgs e)
        {
            if (ready)
            {
                if (Auxiliary.IsNumericAndGreaterThanZero(LeftScale.Text))
                {
                    scale.LeftScale = double.Parse(LeftScale.Text);
                    update();
                }
                else
                {
                    if (Auxiliary.IsEmpty(LeftScale.Text))
                    {
                        scale.LeftScale = 1;
                    }
                    else
                    {
                        LeftScale.Text = Auxiliary.checkZero(scale.LeftScale);
                    }
                }
            }  
        }
        private void ScaleRCalculate_Changed(object sender, TextChangedEventArgs e)
        {
            if (ready)
            {
                if (Auxiliary.IsNumericAndGreaterThanZero(RightScale.Text))
                {
                    scale.RightScale = double.Parse(RightScale.Text);
                    update();
                }
                else
                {
                    if (Auxiliary.IsEmpty(RightScale.Text))
                    {
                        scale.RightScale = 1;
                    }
                    else
                    {
                        RightScale.Text = Auxiliary.checkZero(scale.RightScale);
                    }
                }
            }

        }

       
        /************************** BUTTONS **********************************/
        #region Buttons
        private void ScaleCalculateOwnModel_Changed(object sender, RoutedEventArgs e)
        {
            //przycisk przeliczenia niestandardowej wysokości modelu na skalę i obliczenie pozostałych parametrów modelu
            if (ready && Auxiliary.IsNumericAndGreaterThanZero(HeightModelTextBox.Text))
            {
                modelProperties.Height = double.Parse(HeightModelTextBox.Text);
                update();
            }
            else
            {
                HeightModelTextBox.Text = Auxiliary.checkZero(modelProperties.Height);
            }

        }
        private void RaportTXT(object sender, RoutedEventArgs e)
        {
            //przycisk przeliczenia niestandardowej wysokości modelu na skalę i obliczenie pozostałych parametrów modelu
            if (ready && !gasCalculate.Error)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Pliki tekstowe (*.txt)|*.txt";
                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;

                    // Tworzenie przykładowych linii tekstu
                    string[] lines = {
                    "Data badania: " + DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss")+"\n",
                    vatProperties.ToString(),
                    vatResults.ToString(),
                    "Skala:"+double.Parse(LeftScale.Text)+":"+double.Parse(RightScale.Text)+"\n",
                    modelProperties.ToString(),
                    modelResults.ToString(),
                    gasProperties.ToString(),
                    gasResults.ToString()
                };

                    // Zapisywanie linii tekstu i zmiennych do pliku
                    using (StreamWriter file = new StreamWriter(filePath))
                    {
                        foreach (string line in lines)
                        {
                            file.WriteLine(line);
                        }
                    }

                    MessageBox.Show("Plik został zapisany.");
                }
            }
        }
        private void RaportCSV(object sender, RoutedEventArgs e)
        {
            //przycisk przeliczenia niestandardowej wysokości modelu na skalę i obliczenie pozostałych parametrów modelu
            if (ready && !gasCalculate.Error)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Plik CSV (*.csv)|*.csv";
                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;

                    // Tworzenie przykładowych linii tekstu
                    var combinedList = new List<Tuple<string, string>>();
                    combinedList.Add(new Tuple<string, string>("Data badania", $"{DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss")}"));
                    combinedList.Add(new Tuple<string, string>("", ""));
                    combinedList.AddRange(vatProperties.toList());
                    combinedList.AddRange(vatResults.toList());
                    combinedList.Add(new Tuple<string, string>("Skala", $"{LeftScale.Text:F2}:{RightScale.Text:F2}"));
                    combinedList.Add(new Tuple<string, string>("", ""));
                    combinedList.AddRange(modelProperties.toList());
                    combinedList.AddRange(modelResults.toList());
                    combinedList.AddRange(gasProperties.toList());
                    combinedList.AddRange(gasResults.toList());


                    // Zapisywanie linii tekstu i zmiennych do pliku
                    using (StreamWriter file = new StreamWriter(filePath,false, Encoding.GetEncoding("Windows-1250")))
                    {
                        foreach (var variable in combinedList)
                        {
                            // Formatowanie wartości jako stringa
                            string valueString = variable.Item2.ToString();

                            // Zapisywanie do pliku CSV
                            file.WriteLine($"{variable.Item1};{valueString}");
                        }
                    }
                    MessageBox.Show("Plik został zapisany.");
                }
            }
        }

        #endregion
        /********************** AUTOMATYCZNE OBLICZENIA ***********************/
        private void update()
        {
            //jeżeli program się poprawnie zainicjował
            if (ready)
            {
                //jeżeli wszystkie wartości potrzebne do obliczeń są uzupełnione
                if (vatProperties.checkValue())
                {
                    //oblicz parametry
                    vatProperties.calculateParameters();

                    //jeżeli brak błędów (np. za mały kocioł względem wpisanej stali)
                    if (!vatResults.Error)
                    {
                        
                        //czy obliczać se skali
                        if (SkaleCheckBox.IsChecked == true)
                        {
                            //obliczanie z podanej skali parametrów modelu
                            scale.calculateScale();
                        }
                        else
                        {
                            //obliczanie skali a następnie parametrów modelu z wpisanej wysokości modelu
                            //przyjmujemy, że model wlewka o niestandardowych proporcjach ma zachowany kształt właściwej kadzi w skali
                            //jeżeli jednak ten tryb ma obliczać dla nowego modelu o innym wyglądzie to dodać tutaj obliczenia objętości
                            scale.calculateScaleFromOwnModel();
                        }
                        runGasCalculations();
                    }
                }
            }
        }

        /********************** AUTOMATYCZNE BLOKOWANIE ***********************/

        private void ScaleCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ready)
                {
                    LeftScale.IsEnabled = true;
                    RightScale.IsEnabled = true;
                    HeightModelTextBox.IsEnabled = false;
                    ScaleButton.IsEnabled = false;
                    LeftScale.Background = Brushes.LightCyan;
                    RightScale.Background = Brushes.LightCyan;
                    HeightModelTextBox.Background = Brushes.DarkGray;
                }
            }
            catch (Exception ex) { }
        }

        private void ScaleCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ready)
                {
                    LeftScale.IsEnabled = false;
                    RightScale.IsEnabled = false;
                    HeightModelTextBox.IsEnabled = true;
                    ScaleButton.IsEnabled = true;
                    LeftScale.Background = Brushes.DarkGray;
                    RightScale.Background = Brushes.DarkGray;
                    HeightModelTextBox.Background = Brushes.LightCyan;
                }
            }
            catch (Exception ex) { }
        }

        private void PersentCheckBox(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ready)
                {
                    Vat_Changed((sender as FrameworkElement).Name);
                }
            }
            catch (Exception ex) { }
        }


        /************************* set values to gui *********************************/

        // Wyświetlanie wyników kadzi
        public void setVatResults()
        {
            ResultHeightLabel.Content = Auxiliary.checkZero(vatResults.Height_metal);
            ResultVolumeMetalLabel.Content = Auxiliary.checkZero(vatResults.Volume_metal);
            ResultVolumeVatLabel.Content = Auxiliary.checkZero(vatResults.Volume_vat);
        }
        // Wyświetlenie błędu o za małej kadzi
        public void setError(string err)
        {
            ErrorLabel.Content = err;
        }
        public void setErrorGas(string err)
        {
            ErrorGas.Content = err;
        }

            //ustawienie skali
            public void setScale()
        {
            LeftScale.Text = Auxiliary.checkZero(scale.LeftScale);
            RightScale.Text = Auxiliary.checkZero(scale.RightScale);
        }

        //ustawienie parametrów modelu
        public void setModel()
        {
            setScale();

            ResultVolumeModelLabel.Content= Auxiliary.checkZero(modelResults.Volume_model);
            ResultHeightWaterLabel.Content= Auxiliary.checkZero(modelResults.Height_water);
            ResultVolumeWaterLabel.Content= Auxiliary.checkZero(modelResults.Volume_water);
            ResultWaterLiterLabel.Content = Auxiliary.checkZero(modelResults.WaterLiter);

            HeightModelTextBox.Text= Auxiliary.checkZero(modelProperties.Height);
            TopDiameterModelTextBox.Text = Auxiliary.checkZero(modelProperties.TopDiameter);
            BaseDiameterModelTextBox.Text=Auxiliary.checkZero(modelProperties.BaseDiameter);
            LeftScale.Text = Auxiliary.checkZero(scale.LeftScale);
            RightScale.Text = Auxiliary.checkZero(scale.RightScale);

            SkalaLiniowa.Text = Auxiliary.checkZero(scale.Scale_);
        }
        public void setGas()
        {
            WspolczynnikCprzemysl.Content = gasResults.WspolczynnikCprzemysl.ToString("0.000000");
            WspolczynnikCmodel.Content = gasResults.WspolczynnikCmodel.ToString("0.000000");
            SkalaPrzeplywGazu.Text=gasResults.SkalaPrzeplywuGazu.ToString("0.000");
            StosunekC.Content=gasResults.StosunekC.ToString("0.000");
            PrzeplywGazuModel.Content=gasResults.PrzeplywGazuModel.ToString("0.00");
        }
    }
}
