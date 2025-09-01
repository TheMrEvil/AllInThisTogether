using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000E4 RID: 228
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GameOverlayActivated_t : ICallbackData
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x00015385 File Offset: 0x00013585
		public int DataSize
		{
			get
			{
				return GameOverlayActivated_t._datasize;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0001538C File Offset: 0x0001358C
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GameOverlayActivated;
			}
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00015393 File Offset: 0x00013593
		// Note: this type is marked as 'beforefieldinit'.
		static GameOverlayActivated_t()
		{
		}

		// Token: 0x04000814 RID: 2068
		internal byte Active;

		// Token: 0x04000815 RID: 2069
		public static int _datasize = Marshal.SizeOf(typeof(GameOverlayActivated_t));
	}
}
