using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000110 RID: 272
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateBeaconCallback_t : ICallbackData
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x00015A31 File Offset: 0x00013C31
		public int DataSize
		{
			get
			{
				return CreateBeaconCallback_t._datasize;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x00015A38 File Offset: 0x00013C38
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.CreateBeaconCallback;
			}
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00015A3F File Offset: 0x00013C3F
		// Note: this type is marked as 'beforefieldinit'.
		static CreateBeaconCallback_t()
		{
		}

		// Token: 0x040008BC RID: 2236
		internal Result Result;

		// Token: 0x040008BD RID: 2237
		internal ulong BeaconID;

		// Token: 0x040008BE RID: 2238
		public static int _datasize = Marshal.SizeOf(typeof(CreateBeaconCallback_t));
	}
}
