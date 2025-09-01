using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies the type of persistence to use when serializing a property on a component at design time.</summary>
	// Token: 0x0200036A RID: 874
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event)]
	public sealed class DesignerSerializationVisibilityAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerSerializationVisibilityAttribute" /> class using the specified <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> value.</summary>
		/// <param name="visibility">One of the <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> values.</param>
		// Token: 0x06001CF8 RID: 7416 RVA: 0x00068215 File Offset: 0x00066415
		public DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility visibility)
		{
			this.Visibility = visibility;
		}

		/// <summary>Gets a value indicating the basic serialization mode a serializer should use when determining whether and how to persist the value of a property.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> values. The default is <see cref="F:System.ComponentModel.DesignerSerializationVisibility.Visible" />.</returns>
		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x00068224 File Offset: 0x00066424
		public DesignerSerializationVisibility Visibility
		{
			[CompilerGenerated]
			get
			{
				return this.<Visibility>k__BackingField;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">Another object to compare to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CFA RID: 7418 RVA: 0x0006822C File Offset: 0x0006642C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignerSerializationVisibilityAttribute designerSerializationVisibilityAttribute = obj as DesignerSerializationVisibilityAttribute;
			DesignerSerializationVisibility? designerSerializationVisibility = (designerSerializationVisibilityAttribute != null) ? new DesignerSerializationVisibility?(designerSerializationVisibilityAttribute.Visibility) : null;
			DesignerSerializationVisibility visibility = this.Visibility;
			return designerSerializationVisibility.GetValueOrDefault() == visibility & designerSerializationVisibility != null;
		}

		/// <summary>Returns the hash code for this object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001CFB RID: 7419 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is set to the default value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CFC RID: 7420 RVA: 0x00068278 File Offset: 0x00066478
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DesignerSerializationVisibilityAttribute.Default);
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x00068285 File Offset: 0x00066485
		// Note: this type is marked as 'beforefieldinit'.
		static DesignerSerializationVisibilityAttribute()
		{
		}

		/// <summary>Specifies that a serializer should serialize the contents of the property, rather than the property itself. This field is read-only.</summary>
		// Token: 0x04000EB1 RID: 3761
		public static readonly DesignerSerializationVisibilityAttribute Content = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content);

		/// <summary>Specifies that a serializer should not serialize the value of the property. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000EB2 RID: 3762
		public static readonly DesignerSerializationVisibilityAttribute Hidden = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden);

		/// <summary>Specifies that a serializer should be allowed to serialize the value of the property. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000EB3 RID: 3763
		public static readonly DesignerSerializationVisibilityAttribute Visible = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible);

		/// <summary>Specifies the default value, which is <see cref="F:System.ComponentModel.DesignerSerializationVisibilityAttribute.Visible" />, that is, a visual designer uses default rules to generate the value of a property. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000EB4 RID: 3764
		public static readonly DesignerSerializationVisibilityAttribute Default = DesignerSerializationVisibilityAttribute.Visible;

		// Token: 0x04000EB5 RID: 3765
		[CompilerGenerated]
		private readonly DesignerSerializationVisibility <Visibility>k__BackingField;
	}
}
