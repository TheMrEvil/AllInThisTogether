using System;

namespace System.Diagnostics
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Diagnostics.EventLog.EntryWritten" /> event of an <see cref="T:System.Diagnostics.EventLog" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.Diagnostics.EntryWrittenEventArgs" /> that contains the event data.</param>
	// Token: 0x02000257 RID: 599
	// (Invoke) Token: 0x06001275 RID: 4725
	public delegate void EntryWrittenEventHandler(object sender, EntryWrittenEventArgs e);
}
