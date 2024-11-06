﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SV.BLL.Servicios.Contrato;
using SV.DTO;
using SV.API.Utilidad;

namespace SV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolServicio;

        public RolController(IRolService rolServicio)
        {
            _rolServicio = rolServicio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista() {
            var rsp = new Response<List<RolDTO>>();
            try
            {
                rsp.Status = true;
                rsp.Value=await _rolServicio.Lista();
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.Message=ex.Message;
            }
            return Ok(rsp);
        }
    }
}
