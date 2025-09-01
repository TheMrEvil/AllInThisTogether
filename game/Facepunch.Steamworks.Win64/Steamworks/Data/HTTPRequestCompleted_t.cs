using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000156 RID: 342
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTTPRequestCompleted_t : ICallbackData
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0001655E File Offset: 0x0001475E
		public int DataSize
		{
			get
			{
				return HTTPRequestCompleted_t._datasize;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00016565 File Offset: 0x00014765
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTTPRequestCompleted;
			}
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0001656C File Offset: 0x0001476C
		// Note: this type is marked as 'beforefieldinit'.
		static HTTPRequestCompleted_t()
		{
		}

		// Token: 0x040009AF RID: 2479
		internal uint Request;

		// Token: 0x040009B0 RID: 2480
		internal ulong ContextValue;

		// Token: 0x040009B1 RID: 2481
		[MarshalAs(UnmanagedType.I1)]
		internal bool RequestSuccessful;

		// Token: 0x040009B2 RID: 2482
		internal HTTPStatusCode StatusCode;

		// Token: 0x040009B3 RID: 2483
		internal uint BodySize;

		// Token: 0x040009B4 RID: 2484
		public static int _datasize = Marshal.SizeOf(typeof(HTTPRequestCompleted_t));
	}
}
