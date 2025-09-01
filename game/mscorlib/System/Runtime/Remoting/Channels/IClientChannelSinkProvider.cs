using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Creates client channel sinks for the client channel through which remoting messages flow.</summary>
	// Token: 0x020005B9 RID: 1465
	[ComVisible(true)]
	public interface IClientChannelSinkProvider
	{
		/// <summary>Gets or sets the next sink provider in the channel sink provider chain.</summary>
		/// <returns>The next sink provider in the channel sink provider chain.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06003884 RID: 14468
		// (set) Token: 0x06003885 RID: 14469
		IClientChannelSinkProvider Next { get; set; }

		/// <summary>Creates a sink chain.</summary>
		/// <param name="channel">Channel for which the current sink chain is being constructed.</param>
		/// <param name="url">The URL of the object to connect to. This parameter can be <see langword="null" /> if the connection is based entirely on the information contained in the <paramref name="remoteChannelData" /> parameter.</param>
		/// <param name="remoteChannelData">A channel data object that describes a channel on the remote server.</param>
		/// <returns>The first sink of the newly formed channel sink chain, or <see langword="null" />, which indicates that this provider will not or cannot provide a connection for this endpoint.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06003886 RID: 14470
		IClientChannelSink CreateSink(IChannelSender channel, string url, object remoteChannelData);
	}
}
