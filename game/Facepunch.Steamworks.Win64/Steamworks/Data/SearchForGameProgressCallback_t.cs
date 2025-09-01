using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000108 RID: 264
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct SearchForGameProgressCallback_t : ICallbackData
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x000158F2 File Offset: 0x00013AF2
		public int DataSize
		{
			get
			{
				return SearchForGameProgressCallback_t._datasize;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x000158F9 File Offset: 0x00013AF9
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SearchForGameProgressCallback;
			}
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00015900 File Offset: 0x00013B00
		// Note: this type is marked as 'beforefieldinit'.
		static SearchForGameProgressCallback_t()
		{
		}

		// Token: 0x04000890 RID: 2192
		internal ulong LSearchID;

		// Token: 0x04000891 RID: 2193
		internal Result Result;

		// Token: 0x04000892 RID: 2194
		internal ulong LobbyID;

		// Token: 0x04000893 RID: 2195
		internal ulong SteamIDEndedSearch;

		// Token: 0x04000894 RID: 2196
		internal int SecondsRemainingEstimate;

		// Token: 0x04000895 RID: 2197
		internal int CPlayersSearching;

		// Token: 0x04000896 RID: 2198
		public static int _datasize = Marshal.SizeOf(typeof(SearchForGameProgressCallback_t));
	}
}
