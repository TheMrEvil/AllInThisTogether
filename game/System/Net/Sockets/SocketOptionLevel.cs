using System;

namespace System.Net.Sockets
{
	/// <summary>Defines socket option levels for the <see cref="M:System.Net.Sockets.Socket.SetSocketOption(System.Net.Sockets.SocketOptionLevel,System.Net.Sockets.SocketOptionName,System.Int32)" /> and <see cref="M:System.Net.Sockets.Socket.GetSocketOption(System.Net.Sockets.SocketOptionLevel,System.Net.Sockets.SocketOptionName)" /> methods.</summary>
	// Token: 0x020007B6 RID: 1974
	public enum SocketOptionLevel
	{
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply to all sockets.</summary>
		// Token: 0x04002577 RID: 9591
		Socket = 65535,
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply only to IP sockets.</summary>
		// Token: 0x04002578 RID: 9592
		IP = 0,
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply only to IPv6 sockets.</summary>
		// Token: 0x04002579 RID: 9593
		IPv6 = 41,
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply only to TCP sockets.</summary>
		// Token: 0x0400257A RID: 9594
		Tcp = 6,
		/// <summary>
		///   <see cref="T:System.Net.Sockets.Socket" /> options apply only to UDP sockets.</summary>
		// Token: 0x0400257B RID: 9595
		Udp = 17
	}
}
