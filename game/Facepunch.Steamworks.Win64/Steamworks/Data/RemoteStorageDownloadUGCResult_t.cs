using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x02000121 RID: 289
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageDownloadUGCResult_t : ICallbackData
	{
		// Token: 0x06000BD5 RID: 3029 RVA: 0x00015CD3 File Offset: 0x00013ED3
		internal string PchFileNameUTF8()
		{
			return Encoding.UTF8.GetString(this.PchFileName, 0, Array.IndexOf<byte>(this.PchFileName, 0));
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00015CF2 File Offset: 0x00013EF2
		public int DataSize
		{
			get
			{
				return RemoteStorageDownloadUGCResult_t._datasize;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x00015CF9 File Offset: 0x00013EF9
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageDownloadUGCResult;
			}
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00015D00 File Offset: 0x00013F00
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageDownloadUGCResult_t()
		{
		}

		// Token: 0x040008F7 RID: 2295
		internal Result Result;

		// Token: 0x040008F8 RID: 2296
		internal ulong File;

		// Token: 0x040008F9 RID: 2297
		internal AppId AppID;

		// Token: 0x040008FA RID: 2298
		internal int SizeInBytes;

		// Token: 0x040008FB RID: 2299
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
		internal byte[] PchFileName;

		// Token: 0x040008FC RID: 2300
		internal ulong SteamIDOwner;

		// Token: 0x040008FD RID: 2301
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageDownloadUGCResult_t));
	}
}
