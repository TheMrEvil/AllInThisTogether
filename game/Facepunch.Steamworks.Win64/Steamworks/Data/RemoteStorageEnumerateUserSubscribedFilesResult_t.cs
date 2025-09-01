using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200011E RID: 286
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageEnumerateUserSubscribedFilesResult_t : ICallbackData
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x00015C67 File Offset: 0x00013E67
		public int DataSize
		{
			get
			{
				return RemoteStorageEnumerateUserSubscribedFilesResult_t._datasize;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x00015C6E File Offset: 0x00013E6E
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.RemoteStorageEnumerateUserSubscribedFilesResult;
			}
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00015C75 File Offset: 0x00013E75
		// Note: this type is marked as 'beforefieldinit'.
		static RemoteStorageEnumerateUserSubscribedFilesResult_t()
		{
		}

		// Token: 0x040008EA RID: 2282
		internal Result Result;

		// Token: 0x040008EB RID: 2283
		internal int ResultsReturned;

		// Token: 0x040008EC RID: 2284
		internal int TotalResultCount;

		// Token: 0x040008ED RID: 2285
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal PublishedFileId[] GPublishedFileId;

		// Token: 0x040008EE RID: 2286
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
		internal uint[] GRTimeSubscribed;

		// Token: 0x040008EF RID: 2287
		public static int _datasize = Marshal.SizeOf(typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t));
	}
}
