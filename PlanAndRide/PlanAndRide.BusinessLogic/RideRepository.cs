using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Environment = System.Environment;


namespace PlanAndRide.BusinessLogic
{
    public static class RideRepository
    {
        private static List<Ride> rides = new List<Ride>();

        static RideRepository()
        { 
           var json = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "data.json"));
           rides = JsonConvert.DeserializeObject<List<Ride>>(json);
            
        }

        public static List<Ride> GetAllRides()
        {
            return rides;
        }

        public static void AddRide(Ride ride)
        {
            rides.Add(ride);
        }

        public static void PrintUpdate()
        {
            var json = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "data.json"));

            var loadedRides = JsonConvert.DeserializeObject<List<Ride>>(json);
            Console.WriteLine("Czy chcesz wyświetlić wszystkie dostępne trasy?");
            Console.WriteLine($"Jeśli tak wciśnij 'y'");
            var button= Console.ReadKey();
            
            foreach(var ride in loadedRides)
            {
                Console.WriteLine($"Nazwa przejazdu: {ride.Name}");
                Console.WriteLine($"Data rozpoczęcia przejazdu: {ride.Date}");
                if (ride.IsPrivate == false)
                {
                    Console.WriteLine("Przejazd jest publiczny. Każdy może dołączyć.");
                }
                else
                {
                    Console.WriteLine("Przejazd jest prywatny. Tylko osoby zaproszone mogą dołączyć.");
                }
                Console.WriteLine($"Nazwa trasy: {ride.Route.Name} ");
                Console.WriteLine($"Opis trasy: {ride.Route.Description}");
                
                Console.WriteLine();
                Console.WriteLine("Poniżej zostaną przedstawione opinie dotyczące trasy: ");
                foreach(var rides in ride.Route.Reviews)
                {
                    Console.WriteLine($"Ocena trasy: {rides.Score}");
                    Console.WriteLine($"Opinia: {rides.Description}\n");
                    
                }
                
                Console.WriteLine("-------------------------------------------------");
            }
        }

           
        
    }
}
