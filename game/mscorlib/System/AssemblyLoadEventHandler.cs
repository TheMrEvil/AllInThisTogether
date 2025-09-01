using System;

namespace System
{
	/// <summary>Represents the method that handles the <see cref="E:System.AppDomain.AssemblyLoad" /> event of an <see cref="T:System.AppDomain" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="args">An <see cref="T:System.AssemblyLoadEventArgs" /> that contains the event data.</param>
	// Token: 0x020000FB RID: 251
	// (Invoke) Token: 0x0600074A RID: 1866
	[Serializable]
	public delegate void AssemblyLoadEventHandler(object sender, AssemblyLoadEventArgs args);
}
