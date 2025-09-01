using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Provides type information for an object.</summary>
	// Token: 0x0200055F RID: 1375
	[ComVisible(true)]
	public interface IRemotingTypeInfo
	{
		/// <summary>Gets or sets the fully qualified type name of the server object in a <see cref="T:System.Runtime.Remoting.ObjRef" />.</summary>
		/// <returns>The fully qualified type name of the server object in a <see cref="T:System.Runtime.Remoting.ObjRef" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060035E5 RID: 13797
		// (set) Token: 0x060035E6 RID: 13798
		string TypeName { get; set; }

		/// <summary>Checks whether the proxy that represents the specified object type can be cast to the type represented by the <see cref="T:System.Runtime.Remoting.IRemotingTypeInfo" /> interface.</summary>
		/// <param name="fromType">The type to cast to.</param>
		/// <param name="o">The object for which to check casting.</param>
		/// <returns>
		///   <see langword="true" /> if cast will succeed; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller makes the call through a reference to the interface and does not have infrastructure permission.</exception>
		// Token: 0x060035E7 RID: 13799
		bool CanCastTo(Type fromType, object o);
	}
}
