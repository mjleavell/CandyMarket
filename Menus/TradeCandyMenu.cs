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

            View candyMenuRecieveWhat = new View()
                   .AddMenuText("What would you like to get in this trade?");
            var TheirCandyBag = db.GetCandyByUserId(CandyTradeWho.Id);
            Console.Write(candyMenuRecieveWhat.GetFullMenu());

            var CandyRecieveWhat = Console.ReadLine();

            View candyMenuGiveWhat = new View()
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