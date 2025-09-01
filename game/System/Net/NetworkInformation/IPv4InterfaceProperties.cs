using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about network interfaces that support Internet Protocol version 4 (IPv4).</summary>
	// Token: 0x020006EA RID: 1770
	public abstract class IPv4InterfaceProperties
	{
		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether an interface uses Windows Internet Name Service (WINS).</summary>
		/// <returns>
		///   <see langword="true" /> if the interface uses WINS; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x060038DD RID: 14557
		public abstract bool UsesWins { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the interface is configured to use a Dynamic Host Configuration Protocol (DHCP) server to obtain an IP address.</summary>
		/// <returns>
		///   <see langword="true" /> if the interface is configured to obtain an IP address from a DHCP server; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x060038DE RID: 14558
		public abstract bool IsDhcpEnabled { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface has an automatic private IP addressing (APIPA) address.</summary>
		/// <returns>
		///   <see langword="true" /> if the interface uses an APIPA address; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x060038DF RID: 14559
		public abstract bool IsAutomaticPrivateAddressingActive { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface has automatic private IP addressing (APIPA) enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the interface uses APIPA; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x060038E0 RID: 14560
		public abstract bool IsAutomaticPrivateAddressingEnabled { get; }

		/// <summary>Gets the index of the network interface associated with the Internet Protocol version 4 (IPv4) address.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the index of the IPv4 interface.</returns>
		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x060038E1 RID: 14561
		public abstract int Index { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether this interface can forward (route) packets.</summary>
		/// <returns>
		///   <see langword="true" /> if this interface routes packets; otherwise <see langword="false" />.</returns>
		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x060038E2 RID: 14562
		public abstract bool IsForwardingEnabled { get; }

		/// <summary>Gets the maximum transmission unit (MTU) for this network interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the MTU.</returns>
		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x060038E3 RID: 14563
		public abstract int Mtu { get; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IPv4InterfaceProperties" /> class.</summary>
		// Token: 0x060038E4 RID: 14564 RVA: 0x0000219B File Offset: 0x0000039B
		protected IPv4InterfaceProperties()
		{
		}
	}
}
