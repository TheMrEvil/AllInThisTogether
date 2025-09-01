using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Gathers naming information from the context property and determines whether the new context is ok for the context property.</summary>
	// Token: 0x02000595 RID: 1429
	[ComVisible(true)]
	public interface IContextProperty
	{
		/// <summary>Gets the name of the property under which it will be added to the context.</summary>
		/// <returns>The name of the property.</returns>
		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x060037D1 RID: 14289
		string Name { get; }

		/// <summary>Called when the context is frozen.</summary>
		/// <param name="newContext">The context to freeze.</param>
		// Token: 0x060037D2 RID: 14290
		void Freeze(Context newContext);

		/// <summary>Returns a Boolean value indicating whether the context property is compatible with the new context.</summary>
		/// <param name="newCtx">The new context in which the <see cref="T:System.Runtime.Remoting.Contexts.ContextProperty" /> has been created.</param>
		/// <returns>
		///   <see langword="true" /> if the context property can coexist with the other context properties in the given context; otherwise, <see langword="false" />.</returns>
		// Token: 0x060037D3 RID: 14291
		bool IsNewContextOK(Context newCtx);
	}
}
