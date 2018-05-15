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
	class WarpMenu : IClickableMenu
	{

		private readonly IMonitor _monitor;
		private readonly IModHelper _helper;
		private readonly List<Farmer> _farmers;
		private static int Width = 700;
		private static int Height = 400;

		private List<PlayerBar> _playerBars;


		public WarpMenu(IMonitor monitor, IModHelper helper)
			: base(Game1.viewport.Width / 2 - Width / 2, Game1.viewport.Height / 2 - Height / 2, Width, Height, true)
		{
			this._monitor = monitor;
			this._helper = helper;
			this._farmers = PlayerHelper.GetAllCreatedFarmers();
			this.setUpPlayerBars();
		}

		public void setUpPlayerBars()
		{

			_playerBars = new List<PlayerBar>();

			for (int i = 0; i < _farmers.Count; i++)
			{
				PlayerBar pb = new PlayerBar(_farmers[i]);

				int posX = this.xPositionOnScreen;
				int posY = this.yPositionOnScreen + ((Height / 4) * i);

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
			this.drawMenuTitle(b);
			this.drawMenuBackground(b);
			this.drawPlayerBars(b);
			base.draw(b);
			this.drawMouse(b);
		}

		private void drawMenuTitle(SpriteBatch b)
		{

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
				this.xPositionOnScreen, this.yPositionOnScreen, this.width, this.height + 32, Color.White, 4f, true);

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

			base.receiveLeftClick(x, y, playSound);
		}

		private void warpFarmerToPlayer(Farmer f)
		{
			Game1.warpFarmer(f.currentLocation.name, (int)(f.position.X + 16) / Game1.tileSize, (int)f.position.Y / Game1.tileSize, false);
		}
	}
}
