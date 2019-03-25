using System;
using System.Collections.Generic;
using System.Text;

namespace candy_market.Menus
{
    internal class AddCandyMenu
    {
        internal static void AddCandyMenus(CandyStorage db, int userId)
        {
            int newCandyQuantity = 0;

            var newCandyName = AddCandyMenuName();

            var newCandyFlavor = AddCandyMenuFlavor();

            var newCandyManufacturer = AddCandyMenuManufacturer();

            do
            {
                newCandyQuantity = AddCandyMenuQuantity();
            } while (newCandyQuantity < 1);

            AddNewCandy(db, newCandyName, newCandyFlavor, newCandyManufacturer, newCandyQuantity, userId);
        }

        internal static void AddNewCandy(CandyStorage db, string newCandyName, string newCandyFlavor, string newCandyManufacturer, int newCandyQuantity, int userId)
        {
            for (int i = 0; i < newCandyQuantity; i++)
            {
                var newCandy = new Candy(newCandyName, newCandyFlavor, DateTime.Now, newCandyManufacturer, userId);
                var savedCandy = db.SaveNewCandy(newCandy);
            }

            Console.WriteLine($"You now you own {newCandyQuantity} piece(s) of {newCandyName} candy!");
            Console.WriteLine("Press the 'Enter' key to continue.");
            Console.ReadLine();
        }

        private static string AddCandyMenuName()
        {
            View addCandyMenuName = new View()
                    .AddMenuOption("Please provide the candy name:")
                    .AddMenuText("Press Esc to exit.");
            Console.Write(addCandyMenuName.GetFullMenu());
            var newCandyName = Console.ReadLine();
            return newCandyName;
        }

        private static string AddCandyMenuFlavor()
        {
            View addCandyMenuFlavor = new View()
                    .AddMenuOption("Please provide a flavor or type:")
                    .AddMenuText("Press Esc to exit.");
            Console.Write(addCandyMenuFlavor.GetFullMenu());
            var newCandyFlavor = Console.ReadLine();
            return newCandyFlavor;
        }

        private static string AddCandyMenuManufacturer()
        {
            View addCandyMenuManufacturer = new View()
                   .AddMenuOption("Please provide the manufacturer:")
                   .AddMenuText("Press Esc to exit.");
            Console.Write(addCandyMenuManufacturer.GetFullMenu());
            var newCandyManufacturer = Console.ReadLine();
            return newCandyManufacturer;
        }

        private static int AddCandyMenuQuantity()
        {
            View addCandyMenuQuantity = new View()
                    .AddMenuOption("Please provide the Quantity you want to add:")
                    .AddMenuText("Press Esc to exit.");
            Console.Write(addCandyMenuQuantity.GetFullMenu());
            var newCandyQuantity = Console.ReadLine();
            try
            {
                return int.Parse(newCandyQuantity);
            }
            catch (Exception)
            {
                Console.WriteLine($"The quantity you entered is not valid please try again");
                Console.WriteLine("Press the 'Enter' key to try again");
                Console.ReadLine();
                return 0;

            }
        }
    }
}
