using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200010C RID: 268
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RequestPlayersForGameFinalResultCallback_t : ICallbackData
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00015982 File Offset: 0x00013B82
		public int DataSize
		{
			get
			{
				return RequestPlayersForGameFinalResultCallback_t._datasize;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x00015989 File Offset: 0x00013B89
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RequestPlayersForGameFinalResultCallback;
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00015990 File Offset: 0x00013B90
		// Note: this type is marked as 'beforefieldinit'.
		static RequestPlayersForGameFinalResultCallback_t()
		{
		}

		// Token: 0x040008AC RID: 2220
		internal Result Result;

		// Token: 0x040008AD RID: 2221
		internal ulong LSearchID;

		// Token: 0x040008AE RID: 2222
		internal ulong LUniqueGameID;

		// Token: 0x040008AF RID: 2223
		public static int _datasize = Marshal.SizeOf(typeof(RequestPlayersForGameFinalResultCallback_t));
	}
}
