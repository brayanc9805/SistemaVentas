using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV.DAL.Repositorios.Contrato;
using SV.DAL.DBContext;
using SV.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SV.DAL.Repositorios
{
    public class VentaRepository: GenericRepository<Venta>,IVentaRepository
    {
        private readonly TiendaGenericaContext _dbcontext;

        public VentaRepository(TiendaGenericaContext dbcontext): base(dbcontext) 
        {
            _dbcontext = dbcontext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta ventaGenerada=new Venta();
            using (var transaction = _dbcontext.Database.BeginTransaction())
            {
                try
                {
                    //Restar stock productos
                    foreach(DetalleVenta dv in modelo.DetalleVenta)
                    {
                        Producto producto_found = _dbcontext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();
                        producto_found.Stock = producto_found.Stock - dv.Cantidad;
                        _dbcontext.Productos.Update(producto_found);
                    }
                    await _dbcontext.SaveChangesAsync();

                    //Guadar correlativos de venta
                    NumeroDocumento correlativo = _dbcontext.NumeroDocumentos.First();
                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;
                    _dbcontext.NumeroDocumentos.Update(correlativo);
                    await _dbcontext.SaveChangesAsync();

                    //Generar formato digitos
                    int CantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos,CantidadDigitos);
                    modelo.NumeroDocumento= numeroVenta;

                    //Agregar Venta
                    await _dbcontext.Venta.AddAsync(modelo);
                    await _dbcontext.SaveChangesAsync();

                    ventaGenerada = modelo;
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                return ventaGenerada;
            }
        }
    }
}
