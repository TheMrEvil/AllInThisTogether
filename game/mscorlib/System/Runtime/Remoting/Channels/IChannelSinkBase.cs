using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides the base interface for channel sinks.</summary>
	// Token: 0x020005B7 RID: 1463
	[ComVisible(true)]
	public interface IChannelSinkBase
	{
		/// <summary>Gets a dictionary through which properties on the sink can be accessed.</summary>
		/// <returns>A dictionary through which properties on the sink can be accessed, or <see langword="null" /> if the channel sink does not support properties.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x0600387E RID: 14462
		IDictionary Properties { get; }
	}
}
