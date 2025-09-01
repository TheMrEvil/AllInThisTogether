using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides information about network interfaces that support Internet Protocol version 6 (IPv6).</summary>
	// Token: 0x020006EB RID: 1771
	public abstract class IPv6InterfaceProperties
	{
		/// <summary>Gets the index of the network interface associated with an Internet Protocol version 6 (IPv6) address.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the index of the network interface for IPv6 address.</returns>
		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x060038E5 RID: 14565
		public abstract int Index { get; }

		/// <summary>Gets the maximum transmission unit (MTU) for this network interface.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that specifies the MTU.</returns>
		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x060038E6 RID: 14566
		public abstract int Mtu { get; }

		/// <summary>Gets the scope ID of the network interface associated with an Internet Protocol version 6 (IPv6) address.</summary>
		/// <param name="scopeLevel">The scope level.</param>
		/// <returns>The scope ID of the network interface associated with an IPv6 address.</returns>
		// Token: 0x060038E7 RID: 14567 RVA: 0x0000829A File Offset: 0x0000649A
		public virtual long GetScopeId(ScopeLevel scopeLevel)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IPv6InterfaceProperties" /> class.</summary>
		// Token: 0x060038E8 RID: 14568 RVA: 0x0000219B File Offset: 0x0000039B
		protected IPv6InterfaceProperties()
		{
		}
	}
}
