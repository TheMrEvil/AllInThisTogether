using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Represents the IP address of the network gateway. This class cannot be instantiated.</summary>
	// Token: 0x020006DD RID: 1757
	public abstract class GatewayIPAddressInformation
	{
		/// <summary>Gets the IP address of the gateway.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> object that contains the IP address of the gateway.</returns>
		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x0600385E RID: 14430
		public abstract IPAddress Address { get; }

		/// <summary>Initializes the members of this class.</summary>
		// Token: 0x0600385F RID: 14431 RVA: 0x0000219B File Offset: 0x0000039B
		protected GatewayIPAddressInformation()
		{
		}
	}
}
