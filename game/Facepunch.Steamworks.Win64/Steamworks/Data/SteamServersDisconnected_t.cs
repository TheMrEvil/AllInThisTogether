using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000D7 RID: 215
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamServersDisconnected_t : ICallbackData
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x0001517F File Offset: 0x0001337F
		public int DataSize
		{
			get
			{
				return SteamServersDisconnected_t._datasize;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00015186 File Offset: 0x00013386
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamServersDisconnected;
			}
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0001518A File Offset: 0x0001338A
		// Note: this type is marked as 'beforefieldinit'.
		static SteamServersDisconnected_t()
		{
		}

		// Token: 0x040007E6 RID: 2022
		internal Result Result;

		// Token: 0x040007E7 RID: 2023
		public static int _datasize = Marshal.SizeOf(typeof(SteamServersDisconnected_t));
	}
}
