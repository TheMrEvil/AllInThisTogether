using System;

namespace QFSW.QC
{
	// Token: 0x02000040 RID: 64
	public interface IQcSuggestion
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000162 RID: 354
		string FullSignature { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000163 RID: 355
		string PrimarySignature { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000164 RID: 356
		string SecondarySignature { get; }

		// Token: 0x06000165 RID: 357
		bool MatchesPrompt(string prompt);

		// Token: 0x06000166 RID: 358
		string GetCompletion(string prompt);

		// Token: 0x06000167 RID: 359
		string GetCompletionTail(string prompt);

		// Token: 0x06000168 RID: 360
		SuggestionContext? GetInnerSuggestionContext(SuggestionContext context);
	}
}
