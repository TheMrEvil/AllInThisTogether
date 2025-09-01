using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides a base implementation for channels that want to expose a dictionary interface to their properties.</summary>
	// Token: 0x020005A6 RID: 1446
	[ComVisible(true)]
	public abstract class BaseChannelWithProperties : BaseChannelObjectWithProperties
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.BaseChannelWithProperties" /> class.</summary>
		// Token: 0x06003827 RID: 14375 RVA: 0x000C94BE File Offset: 0x000C76BE
		protected BaseChannelWithProperties()
		{
		}

		/// <summary>Gets a <see cref="T:System.Collections.IDictionary" /> of the channel properties associated with the current channel object.</summary>
		/// <returns>A <see cref="T:System.Collections.IDictionary" /> of the channel properties associated with the current channel object.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06003828 RID: 14376 RVA: 0x000C94C6 File Offset: 0x000C76C6
		public override IDictionary Properties
		{
			get
			{
				if (this.SinksWithProperties == null || this.SinksWithProperties.Properties == null)
				{
					return base.Properties;
				}
				return new AggregateDictionary(new IDictionary[]
				{
					base.Properties,
					this.SinksWithProperties.Properties
				});
			}
		}

		/// <summary>Indicates the top channel sink in the channel sink stack.</summary>
		// Token: 0x040025CE RID: 9678
		protected IChannelSinkBase SinksWithProperties;
	}
}
