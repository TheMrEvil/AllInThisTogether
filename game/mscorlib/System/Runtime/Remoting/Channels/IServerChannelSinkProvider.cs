using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Creates server channel sinks for the server channel through which remoting messages flow.</summary>
	// Token: 0x020005C0 RID: 1472
	[ComVisible(true)]
	public interface IServerChannelSinkProvider
	{
		/// <summary>Gets or sets the next sink provider in the channel sink provider chain.</summary>
		/// <returns>The next sink provider in the channel sink provider chain.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06003892 RID: 14482
		// (set) Token: 0x06003893 RID: 14483
		IServerChannelSinkProvider Next { get; set; }

		/// <summary>Creates a sink chain.</summary>
		/// <param name="channel">The channel for which to create the channel sink chain.</param>
		/// <returns>The first sink of the newly formed channel sink chain, or <see langword="null" />, which indicates that this provider will not or cannot provide a connection for this endpoint.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06003894 RID: 14484
		IServerChannelSink CreateSink(IChannelReceiver channel);

		/// <summary>Returns the channel data for the channel that the current sink is associated with.</summary>
		/// <param name="channelData">A <see cref="T:System.Runtime.Remoting.Channels.IChannelDataStore" /> object in which the channel data is to be returned.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06003895 RID: 14485
		void GetChannelData(IChannelDataStore channelData);
	}
}
