using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x020000E0 RID: 224
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct StoreAuthURLResponse_t : ICallbackData
	{
		// Token: 0x06000B0B RID: 2827 RVA: 0x000152D6 File Offset: 0x000134D6
		internal string URLUTF8()
		{
			return Encoding.UTF8.GetString(this.URL, 0, Array.IndexOf<byte>(this.URL, 0));
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x000152F5 File Offset: 0x000134F5
		public int DataSize
		{
			get
			{
				return StoreAuthURLResponse_t._datasize;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x000152FC File Offset: 0x000134FC
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.StoreAuthURLResponse;
			}
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00015303 File Offset: 0x00013503
		// Note: this type is marked as 'beforefieldinit'.
		static StoreAuthURLResponse_t()
		{
		}

		// Token: 0x04000800 RID: 2048
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
		internal byte[] URL;

		// Token: 0x04000801 RID: 2049
		public static int _datasize = Marshal.SizeOf(typeof(StoreAuthURLResponse_t));
	}
}
