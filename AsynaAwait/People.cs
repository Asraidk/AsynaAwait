using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynaAwait
{
    class people
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string gender { get; set; }
        public string company { get; set; }
        public string email { get; set; }
        public string country { get; set; }

        public static List<people> lleguirLlista()
        {
            var json= File.ReadAllText("people.json");
            var jsonLlegit=JsonConvert.DeserializeObject<List<people>>(json);
            return jsonLlegit;
        }
    }
   
}
