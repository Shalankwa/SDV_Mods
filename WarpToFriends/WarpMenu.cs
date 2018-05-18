using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using WarpToFriends.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpToFriends
{
	public class WarpMenu : IClickableMenu
	{

		private readonly List<Farmer> _farmers;
		private static int Width = 700;
		private static int Height = 400;

		private List<PlayerBar> _playerBars;
		private ClickableTextureComponent _options;

		public WarpMenu()
			: base(Game1.viewport.Width / 2 - Width / 2, Game1.viewport.Height / 2 - Height / 2, Width, Height, true)
		{
			_farmers = PlayerHelper.GetAllCreatedFarmers();
			
			setUpPlayerBars();

			_options = new ClickableTextureComponent("Options", new Rectangle(xPositionOnScreen + Width, yPositionOnScreen + Height, 16 * Game1.pixelZoom, 16 * Game1.pixelZoom),
				"", "Configure mod options", Game1.mouseCursors, new Rectangle(162, 440, 16, 16), Game1.pixelZoom);
		}

		public void setUpPlayerBars()
		{

			_playerBars = new List<PlayerBar>();

			for (int i = 0; i < _farmers.Count; i++)
			{
				PlayerBar pb = new PlayerBar(_farmers[i]);

				int posX = xPositionOnScreen;
				int posY = yPositionOnScreen + ((Height / 4) * i);

				Rectangle sectionBounds = new Rectangle(posX + 16, posY + 16, Width - 32, Height / 4);
				pb.section = new ClickableComponent(sectionBounds, _farmers[i].name);

				int iconStartX = posX + 30;
				int iconStartY = posY + 20;
				pb.icon = new ClickableComponent(new Rectangle(iconStartX, iconStartY, 80, 80), _farmers[i].name);

				Rectangle buttonBounds = new Rectangle(iconStartX + 530, iconStartY + 16, 85, 50);
				pb.warpButton = new ClickableComponent(buttonBounds, _farmers[i].name);

				_playerBars.Add(pb);
			}

		}

		public override void draw(SpriteBatch b)
		{
			drawMenuTitle(b);
			drawMenuBackground(b);
			drawPlayerBars(b);
			drawOptionsButton(b);
			base.draw(b);
			drawMouse(b);
		}

		private void drawMenuTitle(SpriteBatch b)
		{

		}

		private void drawOptionsButton(SpriteBatch b)
		{
			_options.draw(b);
			_options.tryHover(Game1.getMouseX(), Game1.getMouseY());
			if(_options.containsPoint(Game1.getMouseX(), Game1.getMouseY()))
			{
				IClickableMenu.drawToolTip(b, _options.hoverText, _options.name, null);
			}
		}

		private void drawPlayerBars(SpriteBatch b)
		{
			foreach (PlayerBar pb in _playerBars)
			{
				pb.draw(b);
			}
		}

		private void drawMenuBackground(SpriteBatch b)
		{

			b.Draw(Game1.fadeToBlackRect, Game1.graphics.GraphicsDevice.Viewport.Bounds, Color.Black * 0.4f);

			IClickableMenu.drawTextureBox(b, Game1.mouseCursors, new Rectangle(384, 373, 18, 18),
				xPositionOnScreen, yPositionOnScreen, width, height + 32, Color.White, 4f, true);

		}

		public override void receiveLeftClick(int x, int y, bool playSound = true)
		{
			foreach(PlayerBar pb in _playerBars)
			{
				if (!pb.online || pb.farmer == Game1.player) continue;
				if(pb.warpButton.containsPoint(x, y))
				{
					warpFarmerToPlayer(pb.farmer);
				}
			}
			if(_options.containsPoint(x, y))
			{
				Game1.activeClickableMenu.exitThisMenuNoSound();
				Game1.activeClickableMenu = new OptionsMenu<ModConfig>(ModEntry.Helper, 500, 400, Game1.player.uniqueMultiplayerID, ModEntry.config, this);
			}

			base.receiveLeftClick(x, y, playSound);
		}

		private void warpFarmerToPlayer(Farmer f)
		{
			if (f.currentLocation.isFarmBuildingInterior() || f.currentLocation.name.Equals("Cabin"))
			{
				Game1.warpFarmer(f.currentLocation.uniqueName, (int)(f.position.X + 16) / Game1.tileSize, (int)f.position.Y / Game1.tileSize, false);
			}
			else
			{
				Game1.warpFarmer(f.currentLocation.name, (int)(f.position.X + 16) / Game1.tileSize, (int)f.position.Y / Game1.tileSize, false);
			}


		}
	}
}
