using FinalAPI.Models;
using FinalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PricePredictorController : ControllerBase
    {
        private readonly IPricePredictor _fileService;

        public PricePredictorController(IPricePredictor fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("execute-notebook")]
        public async Task<IActionResult> ExecuteNotebook([FromBody] dynamic parameters)
        {
            try
            {
                string notebookPath = "ruta/al/notebook.ipynb";
                await _fileService.ExecuteNotebook(notebookPath, parameters);
                return Ok("Notebook executed successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
