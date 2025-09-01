using System;
using System.Collections.Generic;

namespace QFSW.QC
{
	// Token: 0x02000042 RID: 66
	public interface IQcSuggestor
	{
		// Token: 0x0600016A RID: 362
		IEnumerable<IQcSuggestion> GetSuggestions(SuggestionContext context, SuggestorOptions options);
	}
}
