using System;

namespace System.Net.Sockets
{
	/// <summary>The result of a <see cref="M:System.Net.Sockets.SocketTaskExtensions.ReceiveFromAsync(System.Net.Sockets.Socket,System.ArraySegment{System.Byte},System.Net.Sockets.SocketFlags,System.Net.EndPoint)" /> operation.</summary>
	// Token: 0x020007C5 RID: 1989
	public struct SocketReceiveFromResult
	{
		/// <summary>The number of bytes received. If the <see cref="M:System.Net.Sockets.SocketTaskExtensions.ReceiveFromAsync(System.Net.Sockets.Socket,System.ArraySegment{System.Byte},System.Net.Sockets.SocketFlags,System.Net.EndPoint)" /> operation was unsuccessful, then 0.</summary>
		// Token: 0x04002613 RID: 9747
		public int ReceivedBytes;

		/// <summary>The source <see cref="T:System.Net.EndPoint" />.</summary>
		// Token: 0x04002614 RID: 9748
		public EndPoint RemoteEndPoint;
	}
}
