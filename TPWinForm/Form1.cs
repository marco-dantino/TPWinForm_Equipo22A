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
            cargar();   
        }

        private void cargar()
        {
            NegocioArticulo articulo = new NegocioArticulo();
            try
            {
                Articulos = articulo.listar();
                
                dgvArticulos.DataSource = Articulos;
                dgvArticulos.Columns["UrlImagen"].Visible = false;
                cargarImagen(Articulos[0].Urlimagen);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.Urlimagen);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbArticulo.Load(imagen);
                pbArticulo.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception)
            {
                pbArticulo.Load("https://res.cloudinary.com/dqzfmh5kz/image/upload/v1757619195/pngtree-gray-network-placeholder-png-image_3416659_ihqz1y.jpg");
            }
        }

        private void btnAgregarArt_Click(object sender, EventArgs e)
        {
            frmAltaArticulo frm = new frmAltaArticulo();
            frm.ShowDialog();
            cargar();
        }
    }
}
