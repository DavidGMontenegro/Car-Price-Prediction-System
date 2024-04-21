using FinalAPI.Data;
using FinalAPI.Models;
using FinalAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class DataService : IDataService
{
    private readonly DataContext _context;

    public DataService(DataContext context)
    {
        this._context = context;
    }

    // Obtiene todos los datos de los coches del archivo CSV
    public async Task<IEnumerable<Car>> GetAllData()
    {
        // Ruta al archivo CSV
        string filePath = ".\\Car-dataset\\car details v4.csv";
        var cars = new List<Car>();

        // Leer el archivo CSV y cargar los datos en la lista de objetos Car
        using (var reader = new StreamReader(filePath))
        {
            // Saltar la primera línea (cabecera)
            await reader.ReadLineAsync();

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                var values = line.Split(',');

                if (values.Any(string.IsNullOrWhiteSpace))
                {
                    // Si alguna celda está vacía, saltar esta línea y continuar con la siguiente
                    continue;
                }

                // Crear un objeto Car con los datos de la línea
                var car = new Car
                {
                    Make = values[0],
                    Model = values[1],
                    Price = decimal.Parse(values[2]) * (decimal)0.011,
                    Year = int.Parse(values[3]),
                    Kilometer = int.Parse(values[4]),
                    FuelType = values[5],
                    Transmission = values[6],
                    Location = values[7],
                    Color = values[8],
                    Owner = values[9],
                    SellerType = values[10],
                    Engine = values[11],
                    MaxPower = values[12],
                    MaxTorque = values[13],
                    Drivetrain = values[14],
                    Length = decimal.Parse(values[15]),
                    Width = decimal.Parse(values[16]),
                    Height = decimal.Parse(values[17]),
                    SeatingCapacity = decimal.Parse(values[18]),
                    FuelTankCapacity = decimal.Parse(values[19])
                };

                // Agregar el objeto Car a la lista
                cars.Add(car);
            }
        }

        return cars;
    }

    // Obtiene la distribución de precios por marca de los coches
    public async Task<IDictionary<string, decimal>> GetPriceDistributionByMake()
    {
        var data = await GetAllData();
        return data.GroupBy(car => car.Make)
                   .ToDictionary(group => group.Key, group => group.Average(car => car.Price));
    }

    // Obtiene la distribución de tipos de transmisión de los coches
    public async Task<IDictionary<string, int>> GetTransmissionTypeDistribution()
    {
        var data = await GetAllData();
        return data.GroupBy(car => car.Transmission)
                   .ToDictionary(group => group.Key, group => group.Count());
    }

    // Obtiene la tendencia de precios a lo largo de los años de los coches
    public async Task<IDictionary<int, decimal>> GetPriceTrendByYear()
    {
        var data = await GetAllData();
        return data.GroupBy(car => car.Year)
                   .ToDictionary(group => group.Key, group => group.Average(car => car.Price));
    }

    // Obtiene la distribución de tipos de combustible de los coches
    public async Task<IDictionary<string, double>> GetFuelTypeDistribution()
    {
        var data = await GetAllData();
        var totalCount = data.Count();
        return data.GroupBy(car => car.FuelType)
                   .ToDictionary(group => group.Key, group => (double)group.Count() / totalCount * 100);
    }

    // Obtiene la distribución de años de fabricación de los coches
    public async Task<IDictionary<int, int>> GetYearDistribution()
    {
        var data = await GetAllData();
        return data.GroupBy(car => car.Year)
                   .ToDictionary(group => group.Key, group => group.Count());
    }
}
