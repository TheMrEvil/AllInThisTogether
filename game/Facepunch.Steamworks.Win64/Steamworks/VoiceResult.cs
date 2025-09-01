using System;

namespace Steamworks
{
	// Token: 0x02000034 RID: 52
	internal enum VoiceResult
	{
		// Token: 0x0400015A RID: 346
		OK,
		// Token: 0x0400015B RID: 347
		NotInitialized,
		// Token: 0x0400015C RID: 348
		NotRecording,
		// Token: 0x0400015D RID: 349
		NoData,
		// Token: 0x0400015E RID: 350
		BufferTooSmall,
		// Token: 0x0400015F RID: 351
		DataCorrupted,
		// Token: 0x04000160 RID: 352
		Restricted,
		// Token: 0x04000161 RID: 353
		UnsupportedCodec,
		// Token: 0x04000162 RID: 354
		ReceiverOutOfDate,
		// Token: 0x04000163 RID: 355
		ReceiverDidNotAnswer
	}
}
