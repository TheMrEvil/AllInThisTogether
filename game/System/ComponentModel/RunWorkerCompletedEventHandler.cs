using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents the method that will handle the <see cref="E:System.ComponentModel.BackgroundWorker.RunWorkerCompleted" /> event of a <see cref="T:System.ComponentModel.BackgroundWorker" /> class.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.RunWorkerCompletedEventArgs" /> that contains the event data.</param>
	// Token: 0x02000420 RID: 1056
	// (Invoke) Token: 0x06002259 RID: 8793
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void RunWorkerCompletedEventHandler(object sender, RunWorkerCompletedEventArgs e);
}
