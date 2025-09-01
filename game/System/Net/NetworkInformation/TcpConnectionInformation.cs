using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about the Transmission Control Protocol (TCP) connections on the local computer.</summary>
	// Token: 0x02000705 RID: 1797
	public abstract class TcpConnectionInformation
	{
		/// <summary>Gets the local endpoint of a Transmission Control Protocol (TCP) connection.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> instance that contains the IP address and port on the local computer.</returns>
		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06003998 RID: 14744
		public abstract IPEndPoint LocalEndPoint { get; }

		/// <summary>Gets the remote endpoint of a Transmission Control Protocol (TCP) connection.</summary>
		/// <returns>An <see cref="T:System.Net.IPEndPoint" /> instance that contains the IP address and port on the remote computer.</returns>
		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06003999 RID: 14745
		public abstract IPEndPoint RemoteEndPoint { get; }

		/// <summary>Gets the state of this Transmission Control Protocol (TCP) connection.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.TcpState" /> enumeration values.</returns>
		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x0600399A RID: 14746
		public abstract TcpState State { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.TcpConnectionInformation" /> class.</summary>
		// Token: 0x0600399B RID: 14747 RVA: 0x0000219B File Offset: 0x0000039B
		protected TcpConnectionInformation()
		{
		}
	}
}
