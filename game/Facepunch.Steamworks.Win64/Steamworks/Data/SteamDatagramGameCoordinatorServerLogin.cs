using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x020001B3 RID: 435
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamDatagramGameCoordinatorServerLogin
	{
		// Token: 0x06000DB7 RID: 3511 RVA: 0x000178DF File Offset: 0x00015ADF
		internal string AppDataUTF8()
		{
			return Encoding.UTF8.GetString(this.AppData, 0, Array.IndexOf<byte>(this.AppData, 0));
		}

		// Token: 0x04000B9D RID: 2973
		internal NetIdentity Dentity;

		// Token: 0x04000B9E RID: 2974
		internal SteamDatagramHostedAddress Outing;

		// Token: 0x04000B9F RID: 2975
		internal AppId AppID;

		// Token: 0x04000BA0 RID: 2976
		internal uint Time;

		// Token: 0x04000BA1 RID: 2977
		internal int CbAppData;

		// Token: 0x04000BA2 RID: 2978
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2048)]
		internal byte[] AppData;
	}
}
