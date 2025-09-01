using System;

namespace System.Net.Sockets
{
	/// <summary>The type of asynchronous socket operation most recently performed with this context object.</summary>
	// Token: 0x020007AF RID: 1967
	public enum SocketAsyncOperation
	{
		/// <summary>None of the socket operations.</summary>
		// Token: 0x04002521 RID: 9505
		None,
		/// <summary>A socket Accept operation.</summary>
		// Token: 0x04002522 RID: 9506
		Accept,
		/// <summary>A socket Connect operation.</summary>
		// Token: 0x04002523 RID: 9507
		Connect,
		/// <summary>A socket Disconnect operation.</summary>
		// Token: 0x04002524 RID: 9508
		Disconnect,
		/// <summary>A socket Receive operation.</summary>
		// Token: 0x04002525 RID: 9509
		Receive,
		/// <summary>A socket ReceiveFrom operation.</summary>
		// Token: 0x04002526 RID: 9510
		ReceiveFrom,
		/// <summary>A socket ReceiveMessageFrom operation.</summary>
		// Token: 0x04002527 RID: 9511
		ReceiveMessageFrom,
		/// <summary>A socket Send operation.</summary>
		// Token: 0x04002528 RID: 9512
		Send,
		/// <summary>A socket SendPackets operation.</summary>
		// Token: 0x04002529 RID: 9513
		SendPackets,
		/// <summary>A socket SendTo operation.</summary>
		// Token: 0x0400252A RID: 9514
		SendTo
	}
}
