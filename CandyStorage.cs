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
            var userCandy = _myCandy
            .Where(candy => candy.UserId == userId && candy.isEaten == false)
            .ToList();
            return userCandy;
        }

        internal Candy UpdateCandy(Candy updatedCandy)
        {
            var candyToUpdate = _myCandy
            .Where(x => x.Id == updatedCandy.Id)
            .FirstOrDefault();

            if (candyToUpdate != null)
            {
                var oldIndex = _myCandy
                    .FindIndex(x => x.Id == updatedCandy.Id);
                _myCandy.RemoveAt(oldIndex);

                _myCandy.Add(updatedCandy);
                return updatedCandy;
            } else {
                throw new InvalidOperationException($"No candy found with id {updatedCandy.Id}");
            }
        }
    }
}
