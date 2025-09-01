using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000DA RID: 218
	internal struct LicensesUpdated_t : ICallbackData
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x000151E2 File Offset: 0x000133E2
		public int DataSize
		{
			get
			{
				return LicensesUpdated_t._datasize;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x000151E9 File Offset: 0x000133E9
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.LicensesUpdated;
			}
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x000151ED File Offset: 0x000133ED
		// Note: this type is marked as 'beforefieldinit'.
		static LicensesUpdated_t()
		{
		}

		// Token: 0x040007F0 RID: 2032
		public static int _datasize = Marshal.SizeOf(typeof(LicensesUpdated_t));
	}
}
