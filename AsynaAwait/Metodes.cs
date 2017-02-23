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
        //Llista que omplirem amb objectes extrets del JSON
        static List<people> llistaPeople = new List<people>();
        //Metode simple que utilitzem per lleguir el Json metedode delegat a la clase people
        //retorna una llista de objectes people
        public static void lleguirLlista(){
             llistaPeople = people.lleguirLlista();
        }
        ///METODES MOSTRARAN NOMES 1 CAMP DEL NOSTRE PEOPLE\\\
        ///Els 3 metodes agafan la llista que tenim amb els objectes
        ///Y generan una nova llista que mostrara nomes el camp que volem, ames de fer un distinc
        ///per evitar series repetides
       public static List<string> mostrarCompania()
        {
            var llistaSenseRepetits = llistaPeople.Select(x => x.company).Distinct().ToList();
            return llistaSenseRepetits;
       }
       public static List<string> mostrarGenere()
       {
           var llistaSenseRepetits = llistaPeople.Select(x => x.gender).Distinct().ToList();
           return llistaSenseRepetits;
       }
       public static List<string> mostrarPais()
       {
           var llistaSenseRepetits = llistaPeople.Select(x => x.country).Distinct().ToList();
           return llistaSenseRepetits;

       }
        ///METODES QUE FAN LA LOGICA DE MOSTRAR LA INFORMACIO DEL PER EL CAMP QUE EM SELECIONAT\\\
        //Metode que retorna una task amb llista que aquesta conte els elements que volem
        //introduir en el listbox depenen del case de la selecio es per saber el perque ho tenim que 
        //buscar per busqueda de parallel
        public static async Task<List<String>> mostrarPerSelecioParallel(string campSelecionat,int opcioDeBusqueda)
        {

            List<string> Llista = new List<string>();
            List<string> llistaFiltrada=new List<string>();
            //case de quina opcio ens ve donada desde el forma si vol pasi , gender ,etc i determinar 
            //com omplir aquella llista
            switch (opcioDeBusqueda)
            {
                case 0:
                    llistaFiltrada = await Task.Run(() =>
                    {
                        Parallel.ForEach(llistaPeople, (element) =>
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
                        Parallel.ForEach(llistaPeople, (element) =>
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
                        Parallel.ForEach(llistaPeople, (element) =>
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
        //Metode que retorna una task amb llista que aquesta conte els elements que volem
        //introduir en el listbox depenen del case de la selecio es per saber el perque ho tenim que 
        //buscar, aqust cas sera busqueda sequencial
        public static async Task<List<String>> mostrarPerSelecioSequencial(string campSelecionat,int opcioDeBusqueda)
        {

            List<string> Llista = new List<string>();
            List<string> llistaFiltrat=new List<string>();
            //case de quina opcio ens ve donada desde el forma si vol pasi , gender ,etc i determinar 
            //com omplir aquella llista
            switch (opcioDeBusqueda)
            {
                case 0:
                    llistaFiltrat = await Task.Run(() =>
                    {
                        foreach (var element in llistaPeople)
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
                    foreach (var element in llistaPeople)
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
                    foreach (var element in llistaPeople)
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
        //metode chusta utilitzat per aumentar els temps de resposta dels proces per demo de que es asyncron
        public static string Pausas()
        {
            Thread.Sleep(6500);
            return "Procesos en marxa";
        }
        //Metode que transforma el primer caractater que tenim al array en un numero util per fer servir
        //retornarem per poder fer feina o per si no
         public static int ComprovacioPrimerCaracter(String condicio){
             int opcioPerSwitch = -1;

             if (condicio.Equals("country"))
             {
                 opcioPerSwitch = 0;
             }
             else
             {
                 if(condicio.Equals("gender")){
                     opcioPerSwitch = 1;
                 }else{
                     if (condicio.Equals("company"))
                     {
                         opcioPerSwitch = 2;
                     }
                 }
             }
             return opcioPerSwitch;
         }
    }
}
