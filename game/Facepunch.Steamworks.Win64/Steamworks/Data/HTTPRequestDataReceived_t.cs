using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000158 RID: 344
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTTPRequestDataReceived_t : ICallbackData
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x000165A6 File Offset: 0x000147A6
		public int DataSize
		{
			get
			{
				return HTTPRequestDataReceived_t._datasize;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x000165AD File Offset: 0x000147AD
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTTPRequestDataReceived;
			}
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x000165B4 File Offset: 0x000147B4
		// Note: this type is marked as 'beforefieldinit'.
		static HTTPRequestDataReceived_t()
		{
		}

		// Token: 0x040009B8 RID: 2488
		internal uint Request;

		// Token: 0x040009B9 RID: 2489
		internal ulong ContextValue;

		// Token: 0x040009BA RID: 2490
		internal uint COffset;

		// Token: 0x040009BB RID: 2491
		internal uint CBytesReceived;

		// Token: 0x040009BC RID: 2492
		public static int _datasize = Marshal.SizeOf(typeof(HTTPRequestDataReceived_t));
	}
}
