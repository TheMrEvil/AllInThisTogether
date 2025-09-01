using System;

namespace UnityEngine.Android
{
	// Token: 0x0200000F RID: 15
	public enum AndroidAssetPackStatus
	{
		// Token: 0x0400001F RID: 31
		Unknown,
		// Token: 0x04000020 RID: 32
		Pending,
		// Token: 0x04000021 RID: 33
		Downloading,
		// Token: 0x04000022 RID: 34
		Transferring,
		// Token: 0x04000023 RID: 35
		Completed,
		// Token: 0x04000024 RID: 36
		Failed,
		// Token: 0x04000025 RID: 37
		Canceled,
		// Token: 0x04000026 RID: 38
		WaitingForWifi,
		// Token: 0x04000027 RID: 39
		NotInstalled
	}
}
