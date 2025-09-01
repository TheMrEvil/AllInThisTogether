using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition.Primitives;
using Microsoft.Internal;
using Microsoft.Internal.Collections;

namespace System.ComponentModel.Composition.Hosting
{
	// Token: 0x020000D4 RID: 212
	internal class CompositionScopeDefinitionDebuggerProxy
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x000103ED File Offset: 0x0000E5ED
		public CompositionScopeDefinitionDebuggerProxy(CompositionScopeDefinition compositionScopeDefinition)
		{
			Requires.NotNull<CompositionScopeDefinition>(compositionScopeDefinition, "compositionScopeDefinition");
			this._compositionScopeDefinition = compositionScopeDefinition;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00010407 File Offset: 0x0000E607
		public ReadOnlyCollection<ComposablePartDefinition> Parts
		{
			get
			{
				return this._compositionScopeDefinition.Parts.ToReadOnlyCollection<ComposablePartDefinition>();
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00010419 File Offset: 0x0000E619
		public IEnumerable<ExportDefinition> PublicSurface
		{
			get
			{
				return this._compositionScopeDefinition.PublicSurface.ToReadOnlyCollection<ExportDefinition>();
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0001042B File Offset: 0x0000E62B
		public virtual IEnumerable<CompositionScopeDefinition> Children
		{
			get
			{
				return this._compositionScopeDefinition.Children.ToReadOnlyCollection<CompositionScopeDefinition>();
			}
		}

		// Token: 0x0400025B RID: 603
		private readonly CompositionScopeDefinition _compositionScopeDefinition;
	}
}
