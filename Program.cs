using candy_market.Menus;
using candy_market.SeedData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace candy_market
{
    internal class Program
    {
        // Create our users for the system
        private static List<Users> candyUsers = new List<Users>()
            {
                new Users(1, "Maggie"),
                new Users(2, "Colin"),
                new Users(3, "Tim"),
                new Users(4, "Marco")
            };

        private static void Main(string[] args)
        {
            var db = SetupNewApp();
            DefaultCandy.SeedCandy();

            var exit = false;
            var userSelected = false;
            var user = new Users();

            while (!exit)
            {
                if (!userSelected)
                {
                    var userMenuInput = DisplayUserMenu.UserMenu(candyUsers);

                    var validUserIndex = DisplayUserMenu.GetValidUser(userMenuInput, candyUsers);
                    if (validUserIndex == -1)
                    {
                        continue;
                    }
                    if (validUserIndex == -2)
                    {
                        break;
                    }
                    user = DisplayUserMenu.GetUser(validUserIndex, candyUsers);
                    userSelected = true;
                }  
                    var userInput = MainMenu(user);
                    exit = TakeActions(db, userInput, user.Id);
                        
            }
        }

        internal static CandyStorage SetupNewApp()
        {
            Console.Title = "Cross Confectioneries Incorporated";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;

            var db = new CandyStorage();

            return db;
        }

        internal static ConsoleKeyInfo MainMenu(Users activeUserName)
        {
            View mainMenu = new View()
                    .AddMenuText($"Welcome {activeUserName.Name}!!")
                    .AddMenuOption("[ADD]    - Did you just get some new candy? Add it here.")
                    .AddMenuOption("[EAT]    - Do you want to eat some candy? Take it here.")
                    .AddMenuOption("[RANDOM] - Do you want to eat a random piece of candy? Pick it here.")
                    .AddMenuOption("[TRADE]  - Do you want to trade some candy?  Trade it here.")
                    .AddMenuText("Press Esc to exit.");
            Console.Write(mainMenu.GetFullMenu());
            var userOption = Console.ReadKey();
            return userOption;
        }

        private static bool TakeActions(CandyStorage db, ConsoleKeyInfo userInput, int userId)
        {
            Console.Write(Environment.NewLine);

            if (userInput.Key == ConsoleKey.Escape)
                return true;

            var selection = userInput.KeyChar.ToString();

            switch (selection)
            {
                case "1":
                    AddCandyMenu.AddCandyMenus(db, userId);
                    break;

                case "2":
                    EatCandy(db);
                    break;

                case "3":
                    EatRandomCandyMenu.AddRandomCandyMenu(db, userId);
                    break;

                case "4":
                    TradeCandy(db);
                    break;

                default: return true;
            }
            return false;
        }
//<<<<<<< HEAD
        
//        private static void AddCandyMenu(CandyStorage db, int userId)
//        {
//            View addCandyMenuName = new View()
//                    .AddMenuOption("Please provide the candy name:")
//                    .AddMenuText("Press Esc to exit.");
//            Console.Write(addCandyMenuName.GetFullMenu());
//            var newCandyName = Console.ReadLine();

//            View addCandyMenuFlavor = new View()
//                    .AddMenuOption("Please provide a flavor or type:")
//                    .AddMenuText("Press Esc to exit.");
//            Console.Write(addCandyMenuFlavor.GetFullMenu());
//            var newCandyFlavor = Console.ReadLine();

//            View addCandyMenuManufacturer = new View()
//                   .AddMenuOption("Please provide the manufacturer:")
//                   .AddMenuText("Press Esc to exit.");
//            Console.Write(addCandyMenuManufacturer.GetFullMenu());
//            var newCandyManufacturer = Console.ReadLine();

//            View addCandyMenuQuantity = new View()
//                    .AddMenuOption("Please provide the Quantity you want to add:")
//                    .AddMenuText("Press Esc to exit.");
//            Console.Write(addCandyMenuQuantity.GetFullMenu());
//            var newCandyQuantity = Console.ReadLine();

//            AddNewCandy(db, newCandyName, newCandyFlavor, newCandyManufacturer, newCandyQuantity, userId);
//        }

//        internal static void AddNewCandy(CandyStorage db, string newCandyName, string newCandyFlavor, string newCandyManufacturer, string newCandyQuantity, int userId)
//        {
//            for (int i = 0; i < int.Parse(newCandyQuantity); i++)
//            {
//                var newCandy = new Candy(newCandyName, newCandyFlavor, DateTime.Now, newCandyManufacturer, userId);
//                var savedCandy = db.SaveNewCandy(newCandy);
//            }

//            Console.WriteLine($"You now you own {newCandyQuantity} piece(s) of {newCandyName} candy!");
//        }

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