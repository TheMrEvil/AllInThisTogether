using System;

namespace System.Net.NetworkInformation
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Net.NetworkInformation.Ping.PingCompleted" /> event of a <see cref="T:System.Net.NetworkInformation.Ping" /> object.</summary>
	/// <param name="sender">The source of the <see cref="E:System.Net.NetworkInformation.Ping.PingCompleted" /> event.</param>
	/// <param name="e">A <see cref="T:System.Net.NetworkInformation.PingCompletedEventArgs" /> object that contains the event data.</param>
	// Token: 0x02000722 RID: 1826
	// (Invoke) Token: 0x06003A50 RID: 14928
	public delegate void PingCompletedEventHandler(object sender, PingCompletedEventArgs e);
}
