using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsynaAwait
{

    class Metodes
    {
        static List<people> llistaJson = new List<people>();
        public static void lleguirLlista(){
             llistaJson = people.lleguirLlista();
        }
        ///METODES MOSTRARAN NOMES 1 CAMP DEL NOSTRE PEOPLE\\\
       public static List<string> mostrarCompania()
        {

            var llistaSenseRepetits = llistaJson.Select(x => x.company).Distinct().ToList();
            return llistaSenseRepetits;
            

        }
       public static List<string> mostrarGenere()
       {

           var llistaSenseRepetits = llistaJson.Select(x => x.gender).Distinct().ToList();
           return llistaSenseRepetits;


       }
       public static List<string> mostrarPais()
       {

           var llistaSenseRepetits = llistaJson.Select(x => x.country).Distinct().ToList();
           return llistaSenseRepetits;


       }
        public static async Task<List<String>> mostrarPerSelecioParallel(string campSelecionat,int opcioDeBusqueda)
        {

            List<string> Llista = new List<string>();
            List<string> llistaFiltrada=new List<string>();
           
            switch (opcioDeBusqueda)
            {
                case 0:
                    llistaFiltrada = await Task.Run(() =>
                    {
                        Parallel.ForEach(llistaJson, (element) =>
                        {
                            if (element.country.Equals(campSelecionat))
                            {
                            //posar tots els elemetns en una mateixa linea.
                            Llista.Add("Nom: " + element.name + "\nCognom: " + element.surname + "\nCorreu: " + element.email + "\n----------");
                            }
                        });
                        return Llista;
                    });
                break;
                case 1:
                    llistaFiltrada = await Task.Run(() =>
                    {
                        Parallel.ForEach(llistaJson, (element) =>
                        {
                            if (element.gender.Equals(campSelecionat))
                            {
                                //posar tots els elemetns en una mateixa linea.
                                Llista.Add("Nom: " + element.name + "\nCognom: " + element.surname + "\nCorreu: " + element.email + "\n----------");
                            }
                        });
                        return Llista;
                    });
                break;
                case 2:
                    llistaFiltrada = await Task.Run(() =>
                    {
                        Parallel.ForEach(llistaJson, (element) =>
                        {
                            if (element.company.Equals(campSelecionat))
                            {
                                //posar tots els elemetns en una mateixa linea.
                                Llista.Add("Nom: " + element.name + "\nCognom: " + element.surname + "\nCorreu: " + element.email + "\n----------");
                            }
                        });
                        return Llista;
                    });
                break;
            }            

            return llistaFiltrada;

        }
        public static async Task<List<String>> mostrarPerSelecioSequencial(string campSelecionat,int opcioDeBusqueda)
        {

            List<string> Llista = new List<string>();
            List<string> llistaFiltrat=new List<string>();
            switch (opcioDeBusqueda)
            {
                case 0:
                    llistaFiltrat = await Task.Run(() =>
                    {
                        foreach (var element in llistaJson)
                        {
                            if (element.country.Equals(campSelecionat))
                            {
                                //posar tots els elemetns en una mateixa linea.
                                Llista.Add("Nom: " + element.name + "\nCognom: " + element.surname + "\nCorreu: " + element.email + "\n----------");
                            }

                        }
                        return Llista;
                    });    
                break;
                case 1:
                    llistaFiltrat = await Task.Run(() =>
                    {
                    foreach (var element in llistaJson)
                        {
                            if (element.gender.Equals(campSelecionat))
                            {
                                //posar tots els elemetns en una mateixa linea.
                                Llista.Add("Nom: " + element.name + "\nCognom: " + element.surname + "\nCorreu: " + element.email + "\n----------");
                            }

                        }
                        return Llista;
                    }); 
                break;
                case 2:
                llistaFiltrat = await Task.Run(() =>
                {
                    foreach (var element in llistaJson)
                    {
                        if (element.company.Equals(campSelecionat))
                        {
                            //posar tots els elemetns en una mateixa linea.
                            Llista.Add("Nom: " + element.name + "\nCognom: " + element.surname + "\nCorreu: " + element.email + "\n----------");
                        }

                    }
                    return Llista;
                }); 
                break;
            }               
                 return llistaFiltrat;
        }

        public static string Pausas()
        {
            Thread.Sleep(6500);
            return "Procesos en marxa";
        }
    }
}
