using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Activation
{
	/// <summary>Provides the basic functionality for a remoting activator class.</summary>
	// Token: 0x020005D0 RID: 1488
	[ComVisible(true)]
	public interface IActivator
	{
		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Activation.ActivatorLevel" /> where this activator is active.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Activation.ActivatorLevel" /> where this activator is active.</returns>
		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060038D7 RID: 14551
		ActivatorLevel Level { get; }

		/// <summary>Gets or sets the next activator in the chain.</summary>
		/// <returns>The next activator in the chain.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060038D8 RID: 14552
		// (set) Token: 0x060038D9 RID: 14553
		IActivator NextActivator { get; set; }

		/// <summary>Creates an instance of the object that is specified in the provided <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.</summary>
		/// <param name="msg">The information about the object that is needed to activate it, stored in a <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.</param>
		/// <returns>Status of the object activation contained in a <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x060038DA RID: 14554
		IConstructionReturnMessage Activate(IConstructionCallMessage msg);
	}
}
