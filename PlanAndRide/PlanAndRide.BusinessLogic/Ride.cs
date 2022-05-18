using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.BusinessLogic
{
    public class Ride
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        // do not change in edit
        public List<User> RideMembers;
        public Route Route { get; set; }
        public string Description { get; set; }
        public bool ShareRide { get; set; }
        public bool IsPrivate { get; set; }

        public void NewRide()
        {
            //nazwa przejazdu + sprawdzenie jej wprowadzenia

            Console.WriteLine("Stwórzmy przejazd!\n");
            Console.WriteLine("Nazwa nowego przejazdu:");
            Name = Console.ReadLine().Trim();

            while (Name == "")
            {
                Console.WriteLine("Wprowadź nazwę nowego przejazdu:");
                Name = Console.ReadLine();
            }

            Console.WriteLine($"\nNazwa nowego przejazdu to: {Name}\n");

            //data przejazdu + sprawdzenie poprawności daty

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

            //opis przejazdu

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
                    Description = Console.ReadLine();
                    Console.WriteLine("\nDodano opis przejazdu.\n");
                    break;
                }
                else
                {
                    Console.WriteLine("\nWprowadź 't' lub 'n': ");
                    descriptionYesNo = Console.ReadLine().ToLower().Trim();
                }
            } while (descriptionYesNo != null);

            //sharowanie przejazdu

            Console.WriteLine("Czy chcesz współdzielić przejazd z innymi użytkownikami (tak lub nie): ");
            var shareRideYesNo = Console.ReadLine().ToLower().Trim();

            do
            {
                if (shareRideYesNo == "n" || shareRideYesNo == "nie")
                {
                    ShareRide = false;
                    Console.WriteLine($"Przejazd nie będzie współdzielony z innymi użytkownikami\n");
                    break;
                }
                else if (shareRideYesNo == "t" || shareRideYesNo == "tak")
                {
                    ShareRide = true;
                    Console.WriteLine("Przejazd będzie współdzielony z innymi użytkownikami\n");
                    break;
                }
                else
                {
                    Console.WriteLine("\nCzy chcesz współdzielić przejazd z innymi użytkownikami (tak lub nie): ");
                    shareRideYesNo = Console.ReadLine().ToLower().Trim();
                }
            } while (shareRideYesNo != null);

            // wybór prywatności

            Console.WriteLine("Czy chcesz żeby przejazd był prywatny (tak lub nie): ");
            var isPrivateYesNo = Console.ReadLine().ToLower().Trim();

            do
            {
                if (isPrivateYesNo == "n" || isPrivateYesNo == "nie")
                {
                    IsPrivate = false;
                    Console.WriteLine($"Przejazd będzie publiczny\n");
                    break;
                }
                else if (isPrivateYesNo == "t" || isPrivateYesNo == "tak")
                {
                    IsPrivate = true;
                    Console.WriteLine("Przejazd będzie prywatny\n");
                    break;
                }
                else
                {
                    Console.WriteLine("\nCzy chcesz żeby przejazd był prywatny (tak lub nie): ");
                    isPrivateYesNo = Console.ReadLine().ToLower().Trim();
                }
            } while (isPrivateYesNo != null);

            // dodanie trasy do przejazdu

            Console.WriteLine("Dodaj trasę do przejazdu");
            Route route = new Route();
            route.NewRoute();
        }
    }
}