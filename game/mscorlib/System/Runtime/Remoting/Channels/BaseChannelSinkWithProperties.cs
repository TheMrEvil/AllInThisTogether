using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides a base implementation for channel sinks that want to expose a dictionary interface to their properties.</summary>
	// Token: 0x020005A5 RID: 1445
	[ComVisible(true)]
	public abstract class BaseChannelSinkWithProperties : BaseChannelObjectWithProperties
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.BaseChannelSinkWithProperties" /> class.</summary>
		// Token: 0x06003826 RID: 14374 RVA: 0x000C94BE File Offset: 0x000C76BE
		protected BaseChannelSinkWithProperties()
		{
		}
	}
}
