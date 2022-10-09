using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanAndRide.BusinessLogic;
using System.Text.RegularExpressions;


namespace PlanAndRide.GUI
{
    internal class NewData
    {
        public void NewUser()
        {
            ApplicationUser user = new ApplicationUser();
            NewUserLogin(user);
            NewUserPassword(user);
            NewUserMail(user);
        }

        public void NewRoute()
        {
            Route route = new Route();
            NewRouteName(route);
            NewRouteDescription(route);
            NewRouteShare(route);
            NewRouteIsPrivate(route);
            NewRouteStartingPosition(route);
            NewRouteDestinationPosition(route);
        }

        public void NewRide()
        {
            Ride ride = new Ride();
            NewRideName(ride);
            NewRideDate(ride);
            //NewRideAddRoute(ride);
            NewRideDescription(ride);
            NewRideShareRide(ride);
            NewRideIsPrivate(ride);
        }

        public void NewReview()
        {
            Review review = new Review();
            NewReviewMark(review);
            NewReviewDescription(review);

        }

        private void NewReviewDescription(Review review)
        {
            var checkValue = GetYesNoValue("Czy chcesz dodać opis recenzji (wprowadź 't' lub 'n'): ");
            if (!checkValue)
            {
                Console.WriteLine("\nBez opisu");
                return;
            }
            Console.WriteLine("Wprowadź opis:\n");
            review.Description = Console.ReadLine();
            Console.WriteLine("\nDodano opis recenzji.\n");
        }

        private void NewReviewMark(Review review)
        {

            Console.WriteLine("Stórzmy recenzję!\n");
            Console.WriteLine("Skala: 1-5;");
            Console.WriteLine("1 - bardzo słaba, 5 - rewelacja");
            var newScore = Console.ReadLine().Trim();

            while (newScore == null || !int.TryParse(newScore, out int Score) || Score > 5 || Score <= 0)
            {
                Console.Write("Wprowadź ocenę 1-5:");
                newScore = Console.ReadLine();
                Console.WriteLine("");
            }

            review.Score = int.Parse(newScore);

            Console.WriteLine($"\nTwoja ocenta to: {review.Score}\n");
        }

        private void NewRideIsPrivate(Ride ride)
        {
            var checkValue = GetYesNoValue("Czy chcesz żeby przejazd był prywatny (tak lub nie): ");
            if (!checkValue)
            {
                Console.WriteLine("Przejazd będzie publiczny\n");
                ride.IsPrivate = false;
                return;
            }
            Console.WriteLine("Przejazd będzie prywatny\n");
            ride.IsPrivate = true;
        }

        private void NewRideShareRide(Ride ride)
        {
            var checkValue = GetYesNoValue("Czy chcesz współdzielić przejazd z innymi użytkownikami (tak lub nie): ");
            if (!checkValue)
            {
                Console.WriteLine("Przejazd nie będzie współdzielony z innymi użytkownikami\n");
                ride.ShareRide = false;
                return;
            }
            Console.WriteLine("Przejazd będzie współdzielony z innymi użytkownikami\n");
            ride.ShareRide = true;
        }

        private void NewRideDescription(Ride ride)
        {
            var checkValue = GetYesNoValue("Czy chcesz dodać opis do przejazdu(wprowadź 't' lub 'n': ");
            if (!checkValue)
            {
                Console.WriteLine("\nBez opisu");
                return;
            }
            Console.WriteLine("Wprowadź opis:\n");
            ride.Description = Console.ReadLine();
            Console.WriteLine("\nDodano opis przejazdu.\n");
        }

        //private void NewRideAddRoute(Ride ride)
        //{ ***** dodać możliwość dodania stworzonej już trasy ******
        //    throw new NotImplementedException();
        //}

        private void NewRideDate(Ride ride)
        {
            Console.WriteLine("Data i czas przejazdu (dd.mm.yyyy gg:mm): ");

            if (DateTime.TryParse(Console.ReadLine(), out DateTime Date))
            {
                Console.WriteLine($"\nWprowadzona data i czas przejazdu to: {Date}\n");
            }
            else
            {
                Console.WriteLine("Coś poszło nie tak. Wprowadź datę i czas przejazdu w formacie dd.mm.yyyy gg:mm :");

                while (!DateTime.TryParse(Console.ReadLine(), out Date))
                {
                    Console.Write("Coś poszło nie tak. Wprowadź datę i czas przejazdu w formacie dd.mm.yyyy gg:mm :");
                }
                Console.WriteLine($"\nWprowadzona data i czas przejazdu to: {Date}\n");
            }
        }

        private void NewRideName(Ride ride)
        {
            Console.WriteLine("Stwórzmy przejazd!\n");
            Console.WriteLine("Nazwa nowego przejazdu:");
            ride.Name = Console.ReadLine().Trim();

            while (ride.Name == String.Empty)
            {
                Console.WriteLine("Wprowadź nazwę nowego przejazdu:");
                ride.Name = Console.ReadLine().Trim();
            }

            Console.WriteLine($"\nNazwa nowego przejazdu to: {ride.Name}\n");
        }

        private void NewRouteDestinationPosition(Route route)
        {
            Console.WriteLine("Punkt docelowy: ");
            if (double.TryParse(Console.ReadLine(), out double DestinationPosition))
            {
                Console.WriteLine($"\nWprowadzony punkt docelowy to: {DestinationPosition}\n");
            }
            else
            {
                Console.WriteLine("Coś poszło nie tak. Wprowadź punkt docelowy:");

                while (!double.TryParse(Console.ReadLine(), out DestinationPosition))
                {
                    Console.Write("Coś poszło nie tak. Wprowadź punkt docelowy:");
                }
                Console.WriteLine($"\nWprowadzony punkt docelowy to: {DestinationPosition}\n");
            }
        }

        private void NewRouteStartingPosition(Route route)
        {
            Console.WriteLine("Punkt startowy: ");
            if (double.TryParse(Console.ReadLine(), out double StartingPosition))
            {
                Console.WriteLine($"\nWprowadzony punkt startowy to: {StartingPosition}\n");
            }
            else
            {
                Console.WriteLine("Coś poszło nie tak. Wprowadź punkt startowy:");

                while (!double.TryParse(Console.ReadLine(), out StartingPosition))
                {
                    Console.Write("Coś poszło nie tak. Wprowadź punkt startowy:");
                }
                Console.WriteLine($"\nWprowadzony punkt startowy to: {StartingPosition}\n");
            }
        }

        private void NewRouteIsPrivate(Route route)
        {
            var checkValue = GetYesNoValue("Czy chcesz żeby przejazd był prywatny (tak lub nie): ");
            if (!checkValue)
            {
                Console.WriteLine("Trasa będzie publiczna\n");
                route.IsPrivate = false;
                return;
            }
            Console.WriteLine("Trasa będzie prywatna\n");
            route.IsPrivate = true;
        }

        private void NewRouteShare(Route route)
        {
            var checkValue = GetYesNoValue("Czy chcesz współdzielić przejazd z innymi użytkownikami (tak lub nie): ");
            if (!checkValue)
            {
                Console.WriteLine("Przejazd nie będzie współdzielony z innymi użytkownikami\n");
                route.ShareRoute = false;
                return;
            }
            Console.WriteLine("Przejazd będzie współdzielony z innymi użytkownikami\n");
            route.ShareRoute = true;
        }

        private void NewRouteDescription(Route route)
        {
            var checkValue = GetYesNoValue("Czy chcesz dodać opis do przejazdu(wprowadź 't' lub 'n': ");
            if (!checkValue)
            {
                Console.WriteLine("Trasa bez opisu\n");
                return;
            }
            Console.WriteLine("Wprowadź opis:\n");
            route.Description = Console.ReadLine().Trim();
            Console.WriteLine("\nDodano opis trasy.\n");
        }

        private void NewRouteName(Route route)
        {
            Console.WriteLine("Stwórzmy trasę!\n");
            Console.WriteLine("Nazwa nowej trasy:");
            route.Name = Console.ReadLine().Trim();

            while (route.Name == String.Empty)
            {
                Console.WriteLine("Wprowadź nazwę nowej trasy:");
                route.Name = Console.ReadLine().Trim();
            }

            Console.WriteLine($"\nNazwa nowej trasy to: {route.Name}\n");
        }

        private void NewUserLogin(ApplicationUser user)
        {
            Console.WriteLine("Podaj login:");
            user.Login = Console.ReadLine().Trim();

            while (user.Login == String.Empty)
            {
                Console.WriteLine("Wprowadź login:");
                user.Login = Console.ReadLine().Trim();
            }

            Console.WriteLine($"\nLogin nowego konta to: {user.Login}\n");
        }

        private void NewUserPassword(ApplicationUser user)
        {
            Console.WriteLine("Stwórz hasło:");
            user.Password = Console.ReadLine().Trim();

            while (user.Password == String.Empty)
            {
                Console.WriteLine("Stwórz hasło:");
                user.Password = Console.ReadLine().Trim();
            }

            Console.WriteLine($"\nStworzono hasło!\n");
        }

        private void NewUserMail(ApplicationUser user)
        {
            Console.WriteLine("Wprowadź adres email:");
            user.Email = Console.ReadLine().Trim();

            while (!ValidateEmail(user.Email))
            {
                Console.WriteLine("Wprowadź email:");
                user.Email = Console.ReadLine();
            }

            Console.WriteLine($"\nEmail nowego konta to: {user.Email}\n");
        }
        private bool ValidateEmail(string Email)
        {
            var regex = @"[a-z A-Z 0-9_\-]+[@]+[a-z 0-9]+[\.]+[a-z]{2,4}$";
            bool result = Regex.IsMatch(Email, regex, RegexOptions.IgnoreCase);
            return result;
        }

        private bool GetYesNoValue(string message)
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
