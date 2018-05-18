using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Menus;
using WarpToFriends.Helpers;

namespace WarpToFriends
{
	/// <summary>The mod entry point.</summary>
	public class ModEntry : Mod
	{

		public static IModHelper Helper { get; private set; }
		public static IMonitor Monitor { get; private set; }
		public static ModConfig config;

		public override void Entry(IModHelper helper)
		{
			Helper = helper;
			Monitor = Monitor;
			config = helper.ReadConfig<ModConfig>();
			InputEvents.ButtonPressed += InputEvents_ButtonPressed;
		}

		private void InputEvents_ButtonPressed(object sender, EventArgsInput e)
		{
			if (Context.IsWorldReady && e.Button.ToString() == config.OpenMenuKey)
			{
				if (!Context.IsPlayerFree)
				{
					if(Game1.activeClickableMenu is WarpMenu)
					{
						Game1.activeClickableMenu.exitThisMenu(true);
					}
				}
				else
				{
					Game1.activeClickableMenu = new WarpMenu();
				}
				
			}
		}
	}
}