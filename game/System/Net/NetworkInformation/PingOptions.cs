using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Used to control how <see cref="T:System.Net.NetworkInformation.Ping" /> data packets are transmitted.</summary>
	// Token: 0x020006FC RID: 1788
	public class PingOptions
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingOptions" /> class and sets the Time to Live and fragmentation values.</summary>
		/// <param name="ttl">An <see cref="T:System.Int32" /> value greater than zero that specifies the number of times that the <see cref="T:System.Net.NetworkInformation.Ping" /> data packets can be forwarded.</param>
		/// <param name="dontFragment">
		///   <see langword="true" /> to prevent data sent to the remote host from being fragmented; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="ttl" /> is less than or equal to zero.</exception>
		// Token: 0x06003971 RID: 14705 RVA: 0x000C8AEC File Offset: 0x000C6CEC
		public PingOptions(int ttl, bool dontFragment)
		{
			if (ttl <= 0)
			{
				throw new ArgumentOutOfRangeException("ttl");
			}
			this.ttl = ttl;
			this.dontFragment = dontFragment;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.PingOptions" /> class.</summary>
		// Token: 0x06003972 RID: 14706 RVA: 0x000C8B1C File Offset: 0x000C6D1C
		public PingOptions()
		{
		}

		/// <summary>Gets or sets the number of routing nodes that can forward the <see cref="T:System.Net.NetworkInformation.Ping" /> data before it is discarded.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that specifies the number of times the <see cref="T:System.Net.NetworkInformation.Ping" /> data packets can be forwarded. The default is 128.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than or equal to zero.</exception>
		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06003973 RID: 14707 RVA: 0x000C8B2F File Offset: 0x000C6D2F
		// (set) Token: 0x06003974 RID: 14708 RVA: 0x000C8B37 File Offset: 0x000C6D37
		public int Ttl
		{
			get
			{
				return this.ttl;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ttl = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls fragmentation of the data sent to the remote host.</summary>
		/// <returns>
		///   <see langword="true" /> if the data cannot be sent in multiple packets; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06003975 RID: 14709 RVA: 0x000C8B4F File Offset: 0x000C6D4F
		// (set) Token: 0x06003976 RID: 14710 RVA: 0x000C8B57 File Offset: 0x000C6D57
		public bool DontFragment
		{
			get
			{
				return this.dontFragment;
			}
			set
			{
				this.dontFragment = value;
			}
		}

		// Token: 0x040021A8 RID: 8616
		private const int DontFragmentFlag = 2;

		// Token: 0x040021A9 RID: 8617
		private int ttl = 128;

		// Token: 0x040021AA RID: 8618
		private bool dontFragment;
	}
}
