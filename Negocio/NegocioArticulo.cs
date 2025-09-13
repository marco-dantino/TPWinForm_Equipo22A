using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClasesDominio;
using System.Windows.Forms;

namespace Negocio
{
    public class NegocioArticulo
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            DataAccess datos = new DataAccess();

            string query = @"
                SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, 
                       M.Descripcion AS Marca, C.Descripcion AS Categoria, 
                       A.Precio, M.Id AS IdMarca, C.Id AS IdCategoria, 
                       I.ImagenUrl 
                FROM ARTICULOS A
                LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id
                LEFT JOIN MARCAS M ON A.IdMarca = M.Id
                LEFT JOIN IMAGENES I ON A.Id = I.IdArticulo;";

            try
            {
                datos.setearConsulta(query);
                datos.ejecutaLector();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo
                    {
                        Id = (int)datos.Lector["Id"],
                        Codigo = (string)datos.Lector["Codigo"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Descripcion = (string)datos.Lector["Descripcion"],
                        Marca = new Marca
                        {
                            id = (int)datos.Lector["IdMarca"],
                            descripcion = (string)datos.Lector["Marca"]
                        },
                        Categoria = !datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Categoria")) ? new Categoria { id = (int)datos.Lector["IdCategoria"], descripcion = (string)datos.Lector["Categoria"] } : new Categoria { descripcion = "Sin categoría" },
                        Precio = (float)Convert.ToDecimal(datos.Lector["Precio"])
                    };

                    if (!datos.Lector.IsDBNull(datos.Lector.GetOrdinal("ImagenUrl"))) aux.Urlimagen = (string)datos.Lector["ImagenUrl"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar artículos", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public int agregarArticulo(Articulo newArticulo)
        {
            DataAccess datos = new DataAccess();
            try
            {
                datos.setearConsulta(@" INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) OUTPUT Inserted.Id VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio)");

                datos.setearParametro("@Codigo", newArticulo.Codigo);
                datos.setearParametro("@Nombre", newArticulo.Nombre);
                datos.setearParametro("@Descripcion", newArticulo.Descripcion);
                datos.setearParametro("@IdMarca", newArticulo.Marca.id);
                datos.setearParametro("@IdCategoria", newArticulo.Categoria.id);
                datos.setearParametro("@Precio", newArticulo.Precio);

                int idArticulo = (int)datos.ejecutarScalar();

                if (!string.IsNullOrEmpty(newArticulo.Urlimagen))
                {
                    agregarImagen(idArticulo, newArticulo.Urlimagen);
                }

                return idArticulo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar artículo", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregarImagen(int idArticulo, string urlImagen)
        {
            DataAccess datos = new DataAccess();
            try
            {
                datos.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl)");
                datos.setearParametro("@IdArticulo", idArticulo);
                datos.setearParametro("@ImagenUrl", urlImagen);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar imagen", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
