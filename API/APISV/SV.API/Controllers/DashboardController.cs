using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SV.BLL.Servicios.Contrato;
using SV.DTO;
using SV.API.Utilidad;
using SV.BLL.Servicios;


namespace SV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            this._dashboardService = dashboardService;
        }

        [HttpGet]
        [Route("Resumen")]
        public async Task<IActionResult> Resumen()
        {
            var rsp = new Response<DashboardDTO>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _dashboardService.Resumen();
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Message = ex.Message;
            }
            return Ok(rsp);
        }
    }
}
