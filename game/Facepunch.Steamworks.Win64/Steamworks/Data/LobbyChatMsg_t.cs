using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000101 RID: 257
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyChatMsg_t : ICallbackData
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x000157F6 File Offset: 0x000139F6
		public int DataSize
		{
			get
			{
				return LobbyChatMsg_t._datasize;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x000157FD File Offset: 0x000139FD
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.LobbyChatMsg;
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00015804 File Offset: 0x00013A04
		// Note: this type is marked as 'beforefieldinit'.
		static LobbyChatMsg_t()
		{
		}

		// Token: 0x04000878 RID: 2168
		internal ulong SteamIDLobby;

		// Token: 0x04000879 RID: 2169
		internal ulong SteamIDUser;

		// Token: 0x0400087A RID: 2170
		internal byte ChatEntryType;

		// Token: 0x0400087B RID: 2171
		internal uint ChatID;

		// Token: 0x0400087C RID: 2172
		public static int _datasize = Marshal.SizeOf(typeof(LobbyChatMsg_t));
	}
}
