using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event. This class cannot be inherited.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.DoWorkEventArgs" /> that contains the event data.</param>
	// Token: 0x02000414 RID: 1044
	// (Invoke) Token: 0x060021B9 RID: 8633
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void DoWorkEventHandler(object sender, DoWorkEventArgs e);
}
