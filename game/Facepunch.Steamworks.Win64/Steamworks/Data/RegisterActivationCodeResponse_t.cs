using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200013E RID: 318
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RegisterActivationCodeResponse_t : ICallbackData
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x000161DF File Offset: 0x000143DF
		public int DataSize
		{
			get
			{
				return RegisterActivationCodeResponse_t._datasize;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x000161E6 File Offset: 0x000143E6
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RegisterActivationCodeResponse;
			}
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x000161ED File Offset: 0x000143ED
		// Note: this type is marked as 'beforefieldinit'.
		static RegisterActivationCodeResponse_t()
		{
		}

		// Token: 0x04000981 RID: 2433
		internal RegisterActivationCodeResult Result;

		// Token: 0x04000982 RID: 2434
		internal uint PackageRegistered;

		// Token: 0x04000983 RID: 2435
		public static int _datasize = Marshal.SizeOf(typeof(RegisterActivationCodeResponse_t));
	}
}
