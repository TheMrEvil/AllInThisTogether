using System;

namespace System.ComponentModel.Design
{
	/// <summary>Represents the method that will handle a <see cref="E:System.ComponentModel.Design.IComponentChangeService.ComponentChanging" /> event.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentChangingEventArgs" /> event that contains the event data.</param>
	// Token: 0x02000443 RID: 1091
	// (Invoke) Token: 0x0600239A RID: 9114
	public delegate void ComponentChangingEventHandler(object sender, ComponentChangingEventArgs e);
}
