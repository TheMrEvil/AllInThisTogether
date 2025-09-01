using System;
using System.Net;

namespace Steamworks
{
	// Token: 0x020000B4 RID: 180
	public struct SteamServerInit
	{
		// Token: 0x060009CF RID: 2511 RVA: 0x00011F64 File Offset: 0x00010164
		public SteamServerInit(string modDir, string gameDesc)
		{
			this.DedicatedServer = true;
			this.ModDir = modDir;
			this.GameDescription = gameDesc;
			this.GamePort = 27015;
			this.QueryPort = 27016;
			this.Secure = true;
			this.VersionString = "1.0.0.0";
			this.IpAddress = null;
			this.SteamPort = 0;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00011FC0 File Offset: 0x000101C0
		public SteamServerInit WithRandomSteamPort()
		{
			this.SteamPort = (ushort)new Random().Next(10000, 60000);
			return this;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00011FF4 File Offset: 0x000101F4
		public SteamServerInit WithQueryShareGamePort()
		{
			this.QueryPort = ushort.MaxValue;
			return this;
		}

		// Token: 0x04000763 RID: 1891
		public IPAddress IpAddress;

		// Token: 0x04000764 RID: 1892
		public ushort SteamPort;

		// Token: 0x04000765 RID: 1893
		public ushort GamePort;

		// Token: 0x04000766 RID: 1894
		public ushort QueryPort;

		// Token: 0x04000767 RID: 1895
		public bool Secure;

		// Token: 0x04000768 RID: 1896
		public string VersionString;

		// Token: 0x04000769 RID: 1897
		public string ModDir;

		// Token: 0x0400076A RID: 1898
		public string GameDescription;

		// Token: 0x0400076B RID: 1899
		public bool DedicatedServer;
	}
}
