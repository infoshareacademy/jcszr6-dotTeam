using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanAndRide.BusinessLogic;

namespace PlanAndRide.GUI
{
    internal class RideService
    {
        public void UpdateRide(Ride ride)
        {
            //var myRide = rides.FirstOrDefault(r => r.Name == rideName);
            //if (myRide == null)
            //{
            //    Console.WriteLine($"No ride with name {rideName} has been found.");
            //    return;
            //}

            UpdateRideName(ride);
            UpdateRideDate(ride);
            UpdateRideDescription(ride);
            UpdateShareRide(ride);
            UpdateRideIsPrivate(ride);
            UpdateRideRoute(ride);
            Console.WriteLine("Ride updating is complete");
        }

        private void UpdateRideRoute(Ride ride)
        {
            Console.WriteLine($"This ride has assigned route {ride.Route.Name}");
            var isChange = GetNewYesNoValue("Do you want to change it [y/n]?: ");
            if (isChange)
            {
                UpdateRoute(ride.Route);
            }
        }

        private void UpdateRideIsPrivate(Ride ride)
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
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("New and old value are the same. There is no change..");
                Console.WriteLine();
            }

        }

        private void UpdateShareRide(Ride ride)
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
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("New and old value are the same. There is no change..");
                Console.WriteLine();
            }
        }

        private void UpdateRideDescription(Ride ride)
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
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("There is no correct description. Description hasn't been changed...");
                Console.WriteLine();
            }
        }

        private void UpdateRideDate(Ride ride)
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
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("There is no correct date. It hasn't been changed...");
                Console.WriteLine();
            }
        }

        private void UpdateRideName(Ride ride)
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
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("There is no correct name. Name hasn't been changed...");
                Console.WriteLine();
            }
        }

        private void UpdateRoute(Route route)
        {
            UpdateRouteName(route);
            UpdateRouteDescription(route);
            UpdateShareRoute(route);
            UpdateRouteIsPrivate(route);
            UpdateRouteStartingPosition(route);
            UpdateRouteDestinationPosition(route);

            Console.WriteLine("Route updating is complete");
        }

        private void UpdateRouteDestinationPosition(Route route)
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
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("There is no correct value. It hasn't been changed...");
                Console.WriteLine();
            }

        }

        private void UpdateRouteStartingPosition(Route route)
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
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("There is no correct value. It hasn't been changed...");
                Console.WriteLine();
            }
        }

        private void UpdateRouteIsPrivate(Route route)
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
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("New and old value are the same. There is no change..");
                Console.WriteLine();
            }
        }

        private void UpdateShareRoute(Route route)
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
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("New and old value are the same. There is no change..");
                Console.WriteLine();
            }
        }

        private void UpdateRouteDescription(Route route)
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
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("There is no correct description. Description hasn't been changed...");
                Console.WriteLine();
            }
        }

        private void UpdateRouteName(Route route)
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
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("There is no correct name. Name hasn't been changed...");
                Console.WriteLine();
            }
        }


        private string GetNewTextValue(string message)
        {
            Console.WriteLine(message);
            var newValue = Console.ReadLine();
            return !string.IsNullOrWhiteSpace(newValue) ? newValue : String.Empty;

        }

        private DateTime? GetNewDateTime(string message)
        {
            Console.WriteLine(message);
            var parseSuccess = DateTime.TryParse(Console.ReadLine(), out var newDate);
            return parseSuccess ? newDate : null;
        }

        private bool GetNewYesNoValue(string message)
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
