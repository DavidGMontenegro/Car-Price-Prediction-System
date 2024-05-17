using FinalAPI.Data;
using FinalAPI.Models;
using FinalAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Service class for handling data operations related to cars.
/// </summary>
public class DataService : IDataService
{
    private readonly DataContext _context;

    /// <summary>
    /// Constructor for initializing DataService with DataContext dependency.
    /// </summary>
    /// <param name="context">Data context</param>
    public DataService(DataContext context)
    {
        this._context = context;
    }

    /// <summary>
    /// Retrieves all car data from the CSV file.
    /// </summary>
    public async Task<IEnumerable<Car>> GetAllData()
    {
        // Path to the CSV file
        string filePath = "./Car-dataset/car details v4.csv";
        var cars = new List<Car>();

        // Read the CSV file and load the data into the list of Car objects
        using (var reader = new StreamReader(filePath))
        {
            // Skip the first line (header)
            await reader.ReadLineAsync();

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                var values = line.Split(',');

                if (values.Any(string.IsNullOrWhiteSpace))
                {
                    // If any cell is empty, skip this line and proceed to the next
                    continue;
                }

                // Create a Car object with the data from the line
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

                // Add the Car object to the list
                cars.Add(car);
            }
        }

        return cars;
    }

    /// <summary>
    /// Retrieves the price distribution by make of cars.
    /// </summary>
    public async Task<IDictionary<string, decimal>> GetPriceDistributionByMake()
    {
        var data = await GetAllData();
        return data.GroupBy(car => car.Make)
                   .ToDictionary(group => group.Key, group => group.Average(car => car.Price));
    }

    /// <summary>
    /// Retrieves the transmission type distribution of cars.
    /// </summary>
    public async Task<IDictionary<string, int>> GetTransmissionTypeDistribution()
    {
        var data = await GetAllData();
        return data.GroupBy(car => car.Transmission)
                   .ToDictionary(group => group.Key, group => group.Count());
    }

    /// <summary>
    /// Retrieves the price trend by year of cars.
    /// </summary>
    public async Task<IDictionary<int, decimal>> GetPriceTrendByYear()
    {
        var data = await GetAllData();
        return data.GroupBy(car => car.Year)
                   .ToDictionary(group => group.Key, group => group.Average(car => car.Price));
    }

    /// <summary>
    /// Retrieves the fuel type distribution of cars.
    /// </summary>
    public async Task<IDictionary<string, double>> GetFuelTypeDistribution()
    {
        var data = await GetAllData();
        var totalCount = data.Count();
        return data.GroupBy(car => car.FuelType)
                   .ToDictionary(group => group.Key, group => (double)group.Count() / totalCount * 100);
    }

    /// <summary>
    /// Retrieves the year distribution of cars.
    /// </summary>
    public async Task<IDictionary<int, int>> GetYearDistribution()
    {
        var data = await GetAllData();
        return data.GroupBy(car => car.Year)
                   .ToDictionary(group => group.Key, group => group.Count());
    }

    /// <summary>
    /// Retrieves all car brands.
    /// </summary>
    public async Task<IEnumerable<string>> GetAllCarBrands()
    {
        var data = await GetAllData();
        var uniqueMakes = data.Select(car => car.Make).Distinct().OrderBy(make => make, StringComparer.OrdinalIgnoreCase);
        return uniqueMakes;
    }

    /// <summary>
    /// Retrieves cars by brand.
    /// </summary>
    /// <param name="make">Brand</param>
    public async Task<IEnumerable<string>> GetCarsByBrand(string make)
    {
        var data = await GetAllData();
        var carsByBrand = data
            .Where(car => string.Equals(car.Make, make, StringComparison.OrdinalIgnoreCase))
            .Select(car => car.Model)
            .Distinct()
            .OrderBy(model => model, StringComparer.OrdinalIgnoreCase);

        return carsByBrand;
    }

    /// <summary>
    /// Retrieves powers by brand and model of cars.
    /// </summary>
    public async Task<IEnumerable<string>> GetPowersByBrandAndModel(string brand, string model)
    {
        try
        {
            var data = await GetAllData();
            var powers = data
                .Where(car => string.Equals(car.Make, brand, StringComparison.OrdinalIgnoreCase) &&
                              string.Equals(car.Model, model, StringComparison.OrdinalIgnoreCase))
                .Select(car => car.MaxPower)
                .Distinct()
                .OrderBy(power => power, StringComparer.OrdinalIgnoreCase);

            return powers;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving powers for brand '{brand}' and model '{model}': {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves colors by brand and model of cars.
    /// </summary>
    public async Task<IEnumerable<string>> GetColorsByBrandAndModel(string brand, string model)
    {
        try
        {
            var data = await GetAllData();
            var colors = data
                .Where(car => string.Equals(car.Make, brand, StringComparison.OrdinalIgnoreCase) &&
                              string.Equals(car.Model, model, StringComparison.OrdinalIgnoreCase))
                .Select(car => car.Color)
                .Distinct()
                .OrderBy(color => color, StringComparer.OrdinalIgnoreCase);

            return colors;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving colors for brand '{brand}' and model '{model}': {ex.Message}");
        }
    }
}

