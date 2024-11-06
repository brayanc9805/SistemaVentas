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
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista(int idUsuario)
        {
            var rsp = new Response<List<MenuDTO>>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _menuService.Lista(idUsuario);
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
