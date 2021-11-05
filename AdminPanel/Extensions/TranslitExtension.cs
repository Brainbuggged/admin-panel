using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Extensions
{
	public class TranslitExtension
	{
		public string Run(string startString)
		{
			var newString = "";
			foreach (var symbol in startString.Replace(" ", "-").ToLower())
			{
				switch (symbol.ToString())
				{
					case "а": newString += "a"; break;
					case "б": newString += "b"; break;
					case "в": newString += "v"; break;
					case "г": newString += "g"; break;
					case "д": newString += "d"; break;
					case "е": newString += "e"; break;
					case "ё": newString += "yo"; break;
					case "ж": newString += "zh"; break;
					case "з": newString += "z"; break;
					case "и": newString += "i"; break;
					case "к": newString += "k"; break;
					case "л": newString += "l"; break;
					case "м": newString += "m"; break;
					case "н": newString += "n"; break;
					case "о": newString += "o"; break;
					case "п": newString += "p"; break;
					case "р": newString += "r"; break;
					case "с": newString += "s"; break;
					case "т": newString += "t"; break;
					case "у": newString += "u"; break;
					case "ф": newString += "f"; break;
					case "х": newString += "kh"; break;
					case "ц": newString += "ts"; break;
					case "ч": newString += "ch"; break;
					case "ш": newString += "sh"; break;
					case "щ": newString += "shch"; break;
					case "ъ": newString += ""; break;
					case "ы": newString += "iy"; break;
					case "ь": newString += ""; break;
					case "э": newString += "e"; break;
					case "ю": newString += "yu"; break;
					case "я": newString += "ya"; break;
					default: newString += symbol; break;
				}
			}
			return newString;
		}
		private string Number()
		{
			var date = new SettingsExtension().GetDateTimeNow();
			return $"{date.Year}{date.Month}{date.Day}{date.Hour}{date.Minute}{date.Second}{date.Millisecond}";
		}
		public string MakeName(string startString) => $"{Run(startString)}_{Number()}";
		public string MakeOrderNumber(Guid client) => $"{client.ToString().Substring(0, 4)}_{Number()}";
	}
}
