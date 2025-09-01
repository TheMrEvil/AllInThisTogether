using System;

namespace System.Net.Sockets
{
	/// <summary>Contains option values for joining an IPv6 multicast group.</summary>
	// Token: 0x020007AB RID: 1963
	public class IPv6MulticastOption
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.IPv6MulticastOption" /> class with the specified IP multicast group and the local interface address.</summary>
		/// <param name="group">The group <see cref="T:System.Net.IPAddress" />.</param>
		/// <param name="ifindex">The local interface address.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="ifindex" /> is less than 0.  
		/// -or-  
		/// <paramref name="ifindex" /> is greater than 0x00000000FFFFFFFF.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is <see langword="null" />.</exception>
		// Token: 0x06003EB4 RID: 16052 RVA: 0x000D6899 File Offset: 0x000D4A99
		public IPv6MulticastOption(IPAddress group, long ifindex)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (ifindex < 0L || ifindex > (long)((ulong)-1))
			{
				throw new ArgumentOutOfRangeException("ifindex");
			}
			this.Group = group;
			this.InterfaceIndex = ifindex;
		}

		/// <summary>Initializes a new version of the <see cref="T:System.Net.Sockets.IPv6MulticastOption" /> class for the specified IP multicast group.</summary>
		/// <param name="group">The <see cref="T:System.Net.IPAddress" /> of the multicast group.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is <see langword="null" />.</exception>
		// Token: 0x06003EB5 RID: 16053 RVA: 0x000D68D2 File Offset: 0x000D4AD2
		public IPv6MulticastOption(IPAddress group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			this.Group = group;
			this.InterfaceIndex = 0L;
		}

		/// <summary>Gets or sets the IP address of a multicast group.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> that contains the Internet address of a multicast group.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="group" /> is <see langword="null" />.</exception>
		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06003EB6 RID: 16054 RVA: 0x000D68F7 File Offset: 0x000D4AF7
		// (set) Token: 0x06003EB7 RID: 16055 RVA: 0x000D68FF File Offset: 0x000D4AFF
		public IPAddress Group
		{
			get
			{
				return this.m_Group;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_Group = value;
			}
		}

		/// <summary>Gets or sets the interface index that is associated with a multicast group.</summary>
		/// <returns>A <see cref="T:System.UInt64" /> value that specifies the address of the interface.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value that is specified for a set operation is less than 0 or greater than 0x00000000FFFFFFFF.</exception>
		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06003EB8 RID: 16056 RVA: 0x000D6916 File Offset: 0x000D4B16
		// (set) Token: 0x06003EB9 RID: 16057 RVA: 0x000D691E File Offset: 0x000D4B1E
		public long InterfaceIndex
		{
			get
			{
				return this.m_Interface;
			}
			set
			{
				if (value < 0L || value > (long)((ulong)-1))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.m_Interface = value;
			}
		}

		// Token: 0x040024E0 RID: 9440
		private IPAddress m_Group;

		// Token: 0x040024E1 RID: 9441
		private long m_Interface;
	}
}
