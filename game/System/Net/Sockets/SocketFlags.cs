using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies socket send and receive behaviors.</summary>
	// Token: 0x020007B3 RID: 1971
	[Flags]
	public enum SocketFlags
	{
		/// <summary>Use no flags for this call.</summary>
		// Token: 0x04002564 RID: 9572
		None = 0,
		/// <summary>Process out-of-band data.</summary>
		// Token: 0x04002565 RID: 9573
		OutOfBand = 1,
		/// <summary>Peek at the incoming message.</summary>
		// Token: 0x04002566 RID: 9574
		Peek = 2,
		/// <summary>Send without using routing tables.</summary>
		// Token: 0x04002567 RID: 9575
		DontRoute = 4,
		/// <summary>Provides a standard value for the number of WSABUF structures that are used to send and receive data. This value is not used or supported on .NET Framework 4.5.</summary>
		// Token: 0x04002568 RID: 9576
		MaxIOVectorLength = 16,
		/// <summary>The message was too large to fit into the specified buffer and was truncated.</summary>
		// Token: 0x04002569 RID: 9577
		Truncated = 256,
		/// <summary>Indicates that the control data did not fit into an internal 64-KB buffer and was truncated.</summary>
		// Token: 0x0400256A RID: 9578
		ControlDataTruncated = 512,
		/// <summary>Indicates a broadcast packet.</summary>
		// Token: 0x0400256B RID: 9579
		Broadcast = 1024,
		/// <summary>Indicates a multicast packet.</summary>
		// Token: 0x0400256C RID: 9580
		Multicast = 2048,
		/// <summary>Partial send or receive for message.</summary>
		// Token: 0x0400256D RID: 9581
		Partial = 32768
	}
}
