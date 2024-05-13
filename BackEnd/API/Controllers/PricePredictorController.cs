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
        private readonly IPricePredictor pricePredictor;

        public PricePredictorController(IPricePredictor pricePredictor)
        {
            this.pricePredictor = pricePredictor;
        }

        [HttpPost("predict-price")]
        public async Task<IActionResult> PredictPrice([FromBody] CarParameters parameters)
        {
            try
            {
                // Aquí podrías llamar a tu servicio para hacer la predicción del precio del coche
                var precioPredicho = await pricePredictor.PredictPrice(parameters);

                return Ok(new { precio = precioPredicho });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
