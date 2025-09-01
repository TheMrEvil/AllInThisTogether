using System;

namespace System.Net.NetworkInformation
{
	/// <summary>References one or more methods to be called when the address of a network interface changes.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains data about the event.</param>
	// Token: 0x020006F2 RID: 1778
	// (Invoke) Token: 0x0600393C RID: 14652
	public delegate void NetworkAddressChangedEventHandler(object sender, EventArgs e);
}
