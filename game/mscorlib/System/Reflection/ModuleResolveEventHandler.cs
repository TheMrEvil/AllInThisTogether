using System;

namespace System.Reflection
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Reflection.Assembly.ModuleResolve" /> event of an <see cref="T:System.Reflection.Assembly" />.</summary>
	/// <param name="sender">The assembly that was the source of the event.</param>
	/// <param name="e">The arguments supplied by the object describing the event.</param>
	/// <returns>The module that satisfies the request.</returns>
	// Token: 0x020008B2 RID: 2226
	// (Invoke) Token: 0x060049AF RID: 18863
	public delegate Module ModuleResolveEventHandler(object sender, ResolveEventArgs e);
}
