using System;
using System.Collections.Generic;
using System.Linq;

namespace candy_market
{
    internal class CandyStorage
    {
        static List<Candy> _myCandy = new List<Candy>();

        internal IList<string> GetCandyTypes()
        {
            throw new NotImplementedException();
        }

        internal Candy SaveNewCandy(Candy newCandy)
        {
            _myCandy.Add(newCandy);
            return newCandy;
        }

        internal List<Candy> GetCandyByUserId(int userId)
        {
            var userCandy = _myCandy.Where(candy => candy.UserId == userId).ToList();
            return userCandy;
        }
    }
}
