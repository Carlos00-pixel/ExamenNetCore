using ExamenNetCore.Data;
using ExamenNetCore.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

#region SQL SERVER
/*
    CREATE VIEW V_IMAGENES_ZAPATILLAS
    AS
	    SELECT CAST(
	    ROW_NUMBER() OVER (ORDER BY IDIMAGEN) AS INT) AS POSICION,
	    ISNULL(IDIMAGEN, 0) AS IDIMAGEN, IDPRODUCTO, IMAGEN 
	    FROM IMAGENESZAPASPRACTICA
    GO 
    CREATE PROCEDURE SP_IMAGENES_ZAPATILLAS
    (@POSICION INT, @IDPRODUCTO INT)
    AS
        SELECT POSICION,IDIMAGEN, IDPRODUCTO, IMAGEN
        FROM (SELECT CAST(
	    ROW_NUMBER() OVER (ORDER BY IDIMAGEN) AS INT) AS POSICION,
	    ISNULL(IDIMAGEN, 0) AS IDIMAGEN, IDPRODUCTO, IMAGEN
	    FROM imageneszapaspractica
        WHERE IDPRODUCTO = @IDPRODUCTO) as query
	    where POSICION >= @POSICION AND POSICION < (@POSICION + 1)
    GO
*/
#endregion

namespace ExamenNetCore.Repositories
{
    public class RepositoryZapas
    {
        private ZapatillasContext context;

        public RepositoryZapas(ZapatillasContext context)
        {
            this.context = context;
        }

        public List<Zapatilla> GetZapatillas()
        {
            var consulta = from x in this.context.Zapatillas
                           select x;
            return consulta.ToList();
        }

        public Zapatilla FindZapatilla(int idZapatilla)
        {
            var consulta = from x in this.context.Zapatillas
                           where x.IdProducto == idZapatilla
                           select x;
            return consulta.FirstOrDefault();
        }

        public int GetNumeroRegistrosVistaImagenZapatillas(int idzapatilla)
        {
            return this.context.VistaImagenZapatillas.Where(x => x.IdProducto == idzapatilla).Count();
        }

        public async Task<VistaImagenZapatilla>
            GetVistaImagenZapatillasAsync(int posicion, int idZapatilla)
        {
            string sql = "SP_IMAGENES_ZAPATILLAS @POSICION, @IDPRODUCTO";
            SqlParameter pamposicion =
                new SqlParameter("@POSICION", posicion);
            SqlParameter pamzapatilla =
                new SqlParameter("@IDPRODUCTO", idZapatilla);
            List<VistaImagenZapatilla> imagenes = await this.context.VistaImagenZapatillas.FromSqlRaw(sql, pamzapatilla, pamposicion).ToListAsync();
            return imagenes.FirstOrDefault();

        }
    }
}
