using ElevatorConsoleApp.Models;
using ElevatorConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ElevatorConsoleApp
{
    public static class Program
    {
        static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try
            {
                services.GetRequiredService<App>().Run(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            IHostBuilder CreateHostBuilder(string[] strings)
            {
                return Host.CreateDefaultBuilder()
                    .ConfigureServices((_, services) =>
                    {
                        services.AddSingleton<IElevatorProssesor, ElevatorProssesor>();
                        services.AddSingleton<App>();
                    });
            }
        }
    }

    public class App
    {
        private readonly IElevatorProssesor _elevatorProssesor;

        public App(IElevatorProssesor elevatorProssesor)
        {
            _elevatorProssesor = elevatorProssesor;
        }

        public void Run(string[] args)
        {
            // External Elevator Call with floor number and intended direction
            Console.WriteLine($"Wellcome to Alois Elevators");
            Console.WriteLine($"Enter desired number of Elevators to be generated");
            int numOfElevators = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Enter number of floors available");
            int maxNumOfFloors = Convert.ToInt32(Console.ReadLine());            
            var elevators = _elevatorProssesor.CreateRandomElevators(numOfElevators, maxNumOfFloors);
            var asJson = JsonConvert.SerializeObject(elevators);
            foreach(var elevtor in elevators) 
            {
                _elevatorProssesor.PrintElevatorDetails(elevtor);
            }

            Console.WriteLine($"Enter requesting floor");
            int requestFoor = Convert.ToInt32(Console.ReadLine());

            while( requestFoor > maxNumOfFloors) 
            {
                Console.WriteLine("invalid enty try again, Enter requesting floor");
                requestFoor = Convert.ToInt32(Console.ReadLine());
            }
            
            Console.WriteLine($"Enter number of people waiting floor");
            int numOfPplWaiting = Convert.ToInt32(Console.ReadLine());
            
            var elevator = _elevatorProssesor.CallElevator(elevators, requestFoor, numOfPplWaiting);           

            //Display Elevator as it moves to the requested floor
            _elevatorProssesor.MoveElevator(elevator, requestFoor);

            _elevatorProssesor.OpenDoor(elevator);

            Console.ReadKey();
        }
    }
}