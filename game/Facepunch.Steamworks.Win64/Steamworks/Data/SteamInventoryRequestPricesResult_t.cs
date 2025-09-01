using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x02000188 RID: 392
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamInventoryRequestPricesResult_t : ICallbackData
	{
		// Token: 0x06000D14 RID: 3348 RVA: 0x00016C85 File Offset: 0x00014E85
		internal string CurrencyUTF8()
		{
			return Encoding.UTF8.GetString(this.Currency, 0, Array.IndexOf<byte>(this.Currency, 0));
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x00016CA4 File Offset: 0x00014EA4
		public int DataSize
		{
			get
			{
				return SteamInventoryRequestPricesResult_t._datasize;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x00016CAB File Offset: 0x00014EAB
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamInventoryRequestPricesResult;
			}
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00016CB2 File Offset: 0x00014EB2
		// Note: this type is marked as 'beforefieldinit'.
		static SteamInventoryRequestPricesResult_t()
		{
		}

		// Token: 0x04000A7C RID: 2684
		internal Result Result;

		// Token: 0x04000A7D RID: 2685
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		internal byte[] Currency;

		// Token: 0x04000A7E RID: 2686
		public static int _datasize = Marshal.SizeOf(typeof(SteamInventoryRequestPricesResult_t));
	}
}
