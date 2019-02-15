using StardewValley;
using System.Collections.Generic;
using System.Linq;

namespace WarpToFriends.Helpers
{
	public static class PlayerHelper
	{

		public static List<Farmer> GetAllCreatedFarmers()
		{
			return Game1.getAllFarmers().Where(f => !string.IsNullOrEmpty(f.name)).ToList<Farmer>();
		}

	}
}
