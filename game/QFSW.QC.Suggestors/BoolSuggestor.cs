using System;
using System.Collections.Generic;

namespace QFSW.QC.Suggestors
{
	// Token: 0x02000002 RID: 2
	public class BoolSuggestor : BasicCachedQcSuggestor<string>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		protected override bool CanProvideSuggestions(SuggestionContext context, SuggestorOptions options)
		{
			return context.TargetType == typeof(bool);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002067 File Offset: 0x00000267
		protected override IQcSuggestion ItemToSuggestion(string value)
		{
			return new RawSuggestion(value, false);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002070 File Offset: 0x00000270
		protected override IEnumerable<string> GetItems(SuggestionContext context, SuggestorOptions options)
		{
			return this._values;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002078 File Offset: 0x00000278
		public BoolSuggestor()
		{
		}

		// Token: 0x04000001 RID: 1
		private readonly string[] _values = new string[]
		{
			"true",
			"false"
		};
	}
}
