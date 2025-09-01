using System;
using System.Globalization;
using System.Net.Mail;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Headers
{
	// Token: 0x02000054 RID: 84
	internal static class Parser
	{
		// Token: 0x02000055 RID: 85
		public static class Token
		{
			// Token: 0x06000342 RID: 834 RVA: 0x0000B89E File Offset: 0x00009A9E
			public static bool TryParse(string input, out string result)
			{
				if (input != null && Lexer.IsValidToken(input))
				{
					result = input;
					return true;
				}
				result = null;
				return false;
			}

			// Token: 0x06000343 RID: 835 RVA: 0x0000B8B4 File Offset: 0x00009AB4
			public static void Check(string s)
			{
				if (s == null)
				{
					throw new ArgumentNullException();
				}
				if (Lexer.IsValidToken(s))
				{
					return;
				}
				if (s.Length == 0)
				{
					throw new ArgumentException();
				}
				throw new FormatException(s);
			}

			// Token: 0x06000344 RID: 836 RVA: 0x0000B8DC File Offset: 0x00009ADC
			public static bool TryCheck(string s)
			{
				return s != null && Lexer.IsValidToken(s);
			}

			// Token: 0x06000345 RID: 837 RVA: 0x0000B8EC File Offset: 0x00009AEC
			public static void CheckQuotedString(string s)
			{
				if (s == null)
				{
					throw new ArgumentNullException();
				}
				Lexer lexer = new Lexer(s);
				if (lexer.Scan(false) == System.Net.Http.Headers.Token.Type.QuotedString && lexer.Scan(false) == System.Net.Http.Headers.Token.Type.End)
				{
					return;
				}
				if (s.Length == 0)
				{
					throw new ArgumentException();
				}
				throw new FormatException(s);
			}

			// Token: 0x06000346 RID: 838 RVA: 0x0000B93C File Offset: 0x00009B3C
			public static void CheckComment(string s)
			{
				if (s == null)
				{
					throw new ArgumentNullException();
				}
				string text;
				if (new Lexer(s).ScanCommentOptional(out text))
				{
					return;
				}
				if (s.Length == 0)
				{
					throw new ArgumentException();
				}
				throw new FormatException(s);
			}
		}

		// Token: 0x02000056 RID: 86
		public static class DateTime
		{
			// Token: 0x06000347 RID: 839 RVA: 0x0000B976 File Offset: 0x00009B76
			public static bool TryParse(string input, out DateTimeOffset result)
			{
				return Lexer.TryGetDateValue(input, out result);
			}

			// Token: 0x06000348 RID: 840 RVA: 0x0000B97F File Offset: 0x00009B7F
			// Note: this type is marked as 'beforefieldinit'.
			static DateTime()
			{
			}

			// Token: 0x0400013A RID: 314
			public new static readonly Func<object, string> ToString = (object l) => ((DateTimeOffset)l).ToString("r", CultureInfo.InvariantCulture);

			// Token: 0x02000057 RID: 87
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06000349 RID: 841 RVA: 0x0000B996 File Offset: 0x00009B96
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x0600034A RID: 842 RVA: 0x000022B8 File Offset: 0x000004B8
				public <>c()
				{
				}

				// Token: 0x0600034B RID: 843 RVA: 0x0000B9A4 File Offset: 0x00009BA4
				internal string <.cctor>b__2_0(object l)
				{
					return ((DateTimeOffset)l).ToString("r", CultureInfo.InvariantCulture);
				}

				// Token: 0x0400013B RID: 315
				public static readonly Parser.DateTime.<>c <>9 = new Parser.DateTime.<>c();
			}
		}

		// Token: 0x02000058 RID: 88
		public static class EmailAddress
		{
			// Token: 0x0600034C RID: 844 RVA: 0x0000B9CC File Offset: 0x00009BCC
			public static bool TryParse(string input, out string result)
			{
				bool result2;
				try
				{
					new MailAddress(input);
					result = input;
					result2 = true;
				}
				catch
				{
					result = null;
					result2 = false;
				}
				return result2;
			}
		}

		// Token: 0x02000059 RID: 89
		public static class Host
		{
			// Token: 0x0600034D RID: 845 RVA: 0x0000BA00 File Offset: 0x00009C00
			public static bool TryParse(string input, out string result)
			{
				result = input;
				System.Uri uri;
				return System.Uri.TryCreate("http://u@" + input + "/", UriKind.Absolute, out uri);
			}
		}

		// Token: 0x0200005A RID: 90
		public static class Int
		{
			// Token: 0x0600034E RID: 846 RVA: 0x0000BA28 File Offset: 0x00009C28
			public static bool TryParse(string input, out int result)
			{
				return int.TryParse(input, NumberStyles.None, CultureInfo.InvariantCulture, out result);
			}
		}

		// Token: 0x0200005B RID: 91
		public static class Long
		{
			// Token: 0x0600034F RID: 847 RVA: 0x0000BA37 File Offset: 0x00009C37
			public static bool TryParse(string input, out long result)
			{
				return long.TryParse(input, NumberStyles.None, CultureInfo.InvariantCulture, out result);
			}
		}

		// Token: 0x0200005C RID: 92
		public static class MD5
		{
			// Token: 0x06000350 RID: 848 RVA: 0x0000BA48 File Offset: 0x00009C48
			public static bool TryParse(string input, out byte[] result)
			{
				bool result2;
				try
				{
					result = Convert.FromBase64String(input);
					result2 = true;
				}
				catch
				{
					result = null;
					result2 = false;
				}
				return result2;
			}

			// Token: 0x06000351 RID: 849 RVA: 0x0000BA7C File Offset: 0x00009C7C
			// Note: this type is marked as 'beforefieldinit'.
			static MD5()
			{
			}

			// Token: 0x0400013C RID: 316
			public new static readonly Func<object, string> ToString = (object l) => Convert.ToBase64String((byte[])l);

			// Token: 0x0200005D RID: 93
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06000352 RID: 850 RVA: 0x0000BA93 File Offset: 0x00009C93
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06000353 RID: 851 RVA: 0x000022B8 File Offset: 0x000004B8
				public <>c()
				{
				}

				// Token: 0x06000354 RID: 852 RVA: 0x0000BA9F File Offset: 0x00009C9F
				internal string <.cctor>b__2_0(object l)
				{
					return Convert.ToBase64String((byte[])l);
				}

				// Token: 0x0400013D RID: 317
				public static readonly Parser.MD5.<>c <>9 = new Parser.MD5.<>c();
			}
		}

		// Token: 0x0200005E RID: 94
		public static class TimeSpanSeconds
		{
			// Token: 0x06000355 RID: 853 RVA: 0x0000BAAC File Offset: 0x00009CAC
			public static bool TryParse(string input, out TimeSpan result)
			{
				int num;
				if (Parser.Int.TryParse(input, out num))
				{
					result = TimeSpan.FromSeconds((double)num);
					return true;
				}
				result = TimeSpan.Zero;
				return false;
			}
		}

		// Token: 0x0200005F RID: 95
		public static class Uri
		{
			// Token: 0x06000356 RID: 854 RVA: 0x0000BADE File Offset: 0x00009CDE
			public static bool TryParse(string input, out System.Uri result)
			{
				return System.Uri.TryCreate(input, UriKind.RelativeOrAbsolute, out result);
			}

			// Token: 0x06000357 RID: 855 RVA: 0x0000BAE8 File Offset: 0x00009CE8
			public static void Check(string s)
			{
				if (s == null)
				{
					throw new ArgumentNullException();
				}
				System.Uri uri;
				if (Parser.Uri.TryParse(s, out uri))
				{
					return;
				}
				if (s.Length == 0)
				{
					throw new ArgumentException();
				}
				throw new FormatException(s);
			}
		}
	}
}
