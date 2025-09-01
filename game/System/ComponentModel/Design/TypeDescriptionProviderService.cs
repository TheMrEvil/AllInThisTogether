using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a type description provider for a specified type.</summary>
	// Token: 0x0200047A RID: 1146
	public abstract class TypeDescriptionProviderService
	{
		/// <summary>Gets a type description provider for the specified object.</summary>
		/// <param name="instance">The object to get a type description provider for.</param>
		/// <returns>A <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> that corresponds with <paramref name="instance" />.</returns>
		// Token: 0x060024B7 RID: 9399
		public abstract TypeDescriptionProvider GetProvider(object instance);

		/// <summary>Gets a type description provider for the specified type.</summary>
		/// <param name="type">The type to get a type description provider for.</param>
		/// <returns>A <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> that corresponds with <paramref name="type" />.</returns>
		// Token: 0x060024B8 RID: 9400
		public abstract TypeDescriptionProvider GetProvider(Type type);

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.TypeDescriptionProviderService" /> class.</summary>
		// Token: 0x060024B9 RID: 9401 RVA: 0x0000219B File Offset: 0x0000039B
		protected TypeDescriptionProviderService()
		{
		}
	}
}
