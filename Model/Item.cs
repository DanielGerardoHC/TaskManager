using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorDeTareas.View
{
   public  class Item
   {
        public string Nombre { get; set; }

        public Item(string nombre)
        {
            Nombre = nombre;
        }
   }
}
