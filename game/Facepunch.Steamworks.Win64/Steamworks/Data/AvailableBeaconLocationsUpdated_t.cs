using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000113 RID: 275
	internal struct AvailableBeaconLocationsUpdated_t : ICallbackData
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x00015A9D File Offset: 0x00013C9D
		public int DataSize
		{
			get
			{
				return AvailableBeaconLocationsUpdated_t._datasize;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00015AA4 File Offset: 0x00013CA4
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.AvailableBeaconLocationsUpdated;
			}
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00015AAB File Offset: 0x00013CAB
		// Note: this type is marked as 'beforefieldinit'.
		static AvailableBeaconLocationsUpdated_t()
		{
		}

		// Token: 0x040008C4 RID: 2244
		public static int _datasize = Marshal.SizeOf(typeof(AvailableBeaconLocationsUpdated_t));
	}
}
