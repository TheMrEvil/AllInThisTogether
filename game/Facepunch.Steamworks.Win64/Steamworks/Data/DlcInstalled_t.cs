using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200013D RID: 317
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DlcInstalled_t : ICallbackData
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x000161BB File Offset: 0x000143BB
		public int DataSize
		{
			get
			{
				return DlcInstalled_t._datasize;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x000161C2 File Offset: 0x000143C2
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.DlcInstalled;
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x000161C9 File Offset: 0x000143C9
		// Note: this type is marked as 'beforefieldinit'.
		static DlcInstalled_t()
		{
		}

		// Token: 0x0400097F RID: 2431
		internal AppId AppID;

		// Token: 0x04000980 RID: 2432
		public static int _datasize = Marshal.SizeOf(typeof(DlcInstalled_t));
	}
}
