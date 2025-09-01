using System;

namespace System.Threading
{
	/// <summary>Represents a method to be called when a message is to be dispatched to a synchronization context.</summary>
	/// <param name="state">The object passed to the delegate.</param>
	// Token: 0x02000294 RID: 660
	// (Invoke) Token: 0x06001D94 RID: 7572
	public delegate void SendOrPostCallback(object state);
}
