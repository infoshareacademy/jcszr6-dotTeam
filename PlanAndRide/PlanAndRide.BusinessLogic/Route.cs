using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;


namespace PlanAndRide.BusinessLogic
{
    public class Route
    {
        public string Name { get; set; }
        public double StartingPosition { get; set; }
        public double DestinationPosition { get; set; }
        public string Description { get; set; }
        public bool ShareRoute { get; set; }
        public bool IsPrivate { get; set; }
        public List<Review> Reviews { get; set; }

        public void NewRoute()
        {
            //nazwa trasy + sprawdzenie jej wprowadzenia

            Console.WriteLine("route new route");
            Console.WriteLine("Stwórzmy trasę!\n");
            Console.WriteLine("Nazwa nowej trasy:");
            Name = Console.ReadLine().Trim();

            while (Name == "")
            {
                Console.WriteLine("Wprowadź nazwę nowej trasy:");
                Name = Console.ReadLine();
            }

            Console.WriteLine($"\nNazwa nowej trasy to: {Name}\n");

            //opis trasy

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
                    Description = Console.ReadLine();
                    Console.WriteLine("\nDodano opis trasy.\n");
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
            var shareRouteYesNo = Console.ReadLine().ToLower().Trim();

            do
            {
                if (shareRouteYesNo == "n" || shareRouteYesNo == "nie")
                {
                    ShareRoute = false;
                    Console.WriteLine($"Przejazd nie będzie współdzielony z innymi użytkownikami\n");
                    break;
                }
                else if (shareRouteYesNo == "t" || shareRouteYesNo == "tak")
                {
                    ShareRoute = true;
                    Console.WriteLine("Przejazd będzie współdzielony z innymi użytkownikami\n");
                    break;
                }
                else
                {
                    Console.WriteLine("\nCzy chcesz współdzielić przejazd z innymi użytkownikami (tak lub nie): ");
                    shareRouteYesNo = Console.ReadLine().ToLower().Trim();
                }
            } while (shareRouteYesNo != null);

            // wybór prywatności

            Console.WriteLine("Czy chcesz żeby trasa był prywatna (tak lub nie): ");
            var isPrivateYesNo = Console.ReadLine().ToLower().Trim();

            do
            {
                if (isPrivateYesNo == "n" || isPrivateYesNo == "nie")
                {
                    IsPrivate = false;
                    Console.WriteLine($"Trasa będzie publiczna\n");
                    break;
                }
                else if (isPrivateYesNo == "t" || isPrivateYesNo == "tak")
                {
                    IsPrivate = true;
                    Console.WriteLine("Trasa będzie prywatna\n");
                    break;
                }
                else
                {
                    Console.WriteLine("\nCzy chcesz żeby trasa był prywatna (tak lub nie): ");
                    isPrivateYesNo = Console.ReadLine().ToLower().Trim();
                }
            } while (isPrivateYesNo != null);

            //punkt startowy

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

            //punkt docelowy

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

    }
}
