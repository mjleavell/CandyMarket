using System;
using System.Collections.Generic;
using System.Linq;

namespace candy_market
{
	class Program
	{
        // Create our users for the system
        private static List<Users> candyUsers = new List<Users>()
            {
                new Users(4, "Maggie"),
                new Users(3, "Colin"),
                new Users(2, "Tim"),
                new Users(1, "Marco")
            };

        static void Main(string[] args)
		{
			var db = SetupNewApp();
           
			var exit = false;
			while (!exit)
			{
                var userMenuInput = UserMenu();
                //GetUserId(userMenuInput);
                var user = GetUserId(userMenuInput);
                var userInput = MainMenu(user);
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

<<<<<<< HEAD
        // displays user menu
        internal static ConsoleKeyInfo UserMenu()
        {
            View userMenu = new View()
                    .AddMenuText("Please select a user from the list below")
                    .AddMenuOptions(candyUsers.Select(u => u.Name).ToList())
                    .AddMenuText("Press Esc to exit.");
            Console.Write(userMenu.GetFullMenu());
            var selectedUser = Console.ReadKey();
            return selectedUser;
        }

        internal static ConsoleKeyInfo MainMenu(object selectedUser)
        {
            View mainMenu = new View()
                    .AddMenuText($"Welcome {selectedUser}!!")
                    .AddMenuOption("Did you just get some new candy? Add it here.")
                    .AddMenuOption("Do you want to eat some candy? Take it here.")
                    .AddMenuText("Press Esc to exit.");
            Console.WriteLine(selectedUser["Name"]);
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
				case "1": AddNewCandy(db);
					break;
				case "2": EatCandy(db);
					break;
                case "3": TradeCandy(db);
                    break;
				default: return true;
			}
			return true;
		}

        internal static object GetUserId(ConsoleKeyInfo selectedUser)
        {
            var userInput = selectedUser.KeyChar.ToString();
            var userIndex = int.Parse(userInput);
            var user = candyUsers[userIndex - 1];
            var userId = user.Id;
            var userType = user.GetType();
            return user;
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
