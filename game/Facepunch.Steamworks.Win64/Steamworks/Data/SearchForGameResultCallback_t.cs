using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000109 RID: 265
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct SearchForGameResultCallback_t : ICallbackData
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x00015916 File Offset: 0x00013B16
		public int DataSize
		{
			get
			{
				return SearchForGameResultCallback_t._datasize;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0001591D File Offset: 0x00013B1D
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SearchForGameResultCallback;
			}
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00015924 File Offset: 0x00013B24
		// Note: this type is marked as 'beforefieldinit'.
		static SearchForGameResultCallback_t()
		{
		}

		// Token: 0x04000897 RID: 2199
		internal ulong LSearchID;

		// Token: 0x04000898 RID: 2200
		internal Result Result;

		// Token: 0x04000899 RID: 2201
		internal int CountPlayersInGame;

		// Token: 0x0400089A RID: 2202
		internal int CountAcceptedGame;

		// Token: 0x0400089B RID: 2203
		internal ulong SteamIDHost;

		// Token: 0x0400089C RID: 2204
		[MarshalAs(UnmanagedType.I1)]
		internal bool FinalCallback;

		// Token: 0x0400089D RID: 2205
		public static int _datasize = Marshal.SizeOf(typeof(SearchForGameResultCallback_t));
	}
}
