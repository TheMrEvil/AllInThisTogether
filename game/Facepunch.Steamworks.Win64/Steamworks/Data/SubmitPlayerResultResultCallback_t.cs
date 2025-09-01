using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200010D RID: 269
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct SubmitPlayerResultResultCallback_t : ICallbackData
	{
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x000159A6 File Offset: 0x00013BA6
		public int DataSize
		{
			get
			{
				return SubmitPlayerResultResultCallback_t._datasize;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x000159AD File Offset: 0x00013BAD
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SubmitPlayerResultResultCallback;
			}
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x000159B4 File Offset: 0x00013BB4
		// Note: this type is marked as 'beforefieldinit'.
		static SubmitPlayerResultResultCallback_t()
		{
		}

		// Token: 0x040008B0 RID: 2224
		internal Result Result;

		// Token: 0x040008B1 RID: 2225
		internal ulong UllUniqueGameID;

		// Token: 0x040008B2 RID: 2226
		internal ulong SteamIDPlayer;

		// Token: 0x040008B3 RID: 2227
		public static int _datasize = Marshal.SizeOf(typeof(SubmitPlayerResultResultCallback_t));
	}
}
