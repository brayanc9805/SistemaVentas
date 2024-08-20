using SV.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.BLL.Servicios.Contrato
{
    public interface IDashboardService
    {
        Task<DashboardDTO> Resumen();
    }
}
