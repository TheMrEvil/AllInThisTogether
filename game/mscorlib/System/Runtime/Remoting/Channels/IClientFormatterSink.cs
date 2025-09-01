using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Marks a sink as a client formatter sink that serializes messages into a stream.</summary>
	// Token: 0x020005BB RID: 1467
	[ComVisible(true)]
	public interface IClientFormatterSink : IMessageSink, IClientChannelSink, IChannelSinkBase
	{
	}
}
