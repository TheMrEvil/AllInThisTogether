using System;

namespace System.ComponentModel.Composition
{
	/// <summary>Specifies that a custom attribute's properties provide metadata for exports applied to the same type, property, field, or method.</summary>
	// Token: 0x0200004B RID: 75
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class MetadataAttributeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.MetadataAttributeAttribute" /> class.</summary>
		// Token: 0x0600020F RID: 527 RVA: 0x00003F2F File Offset: 0x0000212F
		public MetadataAttributeAttribute()
		{
		}
	}
}
