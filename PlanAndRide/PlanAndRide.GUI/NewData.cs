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
            User user = new User();
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
            Console.WriteLine("Czy chcesz dodać opis recenzji (wprowadź 't' lub 'n'): ");
            var descriptionYesNo = Console.ReadLine().ToLower().Trim();

            do
            {
                if (descriptionYesNo == "n")
                {
                    Console.WriteLine($"Ocena bez opisu\n");
                    break;
                }
                else if (descriptionYesNo == "t")
                {
                    Console.WriteLine("Wprowadź opis:\n");
                    review.Description = Console.ReadLine();
                    Console.WriteLine("\nDodano opis recenzji.\n");
                    break;
                }
                else
                {
                    Console.WriteLine("\nWprowadź 't' lub 'n': ");
                    descriptionYesNo = Console.ReadLine().ToLower().Trim();
                }
            } while (descriptionYesNo != null);
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
            Console.WriteLine("Czy chcesz żeby przejazd był prywatny (tak lub nie): ");
            var isPrivateYesNo = Console.ReadLine().ToLower().Trim();

            do
            {
                if (isPrivateYesNo == "n" || isPrivateYesNo == "nie")
                {
                    ride.IsPrivate = false;
                    Console.WriteLine($"Przejazd będzie publiczny\n");
                    break;
                }
                else if (isPrivateYesNo == "t" || isPrivateYesNo == "tak")
                {
                    ride.IsPrivate = true;
                    Console.WriteLine("Przejazd będzie prywatny\n");
                    break;
                }
                else
                {
                    Console.WriteLine("\nCzy chcesz żeby przejazd był prywatny (tak lub nie): ");
                    isPrivateYesNo = Console.ReadLine().ToLower().Trim();
                }
            } while (isPrivateYesNo != null);
        }

        private void NewRideShareRide(Ride ride)
        {
            Console.WriteLine("Czy chcesz współdzielić przejazd z innymi użytkownikami (tak lub nie): ");
            var shareRideYesNo = Console.ReadLine().ToLower().Trim();

            do
            {
                if (shareRideYesNo == "n" || shareRideYesNo == "nie")
                {
                    ride.ShareRide = false;
                    Console.WriteLine($"Przejazd nie będzie współdzielony z innymi użytkownikami\n");
                    break;
                }
                else if (shareRideYesNo == "t" || shareRideYesNo == "tak")
                {
                    ride.ShareRide = true;
                    Console.WriteLine("Przejazd będzie współdzielony z innymi użytkownikami\n");
                    break;
                }
                else
                {
                    Console.WriteLine("\nCzy chcesz współdzielić przejazd z innymi użytkownikami (tak lub nie): ");
                    shareRideYesNo = Console.ReadLine().ToLower().Trim();
                }
            } while (shareRideYesNo != null);
        }

        private void NewRideDescription(Ride ride)
        {
            Console.WriteLine("Czy chcesz dodać opis do przejazdu (wprowadź 't' lub 'n': ");
            var descriptionYesNo = Console.ReadLine().ToLower().Trim();

            do
            {
                if (descriptionYesNo == "n" || descriptionYesNo == "nie")
                {
                    Console.WriteLine($"Przejazd bez opisu\n");
                    break;
                }
                else if (descriptionYesNo == "t" || descriptionYesNo == "tak")
                {
                    Console.WriteLine("Wprowadź opis przejazdu:\n");
                    ride.Description = Console.ReadLine();
                    Console.WriteLine("\nDodano opis przejazdu.\n");
                    break;
                }
                else
                {
                    Console.WriteLine("\nWprowadź 't' lub 'n': ");
                    descriptionYesNo = Console.ReadLine().ToLower().Trim();
                }
            } while (descriptionYesNo != null);
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
            Console.WriteLine("Czy chcesz żeby trasa był prywatna (tak lub nie): ");
            var isPrivateYesNo = Console.ReadLine().ToLower().Trim();

            do
            {
                if (isPrivateYesNo == "n" || isPrivateYesNo == "nie")
                {
                    route.IsPrivate = false;
                    Console.WriteLine($"Trasa będzie publiczna\n");
                    break;
                }
                else if (isPrivateYesNo == "t" || isPrivateYesNo == "tak")
                {
                    route.IsPrivate = true;
                    Console.WriteLine("Trasa będzie prywatna\n");
                    break;
                }
                else
                {
                    Console.WriteLine("\nCzy chcesz żeby trasa był prywatna (tak lub nie): ");
                    isPrivateYesNo = Console.ReadLine().ToLower().Trim();
                }
            } while (isPrivateYesNo != null);
        }

        private void NewRouteShare(Route route)
        {
            Console.WriteLine("Czy chcesz współdzielić przejazd z innymi użytkownikami (tak lub nie): ");
            var shareRouteYesNo = Console.ReadLine().ToLower().Trim();

            do
            {
                if (shareRouteYesNo == "n" || shareRouteYesNo == "nie")
                {
                    route.ShareRoute = false;
                    Console.WriteLine($"Przejazd nie będzie współdzielony z innymi użytkownikami\n");
                    break;
                }
                else if (shareRouteYesNo == "t" || shareRouteYesNo == "tak")
                {
                    route.ShareRoute = true;
                    Console.WriteLine("Przejazd będzie współdzielony z innymi użytkownikami\n");
                    break;
                }
                else
                {
                    Console.WriteLine("\nCzy chcesz współdzielić przejazd z innymi użytkownikami (tak lub nie): ");
                    shareRouteYesNo = Console.ReadLine().ToLower().Trim();
                }
            } while (shareRouteYesNo != null);
        }

        private void NewRouteDescription(Route route)
        {
            Console.WriteLine("Czy chcesz dodać opis trasy (wprowadź 't' lub 'n': ");
            var descriptionYesNo = Console.ReadLine().ToLower().Trim();

            do
            {
                if (descriptionYesNo == "n" || descriptionYesNo == "nie")
                {
                    Console.WriteLine($"Trasa bez opisu\n");
                    break;
                }
                else if (descriptionYesNo == "t" || descriptionYesNo == "tak")
                {
                    Console.WriteLine("Wprowadź opis trasy:\n");
                    route.Description = Console.ReadLine().Trim();
                    Console.WriteLine("\nDodano opis trasy.\n");
                    break;
                }
                else
                {
                    Console.WriteLine("\nWprowadź 't' lub 'n': ");
                    descriptionYesNo = Console.ReadLine().ToLower().Trim();
                }
            } while (descriptionYesNo != null);
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

        private void NewUserLogin(User user)
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

        private void NewUserPassword(User user)
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

        private void NewUserMail(User user)
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
        public static bool ValidateEmail(string Email)
        {
            var regex = @"[a-z A-Z 0-9_\-]+[@]+[a-z 0-9]+[\.]+[a-z]{2,4}$";
            bool result = Regex.IsMatch(Email, regex, RegexOptions.IgnoreCase);
            return result;
        }
    }
}
