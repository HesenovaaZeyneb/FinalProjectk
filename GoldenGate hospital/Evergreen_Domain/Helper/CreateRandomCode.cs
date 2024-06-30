using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evergreen_Domain.Helper
{
	public static class CreateRandomCode
	{
		public const string Alphabet =
		"abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

		public static string GenerateCode(int size)
		{
			Random rand = new Random();
			char[] chars = new char[size];
			for (int i = 0; i < size; i++)
			{
				chars[i] = Alphabet[rand.Next(Alphabet.Length)];
			}
			return new string(chars);
		}
	}
}
