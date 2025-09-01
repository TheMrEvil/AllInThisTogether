using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200013F RID: 319
	internal struct NewUrlLaunchParameters_t : ICallbackData
	{
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x00016203 File Offset: 0x00014403
		public int DataSize
		{
			get
			{
				return NewUrlLaunchParameters_t._datasize;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x0001620A File Offset: 0x0001440A
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.NewUrlLaunchParameters;
			}
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00016211 File Offset: 0x00014411
		// Note: this type is marked as 'beforefieldinit'.
		static NewUrlLaunchParameters_t()
		{
		}

		// Token: 0x04000984 RID: 2436
		public static int _datasize = Marshal.SizeOf(typeof(NewUrlLaunchParameters_t));
	}
}
