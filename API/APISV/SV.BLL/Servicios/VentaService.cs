using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SV.BLL.Servicios.Contrato;
using SV.DAL.Repositorios.Contrato;
using SV.DTO;
using SV.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.BLL.Servicios
{
    public class VentaService:IVentaService
    {
        private readonly IVentaRepository _ventaRepositorio;
        private readonly IGenericRepository<DetalleVenta> _detalleVentaRepositorio;
        private readonly IMapper _mapper;

        public VentaService(IVentaRepository ventaRepositorio, IGenericRepository<DetalleVenta> detalleVentaRepositorio, IMapper mapper)
        {
            _ventaRepositorio = ventaRepositorio;
            _detalleVentaRepositorio = detalleVentaRepositorio;
            _mapper = mapper;
        }

        public async Task<VentaDTO> Registrar(VentaDTO modelo)
        {
            try
            {
                var ventaGenerada = await _ventaRepositorio.Registrar(_mapper.Map<Venta>(modelo));
                if (ventaGenerada.IdVenta == 0)
                    throw new TaskCanceledException("No se pudo crear venta");
                return _mapper.Map<VentaDTO>(ventaGenerada);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Venta> query = await _ventaRepositorio.Consultar();
            var listaResultado = new List<Venta>();
            try
            {
                if (buscarPor == "fecha")
                {
                    DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
                    DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));
                    listaResultado = await query.Where(v =>
                    v.FechaRegistro.Value.Date >= fecha_inicio.Date &&
                    v.FechaRegistro.Value.Date <= fecha_fin.Date)
                        .Include(dv=>
                    dv.DetalleVenta)
                        .ThenInclude(p=>
                    p.IdProductoNavigation).ToListAsync();
                }
                else
                {
                    listaResultado = await query.Where(v =>
                                        v.NumeroDocumento==numeroVenta)
                                            .Include(dv =>
                                        dv.DetalleVenta)
                                            .ThenInclude(p =>
                                        p.IdProductoNavigation).ToListAsync();
                }

            }
            catch (Exception)
            {
                throw;
            }
            return _mapper.Map<List<VentaDTO>>(listaResultado);
        }
        public async Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin)
        {
            IQueryable<DetalleVenta> query = await _detalleVentaRepositorio.Consultar();
            var listaResultado = new List<DetalleVenta>();
            try
            {
                DateTime fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
                DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));
                listaResultado = await query.Include(p =>
                p.IdProductoNavigation)
                    .Include(v => v.IdVentaNavigation)
                    .Where(dv =>
                    dv.IdVentaNavigation.FechaRegistro.Value.Date >= fecha_inicio.Date &&
                     dv.IdVentaNavigation.FechaRegistro.Value.Date <= fecha_fin.Date).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return _mapper.Map<List<ReporteDTO>>(listaResultado);
        }
    }
}
