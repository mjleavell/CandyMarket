using System;
using System.Collections.Generic;
using System.Text;

namespace candy_market.Menus
{
    internal class AddCandyMenu
    {
        internal static void AddCandyMenus(CandyStorage db, int userId)
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

            AddNewCandy(db, newCandyName, newCandyFlavor, newCandyManufacturer, newCandyQuantity, userId);
        }

        internal static void AddNewCandy(CandyStorage db, string newCandyName, string newCandyFlavor, string newCandyManufacturer, string newCandyQuantity, int userId)
        {
            for (int i = 0; i < int.Parse(newCandyQuantity); i++)
            {
                var newCandy = new Candy(newCandyName, newCandyFlavor, DateTime.Now, newCandyManufacturer, userId);
                var savedCandy = db.SaveNewCandy(newCandy);
            }

            Console.WriteLine($"You now you own {newCandyQuantity} piece(s) of {newCandyName} candy!");
            Console.WriteLine("Press the 'Enter' key to continue.");
            Console.ReadLine();
        }
       
    }
}
