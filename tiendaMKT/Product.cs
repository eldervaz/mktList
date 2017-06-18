using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;

namespace tiendaMKT
{
    class Product
    {
        public string nombre { get; set; }   
        public string cantidad { get; set; }

		public string modelo { get; set; }
		public string codigo { get; set; }
		public string imagen { get; set; }
		public string instructor { get; set; }
		public int pCiento { get; set; }
		public int pMillar { get; set; }
        public JArray colores { get; set; }
        public JArray items { get; set; }


    }

}
