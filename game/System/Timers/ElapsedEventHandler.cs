using System;

namespace System.Timers
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Timers.Timer.Elapsed" /> event of a <see cref="T:System.Timers.Timer" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.Timers.ElapsedEventArgs" /> object that contains the event data.</param>
	// Token: 0x02000192 RID: 402
	// (Invoke) Token: 0x06000A87 RID: 2695
	public delegate void ElapsedEventHandler(object sender, ElapsedEventArgs e);
}
