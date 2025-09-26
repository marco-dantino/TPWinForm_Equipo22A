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
        DataAccess datos = new DataAccess();
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            string query = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Descripcion AS Marca, C.Descripcion AS Categoria, A.Precio, M.Id, C.Id FROM ARTICULOS A LEFT JOIN Categorias C ON A.IdCategoria = C.Id LEFT JOIN Marcas M ON A.IdMarca = M.Id";

            try
            {
                datos.setearConsulta(query);
                datos.ejecutaLector();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca = new Marca
                    {
                        id = datos.Lector.GetInt32(7),
                        descripcion = (string)datos.Lector["Marca"]
                    };
                    aux.Categoria = !datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Categoria")) ?
                                new Categoria { id = datos.Lector.GetInt32(8), descripcion = (string)datos.Lector["Categoria"] } :
                                new Categoria { descripcion = "Sin categoría" };
                    aux.Precio = (float)Convert.ToDecimal(datos.Lector["Precio"]);
                    aux.Imagenes = GetImagenes(aux.Id);

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

        public List<Imagen> GetImagenes(int idArti)
        {
            List<Imagen> lista = new List<Imagen>();
            DataAccess datos = new DataAccess();
            
            try
            {

                datos.setearConsulta("SELECT Id, ImagenUrl FROM IMAGENES WHERE IdArticulo = @IdArticulo");
                datos.setearParametro("@IdArticulo", idArti);
                datos.ejecutaLector();

                while (datos.Lector.Read())
                {
                    Imagen imagen = new Imagen
                    {
                        Id = (int)datos.Lector["Id"],
                        IdArticulo = idArti,
                        UrlImagen = (string)datos.Lector["ImagenUrl"]
                    };
                    lista.Add(imagen);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public List<string> VerImagenes(int id)
        //{
        //    DataAccess datosArticulo = new DataAccess();
        //    List<string> lista = new List<string>();
        //    try
        //    {
        //        datosArticulo.setearConsulta($"select ImagenUrl from IMAGENES where IdArticulo = '{id}'");
        //        datosArticulo.ejecutaLector();
        //        while (datosArticulo.Lector.Read())
        //        {
        //            lista.Add(datosArticulo.Lector["ImagenUrl"].ToString());

        //        }
        //        return lista;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}


        //public int agregarArticulo(Articulo newArticulo)
        //{
        //    try
        //    {
        //        datos.setearConsulta(@" INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) OUTPUT Inserted.Id VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio)");

        //        datos.setearParametro("@Codigo", newArticulo.Codigo);
        //        datos.setearParametro("@Nombre", newArticulo.Nombre);
        //        datos.setearParametro("@Descripcion", newArticulo.Descripcion);
        //        datos.setearParametro("@IdMarca", newArticulo.Marca.id);
        //        datos.setearParametro("@IdCategoria", newArticulo.Categoria.id);
        //        datos.setearParametro("@Precio", newArticulo.Precio);

        //        int idArticulo = (int)datos.ejecutarScalar();

        //        return idArticulo;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al agregar artículo", ex);
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}

        //public void agregarImagen(int idArticulo, string urlImagen)
        //{
        //    try
        //    {
        //        datos.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl)");
        //        datos.setearParametro("@IdArticulo", idArticulo);
        //        datos.setearParametro("@ImagenUrl", urlImagen);
        //        datos.ejecutarAccion();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al agregar imagen", ex);
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}
        //public void modificar(Articulo articulo)
        //{
        //    try
        //    {
        //        datos.setearConsulta(@"UPDATE ARTICULOS SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, Precio = @Precio WHERE Id = @Id");

        //        datos.setearParametro("@Codigo", articulo.Codigo);
        //        datos.setearParametro("@Nombre", articulo.Nombre);
        //        datos.setearParametro("@Descripcion", articulo.Descripcion);
        //        datos.setearParametro("@IdMarca", articulo.Marca.id);
        //        datos.setearParametro("@IdCategoria", articulo.Categoria.id);
        //        datos.setearParametro("@Precio", articulo.Precio);
        //        datos.setearParametro("@Id", articulo.Id);

        //        datos.ejecutarAccion();
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}
        //public void modificarImagen(int idArticulo, string urlImagen)
        //{
        //    DataAccess datos = new DataAccess();
        //    try
        //    {
        //        datos.setearConsulta(@"IF EXISTS (SELECT 1 FROM IMAGENES WHERE IdArticulo = @IdArticulo) UPDATE IMAGENES SET ImagenUrl = @ImagenUrl WHERE IdArticulo = @IdArticulo ELSE INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl)");
        //        datos.setearParametro("@IdArticulo", idArticulo);
        //        datos.setearParametro("@ImagenUrl", urlImagen);
        //        datos.ejecutarAccion();
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}

        //public void eliminar(int idArticulo) 
        //{
        //    try
        //    {
        //        datos.setearConsulta("DELETE FROM IMAGENES WHERE IdArticulo = @IdArticulo");
        //        datos.setearParametro("@IdArticulo", idArticulo);
        //        datos.ejecutarAccion();
        //        datos.cerrarConexion();

        //        datos = new DataAccess();
        //        datos.setearConsulta("DELETE FROM ARTICULOS WHERE Id = @Id");
        //        datos.setearParametro("@Id", idArticulo);
        //        datos.ejecutarAccion();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al eliminar artículo", ex);
        //    }
        //    finally
        //    {
        //        datos.cerrarConexion();
        //    }
        //}
    }
}
