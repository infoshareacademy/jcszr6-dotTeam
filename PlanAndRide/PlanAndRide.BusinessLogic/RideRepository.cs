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

        public static void EditRide(string rideName)
        {
            var myRide = rides.FirstOrDefault(r => r.Name == rideName);
            if (myRide == null)
            {
                Console.WriteLine($"No ride with name {rideName} has been found.");
            }

            Console.WriteLine($"Current name is: {myRide.Name}");
            Console.WriteLine($"Enter new name: ");
            var newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                myRide.Name = newName;
            }
        }
    }
}
