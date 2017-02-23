using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using System.Diagnostics;


namespace AsynaAwait
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Stopwatch clock = new Stopwatch();
        
        public MainWindow()
        {           
            InitializeComponent();
            Metodes.lleguirLlista();
            comboOpcions.SelectedIndex = 0;
            }
      
        //EVENT\\
        private void comboOpcions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (comboOpcions.SelectedIndex)
            {

                case 0:
                    buscar.ItemsSource = null;
                    buscar.Items.Clear();
                    buscar.ItemsSource = Metodes.mostrarPais();                    
                    break;
                case 1:
                    buscar.ItemsSource = null;
                    buscar.Items.Clear();
                    buscar.ItemsSource = Metodes.mostrarGenere();
                    break;
                case 2:
                    buscar.ItemsSource = null;
                    buscar.Items.Clear();
                    buscar.ItemsSource = Metodes.mostrarCompania();
                    break;

            }
        }

        private async void click_Async(object sender, RoutedEventArgs e)
        {
            parallelWork.Content = "Treball parallel en proces";
            btnparallel.IsEnabled = false;
            try{
                var selecio = buscar.SelectedItem.ToString();
                int v = comboOpcions.SelectedIndex;
                var control= Task<string>.Factory.StartNew(()=>Metodes.Pausas());
                await control;
               // tempsSeque.Content = control.Result.ToString();
                //var selecio = buscar.SelectedItem.ToString();
                //Iniciem el rellotge, compte amb el start i amb el Restart
                clock.Restart();
                switch (v)
                {
                    case 0:
                        control1.ItemsSource = await Metodes.mostrarPerSelecioParallel(selecio,v);
                        break;
                    case 1:
                        control1.ItemsSource = await Metodes.mostrarPerSelecioParallel(selecio,v);
                        break;
                    case 2:
                        control1.ItemsSource = await Metodes.mostrarPerSelecioParallel(selecio,v);
                        break;
                }
               
                //Finalitzem el rellotge i mostrem el temps al lavel corresponent
                clock.Stop();
                tempsAsyn.Content = "Temps["+clock.Elapsed.TotalMilliseconds.ToString()+"]segons";
                
            }
            catch(Exception){
                MessageBox.Show("No tens ningun camp selecionat per fer la busqueda");
            }
            parallelWork.Content = "Treball parallel finalitzat";
            btnparallel.IsEnabled = true;
        }


        private async void click_Seq(object sender, RoutedEventArgs e)
        {
            btnsequencial.IsEnabled = false;
            sequencialWork.Content = "Treball en sequencia en proces";
            try
            {

                var selecio = buscar.SelectedItem.ToString();
                int v = comboOpcions.SelectedIndex;
                var control = Task<string>.Factory.StartNew(() => Metodes.Pausas());
                await control;
                //tempsSeque.Content = control.Result.ToString();
                //var selecio = buscar.SelectedItem.ToString();
                //Iniciem el rellotge, compte amb el start i amb el Restart
                clock.Restart();
                switch (v)
                {
                    case 0:
                        control2.ItemsSource = await Metodes.mostrarPerSelecioSequencial(selecio, v);
                        break;
                    case 1:
                        control2.ItemsSource = await Metodes.mostrarPerSelecioSequencial(selecio, v);
                        break;
                    case 2:
                        control2.ItemsSource = await Metodes.mostrarPerSelecioSequencial(selecio, v);
                        break;
                }

                //Finalitzem el rellotge i mostrem el temps al lavel corresponent
                clock.Stop();
                tempsSeque.Content = "Temps["+clock.Elapsed.TotalMilliseconds.ToString() + "]segons";

            }
            catch (Exception)
            {
                MessageBox.Show("No tens ningun camp selecionat per fer la busqueda");
            }
            sequencialWork.Content = "Treball en sequencia finallitzat";
            btnsequencial.IsEnabled = true;
        }       
    }
}
