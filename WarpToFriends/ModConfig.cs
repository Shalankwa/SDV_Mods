using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpToFriends
{
	public class ModConfig
	{

		[OptionDisplay("Open Menu Key")]
		public SButton OpenMenuKey { get; set; } = SButton.J;

		[OptionDisplay("Allow Console Warp")]
		public bool canConsoleWarp { get; set; } = true;
	}
}
