using System;

namespace System.Net
{
	/// <summary>Defines transport types for the <see cref="T:System.Net.SocketPermission" /> and <see cref="T:System.Net.Sockets.Socket" /> classes.</summary>
	// Token: 0x02000604 RID: 1540
	public enum TransportType
	{
		/// <summary>UDP transport.</summary>
		// Token: 0x04001C3B RID: 7227
		Udp = 1,
		/// <summary>The transport type is connectionless, such as UDP. Specifying this value has the same effect as specifying <see cref="F:System.Net.TransportType.Udp" />.</summary>
		// Token: 0x04001C3C RID: 7228
		Connectionless = 1,
		/// <summary>TCP transport.</summary>
		// Token: 0x04001C3D RID: 7229
		Tcp,
		/// <summary>The transport is connection oriented, such as TCP. Specifying this value has the same effect as specifying <see cref="F:System.Net.TransportType.Tcp" />.</summary>
		// Token: 0x04001C3E RID: 7230
		ConnectionOriented = 2,
		/// <summary>All transport types.</summary>
		// Token: 0x04001C3F RID: 7231
		All
	}
}
