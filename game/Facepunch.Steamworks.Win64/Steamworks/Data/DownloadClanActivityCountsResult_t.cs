using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000EE RID: 238
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DownloadClanActivityCountsResult_t : ICallbackData
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0001554A File Offset: 0x0001374A
		public int DataSize
		{
			get
			{
				return DownloadClanActivityCountsResult_t._datasize;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00015551 File Offset: 0x00013751
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.DownloadClanActivityCountsResult;
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00015558 File Offset: 0x00013758
		// Note: this type is marked as 'beforefieldinit'.
		static DownloadClanActivityCountsResult_t()
		{
		}

		// Token: 0x04000837 RID: 2103
		[MarshalAs(UnmanagedType.I1)]
		internal bool Success;

		// Token: 0x04000838 RID: 2104
		public static int _datasize = Marshal.SizeOf(typeof(DownloadClanActivityCountsResult_t));
	}
}
