using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x020000E5 RID: 229
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GameServerChangeRequested_t : ICallbackData
	{
		// Token: 0x06000B1B RID: 2843 RVA: 0x000153A9 File Offset: 0x000135A9
		internal string ServerUTF8()
		{
			return Encoding.UTF8.GetString(this.Server, 0, Array.IndexOf<byte>(this.Server, 0));
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000153C8 File Offset: 0x000135C8
		internal string PasswordUTF8()
		{
			return Encoding.UTF8.GetString(this.Password, 0, Array.IndexOf<byte>(this.Password, 0));
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x000153E7 File Offset: 0x000135E7
		public int DataSize
		{
			get
			{
				return GameServerChangeRequested_t._datasize;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x000153EE File Offset: 0x000135EE
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GameServerChangeRequested;
			}
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x000153F5 File Offset: 0x000135F5
		// Note: this type is marked as 'beforefieldinit'.
		static GameServerChangeRequested_t()
		{
		}

		// Token: 0x04000816 RID: 2070
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		internal byte[] Server;

		// Token: 0x04000817 RID: 2071
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		internal byte[] Password;

		// Token: 0x04000818 RID: 2072
		public static int _datasize = Marshal.SizeOf(typeof(GameServerChangeRequested_t));
	}
}
