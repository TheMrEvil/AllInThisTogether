using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000157 RID: 343
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTTPRequestHeadersReceived_t : ICallbackData
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x00016582 File Offset: 0x00014782
		public int DataSize
		{
			get
			{
				return HTTPRequestHeadersReceived_t._datasize;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00016589 File Offset: 0x00014789
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTTPRequestHeadersReceived;
			}
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00016590 File Offset: 0x00014790
		// Note: this type is marked as 'beforefieldinit'.
		static HTTPRequestHeadersReceived_t()
		{
		}

		// Token: 0x040009B5 RID: 2485
		internal uint Request;

		// Token: 0x040009B6 RID: 2486
		internal ulong ContextValue;

		// Token: 0x040009B7 RID: 2487
		public static int _datasize = Marshal.SizeOf(typeof(HTTPRequestHeadersReceived_t));
	}
}
