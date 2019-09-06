using System.Drawing;

namespace Utilities
{
	public static class Helpers
	{
		public static string ConvertRgbToHex(string rgbSetting)
		{
			var temp = string.Empty;
			foreach(char ch in rgbSetting)
			{
				int output;
				if (ch == ',' || int.TryParse(ch.ToString(), out output))
					temp += ch.ToString();

			}
			var rgb = temp.Split(",");

			Color rgbColor = Color.FromArgb(int.Parse(rgb[0]), int.Parse(rgb[1]), int.Parse(rgb[2]));

			return $"#{rgbColor.R.ToString("X2")}{rgbColor.G.ToString("X2")}{rgbColor.B.ToString("X2")}";
		}

		// remove all unwanted ascii characters like new-line and tab spaces
		public static string SanitizeString(string input)
		{
			char prevCh = ' ';
			string newString = string.Empty;
			foreach(char ch in input)
			{
				if((char)ch >= 32 && (char)ch <= 122)
				{
					if (!(prevCh == ' ' && ch == ' '))
						newString += ch;
				}

				prevCh = ch;
			}

			return newString.Trim();
		}
	}
}
