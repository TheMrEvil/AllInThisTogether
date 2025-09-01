using System;
using System.Collections.Generic;

namespace QFSW.QC.Suggestors.Tags
{
	// Token: 0x0200005A RID: 90
	public struct InlineSuggestionsTag : IQcSuggestorTag
	{
		// Token: 0x060001EE RID: 494 RVA: 0x00009B90 File Offset: 0x00007D90
		public InlineSuggestionsTag(IEnumerable<string> suggestions)
		{
			this.Suggestions = suggestions;
		}

		// Token: 0x0400013B RID: 315
		public readonly IEnumerable<string> Suggestions;
	}
}
