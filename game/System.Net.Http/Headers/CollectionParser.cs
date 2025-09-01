using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	// Token: 0x02000038 RID: 56
	internal static class CollectionParser
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x00007AE8 File Offset: 0x00005CE8
		public static bool TryParse<T>(string input, int minimalCount, ElementTryParser<T> parser, out List<T> result) where T : class
		{
			Lexer lexer = new Lexer(input);
			result = new List<T>();
			T t;
			Token token;
			while (parser(lexer, out t, out token))
			{
				if (t != null)
				{
					result.Add(t);
				}
				if (token != Token.Type.SeparatorComma)
				{
					if (token != Token.Type.End)
					{
						result = null;
						return false;
					}
					if (minimalCount > result.Count)
					{
						result = null;
						return false;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007B49 File Offset: 0x00005D49
		public static bool TryParse(string input, int minimalCount, out List<string> result)
		{
			return CollectionParser.TryParse<string>(input, minimalCount, new ElementTryParser<string>(CollectionParser.TryParseStringElement), out result);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00007B5F File Offset: 0x00005D5F
		public static bool TryParseRepetition(string input, int minimalCount, out List<string> result)
		{
			return CollectionParser.TryParseRepetition<string>(input, minimalCount, new ElementTryParser<string>(CollectionParser.TryParseStringElement), out result);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00007B78 File Offset: 0x00005D78
		private static bool TryParseStringElement(Lexer lexer, out string parsedValue, out Token t)
		{
			t = lexer.Scan(false);
			if (t == Token.Type.Token)
			{
				parsedValue = lexer.GetStringValue(t);
				if (parsedValue.Length == 0)
				{
					parsedValue = null;
				}
				t = lexer.Scan(false);
			}
			else
			{
				parsedValue = null;
			}
			return true;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00007BD0 File Offset: 0x00005DD0
		public static bool TryParseRepetition<T>(string input, int minimalCount, ElementTryParser<T> parser, out List<T> result) where T : class
		{
			Lexer lexer = new Lexer(input);
			result = new List<T>();
			T t;
			Token token;
			while (parser(lexer, out t, out token))
			{
				if (t != null)
				{
					result.Add(t);
				}
				if (token == Token.Type.End)
				{
					return minimalCount <= result.Count;
				}
			}
			return false;
		}
	}
}
