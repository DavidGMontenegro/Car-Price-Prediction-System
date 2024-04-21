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
    public class DataController : ControllerBase
    {   
        private readonly IDataService dataService;

        public DataController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [HttpGet("get-all-data")]
        public async Task<IActionResult> GetAllData()
        {
            try
            {
                var data = await this.dataService.GetAllData();
                if (data != null)
                    return Ok(data);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // Gráfico de barras - Distribución de precios por marca
        [HttpGet("price-distribution-by-make")]
        public async Task<IActionResult> GetPriceDistributionByMake()
        {
            try
            {
                var data = await this.dataService.GetPriceDistributionByMake();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // Gráfico de barras - Distribución de tipos de transmisión
        [HttpGet("transmission-type-distribution")]
        public async Task<IActionResult> GetTransmissionTypeDistribution()
        {
            try
            {
                var data = await this.dataService.GetTransmissionTypeDistribution();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // Gráfico de líneas - Tendencia de precios a lo largo de los años
        [HttpGet("price-trend-by-year")]
        public async Task<IActionResult> GetPriceTrendByYear()
        {
            try
            {
                var data = await this.dataService.GetPriceTrendByYear();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // Gráfico circular - Distribución de tipos de combustible
        [HttpGet("fuel-type-distribution")]
        public async Task<IActionResult> GetFuelTypeDistribution()
        {
            try
            {
                var data = await this.dataService.GetFuelTypeDistribution();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // Histograma - Distribución de años de los coches
        [HttpGet("year-distribution")]
        public async Task<IActionResult> GetYearDistribution()
        {
            try
            {
                var data = await this.dataService.GetYearDistribution();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
