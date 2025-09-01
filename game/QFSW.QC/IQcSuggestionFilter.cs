using System;

namespace QFSW.QC
{
	// Token: 0x02000041 RID: 65
	public interface IQcSuggestionFilter
	{
		// Token: 0x06000169 RID: 361
		bool IsSuggestionPermitted(IQcSuggestion suggestion, SuggestionContext context);
	}
}
