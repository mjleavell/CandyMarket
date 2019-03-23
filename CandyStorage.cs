using System;
using System.Collections.Generic;
using System.Linq;

namespace candy_market
{
    internal class CandyStorage
    {
        private static List<Candy> _myCandy = new List<Candy>();

        internal IList<string> GetCandyTypes()
        {
            throw new NotImplementedException();
        }

        internal Candy SaveNewCandy(Candy newCandy)
        {
            newCandy.Id = Guid.NewGuid();
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
            // Check to make sure we have a candy in our DB that matches the Id of our updatedCandy we passed in
            var candyToUpdate = _myCandy
                .Where(candy => candy.Id == updatedCandy.Id)
                .FirstOrDefault();
            
            if (candyToUpdate != null)
            {
                // Get the Index of the Candy to Update and Remove from List<Candy>
                var oldIndex = _myCandy
                    .FindIndex(x => x.Id == updatedCandy.Id);
                _myCandy.RemoveAt(oldIndex);

                // Add the new Candy passed in to the List<Candy> and return it in case you want/need to do anything else with it
                _myCandy.Add(updatedCandy);
                return updatedCandy;
            }
            else
            {
                // We did not find a candy in the DB so throw and exception
                throw new InvalidOperationException($"No candy found with id {updatedCandy.Id}");
            }
        }
    }
}