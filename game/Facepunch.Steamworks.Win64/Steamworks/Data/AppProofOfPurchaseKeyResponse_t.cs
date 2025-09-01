using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x02000140 RID: 320
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AppProofOfPurchaseKeyResponse_t : ICallbackData
	{
		// Token: 0x06000C3A RID: 3130 RVA: 0x00016227 File Offset: 0x00014427
		internal string KeyUTF8()
		{
			return Encoding.UTF8.GetString(this.Key, 0, Array.IndexOf<byte>(this.Key, 0));
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x00016246 File Offset: 0x00014446
		public int DataSize
		{
			get
			{
				return AppProofOfPurchaseKeyResponse_t._datasize;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x0001624D File Offset: 0x0001444D
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.AppProofOfPurchaseKeyResponse;
			}
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00016254 File Offset: 0x00014454
		// Note: this type is marked as 'beforefieldinit'.
		static AppProofOfPurchaseKeyResponse_t()
		{
		}

		// Token: 0x04000985 RID: 2437
		internal Result Result;

		// Token: 0x04000986 RID: 2438
		internal uint AppID;

		// Token: 0x04000987 RID: 2439
		internal uint CchKeyLength;

		// Token: 0x04000988 RID: 2440
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 240)]
		internal byte[] Key;

		// Token: 0x04000989 RID: 2441
		public static int _datasize = Marshal.SizeOf(typeof(AppProofOfPurchaseKeyResponse_t));
	}
}
