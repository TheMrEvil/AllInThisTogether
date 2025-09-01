using System;
using System.Diagnostics;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Primitives
{
	// Token: 0x0200008F RID: 143
	[DebuggerTypeProxy(typeof(CompositionElementDebuggerProxy))]
	[Serializable]
	internal class CompositionElement : SerializableCompositionElement
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x0000AE2C File Offset: 0x0000902C
		public CompositionElement(object underlyingObject) : base(underlyingObject.ToString(), CompositionElement.UnknownOrigin)
		{
			this._underlyingObject = underlyingObject;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000AE46 File Offset: 0x00009046
		public object UnderlyingObject
		{
			get
			{
				return this._underlyingObject;
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000AE4E File Offset: 0x0000904E
		// Note: this type is marked as 'beforefieldinit'.
		static CompositionElement()
		{
		}

		// Token: 0x04000179 RID: 377
		private static readonly ICompositionElement UnknownOrigin = new SerializableCompositionElement(Strings.CompositionElement_UnknownOrigin, null);

		// Token: 0x0400017A RID: 378
		private readonly object _underlyingObject;
	}
}
