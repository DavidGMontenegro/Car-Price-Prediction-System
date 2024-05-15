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
    public class DataController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DataController));
        private readonly IDataService dataService;

        public DataController(IDataService dataService)
        {
            XmlConfigurator.Configure(new FileInfo("../../../LoggerConfig.xml"));
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
                log.Error($"Error interno del servidor al obtener todos los datos: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

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
                log.Error($"Error interno del servidor al obtener distribución de precios por marca: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

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
                log.Error($"Error interno del servidor al obtener distribución de tipos de transmisión: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

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
                log.Error($"Error interno del servidor al obtener tendencia de precios por año: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

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
                log.Error($"Error interno del servidor al obtener distribución de tipos de combustible: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

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
                log.Error($"Error interno del servidor al obtener distribución de años de los coches: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("get-car-brands")]
        public async Task<IActionResult> GetAllCarBrands()
        {
            try
            {
                var data = await this.dataService.GetAllCarBrands();
                return Ok(data);
            }
            catch (Exception ex)
            {
                log.Error($"Error interno del servidor al obtener todas las marcas de coches: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("get-cars-by-brand")]
        public async Task<IActionResult> GetCarsByBrand(String make)
        {
            try
            {
                var data = await this.dataService.GetCarsByBrand(make);
                return Ok(data);
            }
            catch (Exception ex)
            {
                log.Error($"Error interno del servidor al obtener coches por marca: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("powers")]
        public async Task<IActionResult> GetPowers(string brand, string model)
        {
            try
            {
                var powers = await dataService.GetPowersByBrandAndModel(brand, model);
                return Ok(powers);
            }
            catch (Exception ex)
            {
                log.Error($"Error interno del servidor al obtener potencias por marca y modelo: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("colors")]
        public async Task<IActionResult> GetColors(string brand, string model)
        {
            try
            {
                var colors = await dataService.GetColorsByBrandAndModel(brand, model);
                return Ok(colors);
            }
            catch (Exception ex)
            {
                log.Error($"Error interno del servidor al obtener colores por marca y modelo: {ex.Message}");
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}
