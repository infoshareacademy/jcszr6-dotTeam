using System.Text.RegularExpressions;

namespace PlanAndRide.BusinessLogic
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }


        public void NewUser()
        {

            //login
            Console.WriteLine("Tworzymy nowe konto!\n");
            Console.WriteLine("Login:");
            Login = Console.ReadLine().Trim();

            while (Login == "")
            {
                Console.WriteLine("Wprowadź login:");
                Login = Console.ReadLine();
            }

            Console.WriteLine($"\nLogin nowego konta to: {Login}\n");

            //hasło
            Console.WriteLine("Hasło:");
            Password = Console.ReadLine().Trim();

            while (Password == "")
            {
                Console.WriteLine("Wprowadź hasło:");
                Password = Console.ReadLine();
            }

            Console.WriteLine($"\nZapisano hasło!\n");

            //email *** dodać walidację adresu email! **
            Console.WriteLine("Wprowadź adres email:");
            Email = Console.ReadLine().Trim();

            while (Email == null || ValidateEmail(Email) == false)
            {
                Console.WriteLine("Wprowadź email:");
                Email = Console.ReadLine();
            }

            Console.WriteLine($"\nEmail nowego konta to: {Email}\n");
        }

        // walidacja regex email
        public static bool ValidateEmail(string Email)
        {
            var regex = @"[a-z A-Z 0-9_\-]+[@]+[a-z 0-9]+[\.]+[a-z]{2,4}$";
            bool result = Regex.IsMatch(Email, regex, RegexOptions.IgnoreCase);
            return result;
        }
    }
}