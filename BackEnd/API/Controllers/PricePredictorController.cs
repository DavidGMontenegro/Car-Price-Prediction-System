using FinalAPI.Models;
using FinalAPI.Services;
using log4net;
using log4net.Config;
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
        private static readonly ILog log = LogManager.GetLogger(typeof(PricePredictorController));
        private readonly IPricePredictor pricePredictor;

        public PricePredictorController(IPricePredictor pricePredictor)
        {
            XmlConfigurator.Configure(new FileInfo("../../../LoggerConfig.xml"));
            this.pricePredictor = pricePredictor;
        }

        [HttpPost("predict-price")]
        public async Task<IActionResult> PredictPrice([FromBody] CarParameters parameters)
        {
            try
            {
                var precioPredicho = await pricePredictor.PredictPrice(parameters);
                log.Info($"Predicción de precio realizada con éxito para el coche de modelo: {parameters}");

                return Ok(new { precio = precioPredicho });
            }
            catch (Exception ex)
            {
                log.Error($"Error interno del servidor al predecir precio: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}
