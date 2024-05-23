// See https://aka.ms/new-console-template for more information
using ChessKeypad.App;
using ChessKeypad.App.Keypads;
using ChessKeypad.App.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        // Apply the config to the logger
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        using IHost host = Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureServices(services =>
            {
                services.AddTransient<KeyBase, PhoneKey>();
                services.AddTransient<IValidate, NumberValidator>();
                services.AddTransient<IValidate, StartValidator>();
                services.AddTransient<KeyNavigator>();
            })
            .Build();


        var keyPad = host.Services.GetRequiredService<KeyBase>();

        //get the chess pieces
        var availableChessPieces = new ChessPieceCreator().GetAvailableChessPieces();

        //create dictionary with key chess piece name 
        var dict = availableChessPieces.ToDictionary(p => p.Name.ToLower(), p => p);

        //chess piecce for display
        var availableChessPiecesString = string.Join("\n", dict.Keys.Select(x => $"-  {x}").OrderBy(x => x).ToArray());

        var navigate = host.Services.GetRequiredService<KeyNavigator>();

        Console.WriteLine("Please enter number of digits between 2 and 10.");

        var defaultLength = 7;

        while (true)
        {
            var input = (Console.ReadLine() ?? "").Trim();

            if (int.TryParse(input, out var length) && length > 1 && length <= 10)
            {
                defaultLength = length;
                break;
            }
            Console.WriteLine("Please enter a valid number between 2 and 10.");
        }

        while (true)
        {
            var prompt = $"\nPlease select one of the following Piece Types or enter q to exit\n";

            prompt += availableChessPiecesString + Environment.NewLine;

            Console.WriteLine(prompt);

            var input = (Console.ReadLine() ?? "").Trim().ToLower();

            if (string.Equals(input, "q"))
            {
                Console.WriteLine("You have requested to exit the application. The application is shutting down...");
                break;
            }
            else if (dict.ContainsKey(input))
            {
                try
                {
                    Console.WriteLine("Processing....");
                    var results = navigate.Explore(dict[input], defaultLength);

                    Console.WriteLine($"Total valid results {results:n0} ");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to explore keypad {ex}");

                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please input from the listed chess piece and try again.");
            }

        }
    }
}