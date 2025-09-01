using System;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Primitives
{
	// Token: 0x0200009C RID: 156
	[Serializable]
	internal class SerializableCompositionElement : ICompositionElement
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x0000B8CB File Offset: 0x00009ACB
		public SerializableCompositionElement(string displayName, ICompositionElement origin)
		{
			Assumes.IsTrue(origin == null || origin.GetType().IsSerializable);
			this._displayName = (displayName ?? string.Empty);
			this._origin = origin;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000B900 File Offset: 0x00009B00
		public string DisplayName
		{
			get
			{
				return this._displayName;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000B908 File Offset: 0x00009B08
		public ICompositionElement Origin
		{
			get
			{
				return this._origin;
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000B910 File Offset: 0x00009B10
		public override string ToString()
		{
			return this.DisplayName;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000B918 File Offset: 0x00009B18
		public static ICompositionElement FromICompositionElement(ICompositionElement element)
		{
			if (element == null)
			{
				return null;
			}
			ICompositionElement origin = SerializableCompositionElement.FromICompositionElement(element.Origin);
			return new SerializableCompositionElement(element.DisplayName, origin);
		}

		// Token: 0x0400019D RID: 413
		private readonly string _displayName;

		// Token: 0x0400019E RID: 414
		private readonly ICompositionElement _origin;
	}
}
