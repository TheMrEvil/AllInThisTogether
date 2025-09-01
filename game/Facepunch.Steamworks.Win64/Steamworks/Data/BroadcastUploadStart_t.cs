using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200018B RID: 395
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct BroadcastUploadStart_t : ICallbackData
	{
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x00016D2F File Offset: 0x00014F2F
		public int DataSize
		{
			get
			{
				return BroadcastUploadStart_t._datasize;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x00016D36 File Offset: 0x00014F36
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.BroadcastUploadStart;
			}
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00016D3D File Offset: 0x00014F3D
		// Note: this type is marked as 'beforefieldinit'.
		static BroadcastUploadStart_t()
		{
		}

		// Token: 0x04000A86 RID: 2694
		[MarshalAs(UnmanagedType.I1)]
		internal bool IsRTMP;

		// Token: 0x04000A87 RID: 2695
		public static int _datasize = Marshal.SizeOf(typeof(BroadcastUploadStart_t));
	}
}
