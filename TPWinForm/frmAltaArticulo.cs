using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClasesDominio;
using Negocio;

namespace TPWinForm
{
    public partial class frmAltaArticulo : Form
    {
        List<Marca> marcas;
        List<Categoria> categorias;

        private Articulo articulo = new Articulo();
        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            NegocioArticulo artiNeg = new NegocioArticulo();

            if (articulo == null)
            {
                articulo = new Articulo();
            }
                try
                {
                    articulo.nombre = txtNombre.Text;
                    articulo.descripcion = txtDescripcion.Text;
                    articulo.codigo = txtCodigo.Text;
                    articulo.precio = float.Parse(txtPrecio.Text);

                    articulo.marca = new Marca();

                    Marca marcaSeleccionada = (Marca)cmbMarca.SelectedItem;
                    articulo.marca.descripcion = marcaSeleccionada.descripcion;
                    articulo.marca.id = marcaSeleccionada.id;

                    articulo.categoria = new Categoria();

                    Categoria categoriaSeleccionada = (Categoria)cmbCategoria.SelectedItem;
                    articulo.categoria.descripcion = categoriaSeleccionada.descripcion;
                    articulo.categoria.id = categoriaSeleccionada.id;

                    int idArticulo = artiNeg.agregarArticulo(articulo);
                    DataAccess hola = new DataAccess();
                    hola.cerrarConexion();
                MessageBox.Show("Agregado exitosamente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
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
            

            marcas = negocioMarca.listar();
            categorias = negocioCategoria.listar();

            foreach (Marca marca in marcas)
            {
                cmbMarca.Items.Add(marca);
            }
            foreach (Categoria categoria in categorias)
            {
                cmbCategoria.Items.Add(categoria);
            }
            cmbMarca.ValueMember = "Id";
            cmbMarca.DisplayMember = "Descripcion";
            cmbCategoria.ValueMember = "Id";
            cmbCategoria.DisplayMember = "Descripcion";

            if (articulo != null) 
            { 
                txtCodigo.Text = articulo.codigo;
                txtNombre.Text = articulo.nombre;
                txtDescripcion.Text = articulo.descripcion;
                txtPrecio.Text = articulo.precio.ToString();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
