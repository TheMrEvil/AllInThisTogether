using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000114 RID: 276
	internal struct ActiveBeaconsUpdated_t : ICallbackData
	{
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00015AC1 File Offset: 0x00013CC1
		public int DataSize
		{
			get
			{
				return ActiveBeaconsUpdated_t._datasize;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x00015AC8 File Offset: 0x00013CC8
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.ActiveBeaconsUpdated;
			}
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00015ACF File Offset: 0x00013CCF
		// Note: this type is marked as 'beforefieldinit'.
		static ActiveBeaconsUpdated_t()
		{
		}

		// Token: 0x040008C5 RID: 2245
		public static int _datasize = Marshal.SizeOf(typeof(ActiveBeaconsUpdated_t));
	}
}
