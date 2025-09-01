using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about a network interface's unicast address.</summary>
	// Token: 0x02000709 RID: 1801
	public abstract class UnicastIPAddressInformation : IPAddressInformation
	{
		/// <summary>Gets the number of seconds remaining during which this address is the preferred address.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the number of seconds left for this address to remain preferred.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x060039B1 RID: 14769
		public abstract long AddressPreferredLifetime { get; }

		/// <summary>Gets the number of seconds remaining during which this address is valid.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the number of seconds left for this address to remain assigned.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x060039B2 RID: 14770
		public abstract long AddressValidLifetime { get; }

		/// <summary>Specifies the amount of time remaining on the Dynamic Host Configuration Protocol (DHCP) lease for this IP address.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that contains the number of seconds remaining before the computer must release the <see cref="T:System.Net.IPAddress" /> instance.</returns>
		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x060039B3 RID: 14771
		public abstract long DhcpLeaseLifetime { get; }

		/// <summary>Gets a value that indicates the state of the duplicate address detection algorithm.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.DuplicateAddressDetectionState" /> values that indicates the progress of the algorithm in determining the uniqueness of this IP address.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x060039B4 RID: 14772
		public abstract DuplicateAddressDetectionState DuplicateAddressDetectionState { get; }

		/// <summary>Gets a value that identifies the source of a unicast Internet Protocol (IP) address prefix.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.PrefixOrigin" /> values that identifies how the prefix information was obtained.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x060039B5 RID: 14773
		public abstract PrefixOrigin PrefixOrigin { get; }

		/// <summary>Gets a value that identifies the source of a unicast Internet Protocol (IP) address suffix.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.SuffixOrigin" /> values that identifies how the suffix information was obtained.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP.</exception>
		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x060039B6 RID: 14774
		public abstract SuffixOrigin SuffixOrigin { get; }

		/// <summary>Gets the IPv4 mask.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> object that contains the IPv4 mask.</returns>
		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x060039B7 RID: 14775
		public abstract IPAddress IPv4Mask { get; }

		/// <summary>Gets the length, in bits, of the prefix or network part of the IP address.</summary>
		/// <returns>The length, in bits, of the prefix or network part of the IP address.</returns>
		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x060039B8 RID: 14776 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual int PrefixLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.UnicastIPAddressInformation" /> class.</summary>
		// Token: 0x060039B9 RID: 14777 RVA: 0x000C827E File Offset: 0x000C647E
		protected UnicastIPAddressInformation()
		{
		}
	}
}
