using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	/// <summary>Represents the construction call request of an object.</summary>
	// Token: 0x020005D1 RID: 1489
	[ComVisible(true)]
	public interface IConstructionCallMessage : IMessage, IMethodCallMessage, IMethodMessage
	{
		/// <summary>Gets the type of the remote object to activate.</summary>
		/// <returns>The type of the remote object to activate.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060038DB RID: 14555
		Type ActivationType { get; }

		/// <summary>Gets the full type name of the remote type to activate.</summary>
		/// <returns>The full type name of the remote type to activate.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x060038DC RID: 14556
		string ActivationTypeName { get; }

		/// <summary>Gets or sets the activator that activates the remote object.</summary>
		/// <returns>The activator that activates the remote object.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060038DD RID: 14557
		// (set) Token: 0x060038DE RID: 14558
		IActivator Activator { get; set; }

		/// <summary>Gets the call site activation attributes.</summary>
		/// <returns>The call site activation attributes.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060038DF RID: 14559
		object[] CallSiteActivationAttributes { get; }

		/// <summary>Gets a list of context properties that define the context in which the object is to be created.</summary>
		/// <returns>A list of properties of the context in which to construct the object.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060038E0 RID: 14560
		IList ContextProperties { get; }
	}
}
