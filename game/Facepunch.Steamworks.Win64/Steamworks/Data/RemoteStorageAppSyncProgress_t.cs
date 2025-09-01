using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x02000117 RID: 279
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageAppSyncProgress_t : ICallbackData
	{
		// Token: 0x06000BB5 RID: 2997 RVA: 0x00015B2D File Offset: 0x00013D2D
		internal string CurrentFileUTF8()
		{
			return Encoding.UTF8.GetString(this.CurrentFile, 0, Array.IndexOf<byte>(this.CurrentFile, 0));
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x00015B4C File Offset: 0x00013D4C
		public int DataSize
		{
			get
			{
				return RemoteStorageAppSyncProgress_t._datasize;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x00015B53 File Offset: 0x00013D53
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageAppSyncProgress;
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00015B5A File Offset: 0x00013D5A
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageAppSyncProgress_t()
		{
		}

		// Token: 0x040008CE RID: 2254
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 260)]
		internal byte[] CurrentFile;

		// Token: 0x040008CF RID: 2255
		internal AppId AppID;

		// Token: 0x040008D0 RID: 2256
		internal uint BytesTransferredThisChunk;

		// Token: 0x040008D1 RID: 2257
		internal double DAppPercentComplete;

		// Token: 0x040008D2 RID: 2258
		[MarshalAs(UnmanagedType.I1)]
		internal bool Uploading;

		// Token: 0x040008D3 RID: 2259
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageAppSyncProgress_t));
	}
}
