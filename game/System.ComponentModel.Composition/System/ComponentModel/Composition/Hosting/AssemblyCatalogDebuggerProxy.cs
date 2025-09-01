using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using Microsoft.Internal;
using Microsoft.Internal.Collections;

namespace System.ComponentModel.Composition.Hosting
{
	// Token: 0x020000A2 RID: 162
	internal class AssemblyCatalogDebuggerProxy
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x0000C420 File Offset: 0x0000A620
		public AssemblyCatalogDebuggerProxy(AssemblyCatalog catalog)
		{
			Requires.NotNull<AssemblyCatalog>(catalog, "catalog");
			this._catalog = catalog;
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0000C43A File Offset: 0x0000A63A
		public Assembly Assembly
		{
			get
			{
				return this._catalog.Assembly;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0000C447 File Offset: 0x0000A647
		public ReadOnlyCollection<ComposablePartDefinition> Parts
		{
			get
			{
				return this._catalog.Parts.ToReadOnlyCollection<ComposablePartDefinition>();
			}
		}

		// Token: 0x040001B1 RID: 433
		private readonly AssemblyCatalog _catalog;
	}
}
