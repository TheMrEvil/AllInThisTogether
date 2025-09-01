using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace QFSW.QC
{
	// Token: 0x02000031 RID: 49
	public static class QuantumMacros
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00006C4F File Offset: 0x00004E4F
		public static IReadOnlyDictionary<string, string> GetMacros()
		{
			return QuantumMacros._macroTable;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006C58 File Offset: 0x00004E58
		public static string ExpandMacros(string text, int maximumExpansions = 1000)
		{
			if (QuantumMacros._macroTable.Count == 0)
			{
				return text;
			}
			KeyValuePair<string, string>[] array = null;
			int num = 0;
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] == '#')
				{
					if (array == null)
					{
						array = (from x in QuantumMacros._macroTable
						orderby x.Key.Length descending
						select x).ToArray<KeyValuePair<string, string>>();
					}
					foreach (KeyValuePair<string, string> keyValuePair in array)
					{
						string key = keyValuePair.Key;
						int length = key.Length;
						if (i + length < text.Length && string.CompareOrdinal(text, i + 1, key, 0, length) == 0)
						{
							if (num >= maximumExpansions)
							{
								throw new ArgumentException(string.Format("Maximum macro expansions of {0} was exhausted: infinitely recursive macro is likely.", maximumExpansions));
							}
							string str = text.Substring(0, i);
							string str2 = text.Substring(i + 1 + length);
							text = str + keyValuePair.Value + str2;
							num++;
							i--;
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006D69 File Offset: 0x00004F69
		// Note: this type is marked as 'beforefieldinit'.
		static QuantumMacros()
		{
		}

		// Token: 0x040000EB RID: 235
		private static readonly Dictionary<string, string> _macroTable = new Dictionary<string, string>();

		// Token: 0x02000094 RID: 148
		private class MacroPreprocessor : IQcPreprocessor
		{
			// Token: 0x17000071 RID: 113
			// (get) Token: 0x060002EA RID: 746 RVA: 0x0000B91E File Offset: 0x00009B1E
			public int Priority
			{
				get
				{
					return 1000;
				}
			}

			// Token: 0x060002EB RID: 747 RVA: 0x0000B925 File Offset: 0x00009B25
			public string Process(string text)
			{
				if (!text.StartsWith("#define", StringComparison.CurrentCulture))
				{
					text = QuantumMacros.ExpandMacros(text, 1000);
				}
				return text;
			}

			// Token: 0x060002EC RID: 748 RVA: 0x0000B943 File Offset: 0x00009B43
			public MacroPreprocessor()
			{
			}
		}

		// Token: 0x02000095 RID: 149
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060002ED RID: 749 RVA: 0x0000B94B File Offset: 0x00009B4B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060002EE RID: 750 RVA: 0x0000B957 File Offset: 0x00009B57
			public <>c()
			{
			}

			// Token: 0x060002EF RID: 751 RVA: 0x0000B95F File Offset: 0x00009B5F
			internal int <ExpandMacros>b__3_0(KeyValuePair<string, string> x)
			{
				return x.Key.Length;
			}

			// Token: 0x040001C8 RID: 456
			public static readonly QuantumMacros.<>c <>9 = new QuantumMacros.<>c();

			// Token: 0x040001C9 RID: 457
			public static Func<KeyValuePair<string, string>, int> <>9__3_0;
		}
	}
}
