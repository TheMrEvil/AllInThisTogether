using System;

namespace System
{
	/// <summary>Represents the method that will handle the event raised by an exception that is not handled by the application domain.</summary>
	/// <param name="sender">The source of the unhandled exception event.</param>
	/// <param name="e">An UnhandledExceptionEventArgs that contains the event data.</param>
	// Token: 0x020001AB RID: 427
	// (Invoke) Token: 0x06001267 RID: 4711
	[Serializable]
	public delegate void UnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs e);
}
