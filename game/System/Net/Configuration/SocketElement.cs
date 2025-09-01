using System;
using System.Configuration;
using System.Net.Sockets;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents information used to configure <see cref="T:System.Net.Sockets.Socket" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200077C RID: 1916
	public sealed class SocketElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.SocketElement" /> class.</summary>
		// Token: 0x06003C61 RID: 15457 RVA: 0x000CE17C File Offset: 0x000CC37C
		public SocketElement()
		{
			SocketElement.alwaysUseCompletionPortsForAcceptProp = new ConfigurationProperty("alwaysUseCompletionPortsForAccept", typeof(bool), false);
			SocketElement.alwaysUseCompletionPortsForConnectProp = new ConfigurationProperty("alwaysUseCompletionPortsForConnect", typeof(bool), false);
			SocketElement.properties = new ConfigurationPropertyCollection();
			SocketElement.properties.Add(SocketElement.alwaysUseCompletionPortsForAcceptProp);
			SocketElement.properties.Add(SocketElement.alwaysUseCompletionPortsForConnectProp);
		}

		/// <summary>Gets or sets a Boolean value that specifies whether completion ports are used when accepting connections.</summary>
		/// <returns>
		///   <see langword="true" /> to use completion ports; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x06003C62 RID: 15458 RVA: 0x000CE1F5 File Offset: 0x000CC3F5
		// (set) Token: 0x06003C63 RID: 15459 RVA: 0x000CE207 File Offset: 0x000CC407
		[ConfigurationProperty("alwaysUseCompletionPortsForAccept", DefaultValue = "False")]
		public bool AlwaysUseCompletionPortsForAccept
		{
			get
			{
				return (bool)base[SocketElement.alwaysUseCompletionPortsForAcceptProp];
			}
			set
			{
				base[SocketElement.alwaysUseCompletionPortsForAcceptProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether completion ports are used when making connections.</summary>
		/// <returns>
		///   <see langword="true" /> to use completion ports; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x06003C64 RID: 15460 RVA: 0x000CE21A File Offset: 0x000CC41A
		// (set) Token: 0x06003C65 RID: 15461 RVA: 0x000CE22C File Offset: 0x000CC42C
		[ConfigurationProperty("alwaysUseCompletionPortsForConnect", DefaultValue = "False")]
		public bool AlwaysUseCompletionPortsForConnect
		{
			get
			{
				return (bool)base[SocketElement.alwaysUseCompletionPortsForConnectProp];
			}
			set
			{
				base[SocketElement.alwaysUseCompletionPortsForConnectProp] = value;
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06003C66 RID: 15462 RVA: 0x000CE23F File Offset: 0x000CC43F
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SocketElement.properties;
			}
		}

		// Token: 0x06003C67 RID: 15463 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		/// <summary>Gets or sets a value that specifies the default <see cref="T:System.Net.Sockets.IPProtectionLevel" /> to use for a socket.</summary>
		/// <returns>The value of the <see cref="T:System.Net.Sockets.IPProtectionLevel" /> for the current instance.</returns>
		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x06003C68 RID: 15464 RVA: 0x000CE248 File Offset: 0x000CC448
		// (set) Token: 0x06003C69 RID: 15465 RVA: 0x00013BCA File Offset: 0x00011DCA
		public IPProtectionLevel IPProtectionLevel
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return (IPProtectionLevel)0;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x040023C8 RID: 9160
		private static ConfigurationPropertyCollection properties;

		// Token: 0x040023C9 RID: 9161
		private static ConfigurationProperty alwaysUseCompletionPortsForAcceptProp;

		// Token: 0x040023CA RID: 9162
		private static ConfigurationProperty alwaysUseCompletionPortsForConnectProp;
	}
}
