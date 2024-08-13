using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
namespace SV.DAL.Repositorios.Contrato
{
    //Metodos bajo el contrato IGenericRepository para interactuar con la información de lso modelos
    public interface IGenericRepository<TModel> where TModel : class
    {
        //Para Obtener algun modelo tipo asincrono
        Task<TModel> Obtener(Expression<Func<TModel, bool>> filtro);
        
        Task<TModel> Crear(TModel modelo);
        Task<bool> Editar(TModel modelo);
        Task<bool> Eliminar(TModel modelo);
        Task<IQueryable<TModel>> Consultar(Expression<Func<TModel, bool>> filtro=null);
    }
}
