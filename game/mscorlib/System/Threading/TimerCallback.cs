using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Represents the method that handles calls from a <see cref="T:System.Threading.Timer" />.</summary>
	/// <param name="state">An object containing application-specific information relevant to the method invoked by this delegate, or <see langword="null" />.</param>
	// Token: 0x020002FC RID: 764
	// (Invoke) Token: 0x06002148 RID: 8520
	[ComVisible(true)]
	public delegate void TimerCallback(object state);
}
