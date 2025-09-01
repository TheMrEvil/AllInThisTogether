using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200014A RID: 330
	internal struct MusicPlayerRemoteToFront_t : ICallbackData
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x000163AE File Offset: 0x000145AE
		public int DataSize
		{
			get
			{
				return MusicPlayerRemoteToFront_t._datasize;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x000163B5 File Offset: 0x000145B5
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.MusicPlayerRemoteToFront;
			}
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x000163BC File Offset: 0x000145BC
		// Note: this type is marked as 'beforefieldinit'.
		static MusicPlayerRemoteToFront_t()
		{
		}

		// Token: 0x0400099D RID: 2461
		public static int _datasize = Marshal.SizeOf(typeof(MusicPlayerRemoteToFront_t));
	}
}
