using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property can only be set at design time.</summary>
	// Token: 0x02000367 RID: 871
	[AttributeUsage(AttributeTargets.All)]
	public sealed class DesignOnlyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignOnlyAttribute" /> class.</summary>
		/// <param name="isDesignOnly">
		///   <see langword="true" /> if a property can be set only at design time; <see langword="false" /> if the property can be set at design time and at run time.</param>
		// Token: 0x06001CEA RID: 7402 RVA: 0x0006808E File Offset: 0x0006628E
		public DesignOnlyAttribute(bool isDesignOnly)
		{
			this.IsDesignOnly = isDesignOnly;
		}

		/// <summary>Gets a value indicating whether a property can be set only at design time.</summary>
		/// <returns>
		///   <see langword="true" /> if a property can be set only at design time; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001CEB RID: 7403 RVA: 0x0006809D File Offset: 0x0006629D
		public bool IsDesignOnly
		{
			[CompilerGenerated]
			get
			{
				return this.<IsDesignOnly>k__BackingField;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DesignOnlyAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CEC RID: 7404 RVA: 0x000680A8 File Offset: 0x000662A8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignOnlyAttribute designOnlyAttribute = obj as DesignOnlyAttribute;
			bool? flag = (designOnlyAttribute != null) ? new bool?(designOnlyAttribute.IsDesignOnly) : null;
			bool isDesignOnly = this.IsDesignOnly;
			return flag.GetValueOrDefault() == isDesignOnly & flag != null;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001CED RID: 7405 RVA: 0x000680F4 File Offset: 0x000662F4
		public override int GetHashCode()
		{
			return this.IsDesignOnly.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is the default value for this attribute class; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CEE RID: 7406 RVA: 0x0006810F File Offset: 0x0006630F
		public override bool IsDefaultAttribute()
		{
			return this.IsDesignOnly == DesignOnlyAttribute.Default.IsDesignOnly;
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x00068123 File Offset: 0x00066323
		// Note: this type is marked as 'beforefieldinit'.
		static DesignOnlyAttribute()
		{
		}

		// Token: 0x04000EA4 RID: 3748
		[CompilerGenerated]
		private readonly bool <IsDesignOnly>k__BackingField;

		/// <summary>Specifies that a property can be set only at design time. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000EA5 RID: 3749
		public static readonly DesignOnlyAttribute Yes = new DesignOnlyAttribute(true);

		/// <summary>Specifies that a property can be set at design time or at run time. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000EA6 RID: 3750
		public static readonly DesignOnlyAttribute No = new DesignOnlyAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DesignOnlyAttribute" />, which is <see cref="F:System.ComponentModel.DesignOnlyAttribute.No" />. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000EA7 RID: 3751
		public static readonly DesignOnlyAttribute Default = DesignOnlyAttribute.No;
	}
}
