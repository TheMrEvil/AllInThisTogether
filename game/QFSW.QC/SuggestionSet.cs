using System;
using System.Collections.Generic;

namespace QFSW.QC
{
	// Token: 0x02000047 RID: 71
	public class SuggestionSet
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00007B35 File Offset: 0x00005D35
		public IQcSuggestion CurrentSelection
		{
			get
			{
				if (this.SelectionIndex < 0 || this.SelectionIndex >= this.Suggestions.Count)
				{
					return null;
				}
				return this.Suggestions[this.SelectionIndex];
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007B66 File Offset: 0x00005D66
		public SuggestionSet()
		{
		}

		// Token: 0x0400011A RID: 282
		public SuggestionContext Context;

		// Token: 0x0400011B RID: 283
		public int SelectionIndex;

		// Token: 0x0400011C RID: 284
		public readonly List<IQcSuggestion> Suggestions = new List<IQcSuggestion>();
	}
}
