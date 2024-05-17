using FinalAPI.Models;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace FinalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PricePredictorController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PricePredictorController));

        public PricePredictorController()
        {
            XmlConfigurator.Configure(new FileInfo("../../../LoggerConfig.xml"));
        }

        [HttpPost("predict-price")]
        public async Task<IActionResult> PredictPrice([FromBody] CarParameters parameters)
        {
            try
            {
                var jsonParameters = JsonConvert.SerializeObject(parameters);
                log.Info($"Parametros recibidos para predicción: {jsonParameters}");

                // Ruta del script de Python
                var scriptPath = "predict_price.py";

                var startInfo = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = $"{scriptPath} \"{jsonParameters}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                    var output = await process.StandardOutput.ReadToEndAsync();
                    var errorOutput = await process.StandardError.ReadToEndAsync();
                    process.WaitForExit();

                    if (!string.IsNullOrWhiteSpace(errorOutput))
                    {
                        log.Error($"Error en el script de Python: {errorOutput}");
                        return StatusCode(500, $"Error en el script de Python: {errorOutput}");
                    }

                    log.Info($"Salida del script de Python: {output}");

                    // Aquí puedes manejar la salida del script de Python y devolverla como respuesta
                    return Ok(output);
                }
            }
            catch (Exception ex)
            {
                log.Error($"Error interno del servidor al predecir el precio: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
