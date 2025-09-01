using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000FA RID: 250
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CheckFileSignature_t : ICallbackData
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x000156FA File Offset: 0x000138FA
		public int DataSize
		{
			get
			{
				return CheckFileSignature_t._datasize;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x00015701 File Offset: 0x00013901
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.CheckFileSignature;
			}
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00015708 File Offset: 0x00013908
		// Note: this type is marked as 'beforefieldinit'.
		static CheckFileSignature_t()
		{
		}

		// Token: 0x04000859 RID: 2137
		internal CheckFileSignature CheckFileSignature;

		// Token: 0x0400085A RID: 2138
		public static int _datasize = Marshal.SizeOf(typeof(CheckFileSignature_t));
	}
}
