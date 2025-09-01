using System;
using System.Text;

namespace System
{
	// Token: 0x02000144 RID: 324
	internal static class PasteArguments
	{
		// Token: 0x060008A4 RID: 2212 RVA: 0x0001FD40 File Offset: 0x0001DF40
		internal static void AppendArgument(StringBuilder stringBuilder, string argument)
		{
			if (stringBuilder.Length != 0)
			{
				stringBuilder.Append(' ');
			}
			if (argument.Length != 0 && PasteArguments.ContainsNoWhitespaceOrQuotes(argument))
			{
				stringBuilder.Append(argument);
				return;
			}
			stringBuilder.Append('"');
			int i = 0;
			while (i < argument.Length)
			{
				char c = argument[i++];
				if (c == '\\')
				{
					int num = 1;
					while (i < argument.Length && argument[i] == '\\')
					{
						i++;
						num++;
					}
					if (i == argument.Length)
					{
						stringBuilder.Append('\\', num * 2);
					}
					else if (argument[i] == '"')
					{
						stringBuilder.Append('\\', num * 2 + 1);
						stringBuilder.Append('"');
						i++;
					}
					else
					{
						stringBuilder.Append('\\', num);
					}
				}
				else if (c == '"')
				{
					stringBuilder.Append('\\');
					stringBuilder.Append('"');
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			stringBuilder.Append('"');
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001FE3C File Offset: 0x0001E03C
		private static bool ContainsNoWhitespaceOrQuotes(string s)
		{
			foreach (char c in s)
			{
				if (char.IsWhiteSpace(c) || c == '"')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000546 RID: 1350
		private const char Quote = '"';

		// Token: 0x04000547 RID: 1351
		private const char Backslash = '\\';
	}
}
