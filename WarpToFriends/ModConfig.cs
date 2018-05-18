using Microsoft.Xna.Framework.Input;
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
		public string OpenMenuKey { get; set; } = Keys.J.ToString();

		[OptionDisplay("This is a checkbox")]
		public bool cb { get; set; } = false;


		[OptionDisplay("Test Key")]
		public string TestKey { get; set; } = Keys.M.ToString();

		[OptionDisplay("Test Key2")]
		public string TestKey2 { get; set; } = Keys.M.ToString();

		[OptionDisplay("Test Key3")]
		public string TestKey3 { get; set; } = Keys.M.ToString();

		[OptionDisplay("Test Key4")]
		public string TestKey4 { get; set; } = Keys.M.ToString();

		[OptionDisplay("Test Key5")]
		public string TestKey5 { get; set; } = Keys.M.ToString();
	}
}
