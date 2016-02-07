using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PasswordGenerator
{
	class Program
	{
		private const string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
		private const string numerical = "0123456789";
		private const string symbols = "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~¡¢£¤¥¦§¨©ª«¬®¯°±²³´µ¶·¸¹º»¼½¾¿";


		private static char[] GenerateCharacterSelection(bool includeSymbols)
		{
			char[] chars = alphabet.Union(numerical).ToArray();
			if (includeSymbols)
			{
				chars = chars.Union(symbols).ToArray();
			}

			return chars;
		}

		private static int ParseLength(string[] args)
		{
			foreach (string arg in args)
			{
				if (Regex.IsMatch(arg, @"^\d+$"))
				{
					return int.Parse(arg);
				}
			}

			return 42;
		}

		private static string GeneratePassword(char[] sourceSymbols, int length)
		{
			Random rng = new Random();
			List<char> password = new List<char>();

			for (int i = 0; i < length; ++i)
			{
				char c = sourceSymbols[rng.Next(0, sourceSymbols.Length)];
				password.Add(c);
			}

			return new string(password.ToArray());
		}

		private static void OutputPassword(string password)
		{
			Console.WriteLine(password);

			Clipboard.SetText(password);
		}

		//STAThread needed to access clipboard.
		[STAThread]
		private static void Main(string[] args)
		{
			bool includeSymbols = !args.Any(arg => arg == "no-symbols");
			char[] chars = GenerateCharacterSelection(includeSymbols);

			int passwordLength = ParseLength(args);

			string password = GeneratePassword(chars, passwordLength);

			OutputPassword(password);
		}
	}
}
