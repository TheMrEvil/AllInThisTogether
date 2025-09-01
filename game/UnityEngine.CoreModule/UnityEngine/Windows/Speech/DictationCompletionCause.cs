using System;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x02000298 RID: 664
	public enum DictationCompletionCause
	{
		// Token: 0x0400094B RID: 2379
		Complete,
		// Token: 0x0400094C RID: 2380
		AudioQualityFailure,
		// Token: 0x0400094D RID: 2381
		Canceled,
		// Token: 0x0400094E RID: 2382
		TimeoutExceeded,
		// Token: 0x0400094F RID: 2383
		PauseLimitExceeded,
		// Token: 0x04000950 RID: 2384
		NetworkFailure,
		// Token: 0x04000951 RID: 2385
		MicrophoneUnavailable,
		// Token: 0x04000952 RID: 2386
		UnknownError
	}
}
