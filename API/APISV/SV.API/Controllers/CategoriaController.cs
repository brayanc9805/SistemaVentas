﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SV.BLL.Servicios.Contrato;
using SV.DTO;
using SV.API.Utilidad;

namespace SV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<CategoriaDTO>>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _categoriaService.Lista();
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
