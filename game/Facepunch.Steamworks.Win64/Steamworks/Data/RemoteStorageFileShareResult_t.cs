using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x02000119 RID: 281
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageFileShareResult_t : ICallbackData
	{
		// Token: 0x06000BBC RID: 3004 RVA: 0x00015B94 File Offset: 0x00013D94
		internal string FilenameUTF8()
		{
			return Encoding.UTF8.GetString(this.Filename, 0, Array.IndexOf<byte>(this.Filename, 0));
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x00015BB3 File Offset: 0x00013DB3
		public int DataSize
		{
			get
			{
				return RemoteStorageFileShareResult_t._datasize;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x00015BBA File Offset: 0x00013DBA
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageFileShareResult;
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00015BC1 File Offset: 0x00013DC1
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageFileShareResult_t()
		{
		}

		// Token: 0x040008D7 RID: 2263
		internal Result Result;

		// Token: 0x040008D8 RID: 2264
		internal ulong File;

		// Token: 0x040008D9 RID: 2265
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
		internal byte[] Filename;

		// Token: 0x040008DA RID: 2266
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageFileShareResult_t));
	}
}
