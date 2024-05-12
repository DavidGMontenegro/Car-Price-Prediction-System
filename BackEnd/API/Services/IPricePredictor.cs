using System.Collections.Generic;
using System.Threading.Tasks;
using FinalAPI.Models;

namespace FinalAPI.Services
{
    public interface IPricePredictor
    {
        Task ExecuteNotebook(string notebookPath, dynamic parameters);
    }
}
