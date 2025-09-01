using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Represents a method to be called when a <see cref="T:System.Threading.WaitHandle" /> is signaled or times out.</summary>
	/// <param name="state">An object containing information to be used by the callback method each time it executes.</param>
	/// <param name="timedOut">
	///   <see langword="true" /> if the <see cref="T:System.Threading.WaitHandle" /> timed out; <see langword="false" /> if it was signaled.</param>
	// Token: 0x020002DB RID: 731
	// (Invoke) Token: 0x06002003 RID: 8195
	[ComVisible(true)]
	public delegate void WaitOrTimerCallback(object state, bool timedOut);
}
