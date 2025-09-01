using System;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x02000078 RID: 120
	internal abstract class ReflectionItem
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000329 RID: 809
		public abstract string Name { get; }

		// Token: 0x0600032A RID: 810
		public abstract string GetDisplayName();

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600032B RID: 811
		public abstract Type ReturnType { get; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600032C RID: 812
		public abstract ReflectionItemType ItemType { get; }

		// Token: 0x0600032D RID: 813 RVA: 0x00002BAC File Offset: 0x00000DAC
		protected ReflectionItem()
		{
		}
	}
}
