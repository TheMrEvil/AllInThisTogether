using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the type of protocol that an instance of the <see cref="T:System.Net.Sockets.Socket" /> class can use.</summary>
	// Token: 0x020007AC RID: 1964
	public enum ProtocolFamily
	{
		/// <summary>Unknown protocol.</summary>
		// Token: 0x040024E3 RID: 9443
		Unknown = -1,
		/// <summary>Unspecified protocol.</summary>
		// Token: 0x040024E4 RID: 9444
		Unspecified,
		/// <summary>Unix local to host protocol.</summary>
		// Token: 0x040024E5 RID: 9445
		Unix,
		/// <summary>IP version 4 protocol.</summary>
		// Token: 0x040024E6 RID: 9446
		InterNetwork,
		/// <summary>ARPANET IMP protocol.</summary>
		// Token: 0x040024E7 RID: 9447
		ImpLink,
		/// <summary>PUP protocol.</summary>
		// Token: 0x040024E8 RID: 9448
		Pup,
		/// <summary>MIT CHAOS protocol.</summary>
		// Token: 0x040024E9 RID: 9449
		Chaos,
		/// <summary>Xerox NS protocol.</summary>
		// Token: 0x040024EA RID: 9450
		NS,
		/// <summary>IPX or SPX protocol.</summary>
		// Token: 0x040024EB RID: 9451
		Ipx = 6,
		/// <summary>ISO protocol.</summary>
		// Token: 0x040024EC RID: 9452
		Iso,
		/// <summary>OSI protocol.</summary>
		// Token: 0x040024ED RID: 9453
		Osi = 7,
		/// <summary>European Computer Manufacturers Association (ECMA) protocol.</summary>
		// Token: 0x040024EE RID: 9454
		Ecma,
		/// <summary>DataKit protocol.</summary>
		// Token: 0x040024EF RID: 9455
		DataKit,
		/// <summary>CCITT protocol, such as X.25.</summary>
		// Token: 0x040024F0 RID: 9456
		Ccitt,
		/// <summary>IBM SNA protocol.</summary>
		// Token: 0x040024F1 RID: 9457
		Sna,
		/// <summary>DECNet protocol.</summary>
		// Token: 0x040024F2 RID: 9458
		DecNet,
		/// <summary>Direct data link protocol.</summary>
		// Token: 0x040024F3 RID: 9459
		DataLink,
		/// <summary>LAT protocol.</summary>
		// Token: 0x040024F4 RID: 9460
		Lat,
		/// <summary>NSC HyperChannel protocol.</summary>
		// Token: 0x040024F5 RID: 9461
		HyperChannel,
		/// <summary>AppleTalk protocol.</summary>
		// Token: 0x040024F6 RID: 9462
		AppleTalk,
		/// <summary>NetBIOS protocol.</summary>
		// Token: 0x040024F7 RID: 9463
		NetBios,
		/// <summary>VoiceView protocol.</summary>
		// Token: 0x040024F8 RID: 9464
		VoiceView,
		/// <summary>FireFox protocol.</summary>
		// Token: 0x040024F9 RID: 9465
		FireFox,
		/// <summary>Banyan protocol.</summary>
		// Token: 0x040024FA RID: 9466
		Banyan = 21,
		/// <summary>Native ATM services protocol.</summary>
		// Token: 0x040024FB RID: 9467
		Atm,
		/// <summary>IP version 6 protocol.</summary>
		// Token: 0x040024FC RID: 9468
		InterNetworkV6,
		/// <summary>Microsoft Cluster products protocol.</summary>
		// Token: 0x040024FD RID: 9469
		Cluster,
		/// <summary>IEEE 1284.4 workgroup protocol.</summary>
		// Token: 0x040024FE RID: 9470
		Ieee12844,
		/// <summary>IrDA protocol.</summary>
		// Token: 0x040024FF RID: 9471
		Irda,
		/// <summary>Network Designers OSI gateway enabled protocol.</summary>
		// Token: 0x04002500 RID: 9472
		NetworkDesigners = 28,
		/// <summary>MAX protocol.</summary>
		// Token: 0x04002501 RID: 9473
		Max
	}
}
