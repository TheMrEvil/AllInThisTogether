using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000F6 RID: 246
	internal struct IPCountry_t : ICallbackData
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0001566A File Offset: 0x0001386A
		public int DataSize
		{
			get
			{
				return IPCountry_t._datasize;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00015671 File Offset: 0x00013871
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.IPCountry;
			}
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00015678 File Offset: 0x00013878
		// Note: this type is marked as 'beforefieldinit'.
		static IPCountry_t()
		{
		}

		// Token: 0x04000851 RID: 2129
		public static int _datasize = Marshal.SizeOf(typeof(IPCountry_t));
	}
}
