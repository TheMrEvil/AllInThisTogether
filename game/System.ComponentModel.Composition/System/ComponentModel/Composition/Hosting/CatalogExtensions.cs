using System;
using System.ComponentModel.Composition.Primitives;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Hosting
{
	/// <summary>Provides extension methods for constructing composition services.</summary>
	// Token: 0x020000BD RID: 189
	public static class CatalogExtensions
	{
		/// <summary>Creates a new composition service by using the specified catalog as a source for exports.</summary>
		/// <param name="composablePartCatalog">The catalog that will provide exports.</param>
		/// <returns>A new composition service.</returns>
		// Token: 0x060004DA RID: 1242 RVA: 0x0000E35F File Offset: 0x0000C55F
		public static CompositionService CreateCompositionService(this ComposablePartCatalog composablePartCatalog)
		{
			Requires.NotNull<ComposablePartCatalog>(composablePartCatalog, "composablePartCatalog");
			return new CompositionService(composablePartCatalog);
		}
	}
}
