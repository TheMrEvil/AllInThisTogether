using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Identifies a context attribute.</summary>
	// Token: 0x02000594 RID: 1428
	[ComVisible(true)]
	public interface IContextAttribute
	{
		/// <summary>Returns context properties to the caller in the given message.</summary>
		/// <param name="msg">The <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> to which to add the context properties.</param>
		// Token: 0x060037CF RID: 14287
		void GetPropertiesForNewContext(IConstructionCallMessage msg);

		/// <summary>Returns a Boolean value indicating whether the specified context meets the context attribute's requirements.</summary>
		/// <param name="ctx">The context to check against the current context attribute.</param>
		/// <param name="msg">The construction call, parameters of which need to be checked against the current context.</param>
		/// <returns>
		///   <see langword="true" /> if the passed in context is okay; otherwise, <see langword="false" />.</returns>
		// Token: 0x060037D0 RID: 14288
		bool IsContextOK(Context ctx, IConstructionCallMessage msg);
	}
}
