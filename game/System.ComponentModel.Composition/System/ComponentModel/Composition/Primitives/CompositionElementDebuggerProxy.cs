using System;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Primitives
{
	// Token: 0x02000090 RID: 144
	internal class CompositionElementDebuggerProxy
	{
		// Token: 0x060003CA RID: 970 RVA: 0x0000AE60 File Offset: 0x00009060
		public CompositionElementDebuggerProxy(CompositionElement element)
		{
			Requires.NotNull<CompositionElement>(element, "element");
			this._element = element;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000AE7A File Offset: 0x0000907A
		public string DisplayName
		{
			get
			{
				return this._element.DisplayName;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000AE87 File Offset: 0x00009087
		public ICompositionElement Origin
		{
			get
			{
				return this._element.Origin;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000AE94 File Offset: 0x00009094
		public object UnderlyingObject
		{
			get
			{
				return this._element.UnderlyingObject;
			}
		}

		// Token: 0x0400017B RID: 379
		private readonly CompositionElement _element;
	}
}
