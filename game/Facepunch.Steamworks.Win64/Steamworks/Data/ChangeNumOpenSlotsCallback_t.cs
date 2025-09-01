using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000112 RID: 274
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ChangeNumOpenSlotsCallback_t : ICallbackData
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x00015A79 File Offset: 0x00013C79
		public int DataSize
		{
			get
			{
				return ChangeNumOpenSlotsCallback_t._datasize;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x00015A80 File Offset: 0x00013C80
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.ChangeNumOpenSlotsCallback;
			}
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00015A87 File Offset: 0x00013C87
		// Note: this type is marked as 'beforefieldinit'.
		static ChangeNumOpenSlotsCallback_t()
		{
		}

		// Token: 0x040008C2 RID: 2242
		internal Result Result;

		// Token: 0x040008C3 RID: 2243
		public static int _datasize = Marshal.SizeOf(typeof(ChangeNumOpenSlotsCallback_t));
	}
}
