using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x02000159 RID: 345
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamUGCQueryCompleted_t : ICallbackData
	{
		// Token: 0x06000C86 RID: 3206 RVA: 0x000165CA File Offset: 0x000147CA
		internal string NextCursorUTF8()
		{
			return Encoding.UTF8.GetString(this.NextCursor, 0, Array.IndexOf<byte>(this.NextCursor, 0));
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x000165E9 File Offset: 0x000147E9
		public int DataSize
		{
			get
			{
				return SteamUGCQueryCompleted_t._datasize;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x000165F0 File Offset: 0x000147F0
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamUGCQueryCompleted;
			}
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x000165F7 File Offset: 0x000147F7
		// Note: this type is marked as 'beforefieldinit'.
		static SteamUGCQueryCompleted_t()
		{
		}

		// Token: 0x040009BD RID: 2493
		internal ulong Handle;

		// Token: 0x040009BE RID: 2494
		internal Result Result;

		// Token: 0x040009BF RID: 2495
		internal uint NumResultsReturned;

		// Token: 0x040009C0 RID: 2496
		internal uint TotalMatchingResults;

		// Token: 0x040009C1 RID: 2497
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData;

		// Token: 0x040009C2 RID: 2498
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		internal byte[] NextCursor;

		// Token: 0x040009C3 RID: 2499
		public static int _datasize = Marshal.SizeOf(typeof(SteamUGCQueryCompleted_t));
	}
}
