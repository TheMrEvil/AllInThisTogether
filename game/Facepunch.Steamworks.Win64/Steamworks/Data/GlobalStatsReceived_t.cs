using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200013C RID: 316
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GlobalStatsReceived_t : ICallbackData
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x00016197 File Offset: 0x00014397
		public int DataSize
		{
			get
			{
				return GlobalStatsReceived_t._datasize;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0001619E File Offset: 0x0001439E
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GlobalStatsReceived;
			}
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x000161A5 File Offset: 0x000143A5
		// Note: this type is marked as 'beforefieldinit'.
		static GlobalStatsReceived_t()
		{
		}

		// Token: 0x0400097C RID: 2428
		internal ulong GameID;

		// Token: 0x0400097D RID: 2429
		internal Result Result;

		// Token: 0x0400097E RID: 2430
		public static int _datasize = Marshal.SizeOf(typeof(GlobalStatsReceived_t));
	}
}
