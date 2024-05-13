using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FinalAPI.Models;

namespace FinalAPI.Services
{
    public interface IPricePredictor
    {
        Task<decimal> PredictPrice(CarParameters parameters);
    }
}
