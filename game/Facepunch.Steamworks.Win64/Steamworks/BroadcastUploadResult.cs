using System;

namespace Steamworks
{
	// Token: 0x02000043 RID: 67
	public enum BroadcastUploadResult
	{
		// Token: 0x040001FE RID: 510
		None,
		// Token: 0x040001FF RID: 511
		OK,
		// Token: 0x04000200 RID: 512
		InitFailed,
		// Token: 0x04000201 RID: 513
		FrameFailed,
		// Token: 0x04000202 RID: 514
		Timeout,
		// Token: 0x04000203 RID: 515
		BandwidthExceeded,
		// Token: 0x04000204 RID: 516
		LowFPS,
		// Token: 0x04000205 RID: 517
		MissingKeyFrames,
		// Token: 0x04000206 RID: 518
		NoConnection,
		// Token: 0x04000207 RID: 519
		RelayFailed,
		// Token: 0x04000208 RID: 520
		SettingsChanged,
		// Token: 0x04000209 RID: 521
		MissingAudio,
		// Token: 0x0400020A RID: 522
		TooFarBehind,
		// Token: 0x0400020B RID: 523
		TranscodeBehind,
		// Token: 0x0400020C RID: 524
		NotAllowedToPlay,
		// Token: 0x0400020D RID: 525
		Busy,
		// Token: 0x0400020E RID: 526
		Banned,
		// Token: 0x0400020F RID: 527
		AlreadyActive,
		// Token: 0x04000210 RID: 528
		ForcedOff,
		// Token: 0x04000211 RID: 529
		AudioBehind,
		// Token: 0x04000212 RID: 530
		Shutdown,
		// Token: 0x04000213 RID: 531
		Disconnect,
		// Token: 0x04000214 RID: 532
		VideoInitFailed,
		// Token: 0x04000215 RID: 533
		AudioInitFailed
	}
}
