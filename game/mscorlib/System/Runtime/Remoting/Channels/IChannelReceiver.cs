using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides required functions and properties for the receiver channels.</summary>
	// Token: 0x020005B4 RID: 1460
	[ComVisible(true)]
	public interface IChannelReceiver : IChannel
	{
		/// <summary>Gets the channel-specific data.</summary>
		/// <returns>The channel data.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06003875 RID: 14453
		object ChannelData { get; }

		/// <summary>Returns an array of all the URLs for a URI.</summary>
		/// <param name="objectURI">The URI for which URLs are required.</param>
		/// <returns>An array of the URLs.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06003876 RID: 14454
		string[] GetUrlsForUri(string objectURI);

		/// <summary>Instructs the current channel to start listening for requests.</summary>
		/// <param name="data">Optional initialization information.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06003877 RID: 14455
		void StartListening(object data);

		/// <summary>Instructs the current channel to stop listening for requests.</summary>
		/// <param name="data">Optional state information for the channel.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x06003878 RID: 14456
		void StopListening(object data);
	}
}
