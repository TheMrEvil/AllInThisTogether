using System;
using System.Collections.ObjectModel;
using Microsoft.Internal;
using Microsoft.Internal.Collections;

namespace System.ComponentModel.Composition.Primitives
{
	// Token: 0x0200008B RID: 139
	internal class ComposablePartCatalogDebuggerProxy
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x0000ACBC File Offset: 0x00008EBC
		public ComposablePartCatalogDebuggerProxy(ComposablePartCatalog catalog)
		{
			Requires.NotNull<ComposablePartCatalog>(catalog, "catalog");
			this._catalog = catalog;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000ACD6 File Offset: 0x00008ED6
		public ReadOnlyCollection<ComposablePartDefinition> Parts
		{
			get
			{
				return this._catalog.Parts.ToReadOnlyCollection<ComposablePartDefinition>();
			}
		}

		// Token: 0x04000175 RID: 373
		private readonly ComposablePartCatalog _catalog;
	}
}
