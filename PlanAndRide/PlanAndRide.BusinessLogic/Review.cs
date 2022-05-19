namespace PlanAndRide.BusinessLogic
{
    public class Review
    {
        public int Score { get; set; }
        public string Description { get; set; }

        private string newScore;

        public void NewReview()
        {
            //ocena

            Console.WriteLine("Stórzmy recenzję!\n");
            Console.WriteLine("Skala: 1-5;");
            Console.WriteLine("1 - bardzo słaba, 5 - rewelacja");
            newScore = Console.ReadLine().Trim();

            while (newScore == "" || !int.TryParse(newScore, out int Score) || Score > 5 || Score <= 0)
            {
                Console.Write("Wprowadź ocenę 1-5:");
                newScore = Console.ReadLine();
                Console.WriteLine("");
            }

            Score = int.Parse(newScore);

            Console.WriteLine($"\nTwoja ocenta to: {Score}\n");

            //opis

            Console.WriteLine("Czy chcesz dodać opis recenzji (wprowadź 't' lub 'n': ");
            var descriptionYesNo = Console.ReadLine().ToLower().Trim();

            do
            {
                if (descriptionYesNo == "n" || descriptionYesNo == "nie")
                {
                    Console.WriteLine($"Ocena bez opisu\n");
                    break;
                }
                else if (descriptionYesNo == "t" || descriptionYesNo == "tak")
                {
                    Console.WriteLine("Wprowadź opis:\n");
                    Description = Console.ReadLine();
                    Console.WriteLine("\nDodano opis recenzji.\n");
                    break;
                }
                else
                {
                    Console.WriteLine("\nWprowadź 't' lub 'n': ");
                    descriptionYesNo = Console.ReadLine().ToLower().Trim();
                }
            } while (descriptionYesNo != null);
        }
    }
}