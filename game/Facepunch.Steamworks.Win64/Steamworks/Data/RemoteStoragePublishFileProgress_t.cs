using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200012D RID: 301
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStoragePublishFileProgress_t : ICallbackData
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00015F3D File Offset: 0x0001413D
		public int DataSize
		{
			get
			{
				return RemoteStoragePublishFileProgress_t._datasize;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x00015F44 File Offset: 0x00014144
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStoragePublishFileProgress;
			}
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00015F4B File Offset: 0x0001414B
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStoragePublishFileProgress_t()
		{
		}

		// Token: 0x04000943 RID: 2371
		internal double DPercentFile;

		// Token: 0x04000944 RID: 2372
		[MarshalAs(UnmanagedType.I1)]
		internal bool Preview;

		// Token: 0x04000945 RID: 2373
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStoragePublishFileProgress_t));
	}
}
