using System;

namespace System.ComponentModel
{
	/// <summary>Provides a base class for the container filter service.</summary>
	// Token: 0x0200038F RID: 911
	public abstract class ContainerFilterService
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ContainerFilterService" /> class.</summary>
		// Token: 0x06001DEE RID: 7662 RVA: 0x0000219B File Offset: 0x0000039B
		protected ContainerFilterService()
		{
		}

		/// <summary>Filters the component collection.</summary>
		/// <param name="components">The component collection to filter.</param>
		/// <returns>A <see cref="T:System.ComponentModel.ComponentCollection" /> that represents a modified collection.</returns>
		// Token: 0x06001DEF RID: 7663 RVA: 0x00003914 File Offset: 0x00001B14
		public virtual ComponentCollection FilterComponents(ComponentCollection components)
		{
			return components;
		}
	}
}
