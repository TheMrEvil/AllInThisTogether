using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000F7 RID: 247
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LowBatteryPower_t : ICallbackData
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0001568E File Offset: 0x0001388E
		public int DataSize
		{
			get
			{
				return LowBatteryPower_t._datasize;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x00015695 File Offset: 0x00013895
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.LowBatteryPower;
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0001569C File Offset: 0x0001389C
		// Note: this type is marked as 'beforefieldinit'.
		static LowBatteryPower_t()
		{
		}

		// Token: 0x04000852 RID: 2130
		internal byte MinutesBatteryLeft;

		// Token: 0x04000853 RID: 2131
		public static int _datasize = Marshal.SizeOf(typeof(LowBatteryPower_t));
	}
}
