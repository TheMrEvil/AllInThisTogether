using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000E2 RID: 226
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DurationControl_t : ICallbackData
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0001533D File Offset: 0x0001353D
		public int DataSize
		{
			get
			{
				return DurationControl_t._datasize;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00015344 File Offset: 0x00013544
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.DurationControl;
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0001534B File Offset: 0x0001354B
		// Note: this type is marked as 'beforefieldinit'.
		static DurationControl_t()
		{
		}

		// Token: 0x04000808 RID: 2056
		internal Result Result;

		// Token: 0x04000809 RID: 2057
		internal AppId Appid;

		// Token: 0x0400080A RID: 2058
		[MarshalAs(UnmanagedType.I1)]
		internal bool Applicable;

		// Token: 0x0400080B RID: 2059
		internal int CsecsLast5h;

		// Token: 0x0400080C RID: 2060
		internal DurationControlProgress Progress;

		// Token: 0x0400080D RID: 2061
		internal DurationControlNotification Otification;

		// Token: 0x0400080E RID: 2062
		internal int CsecsToday;

		// Token: 0x0400080F RID: 2063
		internal int CsecsRemaining;

		// Token: 0x04000810 RID: 2064
		public static int _datasize = Marshal.SizeOf(typeof(DurationControl_t));
	}
}
