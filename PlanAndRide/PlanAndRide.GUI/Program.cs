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
using System.Threading.Channels;
/*
Console.WriteLine("Witaj w Plan&Ride App");
Console.WriteLine("-------------------------");
Console.WriteLine("1. Stwórz konto");
Console.WriteLine("2. Zaloguj się");
Console.WriteLine("-------------------------");
Console.WriteLine("Wybierz opcję:");

ConsoleKeyInfo keyInfo=Console.ReadKey();
switch (keyInfo.Key)
{
    case ConsoleKey.D1: Console.WriteLine("Tworzymy konto...");break;
    case ConsoleKey.D2: Console.WriteLine("Witaj zalogowany użytkowniku...");break;
}
*/
PlanAndRide.BusinessLogic.Route route = new PlanAndRide.BusinessLogic.Route();
PlanAndRide.BusinessLogic.Ride ride = new PlanAndRide.BusinessLogic.Ride();
PlanAndRide.BusinessLogic.User user = new PlanAndRide.BusinessLogic.User();
PlanAndRide.BusinessLogic.Review review = new PlanAndRide.BusinessLogic.Review();
//ride.NewRide();
//route.NewRoute();
user.NewUser();
//review.NewReview();