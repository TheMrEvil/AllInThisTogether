using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides conduits for messages that cross remoting boundaries.</summary>
	// Token: 0x020005B2 RID: 1458
	[ComVisible(true)]
	public interface IChannel
	{
		/// <summary>Gets the name of the channel.</summary>
		/// <returns>The name of the channel.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x0600386F RID: 14447
		string ChannelName { get; }

		/// <summary>Gets the priority of the channel.</summary>
		/// <returns>An integer that indicates the priority of the channel.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06003870 RID: 14448
		int ChannelPriority { get; }

		/// <summary>Returns the object URI as an out parameter, and the URI of the current channel as the return value.</summary>
		/// <param name="url">The URL of the object.</param>
		/// <param name="objectURI">When this method returns, contains a <see cref="T:System.String" /> that holds the object URI. This parameter is passed uninitialized.</param>
		/// <returns>The URI of the current channel, or <see langword="null" /> if the URI does not belong to this channel.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06003871 RID: 14449
		string Parse(string url, out string objectURI);
	}
}
