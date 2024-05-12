using System.Collections.Generic;
using System.Threading.Tasks;
using FinalAPI.Models;

namespace FinalAPI.Services
{
    public interface IDataService
    {
        Task<IEnumerable<Car>> GetAllData();

        // Gráfico de barras - Distribución de precios por marca
        Task<IDictionary<string, decimal>> GetPriceDistributionByMake();

        // Gráfico de barras - Distribución de tipos de transmisión
        Task<IDictionary<string, int>> GetTransmissionTypeDistribution();

        // Gráfico de líneas - Tendencia de precios a lo largo de los años
        Task<IDictionary<int, decimal>> GetPriceTrendByYear();

        // Gráfico circular - Distribución de tipos de combustible
        Task<IDictionary<string, double>> GetFuelTypeDistribution();

        // Histograma - Distribución de años de los coches
        Task<IDictionary<int, int>> GetYearDistribution();
        // Get all diferent brands
        Task<IEnumerable<string>> GetAllCarBrands();
        // Get all cars from a brand
        Task<IEnumerable<string>> GetCarsByBrand(string make);
        Task<IEnumerable<string>> GetPowersByBrandAndModel(string brand, string model);

        Task<IEnumerable<string>> GetColorsByBrandAndModel(string brand, string model);
    }
}
