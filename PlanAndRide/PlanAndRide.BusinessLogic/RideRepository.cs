using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
                return;
            }

            EditRideName(myRide);
            EditRideDate(myRide);
            EditRideDescription(myRide);
            EditShareRide(myRide);
            EditRideIsPrivate(myRide);
            EditRideRoute(myRide);
            Console.WriteLine("Ride editing is complete");
        }

        private static void EditRideRoute(Ride ride)
        {
            Console.WriteLine($"This ride has assigned route {ride.Route.Name}");
            var isChange = GetNewYesNoValue("Do you want to change it [y/n]?: ");
            if (isChange)
            {
                EditRoute(ride.Route);
            }
        }

        private static void EditRideIsPrivate(Ride ride)
        {
            Console.WriteLine($"Current privacy setting is: {ride.IsPrivate}");
            var isChange = GetNewYesNoValue("Do you want to change it [y/n]?: ");
            if (!isChange)
                return;

            var newIsPrivate = GetNewYesNoValue("Enter new privacy setting [y/n]: ");
            if (ride.IsPrivate != newIsPrivate)
            {
                ride.IsPrivate = newIsPrivate;
                Console.WriteLine($"Privacy setting has been changed. Current setting is: {ride.IsPrivate}");
            }
            else
            {
                Console.WriteLine("New and old value are the same. There is no change..");
            }

        }

        private static void EditShareRide(Ride ride)
        {
            Console.WriteLine($"Current sharing setting is: {ride.ShareRide}");
            var isChange = GetNewYesNoValue("Do you want to change it [y/n]?: ");
            if (!isChange)
                return;

            var newShareRide = GetNewYesNoValue("Enter new sharing setting [y/n]: ");
            if (ride.ShareRide != newShareRide)
            {
                ride.ShareRide = newShareRide;
                Console.WriteLine($"Sharing setting has been changed. Current setting is: {ride.ShareRide}");
            }
            else
            {
                Console.WriteLine("New and old value are the same. There is no change..");
            }
        }

        private static void EditRideDescription(Ride ride)
        {
            Console.WriteLine($"Current description is: {ride.Description}");
            var isChange = GetNewYesNoValue("Do you want to change it [y/n]?: ");
            if (!isChange)
                return;

            var newDesc = GetNewTextValue("Enter new description: ");
            if (newDesc != String.Empty)
            {
                ride.Description = newDesc;
                Console.WriteLine($"Description has been changed. Current description is: {ride.Description}");
            }
            else
            {
                Console.WriteLine("There is no correct description. Description hasn't been changed...");
            }
        }

        private static void EditRideDate(Ride ride)
        {
            Console.WriteLine($"Current date is: {ride.Date}");
            var isChange = GetNewYesNoValue("Do you want to change it [y/n]?: ");
            if (!isChange)
                return;

            var newDate = GetNewDateTime("Enter new date (dd.mm.yyyy gg:mm): ");
            if (newDate != null)
            {
                ride.Date = newDate;
                Console.WriteLine($"Date has been changed. Current date is: {ride.Date}");
            }
            else
            {
                Console.WriteLine("There is no correct date. It hasn't been changed...");
            }
        }

        private static void EditRideName(Ride ride)
        {
            Console.WriteLine($"Current name is: {ride.Name}");
            var isChange = GetNewYesNoValue("Do you want to change it [y/n]?: ");
            if (!isChange)
                return;

            var newName = GetNewTextValue("Enter new name: ");
            if (newName != String.Empty)
            {
                ride.Name = newName;
                Console.WriteLine($"Name has been changed. Current name is: {ride.Name}");
            }
            else
            {
                Console.WriteLine("There is no correct name. Name hasn't been changed...");
            }
        }

        public static void EditRoute(Route route)
        {
            EditRouteName(route);
            EditRouteDescription(route);
            EditShareRoute(route);
            EditRouteIsPrivate(route);
            EditRouteStartingPosition(route);
            EditRouteDestinationPosition(route);

            Console.WriteLine("Route editing is complete");
        }

        private static void EditRouteDestinationPosition(Route route)
        {
            Console.WriteLine($"Current route destination position is: {route.DestinationPosition}");
            var isChange = GetNewYesNoValue("Do you want to change it [y/n]?: ");
            if (!isChange)
                return;

            Console.WriteLine($"Enter new destination position: ");
            var parseSuccess = double.TryParse(Console.ReadLine(), out var newDestinationPosition);
            if (parseSuccess)
            {
                route.DestinationPosition = newDestinationPosition;
                Console.WriteLine(
                    $"Route destination position has been changed. Current value is: {route.DestinationPosition}");
            }
            else
            {
                Console.WriteLine("There is no correct value. It hasn't been changed...");
            }

        }

        private static void EditRouteStartingPosition(Route route)
        {
            Console.WriteLine($"Current route starting position is: {route.StartingPosition}");
            var isChange = GetNewYesNoValue("Do you want to change it [y/n]?: ");

            if (!isChange)
                return;

            Console.WriteLine($"Enter new starting position: ");
            var parseSuccess = double.TryParse(Console.ReadLine(), out var newStartingPosition);
            if (parseSuccess)
            {
                route.StartingPosition = newStartingPosition;
                Console.WriteLine(
                    $"Route starting position has been changed. Current value is: {route.StartingPosition}");
            }
            else
            {
                Console.WriteLine("There is no correct value. It hasn't been changed...");
            }
        }

        private static void EditRouteIsPrivate(Route route)
        {
            Console.WriteLine($"Current route privacy setting is: {route.IsPrivate}");
            var isChange = GetNewYesNoValue("Do you want to change it [y/n]?: ");
            if (!isChange)
                return;

            var newIsPrivate = GetNewYesNoValue("Enter new route privacy setting [y/n]: ");
            if (route.IsPrivate != newIsPrivate)
            {
                route.IsPrivate = newIsPrivate;
                Console.WriteLine($"Route privacy setting has been changed. Current setting is: {route.IsPrivate}");
            }
            else
            {
                Console.WriteLine("New and old value are the same. There is no change..");
            }
        }

        private static void EditShareRoute(Route route)
        {
            Console.WriteLine($"Current route sharing setting is: {route.ShareRoute}");
            var isChange = GetNewYesNoValue("Do you want to change it [y/n]?: ");
            if (!isChange)
                return;

            var newShareRoute = GetNewYesNoValue("Enter new sharing setting [y/n]: ");
            if (route.ShareRoute != newShareRoute)
            {
                route.ShareRoute = newShareRoute;
                Console.WriteLine($"Route sharing setting has been changed. Current setting is: {route.ShareRoute}");
            }
            else
            {
                Console.WriteLine("New and old value are the same. There is no change..");
            }
        }

        private static void EditRouteDescription(Route route)
        {
            Console.WriteLine($"Current route description is: {route.Description}");
            var isChange = GetNewYesNoValue("Do you want to change it [y/n]?: ");
            if (!isChange)
                return;

            var newDesc = GetNewTextValue("Enter new description: ");
            if (newDesc != String.Empty)
            {
                route.Description = newDesc;
                Console.WriteLine($"Description has been changed. Current route description is: {route.Description}");
            }
            else
            {
                Console.WriteLine("There is no correct description. Description hasn't been changed...");
            }
        }

        private static void EditRouteName(Route route)
        {
            Console.WriteLine($"Current route name is: {route.Name}");
            var isChange = GetNewYesNoValue("Do you want to change it [y/n]?: ");
            if (!isChange)
                return;

            var newName = GetNewTextValue("Enter new name: ");
            if (newName != String.Empty)
            {
                route.Name = newName;
                Console.WriteLine($"Name has been changed. Current name is: {route.Name}");
            }
            else
            {
                Console.WriteLine("There is no correct name. Name hasn't been changed...");
            }
        }


        private static string GetNewTextValue(string message)
        {
            Console.WriteLine(message);
            var newValue = Console.ReadLine();
            return !string.IsNullOrWhiteSpace(newValue) ? newValue : String.Empty;

        }

        private static DateTime? GetNewDateTime(string message)
        {
            Console.WriteLine(message);
            var parseSuccess = DateTime.TryParse(Console.ReadLine(), out var newDate);
            return parseSuccess ? newDate : null;
        }

        private static bool GetNewYesNoValue(string message)
        {
            Console.WriteLine(message);
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while (keyInfo.Key != ConsoleKey.Y && keyInfo.Key != ConsoleKey.N)
            {
                Console.WriteLine("Press key 'y' or 'n' to continue...");
                keyInfo = Console.ReadKey(true);
            }

            return keyInfo.Key == ConsoleKey.Y ? true : false;
        }
    }
}
