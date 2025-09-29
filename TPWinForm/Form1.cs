using ClasesDominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPWinForm
{
    public partial class cart : Form
    {
        private List<Articulo> listaArticulos;
        int indiceImagen = 0;
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
            NegocioArticulo negocio = new NegocioArticulo();
            
            try
            {
                listaArticulos = negocio.listar();
                dgvArticulos.DataSource = negocio.listar();

                pbArticulo.SizeMode = PictureBoxSizeMode.StretchImage;
                cargarImagen(listaArticulos[0].Imagenes[indiceImagen].ImagenUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow == null) return;
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            indiceImagen = 0;

            if (seleccionado.Imagenes.Count > 0)
            {
                cargarImagen(seleccionado.Imagenes[indiceImagen].ImagenUrl);
            }
            else
            {
                cargarImagen(null);
            }

            validarBotones();
        }
        private void validarBotones()
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            if (seleccionado == null || seleccionado.Imagenes.Count == 0)
            {
                btnAnterior.Enabled = false;
                btnSiguiente.Enabled = false;
                return;
            }

            btnAnterior.Enabled = indiceImagen > 0;
            btnSiguiente.Enabled = indiceImagen < seleccionado.Imagenes.Count - 1;
        }


        private void cargarImagen(string URL)
        {
            string placeHolder = "https://res.cloudinary.com/dqzfmh5kz/image/upload/v1757619195/pngtree-gray-network-placeholder-png-image_3416659_ihqz1y.jpg";
            try
            {
                pbArticulo.Load(URL);
            }
            catch (Exception)
            {
                pbArticulo.Load(placeHolder);
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
                //negArt.eliminar(seleccionado.Id);
                dgvArticulos.DataSource = null;
                dgvArticulos.DataSource = negArt.listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            if (seleccionado.Imagenes.Count > 0 && indiceImagen < seleccionado.Imagenes.Count - 1)
            {
                indiceImagen++;
                cargarImagen(seleccionado.Imagenes[indiceImagen].ImagenUrl);
                validarBotones();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            if (seleccionado.Imagenes.Count > 0 && indiceImagen > 0)
            {
                indiceImagen--;
                cargarImagen(seleccionado.Imagenes[indiceImagen].ImagenUrl);
                validarBotones();
            }
        }
    }
}
