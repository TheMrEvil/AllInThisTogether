using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000103 RID: 259
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyMatchList_t : ICallbackData
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0001583E File Offset: 0x00013A3E
		public int DataSize
		{
			get
			{
				return LobbyMatchList_t._datasize;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x00015845 File Offset: 0x00013A45
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.LobbyMatchList;
			}
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0001584C File Offset: 0x00013A4C
		// Note: this type is marked as 'beforefieldinit'.
		static LobbyMatchList_t()
		{
		}

		// Token: 0x04000882 RID: 2178
		internal uint LobbiesMatching;

		// Token: 0x04000883 RID: 2179
		public static int _datasize = Marshal.SizeOf(typeof(LobbyMatchList_t));
	}
}
