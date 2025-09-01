using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies that the property can be used as an application setting.</summary>
	// Token: 0x020003E3 RID: 995
	[AttributeUsage(AttributeTargets.Property)]
	[Obsolete("Use System.ComponentModel.SettingsBindableAttribute instead to work with the new settings model.")]
	public class RecommendedAsConfigurableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RecommendedAsConfigurableAttribute" /> class.</summary>
		/// <param name="recommendedAsConfigurable">
		///   <see langword="true" /> if the property this attribute is bound to can be used as an application setting; otherwise, <see langword="false" />.</param>
		// Token: 0x060020AC RID: 8364 RVA: 0x00070CE0 File Offset: 0x0006EEE0
		public RecommendedAsConfigurableAttribute(bool recommendedAsConfigurable)
		{
			this.RecommendedAsConfigurable = recommendedAsConfigurable;
		}

		/// <summary>Gets a value indicating whether the property this attribute is bound to can be used as an application setting.</summary>
		/// <returns>
		///   <see langword="true" /> if the property this attribute is bound to can be used as an application setting; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060020AD RID: 8365 RVA: 0x00070CEF File Offset: 0x0006EEEF
		public bool RecommendedAsConfigurable
		{
			[CompilerGenerated]
			get
			{
				return this.<RecommendedAsConfigurable>k__BackingField;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">Another object to compare to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060020AE RID: 8366 RVA: 0x00070CF8 File Offset: 0x0006EEF8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RecommendedAsConfigurableAttribute recommendedAsConfigurableAttribute = obj as RecommendedAsConfigurableAttribute;
			return recommendedAsConfigurableAttribute != null && recommendedAsConfigurableAttribute.RecommendedAsConfigurable == this.RecommendedAsConfigurable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.RecommendedAsConfigurableAttribute" />.</returns>
		// Token: 0x060020AF RID: 8367 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether the value of this instance is the default value for the class.</summary>
		/// <returns>
		///   <see langword="true" /> if this instance is the default attribute for the class; otherwise, <see langword="false" />.</returns>
		// Token: 0x060020B0 RID: 8368 RVA: 0x00070D25 File Offset: 0x0006EF25
		public override bool IsDefaultAttribute()
		{
			return !this.RecommendedAsConfigurable;
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x00070D30 File Offset: 0x0006EF30
		// Note: this type is marked as 'beforefieldinit'.
		static RecommendedAsConfigurableAttribute()
		{
		}

		// Token: 0x04000FD3 RID: 4051
		[CompilerGenerated]
		private readonly bool <RecommendedAsConfigurable>k__BackingField;

		/// <summary>Specifies that a property cannot be used as an application setting. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000FD4 RID: 4052
		public static readonly RecommendedAsConfigurableAttribute No = new RecommendedAsConfigurableAttribute(false);

		/// <summary>Specifies that a property can be used as an application setting. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000FD5 RID: 4053
		public static readonly RecommendedAsConfigurableAttribute Yes = new RecommendedAsConfigurableAttribute(true);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.RecommendedAsConfigurableAttribute" />, which is <see cref="F:System.ComponentModel.RecommendedAsConfigurableAttribute.No" />. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000FD6 RID: 4054
		public static readonly RecommendedAsConfigurableAttribute Default = RecommendedAsConfigurableAttribute.No;
	}
}
