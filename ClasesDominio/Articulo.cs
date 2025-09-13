using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesDominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marca Marca { get; set; } = new Marca();
        public Categoria Categoria { get; set; } = new Categoria();
        public string Urlimagen { get; set; }
        public float Precio { get; set; }
    }
}
