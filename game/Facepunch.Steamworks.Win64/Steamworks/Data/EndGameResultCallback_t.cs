using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200010E RID: 270
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EndGameResultCallback_t : ICallbackData
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x000159CA File Offset: 0x00013BCA
		public int DataSize
		{
			get
			{
				return EndGameResultCallback_t._datasize;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x000159D1 File Offset: 0x00013BD1
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.EndGameResultCallback;
			}
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x000159D8 File Offset: 0x00013BD8
		// Note: this type is marked as 'beforefieldinit'.
		static EndGameResultCallback_t()
		{
		}

		// Token: 0x040008B4 RID: 2228
		internal Result Result;

		// Token: 0x040008B5 RID: 2229
		internal ulong UllUniqueGameID;

		// Token: 0x040008B6 RID: 2230
		public static int _datasize = Marshal.SizeOf(typeof(EndGameResultCallback_t));
	}
}
