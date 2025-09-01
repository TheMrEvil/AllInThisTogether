using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace QFSW.QC.Suggestors
{
	// Token: 0x0200000B RID: 11
	public class MacroSuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000028B9 File Offset: 0x00000AB9
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.Prompt.StartsWith("#");
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028CB File Offset: 0x00000ACB
		protected override IQcSuggestion ItemToSuggestion(string macro)
		{
			return new RawSuggestion("#" + macro, false);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000028DE File Offset: 0x00000ADE
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			return from x in QuantumMacros.GetMacros()
			select x.Key;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002909 File Offset: 0x00000B09
		public MacroSuggestor()
		{
		}

		// Token: 0x02000017 RID: 23
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000063 RID: 99 RVA: 0x0000305B File Offset: 0x0000125B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000064 RID: 100 RVA: 0x00003067 File Offset: 0x00001267
			public <>c()
			{
			}

			// Token: 0x06000065 RID: 101 RVA: 0x0000306F File Offset: 0x0000126F
			internal string <GetItems>b__2_0(KeyValuePair<string, string> x)
			{
				return x.Key;
			}

			// Token: 0x04000034 RID: 52
			public static readonly MacroSuggestor.<>c <>9 = new MacroSuggestor.<>c();

			// Token: 0x04000035 RID: 53
			public static Func<KeyValuePair<string, string>, string> <>9__2_0;
		}
	}
}
