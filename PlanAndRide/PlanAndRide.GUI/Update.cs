﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PlanAndRide.BusinessLogic;
using Environment = System.Environment;
namespace PlanAndRide.GUI
{
    public class Update
    {
        private readonly RideService _rideService;
        public Update(RideService service)
        {
            _rideService=service;
        }
        public void PrintUpdate()
        {
            //Wyświetlenie wszystkich dostępnych tras zapisanych w bazie danych


            Console.WriteLine("Wciśnij dowolny klawisz aby wyświetlić wszystkie trasy");
            Console.ReadKey();
            Console.WriteLine();


            foreach (var ride in _rideService.GetAll())
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
                Console.WriteLine($"Średnia ocena trasy: {ride.Route.AverageScore}");

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
    }
}
