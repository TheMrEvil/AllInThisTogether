using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Contains communication data sent between cooperating message sinks.</summary>
	// Token: 0x0200061B RID: 1563
	[ComVisible(true)]
	public interface IMessage
	{
		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> that represents a collection of the message's properties.</summary>
		/// <returns>A dictionary that represents a collection of the message's properties.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06003AF8 RID: 15096
		IDictionary Properties { get; }
	}
}
