using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000F4 RID: 244
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetPersonaNameResponse_t : ICallbackData
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x00015622 File Offset: 0x00013822
		public int DataSize
		{
			get
			{
				return SetPersonaNameResponse_t._datasize;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00015629 File Offset: 0x00013829
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SetPersonaNameResponse;
			}
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00015630 File Offset: 0x00013830
		// Note: this type is marked as 'beforefieldinit'.
		static SetPersonaNameResponse_t()
		{
		}

		// Token: 0x0400084C RID: 2124
		[MarshalAs(UnmanagedType.I1)]
		internal bool Success;

		// Token: 0x0400084D RID: 2125
		[MarshalAs(UnmanagedType.I1)]
		internal bool LocalSuccess;

		// Token: 0x0400084E RID: 2126
		internal Result Result;

		// Token: 0x0400084F RID: 2127
		public static int _datasize = Marshal.SizeOf(typeof(SetPersonaNameResponse_t));
	}
}
