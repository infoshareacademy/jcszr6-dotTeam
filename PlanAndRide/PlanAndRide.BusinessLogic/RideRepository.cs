using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public static void EditRide(string rideName)
        {
            var myRide = rides.FirstOrDefault(r => r.Name == rideName);
            if (myRide == null)
            {
                Console.WriteLine($"No ride with name {rideName} has been found.");
            }
            //Name edit
            {
                Console.WriteLine($"Current name is: {myRide.Name}");
                Console.WriteLine("Do you want to change it [y/n]?:");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                while (keyInfo.Key != ConsoleKey.Y && keyInfo.Key != ConsoleKey.N)
                {
                    Console.WriteLine("Press key 'y' or 'n' to continue...");
                    keyInfo = Console.ReadKey(true);
                }

                if (keyInfo.Key == ConsoleKey.Y)
                {
                    Console.WriteLine($"Enter new name: ");
                    var newName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newName))
                    {
                        myRide.Name = newName;
                        Console.WriteLine($"Name has been changed. Current name is: {myRide.Name}");
                    }
                    else
                    {
                        Console.WriteLine("There is no correct name. Name hasn't been changed...");
                    }
                }
            }
            //Date edit
            {
                Console.WriteLine($"Current date is: {myRide.Date}");
                Console.WriteLine("Do you want to change it [y/n]?:");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                while (keyInfo.Key != ConsoleKey.Y && keyInfo.Key != ConsoleKey.N)
                {
                    Console.WriteLine("Press key 'y' or 'n' to continue...");
                    keyInfo = Console.ReadKey(true);
                }

                if (keyInfo.Key == ConsoleKey.Y)
                {
                    Console.WriteLine($"Enter new date (dd.mm.yyyy gg:mm): ");
                    var parseSuccess = DateTime.TryParse(Console.ReadLine(), out var newDate);
                    if (parseSuccess)
                    {
                        myRide.Date = newDate;
                        Console.WriteLine($"Date has been changed. Current date is: {myRide.Date}");
                    }
                    else
                    {
                        Console.WriteLine("There is no correct date. It hasn't been changed...");
                    }
                }
            }
            //Description edit
            {
                Console.WriteLine($"Current description is: {myRide.Description}");
                Console.WriteLine("Do you want to change it [y/n]?:");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                while (keyInfo.Key != ConsoleKey.Y && keyInfo.Key != ConsoleKey.N)
                {
                    Console.WriteLine("Press key 'y' or 'n' to continue...");
                    keyInfo = Console.ReadKey(true);
                }

                if (keyInfo.Key == ConsoleKey.Y)
                {
                    Console.WriteLine($"Enter new description: ");
                    var newDesc = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newDesc))
                    {
                        myRide.Description = newDesc;
                        Console.WriteLine($"Description has been changed. Current description is: {myRide.Description}");
                    }
                    else
                    {
                        Console.WriteLine("There is no correct description. Description hasn't been changed...");
                    }
                }
            }
            //ShareRide edit
            {
                Console.WriteLine($"Current sharing setting is: {myRide.ShareRide}");
                Console.WriteLine("Do you want to change it [y/n]?:");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                while (keyInfo.Key != ConsoleKey.Y && keyInfo.Key != ConsoleKey.N)
                {
                    Console.WriteLine("Press key 'y' or 'n' to continue...");
                    keyInfo = Console.ReadKey(true);
                }

                if (keyInfo.Key == ConsoleKey.Y)
                {
                    Console.WriteLine($"Enter new sharing setting (true/false): ");
                    var parseSuccess = Boolean.TryParse(Console.ReadLine(), out var newShareRide);
                    if (parseSuccess)
                    {
                        myRide.ShareRide = newShareRide;
                        Console.WriteLine($"Sharing setting has been changed. Current setting is: {myRide.ShareRide}");
                    }
                    else
                    {
                        Console.WriteLine("There is no correct setting. It hasn't been changed...");
                    }
                }
            }
            //IsPrivate edit
            {
                Console.WriteLine($"Current privacy setting is: {myRide.IsPrivate}");
                Console.WriteLine("Do you want to change it [y/n]?:");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                while (keyInfo.Key != ConsoleKey.Y && keyInfo.Key != ConsoleKey.N)
                {
                    Console.WriteLine("Press key 'y' or 'n' to continue...");
                    keyInfo = Console.ReadKey(true);
                }

                if (keyInfo.Key == ConsoleKey.Y)
                {
                    Console.WriteLine($"Enter new privacy setting (true/false): ");
                    var parseSuccess = Boolean.TryParse(Console.ReadLine(), out var newIsPrivate);
                    if (parseSuccess)
                    {
                        myRide.IsPrivate = newIsPrivate;
                        Console.WriteLine($"Privacy setting has been changed. Current setting is: {myRide.IsPrivate}");
                    }
                    else
                    {
                        Console.WriteLine("There is no correct setting. It hasn't been changed...");
                    }
                }
            }

        }
    }
}
