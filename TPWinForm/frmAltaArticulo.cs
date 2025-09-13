using ClasesDominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace TPWinForm
{
    public partial class frmAltaArticulo : Form
    {
        private Articulo articulo;


        public frmAltaArticulo()
        {
            InitializeComponent();
            articulo = new Articulo();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            NegocioArticulo negArt = new NegocioArticulo();

            try
            {
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Codigo = txtCodigo.Text;
                articulo.Precio = float.Parse(txtPrecio.Text);
                articulo.Urlimagen = txtImagen.Text;

                articulo.Marca = (Marca)cmbMarca.SelectedItem;
                articulo.Categoria = (Categoria)cmbCategoria.SelectedItem;

                negArt.agregarArticulo(articulo);
                MessageBox.Show("Agregado exitosamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Close();
            }
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            NegocioMarca negocioMarca = new NegocioMarca();
            NegocioCategoria negocioCategoria = new NegocioCategoria();

            cmbMarca.DataSource = negocioMarca.listar();
            cmbMarca.ValueMember = "id";
            cmbMarca.DisplayMember = "descripcion";

            cmbCategoria.DataSource = negocioCategoria.listar();
            cmbCategoria.ValueMember = "id";
            cmbCategoria.DisplayMember = "descripcion";

            if (articulo != null && articulo.Id != 0)
            {
                txtCodigo.Text = articulo.Codigo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                txtPrecio.Text = articulo.Precio.ToString();
                txtImagen.Text = articulo.Urlimagen;

                cmbMarca.SelectedValue = articulo.Marca.id;
                cmbCategoria.SelectedValue = articulo.Categoria.id;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
