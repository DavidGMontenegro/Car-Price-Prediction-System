using FinalAPI.Models;
using FinalAPI.Services;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO; // Required for FileInfo

namespace FinalAPI.Controllers
{
    /// <summary>
    /// Controller for managing data-related endpoints.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DataController));
        private readonly IDataService dataService;

        /// <summary>
        /// Constructor for initializing DataController with IDataService dependency.
        /// </summary>
        /// <param name="dataService">Data service dependency</param>
        public DataController(IDataService dataService)
        {
            // Configure log4net
            XmlConfigurator.Configure(new FileInfo("../../../LoggerConfig.xml"));
            this.dataService = dataService;
        }

        /// <summary>
        /// Retrieves all data.
        /// </summary>
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
                // Log and handle internal server error
                log.Error($"Internal server error while retrieving all data: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves price distribution by car make.
        /// </summary>
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
                // Log and handle internal server error
                log.Error($"Internal server error while retrieving price distribution by make: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves transmission type distribution.
        /// </summary>
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
                // Log and handle internal server error
                log.Error($"Internal server error while retrieving transmission type distribution: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves price trend by year.
        /// </summary>
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
                // Log and handle internal server error
                log.Error($"Internal server error while retrieving price trend by year: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves fuel type distribution.
        /// </summary>
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
                // Log and handle internal server error
                log.Error($"Internal server error while retrieving fuel type distribution: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves year distribution of cars.
        /// </summary>
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
                // Log and handle internal server error
                log.Error($"Internal server error while retrieving year distribution of cars: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all car brands.
        /// </summary>
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
                // Log and handle internal server error
                log.Error($"Internal server error while retrieving all car brands: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves cars by brand.
        /// </summary>
        /// <param name="make">Car make</param>
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
                // Log and handle internal server error
                log.Error($"Internal server error while retrieving cars by brand: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves powers by brand and model.
        /// </summary>
        /// <param name="brand">Car brand</param>
        /// <param name="model">Car model</param>
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
                // Log and handle internal server error
                log.Error($"Internal server error while retrieving powers by brand and model: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves colors by brand and model.
        /// </summary>
        /// <param name="brand">Car brand</param>
        /// <param name="model">Car model</param>
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
                // Log and handle internal server error
                log.Error($"Internal server error while retrieving colors by brand and model: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
