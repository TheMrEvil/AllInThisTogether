using System;

namespace System.Configuration.Internal
{
	/// <summary>Defines a class used by the .NET Framework infrastructure to support configuration events.</summary>
	/// <param name="sender">The source object of the event.</param>
	/// <param name="e">A configuration event argument.</param>
	// Token: 0x02000087 RID: 135
	// (Invoke) Token: 0x06000485 RID: 1157
	public delegate void InternalConfigEventHandler(object sender, InternalConfigEventArgs e);
}
