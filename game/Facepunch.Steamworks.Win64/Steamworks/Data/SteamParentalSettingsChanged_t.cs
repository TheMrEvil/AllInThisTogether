using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200018D RID: 397
	internal struct SteamParentalSettingsChanged_t : ICallbackData
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x00016D77 File Offset: 0x00014F77
		public int DataSize
		{
			get
			{
				return SteamParentalSettingsChanged_t._datasize;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00016D7E File Offset: 0x00014F7E
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamParentalSettingsChanged;
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00016D85 File Offset: 0x00014F85
		// Note: this type is marked as 'beforefieldinit'.
		static SteamParentalSettingsChanged_t()
		{
		}

		// Token: 0x04000A8A RID: 2698
		public static int _datasize = Marshal.SizeOf(typeof(SteamParentalSettingsChanged_t));
	}
}
