using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace candy_market.Menus
{
    internal class TradeCandyMenu
    {
        public static void AddTradeCandyMenu(CandyStorage db, int userId, Users user, List<Users> candyUsers)
        {
            View candyMenuTradeWho = new View()
                    .AddMenuText($"Hello {user.Name}. Who would you like to trade with?")
                    .AddMenuOptions(candyUsers.Select(u => u.Name).ToList());
            Console.Write(candyMenuTradeWho.GetFullMenu());
            var selectedTradeUser = Console.ReadKey();
            var userInput = selectedTradeUser.KeyChar.ToString();
            var ParsedTradeUser = int.Parse(userInput);
            var CandyTradeWho = DisplayUserMenu.GetUser(ParsedTradeUser, candyUsers);

            var theirCandyBag = db.GetCandyByUserId(CandyTradeWho.Id);
            var listTheirCandyBag = theirCandyBag.Select(x => x.Name).ToList();
            var showTheirCandyBag = string.Join("\n", listTheirCandyBag);
            View candyMenuRecieveWhat = new View()
                   .AddMenuText($"This is what is in {CandyTradeWho.Name}'s candy bag:")
                   .AddMenuText(showTheirCandyBag)
                   .AddMenuText("What would you like to get in this trade?");
            Console.Write(candyMenuRecieveWhat.GetFullMenu());
            var CandyRecieveWhat = Console.ReadLine();

            var myCandyBag = db.GetCandyByUserId(userId);
            var listMyCandyBag = myCandyBag.Select(x => x.Name).ToList();
            var showMyCandyBag = string.Join("\n", listMyCandyBag);
            View candyMenuGiveWhat = new View()
                    .AddMenuText("This is what is in your candy bag:")
                    .AddMenuText(showMyCandyBag)
                    .AddMenuText("What would you like to give in this trade?");
            Console.Write(candyMenuGiveWhat.GetFullMenu());
            var CandyGiveWhat = Console.ReadLine();

            TradeCandy(db, userId, CandyTradeWho, CandyRecieveWhat, CandyGiveWhat);
        }

        internal static void TradeCandy(CandyStorage db, int userId, Users CandyTradeWho, string CandyRecieveWhat, string CandyGiveWhat)
        {
            var MyCandyBag = db.GetCandyByUserId(userId);
            var PieceToTrade = MyCandyBag.Find(x => x.Name == CandyGiveWhat);

            var TheirCandyBag = db.GetCandyByUserId(CandyTradeWho.Id);
            var PieceToGet = TheirCandyBag.Find(x => x.Name == CandyRecieveWhat);

            PieceToGet.UserId = userId;
            PieceToTrade.UserId = CandyTradeWho.Id;

            Console.WriteLine($"You traded your {PieceToTrade.Name} for {CandyTradeWho.Name}'s {PieceToGet.Name}!");
            Console.ReadLine();
        }
    }
}