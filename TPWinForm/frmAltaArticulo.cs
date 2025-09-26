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
        private Articulo articulo = null;

        public frmAltaArticulo()
        {
            InitializeComponent();
            
        }
        public frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            Text = "Modificar Artículo";
            lblTitulo.Text = "Modificar Artículo";
            this.articulo = articulo;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            NegocioArticulo negArt = new NegocioArticulo();

            try
            {
                if (!validarCampos())
                {
                    return;
                }

                if (articulo == null)
                {
                    articulo = new Articulo();
                }

                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Codigo = txtCodigo.Text;
                articulo.Precio = float.Parse(txtPrecio.Text);

                articulo.Marca = (Marca)cmbMarca.SelectedItem;
                articulo.Categoria = (Categoria)cmbCategoria.SelectedItem;


                //stackoverflow.com/questions/3703256/linq-extension-methods-any-vs-where-vs-exists
                List<Articulo> articulos = negArt.listar();
                if (articulos.Any(a => a.Codigo == articulo.Codigo && a.Id != articulo.Id))
                {
                    MessageBox.Show("Ya existe un artículo con ese Código.");
                    return;
                }

                //if (articulo.Id == 0)
                //{
                //    negArt.agregarArticulo(articulo);
                //    MessageBox.Show("Agregado exitosamente");
                //}
                //else
                //{
                //    negArt.modificar(articulo);
                //    MessageBox.Show("Modificado exitosamente");
                //}
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private bool validarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Debe ingresar un Código.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar un Nombre.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("Debe ingresar un Precio válido.");
                return false;
            }
            else if (!float.TryParse(txtPrecio.Text, out _))
            {
                MessageBox.Show("El Precio debe ser un número válido.");
                return false;
            }

            return true;
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
