using FinalAPI.Data;
using FinalAPI.Models;
using FinalAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class PricePredictor : IPricePredictor
{
    private readonly DataContext _context;

    public PricePredictor(DataContext context)
    {
        this._context = context;
    }

    public async Task ExecuteNotebook(string notebookPath, dynamic parameters)
    {
        await Task.Run(() =>
        {
            var psi = new ProcessStartInfo
            {
                FileName = "jupyter",
                Arguments = $"nbconvert --to notebook --execute {notebookPath} --output output.ipynb",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(psi))
            {
                process.WaitForExit();
                if (process.ExitCode != 0)
                {
                    Console.WriteLine($"Error executing notebook: {process.StandardError.ReadToEnd()}");
                }
                else
                {
                    Console.WriteLine("Notebook executed successfully");
                }
            }
        });
    }
}
