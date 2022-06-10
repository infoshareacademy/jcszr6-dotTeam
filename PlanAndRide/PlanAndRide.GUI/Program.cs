// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Witaj w Plan&Ride App");
//Console.WriteLine("-------------------------");
//Console.WriteLine("1. Stwórz konto");
//Console.WriteLine("2. Zaloguj się");
//Console.WriteLine("3. Stwórz trasę");
//Console.WriteLine("4. Wyszukaj trasę");
//Console.WriteLine("5. Edytuj trasę");
//Console.WriteLine("6. Usuń trasę");
//Console.WriteLine("7. Udostępnij trasę");
//Console.WriteLine("8. Oceń trasę");
//Console.WriteLine("9. Historia tras");
//Console.WriteLine("10. Stwórz event");
//Console.WriteLine("11. Wyszukaj event");
//Console.WriteLine("12. Wyszukaj event");
//Console.WriteLine("13. Ustawienia powiadomień");
//Console.WriteLine("14. Wyjdź");
//Console.WriteLine("-------------------------");
//Console.WriteLine("");

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Channels;
using Newtonsoft.Json;
using PlanAndRide.BusinessLogic;
using PlanAndRide.GUI;

Console.WriteLine("Witaj w Plan&Ride App");
//Search.AverageGradeRoute();
//Search.SearchByGradeRoute();
//Update.PrintUpdate();
//Search.SearchByReviewDates();
//var rides = RideRepository.GetAllRides();
//RideRepository.AddRide(new Ride());

//Console.WriteLine("-------------------------");
//Console.WriteLine("1. Stwórz konto");
//Console.WriteLine("2. Zaloguj się");
//Console.WriteLine("-------------------------");
//Console.WriteLine("Wybierz opcję:");

//ConsoleKeyInfo keyInfo=Console.ReadKey();
//switch (keyInfo.Key)
//{
//    case ConsoleKey.D1: Console.WriteLine("Tworzymy konto...");break;
//    case ConsoleKey.D2: Console.WriteLine("Witaj zalogowany użytkowniku...");break;
//}


//var rides2 = new List<Ride>()
//{
//    new Ride()
//    {
//        Description = "A test ride",
//        IsPrivate = false,
//        Name = "Test-Ride",
//        RideMembers = new List<User>()
//        {
//            new User()
//            {
//                Login = "Test",
//                Email = "test@mail.com",
//                Password = "dupa1234"
//            }
//        },
//        Route = new Route
//        {
//            Description = "Test Route",
//            DestinationPosition = 10,
//            IsPrivate = false,
//            Name = "Test Route",
//            Reviews = new List<Review>
//            {
//                new Review()
//                {
//                    Description = "Test desc",
//                    Score = 100
//                }
//            },
//            ShareRoute = true,
//            StartingPosition = 25
//        }
//    }
//};

//var jsonString = JsonConvert.SerializeObject(rides);

//var json = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "data.json"));

//var loadedRides = JsonConvert.DeserializeObject<List<Ride>>(json);

//Console.WriteLine();