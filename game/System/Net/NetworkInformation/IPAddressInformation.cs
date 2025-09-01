using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about a network interface address.</summary>
	// Token: 0x020006E0 RID: 1760
	public abstract class IPAddressInformation
	{
		/// <summary>Gets the Internet Protocol (IP) address.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> instance that contains the IP address of an interface.</returns>
		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06003878 RID: 14456
		public abstract IPAddress Address { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the Internet Protocol (IP) address is valid to appear in a Domain Name System (DNS) server database.</summary>
		/// <returns>
		///   <see langword="true" /> if the address can appear in a DNS database; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06003879 RID: 14457
		public abstract bool IsDnsEligible { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the Internet Protocol (IP) address is transient (a cluster address).</summary>
		/// <returns>
		///   <see langword="true" /> if the address is transient; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x0600387A RID: 14458
		public abstract bool IsTransient { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IPAddressInformation" /> class.</summary>
		// Token: 0x0600387B RID: 14459 RVA: 0x0000219B File Offset: 0x0000039B
		protected IPAddressInformation()
		{
		}
	}
}
