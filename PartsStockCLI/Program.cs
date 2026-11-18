namespace PartsStockCLI
{
    class Program
    {
        public static void Main()
        {
            int navChoice = 0;
            Display.Art();
            navChoice = Display.Menu();
            Display.Navigate(navChoice);
        }

        public class Display
        {
            public static void Art()
            {
                Console.Title = "Parts Stock CLI";
                Console.ForegroundColor = ConsoleColor.Cyan;
                string art =
                    "__________                __           ____     _________ __                 __    \n\\______   \\_____ ________/  |_  ______/  _ \\   /   _____//  |_  ____   ____ |  | __\n |     ___/\\__  \\_  __ \\   __\\/  ___/>  _ </\\ \\_____  \\   __\\/  _ \\_/ ___\\|  |/ /\n |    |     / __ \\|  | \\/|  |  \\___ \\/  <_\\ \\/ /        \\|  | (  <_> )  \\___|    < \n |____|    (____  /__|   |__| /____  >_____\\ \\/_______  /|__|  \\____/ \\___  >__|_ \\\n                \\/                 \\/       \\/        \\/                  \\/     \\/\n";
                Console.WriteLine(art);
                Console.ResetColor();
            }

            public static int Menu()
            {
                int input = 0;
                Console.WriteLine("-------------------------");
                Console.WriteLine("1) New Item");
                Console.WriteLine("2) Item Search");
                Console.WriteLine("3) Sourcing");
                Console.WriteLine("4) Storage Location Setup");
                Console.WriteLine("5) Exit");
                Console.WriteLine("------------------------");
                input = Convert.ToInt32(Console.ReadLine());
                return input;
            }

            public static void Navigate(int navChoice)
            {
                StorageLocations storageLocations = new StorageLocations();
                AddItem add = new AddItem();
                Sourcing sourcing = new Sourcing();
                ItemSearch search = new ItemSearch();
                switch (navChoice)
                {
                    case 1: add.NewItem(); break;
                    case 2: search.Menu(); break;
                    case 3: sourcing.Main(); break;
                    case 4: storageLocations.StorageMain(); break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Come back soon!");
                        break;
                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }



            }




        }
    }
}
