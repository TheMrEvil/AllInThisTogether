using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000149 RID: 329
	internal struct MusicPlayerRemoteWillDeactivate_t : ICallbackData
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0001638A File Offset: 0x0001458A
		public int DataSize
		{
			get
			{
				return MusicPlayerRemoteWillDeactivate_t._datasize;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x00016391 File Offset: 0x00014591
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.MusicPlayerRemoteWillDeactivate;
			}
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00016398 File Offset: 0x00014598
		// Note: this type is marked as 'beforefieldinit'.
		static MusicPlayerRemoteWillDeactivate_t()
		{
		}

		// Token: 0x0400099C RID: 2460
		public static int _datasize = Marshal.SizeOf(typeof(MusicPlayerRemoteWillDeactivate_t));
	}
}
