using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Contributes an object-specific interception sink on the server end of a remoting call.</summary>
	// Token: 0x0200059A RID: 1434
	[ComVisible(true)]
	public interface IContributeObjectSink
	{
		/// <summary>Chains the message sink of the provided server object in front of the given sink chain.</summary>
		/// <param name="obj">The server object which provides the message sink that is to be chained in front of the given chain.</param>
		/// <param name="nextSink">The chain of sinks composed so far.</param>
		/// <returns>The composite sink chain.</returns>
		// Token: 0x060037DC RID: 14300
		IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink);
	}
}
