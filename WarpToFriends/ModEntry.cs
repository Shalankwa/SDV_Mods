using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace WarpToFriends
{
	/// <summary>The mod entry point.</summary>
	public class ModEntry : Mod
	{

		public static IModHelper Helper { get; private set; }
		public static IMonitor Monitor { get; private set; }
		public static ModConfig config;

		/// <summary>The mod entry point, called after the mod is first loaded.</summary>
		/// <param name="helper">Provides simplified APIs for writing mods.</param>
		public override void Entry(IModHelper helper)
		{
			Helper = helper;
			Monitor = base.Monitor;
			config = helper.ReadConfig<ModConfig>();
			helper.Events.Input.ButtonPressed += OnButtonPressed;

			// Debug logs
			//helper.Events.Player.Warped += Warped;
		}

		/// <summary>Raised after a player warps to a new location.</summary>
		/// <param name="sender">The event sender.</param>
		/// <param name="e">The event data.</param>
		private void Warped(object sender, WarpedEventArgs e)
		{
			if (!e.IsLocalPlayer)
				return;

			Monitor.Log(e.NewLocation.Name);
			Monitor.Log(e.NewLocation.uniqueName.Value);
			Monitor.Log(e.NewLocation.Name);
		}

		/// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
		/// <param name="sender">The event sender.</param>
		/// <param name="e">The event data.</param>
		private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
		{
			if (Context.IsWorldReady && e.Button == config.OpenMenuKey)
			{
				if (!Context.IsPlayerFree)
				{
					if (Game1.activeClickableMenu is WarpMenu)
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
