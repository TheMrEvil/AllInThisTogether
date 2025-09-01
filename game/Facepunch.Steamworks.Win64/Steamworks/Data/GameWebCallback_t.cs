using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x020000DF RID: 223
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GameWebCallback_t : ICallbackData
	{
		// Token: 0x06000B07 RID: 2823 RVA: 0x00015293 File Offset: 0x00013493
		internal string URLUTF8()
		{
			return Encoding.UTF8.GetString(this.URL, 0, Array.IndexOf<byte>(this.URL, 0));
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x000152B2 File Offset: 0x000134B2
		public int DataSize
		{
			get
			{
				return GameWebCallback_t._datasize;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x000152B9 File Offset: 0x000134B9
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GameWebCallback;
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x000152C0 File Offset: 0x000134C0
		// Note: this type is marked as 'beforefieldinit'.
		static GameWebCallback_t()
		{
		}

		// Token: 0x040007FE RID: 2046
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		internal byte[] URL;

		// Token: 0x040007FF RID: 2047
		public static int _datasize = Marshal.SizeOf(typeof(GameWebCallback_t));
	}
}
