using ClasesDominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPWinForm
{
    public partial class cart : Form
    {
        private List<Articulo> Articulos;
        public cart()
        {
            InitializeComponent();
        }
        private void cart_Load(object sender, EventArgs e)
        {
            NegocioArticulo articulo = new NegocioArticulo();
            Articulos = articulo.listar();

            List<Imagen> imagenes = articulo.getImagens(Articulos[0].id);
            dgvArticulos.DataSource = Articulos;
            pbArticulo.Load(imagenes[0].urlImagen);
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            NegocioArticulo articulo = new NegocioArticulo();
            List<Imagen> imagenes = articulo.getImagens(seleccionado.id);
            pbArticulo.Load(imagenes[0].urlImagen);

            if (imagenes != null && imagenes.Count > 0)
            {
                pbArticulo.SizeMode = PictureBoxSizeMode.Zoom;

            }
            else
            {
                pbArticulo.Load("https://res.cloudinary.com/dqzfmh5kz/image/upload/v1757619195/pngtree-gray-network-placeholder-png-image_3416659_ihqz1y.jpg");
            }
        }

        private void btnAgregarArt_Click(object sender, EventArgs e)
        {
            frmAltaArticulo frm = new frmAltaArticulo();
            frm.ShowDialog();
        }
    }
}
