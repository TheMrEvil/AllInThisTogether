using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200010A RID: 266
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RequestPlayersForGameProgressCallback_t : ICallbackData
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0001593A File Offset: 0x00013B3A
		public int DataSize
		{
			get
			{
				return RequestPlayersForGameProgressCallback_t._datasize;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x00015941 File Offset: 0x00013B41
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RequestPlayersForGameProgressCallback;
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00015948 File Offset: 0x00013B48
		// Note: this type is marked as 'beforefieldinit'.
		static RequestPlayersForGameProgressCallback_t()
		{
		}

		// Token: 0x0400089E RID: 2206
		internal Result Result;

		// Token: 0x0400089F RID: 2207
		internal ulong LSearchID;

		// Token: 0x040008A0 RID: 2208
		public static int _datasize = Marshal.SizeOf(typeof(RequestPlayersForGameProgressCallback_t));
	}
}
