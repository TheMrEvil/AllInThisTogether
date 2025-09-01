using System;

namespace Photon.Realtime
{
	// Token: 0x0200000D RID: 13
	public struct PhotonPortDefinition
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002F54 File Offset: 0x00001154
		// Note: this type is marked as 'beforefieldinit'.
		static PhotonPortDefinition()
		{
		}

		// Token: 0x0400006C RID: 108
		public static readonly PhotonPortDefinition AlternativeUdpPorts = new PhotonPortDefinition
		{
			NameServerPort = 27000,
			MasterServerPort = 27001,
			GameServerPort = 27002
		};

		// Token: 0x0400006D RID: 109
		public ushort NameServerPort;

		// Token: 0x0400006E RID: 110
		public ushort MasterServerPort;

		// Token: 0x0400006F RID: 111
		public ushort GameServerPort;
	}
}
