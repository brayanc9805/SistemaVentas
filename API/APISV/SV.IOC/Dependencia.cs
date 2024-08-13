using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SV.DAL.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV.DAL.Repositorios.Contrato;
using SV.DAL.Repositorios;
using SV.Utility;
namespace SV.IOC
{
    public static class Dependencia
    {
        //Esta clase se encarga de inyectar la dependencia con IServiceCollection->metodo de extension
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            //De esta forma, se obtiene la cadena de conexion
            services.AddDbContext<TiendaGenericaContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
            });

            //ACA UTILIZA PARA CUALQUIER MODELO, GENERICO
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //ACA SI ES SOLO PARA UN MODELO, PARA VENTA
            services.AddScoped<IVentaRepository, VentaRepository>();
            //Se añade dependencia de automapper con todos los mapeos
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
