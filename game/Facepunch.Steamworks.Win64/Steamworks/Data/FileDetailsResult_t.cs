using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000141 RID: 321
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FileDetailsResult_t : ICallbackData
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x0001626A File Offset: 0x0001446A
		public int DataSize
		{
			get
			{
				return FileDetailsResult_t._datasize;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x00016271 File Offset: 0x00014471
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.FileDetailsResult;
			}
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00016278 File Offset: 0x00014478
		// Note: this type is marked as 'beforefieldinit'.
		static FileDetailsResult_t()
		{
		}

		// Token: 0x0400098A RID: 2442
		internal Result Result;

		// Token: 0x0400098B RID: 2443
		internal ulong FileSize;

		// Token: 0x0400098C RID: 2444
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
		internal byte[] FileSHA;

		// Token: 0x0400098D RID: 2445
		internal uint Flags;

		// Token: 0x0400098E RID: 2446
		public static int _datasize = Marshal.SizeOf(typeof(FileDetailsResult_t));
	}
}
