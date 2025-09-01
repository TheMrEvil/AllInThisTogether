using System;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides an interface that can extend a designer host to support loading from a serialized state.</summary>
	// Token: 0x02000485 RID: 1157
	public interface IDesignerLoaderHost : IDesignerHost, IServiceContainer, IServiceProvider
	{
		/// <summary>Ends the designer loading operation.</summary>
		/// <param name="baseClassName">The fully qualified name of the base class of the document that this designer is designing.</param>
		/// <param name="successful">
		///   <see langword="true" /> if the designer is successfully loaded; otherwise, <see langword="false" />.</param>
		/// <param name="errorCollection">A collection containing the errors encountered during load, if any. If no errors were encountered, pass either an empty collection or <see langword="null" />.</param>
		// Token: 0x06002515 RID: 9493
		void EndLoad(string baseClassName, bool successful, ICollection errorCollection);

		/// <summary>Reloads the design document.</summary>
		// Token: 0x06002516 RID: 9494
		void Reload();
	}
}
