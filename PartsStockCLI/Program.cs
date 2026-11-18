namespace PartsStockCLI
{
    class Program
    {
        public static void Main()
        {
            int returnVal = 0;
            int navChoice = 0;
            Display.Art();
            navChoice = Display.Menu();
            returnVal = Display.MenuNavigate(navChoice);
            if (returnVal == 0)
            {
                throw new IOException();
            }
        }

        public class Display
        {
            public static void Art()
            {
                Console.Title = "Parts Stock CLI";
                Console.ForegroundColor = ConsoleColor.DarkCyan;
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

            public static int MenuNavigate(int navChoice)
            {
                StorageLocations storageLocations = new StorageLocations();
                AddItem add = new AddItem();
                Sourcing sourcing = new Sourcing();
                ItemSearch search = new ItemSearch();
                int ret = 0;
                switch (navChoice)
                {
                    case 1: ret = add.NewItem(); break;
                    case 2: ret = search.Menu(); break;
                    case 3: ret = sourcing.Main(); break;
                    case 4: ret = storageLocations.StorageMain(); break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Come back soon!");
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }

                return ret;




            }




        }
    }
}
