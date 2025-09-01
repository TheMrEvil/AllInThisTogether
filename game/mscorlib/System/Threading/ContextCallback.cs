using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Represents a method to be called within a new context.</summary>
	/// <param name="state">An object containing information to be used by the callback method each time it executes.</param>
	// Token: 0x020002C7 RID: 711
	// (Invoke) Token: 0x06001EDF RID: 7903
	[ComVisible(true)]
	public delegate void ContextCallback(object state);
}
