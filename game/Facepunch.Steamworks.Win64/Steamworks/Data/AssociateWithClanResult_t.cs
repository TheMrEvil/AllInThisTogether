using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200019B RID: 411
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AssociateWithClanResult_t : ICallbackData
	{
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x00016FE8 File Offset: 0x000151E8
		public int DataSize
		{
			get
			{
				return AssociateWithClanResult_t._datasize;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x00016FEF File Offset: 0x000151EF
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.AssociateWithClanResult;
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00016FF6 File Offset: 0x000151F6
		// Note: this type is marked as 'beforefieldinit'.
		static AssociateWithClanResult_t()
		{
		}

		// Token: 0x04000ABE RID: 2750
		internal Result Result;

		// Token: 0x04000ABF RID: 2751
		public static int _datasize = Marshal.SizeOf(typeof(AssociateWithClanResult_t));
	}
}
