using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000137 RID: 311
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct NumberOfCurrentPlayers_t : ICallbackData
	{
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x000160C4 File Offset: 0x000142C4
		public int DataSize
		{
			get
			{
				return NumberOfCurrentPlayers_t._datasize;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x000160CB File Offset: 0x000142CB
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.NumberOfCurrentPlayers;
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x000160D2 File Offset: 0x000142D2
		// Note: this type is marked as 'beforefieldinit'.
		static NumberOfCurrentPlayers_t()
		{
		}

		// Token: 0x0400096C RID: 2412
		internal byte Success;

		// Token: 0x0400096D RID: 2413
		internal int CPlayers;

		// Token: 0x0400096E RID: 2414
		public static int _datasize = Marshal.SizeOf(typeof(NumberOfCurrentPlayers_t));
	}
}
