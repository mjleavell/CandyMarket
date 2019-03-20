using System;
using System.Collections.Generic;

namespace candy_market
{
	class Program
	{
		static void Main(string[] args)
		{
			var db = SetupNewApp();
            //var flavorsArray = Enum.GetNames(typeof(FlavorCategory));
            
            // Create our users for the system
            var candyUsers = new List<Users>
            {
                new Users(1, "Maggie"),
                new Users(2, "Colin"),
                new Users(3, "Tim"),
                new Users(4, "Marco")
            };
            
			var exit = false;
			while (!exit)
			{
				var userInput = MainMenu();

                //var temp = TakeActions(db, userInput);

                exit = TakeActions(db, userInput);
			}
		}

		internal static CandyStorage SetupNewApp()
		{
			Console.Title = "Cross Confectioneries Incorporated";
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;

			var db = new CandyStorage();

			return db;
		}

		internal static ConsoleKeyInfo MainMenu()
		{
			View mainMenu = new View()
					.AddMenuOption("Did you just get some new candy? Add it here.")
					.AddMenuOption("Do you want to eat some candy? Take it here.")
                    .AddMenuOption ("Do you want to trade some candy?  Trade it here.")
					.AddMenuText("Press Esc to exit.");
			Console.Write(mainMenu.GetFullMenu());
			var userOption = Console.ReadKey();
			return userOption;
		}

		private static bool TakeActions(CandyStorage db, ConsoleKeyInfo userInput)
		{
			Console.Write(Environment.NewLine);

			if (userInput.Key == ConsoleKey.Escape)
				return true;

			var selection = userInput.KeyChar.ToString();
			switch (selection)
			{
				case "1":
                    AddCandyMenu(db, 1);
                    //AddNewCandy(db);
					break;
				case "2": EatCandy(db);
					break;
                case "3": TradeCandy(db);
                    break;
				default: return true;
			}
			return true;
		}

        private static void AddCandyMenu(CandyStorage db, int userId)
        {
            //var flavorCategoryString = string.Join(",", flavorCategory);
            View addCandyMenuName = new View()
                    .AddMenuOption("Please provide the candy name:")
                    .AddMenuText("Press Esc to exit.");
            Console.Write(addCandyMenuName.GetFullMenu());
            var newCandyName = Console.ReadLine();

            View addCandyMenuFlavor = new View()
                    .AddMenuOption("Please provide a flavor or type:")
                    .AddMenuText("Press Esc to exit.");
            Console.Write(addCandyMenuFlavor.GetFullMenu());
            var newCandyFlavor = Console.ReadLine();

            View addCandyMenuManufacturer = new View()
                   .AddMenuOption("Please provide the manufacturer:")
                   .AddMenuText("Press Esc to exit.");
            Console.Write(addCandyMenuManufacturer.GetFullMenu());
            var newCandyManufacturer = Console.ReadLine();

            View addCandyMenuQuantity = new View()
                    .AddMenuOption("Please provide the Quantity you want to add:")
                    .AddMenuText("Press Esc to exit.");
            Console.Write(addCandyMenuQuantity.GetFullMenu());
            var newCandyQuantity = Console.ReadLine();

            AddNewCandy(db, newCandyName, newCandyFlavor, newCandyManufacturer, newCandyQuantity);
            //return newCandy;
        }

        internal static void AddNewCandy(CandyStorage db, string newCandyName, string newCandyFlavor, string newCandyManufacturer, string newCandyQuantity )
		{
            var newCandy = new Candy(newCandyName, newCandyFlavor, DateTime.Now, newCandyManufacturer);
           

			var savedCandy = db.SaveNewCandy(newCandy);
			Console.WriteLine($"Now you own the candy {savedCandy.Name}");
		}

		private static void EatCandy(CandyStorage db)
		{
			throw new NotImplementedException();
		}

        public static void TradeCandy(CandyStorage db)
        {
            throw new NotImplementedException();
        }
	}
}
