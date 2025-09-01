using System;
using System.Runtime.Serialization;

namespace System.Net.Sockets
{
	/// <summary>Encapsulates the information that is necessary to duplicate a <see cref="T:System.Net.Sockets.Socket" />.</summary>
	// Token: 0x020007B4 RID: 1972
	[Serializable]
	public struct SocketInformation
	{
		/// <summary>Gets or sets the protocol information for a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An array of type <see cref="T:System.Byte" />.</returns>
		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06003EC7 RID: 16071 RVA: 0x000D6A5C File Offset: 0x000D4C5C
		// (set) Token: 0x06003EC8 RID: 16072 RVA: 0x000D6A64 File Offset: 0x000D4C64
		public byte[] ProtocolInformation
		{
			get
			{
				return this.protocolInformation;
			}
			set
			{
				this.protocolInformation = value;
			}
		}

		/// <summary>Gets or sets the options for a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketInformationOptions" /> instance.</returns>
		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06003EC9 RID: 16073 RVA: 0x000D6A6D File Offset: 0x000D4C6D
		// (set) Token: 0x06003ECA RID: 16074 RVA: 0x000D6A75 File Offset: 0x000D4C75
		public SocketInformationOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06003ECB RID: 16075 RVA: 0x000D6A7E File Offset: 0x000D4C7E
		// (set) Token: 0x06003ECC RID: 16076 RVA: 0x000D6A8B File Offset: 0x000D4C8B
		internal bool IsNonBlocking
		{
			get
			{
				return (this.options & SocketInformationOptions.NonBlocking) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.NonBlocking;
					return;
				}
				this.options &= ~SocketInformationOptions.NonBlocking;
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06003ECD RID: 16077 RVA: 0x000D6AAE File Offset: 0x000D4CAE
		// (set) Token: 0x06003ECE RID: 16078 RVA: 0x000D6ABB File Offset: 0x000D4CBB
		internal bool IsConnected
		{
			get
			{
				return (this.options & SocketInformationOptions.Connected) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.Connected;
					return;
				}
				this.options &= ~SocketInformationOptions.Connected;
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06003ECF RID: 16079 RVA: 0x000D6ADE File Offset: 0x000D4CDE
		// (set) Token: 0x06003ED0 RID: 16080 RVA: 0x000D6AEB File Offset: 0x000D4CEB
		internal bool IsListening
		{
			get
			{
				return (this.options & SocketInformationOptions.Listening) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.Listening;
					return;
				}
				this.options &= ~SocketInformationOptions.Listening;
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06003ED1 RID: 16081 RVA: 0x000D6B0E File Offset: 0x000D4D0E
		// (set) Token: 0x06003ED2 RID: 16082 RVA: 0x000D6B1B File Offset: 0x000D4D1B
		internal bool UseOnlyOverlappedIO
		{
			get
			{
				return (this.options & SocketInformationOptions.UseOnlyOverlappedIO) > (SocketInformationOptions)0;
			}
			set
			{
				if (value)
				{
					this.options |= SocketInformationOptions.UseOnlyOverlappedIO;
					return;
				}
				this.options &= ~SocketInformationOptions.UseOnlyOverlappedIO;
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06003ED3 RID: 16083 RVA: 0x000D6B3E File Offset: 0x000D4D3E
		// (set) Token: 0x06003ED4 RID: 16084 RVA: 0x000D6B46 File Offset: 0x000D4D46
		internal EndPoint RemoteEndPoint
		{
			get
			{
				return this.remoteEndPoint;
			}
			set
			{
				this.remoteEndPoint = value;
			}
		}

		// Token: 0x0400256E RID: 9582
		private byte[] protocolInformation;

		// Token: 0x0400256F RID: 9583
		private SocketInformationOptions options;

		// Token: 0x04002570 RID: 9584
		[OptionalField]
		private EndPoint remoteEndPoint;
	}
}
