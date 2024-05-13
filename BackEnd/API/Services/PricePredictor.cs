using FinalAPI.Data;
using FinalAPI.Models;
using FinalAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Python.Runtime;


public class PricePredictor : IPricePredictor
{
    private readonly DataContext _context;

    public PricePredictor(DataContext context)
    {
        this._context = context;
    }

    public async Task<decimal> PredictPrice(CarParameters parameters)
    {
        try
        {
            // Initialize Python runtime
            PythonEngine.Initialize();

            // Load the model from the pkl file
            using (Py.GIL())
            {
                dynamic sys = Py.Import("sys");
                sys.path.append("path_to_your_python_script"); // Path where your Python script is located

                dynamic pickle = Py.Import("pickle");
                dynamic io = Py.Import("io");
                dynamic file = io.File;
                dynamic model = pickle.load(file("../IA/ia_final_GETD.pkl", "rb"));

                // Create a dictionary with the car parameters
                dynamic new_car = new PyDict();
                new_car["Year"] = parameters.Year;
                new_car["Kilometer"] = parameters.Kilometer;
                new_car["Fuel Type"] = parameters.FuelType;
                new_car["Transmission"] = parameters.Transmission;
                new_car["Owner"] = parameters.Owner;
                new_car["Make"] = parameters.Make;
                new_car["Model"] = parameters.Model;
                new_car["Color"] = parameters.Color;
                new_car["Seller Type"] = parameters.SellerType;
                new_car["Engine"] = parameters.Engine;
                new_car["Drivetrain"] = parameters.Drivetrain;

                // Encode the new car using the same encoding as used in the training dataset
                dynamic new_car_encoded = model.get_dummies(new_car);

                // Ensure that the new car dataset has the same columns and in the same order as the training dataset
                dynamic X_train_columns = model.get("X_train").get("columns");
                dynamic new_car_encoded_ordered = new_car_encoded.reindex(columns: X_train_columns, fill_value: 0);

                // Perform the prediction of the new car price
                dynamic predicted_price = model.predict(new_car_encoded_ordered) * 0.011;

                // Convert the predicted price from Python to decimal in C#
                decimal predicted_price_decimal = Convert.ToDecimal(predicted_price[0]);

                return predicted_price_decimal;
            }
        }
        catch (Exception ex)
        {
            // Error handling
            throw new Exception("Error predicting car price: " + ex.Message);
        }
        finally
        {
            // Finalize Python runtime
            PythonEngine.Shutdown();
        }
    }
}
