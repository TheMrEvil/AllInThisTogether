using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200019A RID: 410
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GSReputation_t : ICallbackData
	{
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x00016FC4 File Offset: 0x000151C4
		public int DataSize
		{
			get
			{
				return GSReputation_t._datasize;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x00016FCB File Offset: 0x000151CB
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GSReputation;
			}
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00016FD2 File Offset: 0x000151D2
		// Note: this type is marked as 'beforefieldinit'.
		static GSReputation_t()
		{
		}

		// Token: 0x04000AB6 RID: 2742
		internal Result Result;

		// Token: 0x04000AB7 RID: 2743
		internal uint ReputationScore;

		// Token: 0x04000AB8 RID: 2744
		[MarshalAs(UnmanagedType.I1)]
		internal bool Banned;

		// Token: 0x04000AB9 RID: 2745
		internal uint BannedIP;

		// Token: 0x04000ABA RID: 2746
		internal ushort BannedPort;

		// Token: 0x04000ABB RID: 2747
		internal ulong BannedGameID;

		// Token: 0x04000ABC RID: 2748
		internal uint BanExpires;

		// Token: 0x04000ABD RID: 2749
		public static int _datasize = Marshal.SizeOf(typeof(GSReputation_t));
	}
}
