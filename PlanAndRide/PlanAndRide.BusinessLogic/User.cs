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

            Console.WriteLine($"\nHasło wprowadzone pomyślnie!\n");

            //email *** dodać walidację adresu email! **
            Console.WriteLine("Wprowadź adres email:");
            Email = Console.ReadLine().Trim();

            while (Email == "")
            {
                Console.WriteLine("Wprowadź email:");
                Email = Console.ReadLine();
            }

            Console.WriteLine($"\nEmail nowego konta to: {Email}\n");
        }
    }
}