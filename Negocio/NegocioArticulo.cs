using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClasesDominio;

namespace Negocio
{
    public class NegocioArticulo
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            DataAccess datosArticulo = new DataAccess();

            string query = "SELECT ARTICULOS.Id, ARTICULOS.Codigo, ARTICULOS.Nombre, ARTICULOS.Descripcion, MARCAS.Descripcion AS Marca, CATEGORIAS.Descripcion AS Categoria, ARTICULOS.Precio, MARCAS.Id AS IdMarca, CATEGORIAS.Id AS IdCategoria FROM ARTICULOS LEFT JOIN CATEGORIAS ON ARTICULOS.IdCategoria = CATEGORIAS.Id LEFT JOIN MARCAS ON ARTICULOS.IdMarca = MARCAS.Id";

            try
            {
                datosArticulo.setearConsulta(query);
                datosArticulo.ejecutaLector();

                while (datosArticulo.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.id = (int)datosArticulo.Lector["Id"];
                    aux.codigo = (string)datosArticulo.Lector["Codigo"];
                    aux.nombre = (string)datosArticulo.Lector["Nombre"];
                    aux.descripcion = (string)datosArticulo.Lector["Descripcion"];

                    aux.marca = new Marca() { id = (int)datosArticulo.Lector["IdMarca"], descripcion = (string)datosArticulo.Lector["Marca"] };

                    aux.categoria = new Categoria();
                    aux.categoria = !datosArticulo.Lector.IsDBNull(datosArticulo.Lector.GetOrdinal("Categoria")) ? new Categoria() { id = (int)datosArticulo.Lector["IdCategoria"], descripcion = (string)datosArticulo.Lector["Categoria"] } : new Categoria { descripcion = "Sin categoria" };


                    aux.precio = (float)Convert.ToDecimal(datosArticulo.Lector["Precio"]);

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datosArticulo.cerrarConexion();
            }
        }

        public List<Imagen> getImagens(int id)
        {
            List<Imagen> lista = new List<Imagen>();
            DataAccess datosArticulo = new DataAccess();
            try
            {
                datosArticulo.setearConsulta("SELECT ImagenUrl FROM IMAGENES WHERE IdArticulo = @IdArticuloSeleccionado");
                datosArticulo.setearParametro("@IdArticuloSeleccionado", id);
                datosArticulo.ejecutaLector();

                
                while (datosArticulo.Lector.Read())
                {
                    lista.Add(new Imagen
                    {
                        urlImagen = datosArticulo.Lector["ImagenUrl"]?.ToString()
                    });
                }


                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datosArticulo.cerrarConexion();
            }

        }
    }
}
