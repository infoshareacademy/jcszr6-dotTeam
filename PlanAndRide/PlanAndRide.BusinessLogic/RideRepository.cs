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
            //Wyświetlenie wszystkich dostępnych tras zapisanych w bazie danych

            var loadedRides= RideRepository.GetAllRides();
            Console.WriteLine("Wciśnij dowolny klawisz aby wyświetlić wszystkie trasy");
             Console.ReadKey();

            foreach (var ride in loadedRides)
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
                Console.WriteLine($"Średnia ocena trasy: {ride.Route.Score}");

                Console.WriteLine();
                Console.WriteLine("Poniżej zostaną przedstawione opinie dotyczące trasy: ");
                foreach (var rides in ride.Route.Reviews)
                {
                    Console.WriteLine($"Data utworzenia oceny: {rides.Date}");
                    Console.WriteLine($"Ocena trasy: {rides.Score}");
                    Console.WriteLine($"Opinia: {rides.Description}\n");

                }
                Console.WriteLine("-------------------------------------------------");
            }
        }
        public static void AverageGradeRoute()
        {
            var loadedRides = RideRepository.GetAllRides();


            //obliczanie średniej oceny dla wszystkich ocen z recenzji danej trasy

            foreach (var ride in loadedRides)
            {
                List<int> listToSum = new List<int>();
                ride.Route.Reviews.ForEach(r => listToSum.Add(r.Score));
                int sum = listToSum.Sum();
                var averageGrade = sum / listToSum.Count;
                ride.Route.Score = averageGrade;
            }
            var jsonString = JsonConvert.SerializeObject(loadedRides);
        }
        public static DateTime  WriteDate()
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
        public static void SearchByReviewDates()
        {
            
            //Wprowadzanie przez użytkonika danych do szukania.

            Console.WriteLine("Aby rozpocząć szukanie wprowadź dane,aby zacząć szukać");
            DateTime firstDateTimeToSearch = WriteDate();

            Console.WriteLine("Teraz wprowadź dane do którego mamy  szukać (liczbowo)");
            DateTime lastDateTimeToSearch = WriteDate();

            //Szukanie po datach i wyświetlanie wyników

            var loadedRides = RideRepository.GetAllRides();
            var filtredReviews = loadedRides.SelectMany(r => r.Route.Reviews).Where(r => r.Date.Date >= firstDateTimeToSearch.Date && r.Date.Date <= lastDateTimeToSearch.Date).ToList();
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
        public static void SearchByGradeRoute()
        {
            //Wprowadzania przez użytkownika zakres szukania tras po ocenach

            Console.WriteLine("Wprowadź minimalny zakres punktacji (liczbowo)");
            int minValueScore= int.Parse(Console.ReadLine());
            Console.WriteLine("Wprowadź maksymalny zakres punktacji (liczbowo)");
            int maxValueScore= int.Parse(Console.ReadLine());

            //Szukanie po ocenach i wyświetlanie wyników

            var loadedRides = RideRepository.GetAllRides();
            var filtredLoadRides= loadedRides.Where(r=>r.Route.Score >= minValueScore && r.Route.Score <=maxValueScore).ToList();
            if(filtredLoadRides.Count != 0)
            {
                foreach (var ride in filtredLoadRides)
                {
                    Console.WriteLine($"Nazwa trasy: {ride.Route.Name} ");
                    Console.WriteLine($"Opis trasy: {ride.Route.Description}");
                    Console.WriteLine($"Średnia ocena trasy: {ride.Route.Score}\n");
                }
            }else
            {
                Console.WriteLine("Brak wyników.");
            }
        }
    }
}
