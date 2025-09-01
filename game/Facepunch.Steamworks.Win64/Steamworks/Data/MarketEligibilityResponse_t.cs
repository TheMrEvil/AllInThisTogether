using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000E1 RID: 225
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct MarketEligibilityResponse_t : ICallbackData
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00015319 File Offset: 0x00013519
		public int DataSize
		{
			get
			{
				return MarketEligibilityResponse_t._datasize;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00015320 File Offset: 0x00013520
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.MarketEligibilityResponse;
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00015327 File Offset: 0x00013527
		// Note: this type is marked as 'beforefieldinit'.
		static MarketEligibilityResponse_t()
		{
		}

		// Token: 0x04000802 RID: 2050
		[MarshalAs(UnmanagedType.I1)]
		internal bool Allowed;

		// Token: 0x04000803 RID: 2051
		internal MarketNotAllowedReasonFlags NotAllowedReason;

		// Token: 0x04000804 RID: 2052
		internal uint TAllowedAtTime;

		// Token: 0x04000805 RID: 2053
		internal int CdaySteamGuardRequiredDays;

		// Token: 0x04000806 RID: 2054
		internal int CdayNewDeviceCooldown;

		// Token: 0x04000807 RID: 2055
		public static int _datasize = Marshal.SizeOf(typeof(MarketEligibilityResponse_t));
	}
}
