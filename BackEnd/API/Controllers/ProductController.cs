using FinalAPI.Models;
using FinalAPI.Services;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        
        private static readonly ILog log = LogManager.GetLogger(typeof(ProductController));

        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            try
            {
                log.Info("Obteniendo todos los productos");
                return Ok(await productService.GetProducts());
            }
            catch (Exception ex)
            {
                log.Error($"Error al obtener todos los productos: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetSingleProduct(int id)
        {
            try
            {
                log.Info($"Obteniendo el producto con ID: {id}");
                return Ok(await productService.GetSingleProduct(id));
            }
            catch (Exception ex)
            {
                log.Error($"Error al obtener el producto con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<List<Product>>> Post(Product newProduct)
        {
            try
            {
                log.Info("Creando un nuevo producto");
                return Ok(await productService.Post(newProduct));
            }
            catch (Exception ex)
            {
                log.Error($"Error al crear un nuevo producto: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Product>> Put(int id, Product updatedProduct)
        {
            try
            {
                log.Info($"Actualizando el producto con ID: {id}");
                return Ok(await productService.Put(id, updatedProduct));

            }
            catch (Exception ex)
            {
                log.Error($"Error al actualizar el producto con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                log.Info($"Eliminando el producto con ID: {id}");
                return Ok(await productService.Delete(id));
            }
            catch (Exception ex)
            {
                log.Error($"Error al eliminar el producto con ID {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor");
            }
        }
    }
}
