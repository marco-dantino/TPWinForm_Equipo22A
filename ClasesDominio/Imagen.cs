using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace ClasesDominio
{
    public class Imagen
    {
        public int id { get; set; }
        public string urlImagen { get; set; }
        public int idArticulo { get; set; }
        public override string ToString()
        {
            return urlImagen;
        }
    }
}
