using System;
using System.Collections.Generic;

namespace candy_market
{
	class Program
	{
		static void Main(string[] args)
		{
			var db = SetupNewApp();
            
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
                    var userNewCandy = AddCandyMenu(1, "Vanilla");
                    AddNewCandy(db);
					break;
				case "2": EatCandy(db);
					break;
                case "3": TradeCandy(db);
                    break;
				default: return true;
			}
			return true;
		}

        private static string AddCandyMenu(int userId, string flavorCategory)
        {
            View addCandyMenu = new View()
                    .AddMenuOption("Please provide the following as a Comma Seperated List:")
                    .AddMenuText($"\nName, [{flavorCategory}], Quantity\n")
                    //.AddMenuText($"[{flavorCategory}]")
                    //.AddMenuText("Quantity")
                    .AddMenuText("Press Esc to exit.");
            Console.Write(addCandyMenu.GetFullMenu());
            var newCandy = Console.ReadLine();
            return newCandy;
        }

		internal static void AddNewCandy(CandyStorage db)
		{
			var newCandy = new Candy
			{
				Name = "Whatchamacallit"
			};

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
