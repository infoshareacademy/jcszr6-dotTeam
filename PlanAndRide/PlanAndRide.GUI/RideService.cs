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
            Console.WriteLine("Edycja wydarzenia została zakończona");
        }

        private void UpdateRideRoute(Ride ride)
        {
            Console.WriteLine($"Aktualnie przypisana trasa to: {ride.Route.Name}");
            var isChange = GetNewYesNoValue("Czy chcesz ją zmienić [t/n]?: ");
            if (isChange)
            {
                UpdateRoute(ride.Route);
            }
        }

        private void UpdateRideIsPrivate(Ride ride)
        {
            Console.WriteLine($"Wydarzenie{(ride.IsPrivate ? " " : " nie ")}jest prywatne");
            var isChange = GetNewYesNoValue("Czy chcesz to zmienić [t/n]?: ");
            if (!isChange)
                return;

            ride.IsPrivate = ride.IsPrivate ? false : true;
            Console.WriteLine($"Ustawienia prywatności zostały zmienione");
            Console.WriteLine($"Wydarzenie{(ride.IsPrivate ? " " : " nie ")}jest prywatne");
            Console.WriteLine();
        }

        private void UpdateShareRide(Ride ride)
        {
            Console.WriteLine($"Wydarzenie{(ride.ShareRide ? " " : " nie ")}jest udostępniane");
            var isChange = GetNewYesNoValue("Czy chcesz to zmienić [t/n]?: ");
            if (!isChange)
                return;

            ride.ShareRide = ride.ShareRide ? false : true;
            Console.WriteLine($"Ustawienia udostępniania zostały zmienione");
            Console.WriteLine($"Wydarzenie{(ride.ShareRide ? " " : " nie ")}jest udostępniane");
            Console.WriteLine();
        }

        private void UpdateRideDescription(Ride ride)
        {
            Console.WriteLine($"Aktualny opis to: {ride.Description}");
            var isChange = GetNewYesNoValue("Czy chcesz go zmienić [t/n]?: ");
            if (!isChange)
                return;

            var newDesc = GetNewTextValue("Wprowadź nowy opis: ");
            if (newDesc != String.Empty)
            {
                ride.Description = newDesc;
                Console.WriteLine($"Opis wydarzenia został zmieniony\nAktualny opis to: {ride.Description}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Nie wprowadziłeś poprawnej wartości. Opis pozostaje bez zmian...");
                Console.WriteLine();
            }
        }

        private void UpdateRideDate(Ride ride)
        {
            Console.WriteLine($"Aktualna data wydarzenia to: {ride.Date}");
            var isChange = GetNewYesNoValue("Czy chcesz ją zmienić [t/n]?: ");
            if (!isChange)
                return;

            var newDate = GetNewDateTime("Wprowadź nową datę (dd.mm.yyyy gg:mm): ");
            if (newDate != null)
            {
                ride.Date = newDate;
                Console.WriteLine($"Data została zmieniona\nAktualna data to: {ride.Date}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Nie wprowadziłeś poprawnej wartości. Data pozostaje bez zmian...");
                Console.WriteLine();
            }
        }

        private void UpdateRideName(Ride ride)
        {
            Console.WriteLine($"Aktualna nazwa wydarzenia to: {ride.Name}");
            var isChange = GetNewYesNoValue("Czy chcesz ją zmienić [t/n]?: ");
            if (!isChange)
                return;

            var newName = GetNewTextValue("Wprowadź nową nazwę: ");
            if (newName != String.Empty)
            {
                ride.Name = newName;
                Console.WriteLine($"Nazwa została zmieniona\nAktualna nazwa to: {ride.Name}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Nie wprowadziłeś poprawnej wartości. Nazwa pozostaje bez zmian...");
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

            Console.WriteLine("Edycja trasy została zakończona");
        }

        private void UpdateRouteDestinationPosition(Route route)
        {
            Console.WriteLine($"Aktualna pozycji docelowa to: {route.DestinationPosition}");
            var isChange = GetNewYesNoValue("Czy chcesz ją zmienić [t/n]?: ");
            if (!isChange)
                return;

            Console.WriteLine($"Wprowadź nową wartość [lat lng]: ");
            var enteredValues = Console.ReadLine().Split(' ');
            double[] newDestinationPosition = new double[2];
            var parseSuccess = double.TryParse(enteredValues[0], out newDestinationPosition[0]);
            if (parseSuccess)
            {
                parseSuccess=double.TryParse(enteredValues[1], out newDestinationPosition[1]);
            }
            if (parseSuccess)
            {
                route.DestinationPosition.Latitude = newDestinationPosition[0];
                route.DestinationPosition.Longitude = newDestinationPosition[1];
                Console.WriteLine(
                    $"Pozycja docelowa trasy została zmieniona. Aktualna wartość to: {route.DestinationPosition}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Nie wprowadziłeś poprawnej wartości. Pozycja docelowa pozostaje bez zmian...");
                Console.WriteLine();
            }

        }

        private void UpdateRouteStartingPosition(Route route)
        {
            Console.WriteLine($"Aktualna pozycja startowa to: {route.StartingPosition}");
            var isChange = GetNewYesNoValue("Czy chcesz ją zmienić [t/n]?: ");

            if (!isChange)
                return;

            Console.WriteLine($"Wprowadź nowe wartości[lat lng]: ");
            var enteredValues = Console.ReadLine().Split(' ');
            double[] newStartingPosition = new double[2];
            var parseSuccess = double.TryParse(enteredValues[0], out newStartingPosition[0]);
            if (parseSuccess)
            {
                parseSuccess = double.TryParse(enteredValues[1], out newStartingPosition[1]);
            }
              
            if (parseSuccess)
            {
                route.StartingPosition.Latitude = newStartingPosition[0];
                route.StartingPosition.Longitude = newStartingPosition[1];
                Console.WriteLine(
                    $"Pozycja startowa trasy została zmieniona. Aktualna wartość to: {route.StartingPosition}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Nie wprowadziłeś poprawnej wartości. Pozycja startowa pozostaje bez zmian...");
                Console.WriteLine();
            }
        }

        private void UpdateRouteIsPrivate(Route route)
        {
            Console.WriteLine($"Trasa{(route.IsPrivate ? " " : " nie ")}jest prywatna");
            var isChange = GetNewYesNoValue("Czy chcesz to zmienić [t/n]?: ");
            if (!isChange)
                return;

            route.IsPrivate = route.IsPrivate ? false : true;
            Console.WriteLine($"Ustawienia prywatności zostały zmienione");
            Console.WriteLine($"Trasa{(route.IsPrivate ? " " : " nie ")}jest prywatna");
            Console.WriteLine();
        }

        private void UpdateShareRoute(Route route)
        {
            Console.WriteLine($"Trasa{(route.ShareRoute ? " " : " nie ")}jest udostępniana");
            var isChange = GetNewYesNoValue("Czy chcesz to zmienić [t/n]?: ");
            if (!isChange)
                return;

            route.ShareRoute = route.ShareRoute ? false : true;
            Console.WriteLine($"Ustawienia udostępniania zostały zmienione");
            Console.WriteLine($"Trasa{(route.ShareRoute ? " " : " nie ")}jest udostępniana");
            Console.WriteLine();
        }

        private void UpdateRouteDescription(Route route)
        {
            Console.WriteLine($"Aktualny opis trasy to: {route.Description}");
            var isChange = GetNewYesNoValue("Czy chcesz go zmienić [t/n]?: ");
            if (!isChange)
                return;

            var newDesc = GetNewTextValue("Wprowadź nowy opis: ");
            if (newDesc != String.Empty)
            {
                route.Description = newDesc;
                Console.WriteLine($"Opis trasy został zmieniony.\nAktualny opis to: {route.Description}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Nie wprowadziłeś poprawnej wartości. Opis pozostaje bez zmian...");
                Console.WriteLine();
            }
        }

        private void UpdateRouteName(Route route)
        {
            Console.WriteLine($"Aktualna nazwa trasy to: {route.Name}");
            var isChange = GetNewYesNoValue("Czy chcesz ją zmienić [t/n]?: ");
            if (!isChange)
                return;

            var newName = GetNewTextValue("Wprowadż nową nazwę: ");
            if (newName != String.Empty)
            {
                route.Name = newName;
                Console.WriteLine($"Nazwa trasy została zmieniona.\nAktualna nazwa to: {route.Name}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Nie wprowadziłeś poprawnej wartości. Nazwa pozostaje bez zmian...");
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
            while (keyInfo.Key != ConsoleKey.T && keyInfo.Key != ConsoleKey.N)
            {
                Console.WriteLine("Naciśnij 'T' lub 'N' aby przejść dalej...");
                keyInfo = Console.ReadKey(true);
            }

            return keyInfo.Key == ConsoleKey.T ? true : false;
        }
    }
}
