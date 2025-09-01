using System;

namespace System.Data
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.Common.DbConnection.StateChange" /> event.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="T:System.Data.StateChangeEventArgs" /> that contains the event data.</param>
	// Token: 0x02000132 RID: 306
	// (Invoke) Token: 0x060010A6 RID: 4262
	public delegate void StateChangeEventHandler(object sender, StateChangeEventArgs e);
}
