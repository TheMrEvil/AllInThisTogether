using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies whether a <see cref="T:System.Net.Sockets.Socket" /> will remain connected after a call to the <see cref="M:System.Net.Sockets.Socket.Close" /> or <see cref="M:System.Net.Sockets.TcpClient.Close" /> methods and the length of time it will remain connected, if data remains to be sent.</summary>
	// Token: 0x020007A9 RID: 1961
	public class LingerOption
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.LingerOption" /> class.</summary>
		/// <param name="enable">
		///   <see langword="true" /> to remain connected after the <see cref="M:System.Net.Sockets.Socket.Close" /> method is called; otherwise, <see langword="false" />.</param>
		/// <param name="seconds">The number of seconds to remain connected after the <see cref="M:System.Net.Sockets.Socket.Close" /> method is called.</param>
		// Token: 0x06003EA6 RID: 16038 RVA: 0x000D6774 File Offset: 0x000D4974
		public LingerOption(bool enable, int seconds)
		{
			this.Enabled = enable;
			this.LingerTime = seconds;
		}

		/// <summary>Gets or sets a value that indicates whether to linger after the <see cref="T:System.Net.Sockets.Socket" /> is closed.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Sockets.Socket" /> should linger after <see cref="M:System.Net.Sockets.Socket.Close" /> is called; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06003EA7 RID: 16039 RVA: 0x000D678A File Offset: 0x000D498A
		// (set) Token: 0x06003EA8 RID: 16040 RVA: 0x000D6792 File Offset: 0x000D4992
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		/// <summary>Gets or sets the amount of time to remain connected after calling the <see cref="M:System.Net.Sockets.Socket.Close" /> method if data remains to be sent.</summary>
		/// <returns>The amount of time, in seconds, to remain connected after calling <see cref="M:System.Net.Sockets.Socket.Close" />.</returns>
		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06003EA9 RID: 16041 RVA: 0x000D679B File Offset: 0x000D499B
		// (set) Token: 0x06003EAA RID: 16042 RVA: 0x000D67A3 File Offset: 0x000D49A3
		public int LingerTime
		{
			get
			{
				return this.lingerTime;
			}
			set
			{
				this.lingerTime = value;
			}
		}

		// Token: 0x040024DB RID: 9435
		private bool enabled;

		// Token: 0x040024DC RID: 9436
		private int lingerTime;
	}
}
