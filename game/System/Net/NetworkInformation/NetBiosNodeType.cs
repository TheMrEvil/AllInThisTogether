using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Specifies the Network Basic Input/Output System (NetBIOS) node type.</summary>
	// Token: 0x0200070C RID: 1804
	public enum NetBiosNodeType
	{
		/// <summary>An unknown node type.</summary>
		// Token: 0x040021FB RID: 8699
		Unknown,
		/// <summary>A broadcast node.</summary>
		// Token: 0x040021FC RID: 8700
		Broadcast,
		/// <summary>A peer-to-peer node.</summary>
		// Token: 0x040021FD RID: 8701
		Peer2Peer,
		/// <summary>A mixed node.</summary>
		// Token: 0x040021FE RID: 8702
		Mixed = 4,
		/// <summary>A hybrid node.</summary>
		// Token: 0x040021FF RID: 8703
		Hybrid = 8
	}
}
