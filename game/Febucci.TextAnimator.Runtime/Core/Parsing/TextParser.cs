using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x0200004F RID: 79
	public static class TextParser
	{
		// Token: 0x06000191 RID: 401 RVA: 0x0000785C File Offset: 0x00005A5C
		public static string ParseText(string text, params TagParserBase[] rules)
		{
			if (rules == null || rules.Length == 0)
			{
				Debug.LogWarning("No rules were provided to parse the text. Skipping");
				return text;
			}
			for (int i = 0; i < rules.Length; i++)
			{
				rules[i].Initialize();
			}
			TextParser.<>c__DisplayClass0_0 CS$<>8__locals1;
			CS$<>8__locals1.result = new StringBuilder();
			char[] array = text.ToCharArray();
			int num = array.Length;
			bool flag = true;
			TextParser.<>c__DisplayClass0_1 CS$<>8__locals2;
			CS$<>8__locals2.textIndex = 0;
			int num2 = 0;
			while (CS$<>8__locals2.textIndex < num)
			{
				CS$<>8__locals1.foundTag = false;
				if (array[CS$<>8__locals2.textIndex] == '<')
				{
					TextParser.<>c__DisplayClass0_2 CS$<>8__locals3;
					CS$<>8__locals3.closeIndex = text.IndexOf('>', CS$<>8__locals2.textIndex + 1);
					if (CS$<>8__locals3.closeIndex > 0)
					{
						int length = CS$<>8__locals3.closeIndex - CS$<>8__locals2.textIndex + 1;
						CS$<>8__locals1.fullTag = text.Substring(CS$<>8__locals2.textIndex, length);
						string a = CS$<>8__locals1.fullTag.ToLower();
						if (!(a == "<noparse>"))
						{
							if (a == "</noparse>")
							{
								flag = true;
								TextParser.<ParseText>g__PasteTagToText|0_0(ref CS$<>8__locals1, ref CS$<>8__locals2, ref CS$<>8__locals3);
							}
						}
						else
						{
							flag = false;
							TextParser.<ParseText>g__PasteTagToText|0_0(ref CS$<>8__locals1, ref CS$<>8__locals2, ref CS$<>8__locals3);
						}
					}
				}
				if (flag && !CS$<>8__locals1.foundTag)
				{
					foreach (TagParserBase tagParserBase in rules)
					{
						if (array[CS$<>8__locals2.textIndex] == tagParserBase.startSymbol)
						{
							int num3 = CS$<>8__locals2.textIndex + 1;
							while (num3 < num && !CS$<>8__locals1.foundTag && array[num3] != tagParserBase.startSymbol)
							{
								if (array[num3] == tagParserBase.endSymbol)
								{
									int num4 = num3 - CS$<>8__locals2.textIndex - 1;
									if (num4 == 0)
									{
										break;
									}
									if (tagParserBase.TryProcessingTag(text.Substring(CS$<>8__locals2.textIndex + 1, num4), num4, num2, CS$<>8__locals2.textIndex))
									{
										if (tagParserBase.shouldPasteTag)
										{
											CS$<>8__locals1.result.Append(text.Substring(CS$<>8__locals2.textIndex, num4 + 2));
										}
										CS$<>8__locals1.foundTag = true;
										CS$<>8__locals2.textIndex = num3;
										break;
									}
								}
								num3++;
							}
						}
					}
				}
				if (!CS$<>8__locals1.foundTag)
				{
					CS$<>8__locals1.result.Append(array[CS$<>8__locals2.textIndex]);
					num2++;
				}
				int i = CS$<>8__locals2.textIndex;
				CS$<>8__locals2.textIndex = i + 1;
			}
			return CS$<>8__locals1.result.ToString();
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00007ABA File Offset: 0x00005CBA
		[CompilerGenerated]
		internal static void <ParseText>g__PasteTagToText|0_0(ref TextParser.<>c__DisplayClass0_0 A_0, ref TextParser.<>c__DisplayClass0_1 A_1, ref TextParser.<>c__DisplayClass0_2 A_2)
		{
			A_0.foundTag = true;
			A_0.result.Append(A_0.fullTag);
			A_1.textIndex = A_2.closeIndex;
		}

		// Token: 0x0200005E RID: 94
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass0_0
		{
			// Token: 0x04000154 RID: 340
			public bool foundTag;

			// Token: 0x04000155 RID: 341
			public StringBuilder result;

			// Token: 0x04000156 RID: 342
			public string fullTag;
		}

		// Token: 0x0200005F RID: 95
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass0_1
		{
			// Token: 0x04000157 RID: 343
			public int textIndex;
		}

		// Token: 0x02000060 RID: 96
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass0_2
		{
			// Token: 0x04000158 RID: 344
			public int closeIndex;
		}
	}
}
