using System;

namespace System.ComponentModel.Composition
{
	/// <summary>Specifies that this type's exports won't be included in a <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" />.</summary>
	// Token: 0x02000052 RID: 82
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class PartNotDiscoverableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.PartNotDiscoverableAttribute" /> class.</summary>
		// Token: 0x06000229 RID: 553 RVA: 0x00003F2F File Offset: 0x0000212F
		public PartNotDiscoverableAttribute()
		{
		}
	}
}
