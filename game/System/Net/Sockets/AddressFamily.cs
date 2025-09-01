using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies the addressing scheme that an instance of the <see cref="T:System.Net.Sockets.Socket" /> class can use.</summary>
	// Token: 0x020007A5 RID: 1957
	public enum AddressFamily
	{
		/// <summary>Unknown address family.</summary>
		// Token: 0x04002492 RID: 9362
		Unknown = -1,
		/// <summary>Unspecified address family.</summary>
		// Token: 0x04002493 RID: 9363
		Unspecified,
		/// <summary>Unix local to host address.</summary>
		// Token: 0x04002494 RID: 9364
		Unix,
		/// <summary>Address for IP version 4.</summary>
		// Token: 0x04002495 RID: 9365
		InterNetwork,
		/// <summary>ARPANET IMP address.</summary>
		// Token: 0x04002496 RID: 9366
		ImpLink,
		/// <summary>Address for PUP protocols.</summary>
		// Token: 0x04002497 RID: 9367
		Pup,
		/// <summary>Address for MIT CHAOS protocols.</summary>
		// Token: 0x04002498 RID: 9368
		Chaos,
		/// <summary>Address for Xerox NS protocols.</summary>
		// Token: 0x04002499 RID: 9369
		NS,
		/// <summary>IPX or SPX address.</summary>
		// Token: 0x0400249A RID: 9370
		Ipx = 6,
		/// <summary>Address for ISO protocols.</summary>
		// Token: 0x0400249B RID: 9371
		Iso,
		/// <summary>Address for OSI protocols.</summary>
		// Token: 0x0400249C RID: 9372
		Osi = 7,
		/// <summary>European Computer Manufacturers Association (ECMA) address.</summary>
		// Token: 0x0400249D RID: 9373
		Ecma,
		/// <summary>Address for Datakit protocols.</summary>
		// Token: 0x0400249E RID: 9374
		DataKit,
		/// <summary>Addresses for CCITT protocols, such as X.25.</summary>
		// Token: 0x0400249F RID: 9375
		Ccitt,
		/// <summary>IBM SNA address.</summary>
		// Token: 0x040024A0 RID: 9376
		Sna,
		/// <summary>DECnet address.</summary>
		// Token: 0x040024A1 RID: 9377
		DecNet,
		/// <summary>Direct data-link interface address.</summary>
		// Token: 0x040024A2 RID: 9378
		DataLink,
		/// <summary>LAT address.</summary>
		// Token: 0x040024A3 RID: 9379
		Lat,
		/// <summary>NSC Hyperchannel address.</summary>
		// Token: 0x040024A4 RID: 9380
		HyperChannel,
		/// <summary>AppleTalk address.</summary>
		// Token: 0x040024A5 RID: 9381
		AppleTalk,
		/// <summary>NetBios address.</summary>
		// Token: 0x040024A6 RID: 9382
		NetBios,
		/// <summary>VoiceView address.</summary>
		// Token: 0x040024A7 RID: 9383
		VoiceView,
		/// <summary>FireFox address.</summary>
		// Token: 0x040024A8 RID: 9384
		FireFox,
		/// <summary>Banyan address.</summary>
		// Token: 0x040024A9 RID: 9385
		Banyan = 21,
		/// <summary>Native ATM services address.</summary>
		// Token: 0x040024AA RID: 9386
		Atm,
		/// <summary>Address for IP version 6.</summary>
		// Token: 0x040024AB RID: 9387
		InterNetworkV6,
		/// <summary>Address for Microsoft cluster products.</summary>
		// Token: 0x040024AC RID: 9388
		Cluster,
		/// <summary>IEEE 1284.4 workgroup address.</summary>
		// Token: 0x040024AD RID: 9389
		Ieee12844,
		/// <summary>IrDA address.</summary>
		// Token: 0x040024AE RID: 9390
		Irda,
		/// <summary>Address for Network Designers OSI gateway-enabled protocols.</summary>
		// Token: 0x040024AF RID: 9391
		NetworkDesigners = 28,
		/// <summary>MAX address.</summary>
		// Token: 0x040024B0 RID: 9392
		Max
	}
}
