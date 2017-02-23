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
        //si el combobox de les opcions cambia tindrem que mostrar una llista de items diferents
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
        //event del click per fer busquedas asincoronas de manera paralela, aquet metode depent del
        //index del combo box y de quin es el camp per el que volem fer la busqueda(pais-aquest,gendre-aques)
        //ames tenim un clock y utils per fer la interface mes digna alhora de fer la feina sigui mes coherent
        private async void click_Async(object sender, RoutedEventArgs e)
        {
            //bloquem el que es l'interfice que esta utilitzan y fa la feina pero no la resta
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
            //mostrem finalitzat el treball del buto i reactivem el boto
            parallelWork.Content = "Treball parallel finalitzat";
            btnparallel.IsEnabled = true;
        }
        //event del click per fer busquedas asincoronas de manera sequencial, aquet metode depent del
        //index del combo box y de quin es el camp per el que volem fer la busqueda(pais-aquest,gendre-aques)
        //ames tenim un clock y utils per fer la interface mes digna alhora de fer la feina sigui mes coherent
        private async void click_Seq(object sender, RoutedEventArgs e)
        {
            //bloquem les opcions que tenim en tractament y li comuniquem al usuari
            btnsequencial.IsEnabled = false;
            btnParaules.IsEnabled = false;
            sequencialWork.Content = "Treball en sequencia en proces";
            try
            {
                var selecio = buscar.SelectedItem.ToString();
                int v = comboOpcions.SelectedIndex;
                //nostre metode de pausa per donar un tems afeguit a les tasques
                var control = Task<string>.Factory.StartNew(() => Metodes.Pausas());
                await control;
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
            catch (Exception)//catch de control per si no tenim res seleciona al listbox dels items pais/gender,etc
            {
                MessageBox.Show("No tens ningun camp selecionat per fer la busqueda");
            }
            //un cop feina feta o no, activarem i direm que el proces a finalitzat
            sequencialWork.Content = "Treball en sequencia finallitzat";
            btnsequencial.IsEnabled = true;
            btnParaules.IsEnabled = true;
        }
        //Proves del metode boto per fer busquedas simultaneas
        private async void btnParaules_Click(object sender, RoutedEventArgs e)
        {            
            //trencar el string en dos, retornarem un bool per saber si esta be trencat en cas si
            //anirem a un metode pasant informacio per tal de saber que el control estabe
            //sino posarem un msg box que no s'ha pugut fer la feina
            
            //bloquem el que es l'interfice que esta utilitzan y fa la feina pero no la resta
            parallelWork.Content = "Treball parallel en proces";
            btnparallel.IsEnabled = false;
            btnParaules.IsEnabled = false;
            try
            {
                //obtenim el string del text box y li fem un tractament als seus camps
                var selecio = buscarParaules.Text.ToString();
                var subSelecio = Metodes.Splitear(selecio);
                int v = Metodes.ComprovacioPrimerCaracter(subSelecio[0]);//retorna un int per mirar si la condicio
                //esta ben posada
                //per fer la pausa
                var control = Task<string>.Factory.StartNew(() => Metodes.Pausas());
                await control;
                //Iniciem el rellotge, compte amb el start i amb el Restart
                clock.Restart();
                switch (v)
                {
                    case 0:
                        control1.ItemsSource = await Metodes.mostrarPerSelecioParallel(subSelecio[1], v);
                        break;
                    case 1:
                        control1.ItemsSource = await Metodes.mostrarPerSelecioParallel(subSelecio[1], v);
                        break;
                    case 2:
                        control1.ItemsSource = await Metodes.mostrarPerSelecioParallel(subSelecio[1], v);
                        break;
                    default:
                        MessageBox.Show("La condicio del TB no es l'adecuat per fer la feina \n recorda [country,gender,company]");
                        break;
                }

                //Finalitzem el rellotge i mostrem el temps al lavel corresponent
                clock.Stop();
                tempsAsyn.Content = "Temps[" + clock.Elapsed.TotalMilliseconds.ToString() + "]segons";

            }
            catch (Exception)
            {
                MessageBox.Show("Wops!!! alguna cosa no acaba de funcionar");
            }
            //mostrem finalitzat el treball del buto i reactivem el boto
            parallelWork.Content = "Treball parallel finalitzat";
            btnparallel.IsEnabled = true;
            btnParaules.IsEnabled = true;
        }       
    }
}
