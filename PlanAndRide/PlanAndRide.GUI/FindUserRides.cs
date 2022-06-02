using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanAndRide.BusinessLogic;

namespace PlanAndRide.GUI
{
    internal class FindUserRides
    {
        public void UserRides(Ride ride)
        {

            Console.WriteLine("Wprowadź login szukanego użytkownika\n");
            var input = Console.ReadLine();
            var findUserRides = ride.RideMembers.First(r => r.Login == input);
            if (findUserRides == null)
            {
                Console.WriteLine("Nie znaleziono podanego użytkownika\n");
            }
        }
        
    }
}
