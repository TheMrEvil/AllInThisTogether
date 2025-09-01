using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Indicates whether the component associated with this attribute has been inherited from a base class. This class cannot be inherited.</summary>
	// Token: 0x0200039E RID: 926
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event)]
	public sealed class InheritanceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InheritanceAttribute" /> class.</summary>
		// Token: 0x06001E4D RID: 7757 RVA: 0x0006BB33 File Offset: 0x00069D33
		public InheritanceAttribute()
		{
			this.InheritanceLevel = InheritanceAttribute.Default.InheritanceLevel;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InheritanceAttribute" /> class with the specified inheritance level.</summary>
		/// <param name="inheritanceLevel">An <see cref="T:System.ComponentModel.InheritanceLevel" /> that indicates the level of inheritance to set this attribute to.</param>
		// Token: 0x06001E4E RID: 7758 RVA: 0x0006BB4B File Offset: 0x00069D4B
		public InheritanceAttribute(InheritanceLevel inheritanceLevel)
		{
			this.InheritanceLevel = inheritanceLevel;
		}

		/// <summary>Gets or sets the current inheritance level stored in this attribute.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.InheritanceLevel" /> stored in this attribute.</returns>
		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001E4F RID: 7759 RVA: 0x0006BB5A File Offset: 0x00069D5A
		public InheritanceLevel InheritanceLevel
		{
			[CompilerGenerated]
			get
			{
				return this.<InheritanceLevel>k__BackingField;
			}
		}

		/// <summary>Override to test for equality.</summary>
		/// <param name="value">The object to test.</param>
		/// <returns>
		///   <see langword="true" /> if the object is the same; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E50 RID: 7760 RVA: 0x0006BB62 File Offset: 0x00069D62
		public override bool Equals(object value)
		{
			return value == this || (value is InheritanceAttribute && ((InheritanceAttribute)value).InheritanceLevel == this.InheritanceLevel);
		}

		/// <summary>Returns the hashcode for this object.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.InheritanceAttribute" />.</returns>
		// Token: 0x06001E51 RID: 7761 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if the current value of the attribute is the default; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E52 RID: 7762 RVA: 0x0006BB87 File Offset: 0x00069D87
		public override bool IsDefaultAttribute()
		{
			return this.Equals(InheritanceAttribute.Default);
		}

		/// <summary>Converts this attribute to a string.</summary>
		/// <returns>A string that represents this <see cref="T:System.ComponentModel.InheritanceAttribute" />.</returns>
		// Token: 0x06001E53 RID: 7763 RVA: 0x0006BB94 File Offset: 0x00069D94
		public override string ToString()
		{
			return TypeDescriptor.GetConverter(typeof(InheritanceLevel)).ConvertToString(this.InheritanceLevel);
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x0006BBB5 File Offset: 0x00069DB5
		// Note: this type is marked as 'beforefieldinit'.
		static InheritanceAttribute()
		{
		}

		/// <summary>Specifies that the component is inherited. This field is read-only.</summary>
		// Token: 0x04000F1D RID: 3869
		public static readonly InheritanceAttribute Inherited = new InheritanceAttribute(InheritanceLevel.Inherited);

		/// <summary>Specifies that the component is inherited and is read-only. This field is read-only.</summary>
		// Token: 0x04000F1E RID: 3870
		public static readonly InheritanceAttribute InheritedReadOnly = new InheritanceAttribute(InheritanceLevel.InheritedReadOnly);

		/// <summary>Specifies that the component is not inherited. This field is read-only.</summary>
		// Token: 0x04000F1F RID: 3871
		public static readonly InheritanceAttribute NotInherited = new InheritanceAttribute(InheritanceLevel.NotInherited);

		/// <summary>Specifies that the default value for <see cref="T:System.ComponentModel.InheritanceAttribute" /> is <see cref="F:System.ComponentModel.InheritanceAttribute.NotInherited" />. This field is read-only.</summary>
		// Token: 0x04000F20 RID: 3872
		public static readonly InheritanceAttribute Default = InheritanceAttribute.NotInherited;

		// Token: 0x04000F21 RID: 3873
		[CompilerGenerated]
		private readonly InheritanceLevel <InheritanceLevel>k__BackingField;
	}
}
