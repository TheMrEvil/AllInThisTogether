using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000197 RID: 407
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GSPolicyResponse_t : ICallbackData
	{
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x00016F5B File Offset: 0x0001515B
		public int DataSize
		{
			get
			{
				return GSPolicyResponse_t._datasize;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00016F62 File Offset: 0x00015162
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GSPolicyResponse;
			}
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x00016F66 File Offset: 0x00015166
		// Note: this type is marked as 'beforefieldinit'.
		static GSPolicyResponse_t()
		{
		}

		// Token: 0x04000AAA RID: 2730
		internal byte Secure;

		// Token: 0x04000AAB RID: 2731
		public static int _datasize = Marshal.SizeOf(typeof(GSPolicyResponse_t));
	}
}
