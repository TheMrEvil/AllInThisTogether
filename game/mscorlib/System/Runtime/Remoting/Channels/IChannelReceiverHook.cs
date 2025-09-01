using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Indicates that the implementing channel wants to hook into the outside listener service.</summary>
	// Token: 0x020005B5 RID: 1461
	[ComVisible(true)]
	public interface IChannelReceiverHook
	{
		/// <summary>Gets the type of listener to hook into.</summary>
		/// <returns>The type of listener to hook into (for example, "http").</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06003879 RID: 14457
		string ChannelScheme { get; }

		/// <summary>Gets the channel sink chain that the current channel is using.</summary>
		/// <returns>The channel sink chain that the current channel is using.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x0600387A RID: 14458
		IServerChannelSink ChannelSinkChain { get; }

		/// <summary>Gets a Boolean value that indicates whether <see cref="T:System.Runtime.Remoting.Channels.IChannelReceiverHook" /> needs to be hooked into the outside listener service.</summary>
		/// <returns>A Boolean value that indicates whether <see cref="T:System.Runtime.Remoting.Channels.IChannelReceiverHook" /> needs to be hooked into the outside listener service.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x0600387B RID: 14459
		bool WantsToListen { get; }

		/// <summary>Adds a URI on which the channel hook will listen.</summary>
		/// <param name="channelUri">A URI on which the channel hook will listen.</param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x0600387C RID: 14460
		void AddHookChannelUri(string channelUri);
	}
}
