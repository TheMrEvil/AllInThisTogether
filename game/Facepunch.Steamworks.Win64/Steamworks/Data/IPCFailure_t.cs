using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000D9 RID: 217
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IPCFailure_t : ICallbackData
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x000151C1 File Offset: 0x000133C1
		public int DataSize
		{
			get
			{
				return IPCFailure_t._datasize;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x000151C8 File Offset: 0x000133C8
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.IPCFailure;
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000151CC File Offset: 0x000133CC
		// Note: this type is marked as 'beforefieldinit'.
		static IPCFailure_t()
		{
		}

		// Token: 0x040007EE RID: 2030
		internal byte FailureType;

		// Token: 0x040007EF RID: 2031
		public static int _datasize = Marshal.SizeOf(typeof(IPCFailure_t));

		// Token: 0x02000280 RID: 640
		internal enum EFailureType
		{
			// Token: 0x04000EF7 RID: 3831
			FlushedCallbackQueue,
			// Token: 0x04000EF8 RID: 3832
			PipeFail
		}
	}
}
