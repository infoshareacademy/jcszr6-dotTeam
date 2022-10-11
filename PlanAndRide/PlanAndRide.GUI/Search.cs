using PlanAndRide.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.GUI
{
    public class Search
    {
      private readonly IRideService _rideService;
      private readonly IRouteService _routeService;
        public Search(IRideService rideService, IRouteService routeService)
        {
            _rideService=rideService;
            _routeService=routeService;
        }
        public DateTime WriteDate()
        {
            //wprowadzanie przez użytwkownika daty 

            Console.WriteLine("Wprowadź rok (liczbowo)");
            var YearSearch = Console.ReadLine();
            Console.WriteLine($"Natępnie wprowadź miesiąc (liczbowo, pamietaj aby poniżej 10 wpisać z przodu '0'");
            var MonthSearch = Console.ReadLine();
            Console.WriteLine("Teraz wprowadź dzień (liczbowo, pamietaj aby poniżej 10 wpisać z przodu '0')");
            var DaySearch = Console.ReadLine();
            var DateSearch = $"{DaySearch}/{MonthSearch}/{YearSearch}";

            //przekształcanie daty ze string do DateTime

            DateTime dateTimeToSearch;
            try
            {
                dateTimeToSearch = DateTime.ParseExact(DateSearch, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                Console.WriteLine($"{dateTimeToSearch:dd/MM/yyyy} o to jeden z parametów, który posłuży do szkuania");
                return dateTimeToSearch;
            }
            catch (Exception Datetime)
            {
                Console.WriteLine($"{DateSearch} błednie wpisane dane");
                return WriteDate();
            }

        }
        public async Task SearchByReviewDates()
        {

            //Wprowadzanie przez użytkonika danych do szukania.

            Console.WriteLine("Aby rozpocząć szukanie wprowadź dane,aby zacząć szukać");
            DateTime firstDateTimeToSearch = WriteDate();

            Console.WriteLine("Teraz wprowadź dane do którego mamy  szukać (liczbowo)");
            DateTime lastDateTimeToSearch = WriteDate();

            //Szukanie po datach i wyświetlanie wyników

            var loadedRides = await _routeService.GetAll();
            var filtredReviews = loadedRides.SelectMany(r => r.Reviews).Where(r => r.Date.Date >= firstDateTimeToSearch.Date && r.Date.Date <= lastDateTimeToSearch.Date).ToList();
            if (filtredReviews.Count != 0)
            {
                foreach (var filtredReview in filtredReviews)
                {
                    Console.WriteLine($"Data:{filtredReview.Date}");
                    Console.WriteLine($"Ocena:{filtredReview.Score}");
                    Console.WriteLine($"Opis: {filtredReview.Description}\n");
                }
            }
            else
            {
                Console.WriteLine("Brak wyników");
            }

        }
        public async Task SearchByGradeRoute()
        {
            //Wprowadzania przez użytkownika zakres szukania tras po ocenach

            Console.WriteLine("Wprowadź minimalny zakres punktacji (liczbowo)");
            int minValueScore = int.Parse(Console.ReadLine());
            Console.WriteLine("Wprowadź maksymalny zakres punktacji (liczbowo)");
            int maxValueScore = int.Parse(Console.ReadLine());

            //Szukanie po ocenach i wyświetlanie wyników

            var loadedRides = await _rideService.GetAll();
            var filtredLoadRides = loadedRides.Where(r=>_routeService.AverageScore(r.Route) >= minValueScore && _routeService.AverageScore(r.Route) <= maxValueScore).ToList();
            if (filtredLoadRides.Count != 0)
            {
                foreach (var ride in filtredLoadRides)
                {
                    Console.WriteLine($"Nazwa trasy: {ride.Route.Name} ");
                    Console.WriteLine($"Opis trasy: {ride.Route.Description}");
                    Console.WriteLine($"Średnia ocena trasy: {_routeService.AverageScore(ride.Route)}\n");
                }
            }
            else
            {
                Console.WriteLine("Brak wyników.");
            }
        }

        public async Task SearchByName()
        {
            Console.WriteLine("Wprowadź nazwę:");
            var searchName = Console.ReadLine();

            while (searchName == String.Empty)
            {
                Console.WriteLine("Nic nie zostało wpisane. Wprowadź nazwę:");
                searchName = Console.ReadLine();
            }

            var loadedRides = await _rideService.GetAll();
            var filtredLoadRides = loadedRides.Where(r => r.Name == searchName);
            if (filtredLoadRides.Count() != 0)
            {
                foreach (var ride in filtredLoadRides)
                {
                    Console.WriteLine($"Nazwa trasy: {ride.Route.Name} ");
                    Console.WriteLine($"Opis trasy: {ride.Route.Description}");
                    Console.WriteLine($"Średnia ocena trasy: {_routeService.AverageScore(ride.Route)}\n");
                }
            }
            else
            {
                Console.WriteLine("Brak wyników.");
            }
        }
    }
}
