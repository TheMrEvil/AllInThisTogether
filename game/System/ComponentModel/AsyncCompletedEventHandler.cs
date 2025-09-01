using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents the method that will handle the MethodName<see langword="Completed" /> event of an asynchronous operation.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> that contains the event data.</param>
	// Token: 0x02000409 RID: 1033
	// (Invoke) Token: 0x06002160 RID: 8544
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public delegate void AsyncCompletedEventHandler(object sender, AsyncCompletedEventArgs e);
}
