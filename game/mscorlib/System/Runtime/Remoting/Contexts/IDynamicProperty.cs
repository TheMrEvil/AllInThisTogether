using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Indicates that the implementing property should be registered at runtime through the <see cref="M:System.Runtime.Remoting.Contexts.Context.RegisterDynamicProperty(System.Runtime.Remoting.Contexts.IDynamicProperty,System.ContextBoundObject,System.Runtime.Remoting.Contexts.Context)" /> method.</summary>
	// Token: 0x0200059D RID: 1437
	[ComVisible(true)]
	public interface IDynamicProperty
	{
		/// <summary>Gets the name of the dynamic property.</summary>
		/// <returns>The name of the dynamic property.</returns>
		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x060037E0 RID: 14304
		string Name { get; }
	}
}
