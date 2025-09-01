using System;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>The <see cref="T:System.Runtime.Remoting.Channels.ISecurableChannel" /> contains one property, <see cref="P:System.Runtime.Remoting.Channels.ISecurableChannel.IsSecured" />, which gets or sets a Boolean value that indicates whether the current channel is secure.</summary>
	// Token: 0x020005BE RID: 1470
	public interface ISecurableChannel
	{
		/// <summary>Gets or sets a Boolean value that indicates whether the current channel is secure.</summary>
		/// <returns>A Boolean value that indicates whether the current channel is secure.</returns>
		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x0600388C RID: 14476
		// (set) Token: 0x0600388D RID: 14477
		bool IsSecured { get; set; }
	}
}
