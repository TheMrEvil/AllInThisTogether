using System;

namespace System.Net.Sockets
{
	/// <summary>Describes states for a <see cref="T:System.Net.Sockets.Socket" />.</summary>
	// Token: 0x020007B5 RID: 1973
	[Flags]
	public enum SocketInformationOptions
	{
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> is nonblocking.</summary>
		// Token: 0x04002572 RID: 9586
		NonBlocking = 1,
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> is connected.</summary>
		// Token: 0x04002573 RID: 9587
		Connected = 2,
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> is listening for new connections.</summary>
		// Token: 0x04002574 RID: 9588
		Listening = 4,
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> uses overlapped I/O.</summary>
		// Token: 0x04002575 RID: 9589
		UseOnlyOverlappedIO = 8
	}
}
