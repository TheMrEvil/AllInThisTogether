using System;
using System.ComponentModel.Composition.Primitives;

namespace System.ComponentModel.Composition
{
	/// <summary>Provides methods to satisfy imports on an existing part instance.</summary>
	// Token: 0x02000042 RID: 66
	public interface ICompositionService
	{
		/// <summary>Composes the specified part, with recomposition and validation disabled.</summary>
		/// <param name="part">The part to compose.</param>
		// Token: 0x060001E1 RID: 481
		void SatisfyImportsOnce(ComposablePart part);
	}
}
