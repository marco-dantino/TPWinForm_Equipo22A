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
            string placeholder = "https://res.cloudinary.com/dqzfmh5kz/image/upload/v1757619195/pngtree-gray-network-placeholder-png-image_3416659_ihqz1y.jpg";

            try
            {
                if (string.IsNullOrWhiteSpace(imagen)) { 
                    pbArticulo.Load(placeholder);
                }
                else { 
                    pbArticulo.Load(imagen);
                }
                pbArticulo.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception)
            {
                pbArticulo.Load(placeholder);
            }
        }

        private void btnAgregarArt_Click(object sender, EventArgs e)
        {
            frmAltaArticulo frm = new frmAltaArticulo();
            frm.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            frmAltaArticulo modificar = new frmAltaArticulo(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            NegocioArticulo negArt = new NegocioArticulo();
            Articulo seleccionado;

            try
            {
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                negArt.eliminar(seleccionado.Id);
                dgvArticulos.DataSource = null;
                dgvArticulos.DataSource = negArt.listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
