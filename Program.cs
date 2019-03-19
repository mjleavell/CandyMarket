﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace candy_market
{
	class Program
	{
        // Create our users for the system
        private static List<Users> candyUsers = new List<Users>()
            {
                new Users(1, "Maggie"),
                new Users(2, "Colin"),
                new Users(3, "Tim"),
                new Users(4, "Marco")
            };

        static void Main(string[] args)
		{
			var db = SetupNewApp();
           

			var exit = false;
			while (!exit)
			{
                ConsoleKeyInfo userMenuInput = UserMenu();
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


        internal static ConsoleKeyInfo UserMenu()
        {
            View userMenu = new View()
                    .AddMenuText("Please select a user from the list below")
                    .AddMenuOptions(candyUsers.Select(u => u.Name).ToList())
                    .AddMenuText("Press Esc to exit.");
            Console.Write(userMenu.GetFullMenu());
            var userOption = Console.ReadKey();
            return userOption;
        }

        internal static ConsoleKeyInfo MainMenu()
		{
			View mainMenu = new View()
					.AddMenuOption("Did you just get some new candy? Add it here.")
					.AddMenuOption("Do you want to eat some candy? Take it here.")
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
                    AddNewCandy(db);
                    break;
                case "2":
                    EatCandy(db);
                    break;
                default: return true;
            }
 
			return true;
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
	}
}
