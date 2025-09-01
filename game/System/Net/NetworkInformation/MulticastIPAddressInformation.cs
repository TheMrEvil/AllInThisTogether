using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about a network interface's multicast address.</summary>
	// Token: 0x020006EE RID: 1774
	public abstract class MulticastIPAddressInformation : IPAddressInformation
	{
		/// <summary>Gets the number of seconds remaining during which this address is the preferred address.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the number of seconds left for this address to remain preferred.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06003925 RID: 14629
		public abstract long AddressPreferredLifetime { get; }

		/// <summary>Gets the number of seconds remaining during which this address is valid.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the number of seconds left for this address to remain assigned.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06003926 RID: 14630
		public abstract long AddressValidLifetime { get; }

		/// <summary>Specifies the amount of time remaining on the Dynamic Host Configuration Protocol (DHCP) lease for this IP address.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that contains the number of seconds remaining before the computer must release the <see cref="T:System.Net.IPAddress" /> instance.</returns>
		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06003927 RID: 14631
		public abstract long DhcpLeaseLifetime { get; }

		/// <summary>Gets a value that indicates the state of the duplicate address detection algorithm.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.DuplicateAddressDetectionState" /> values that indicates the progress of the algorithm in determining the uniqueness of this IP address.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06003928 RID: 14632
		public abstract DuplicateAddressDetectionState DuplicateAddressDetectionState { get; }

		/// <summary>Gets a value that identifies the source of a Multicast Internet Protocol (IP) address prefix.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.PrefixOrigin" /> values that identifies how the prefix information was obtained.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06003929 RID: 14633
		public abstract PrefixOrigin PrefixOrigin { get; }

		/// <summary>Gets a value that identifies the source of a Multicast Internet Protocol (IP) address suffix.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.SuffixOrigin" /> values that identifies how the suffix information was obtained.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x0600392A RID: 14634
		public abstract SuffixOrigin SuffixOrigin { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.MulticastIPAddressInformation" /> class.</summary>
		// Token: 0x0600392B RID: 14635 RVA: 0x000C827E File Offset: 0x000C647E
		protected MulticastIPAddressInformation()
		{
		}
	}
}
