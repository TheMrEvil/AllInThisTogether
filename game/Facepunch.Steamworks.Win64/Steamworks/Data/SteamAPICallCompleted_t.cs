using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000F8 RID: 248
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamAPICallCompleted_t : ICallbackData
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x000156B2 File Offset: 0x000138B2
		public int DataSize
		{
			get
			{
				return SteamAPICallCompleted_t._datasize;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x000156B9 File Offset: 0x000138B9
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamAPICallCompleted;
			}
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x000156C0 File Offset: 0x000138C0
		// Note: this type is marked as 'beforefieldinit'.
		static SteamAPICallCompleted_t()
		{
		}

		// Token: 0x04000854 RID: 2132
		internal ulong AsyncCall;

		// Token: 0x04000855 RID: 2133
		internal int Callback;

		// Token: 0x04000856 RID: 2134
		internal uint ParamCount;

		// Token: 0x04000857 RID: 2135
		public static int _datasize = Marshal.SizeOf(typeof(SteamAPICallCompleted_t));
	}
}
