using System;

namespace System.ComponentModel
{
	/// <summary>Indicates that the property grid should refresh when the associated property value changes. This class cannot be inherited.</summary>
	// Token: 0x02000437 RID: 1079
	[AttributeUsage(AttributeTargets.All)]
	public sealed class RefreshPropertiesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RefreshPropertiesAttribute" /> class.</summary>
		/// <param name="refresh">A <see cref="T:System.ComponentModel.RefreshProperties" /> value indicating the nature of the refresh.</param>
		// Token: 0x06002367 RID: 9063 RVA: 0x00080D14 File Offset: 0x0007EF14
		public RefreshPropertiesAttribute(RefreshProperties refresh)
		{
			this.refresh = refresh;
		}

		/// <summary>Gets the refresh properties for the member.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.RefreshProperties" /> that indicates the current refresh properties for the member.</returns>
		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06002368 RID: 9064 RVA: 0x00080D23 File Offset: 0x0007EF23
		public RefreshProperties RefreshProperties
		{
			get
			{
				return this.refresh;
			}
		}

		/// <summary>Overrides the object's <see cref="Overload:System.Object.Equals" /> method.</summary>
		/// <param name="value">The object to test for equality.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is the same; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002369 RID: 9065 RVA: 0x00080D2B File Offset: 0x0007EF2B
		public override bool Equals(object value)
		{
			return value is RefreshPropertiesAttribute && ((RefreshPropertiesAttribute)value).RefreshProperties == this.refresh;
		}

		/// <summary>Returns the hash code for this object.</summary>
		/// <returns>The hash code for the object that the attribute belongs to.</returns>
		// Token: 0x0600236A RID: 9066 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if the current value of the attribute is the default; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600236B RID: 9067 RVA: 0x00080D4A File Offset: 0x0007EF4A
		public override bool IsDefaultAttribute()
		{
			return this.Equals(RefreshPropertiesAttribute.Default);
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x00080D57 File Offset: 0x0007EF57
		// Note: this type is marked as 'beforefieldinit'.
		static RefreshPropertiesAttribute()
		{
		}

		/// <summary>Indicates that all properties are queried again and refreshed if the property value is changed. This field is read-only.</summary>
		// Token: 0x040010A2 RID: 4258
		public static readonly RefreshPropertiesAttribute All = new RefreshPropertiesAttribute(RefreshProperties.All);

		/// <summary>Indicates that all properties are repainted if the property value is changed. This field is read-only.</summary>
		// Token: 0x040010A3 RID: 4259
		public static readonly RefreshPropertiesAttribute Repaint = new RefreshPropertiesAttribute(RefreshProperties.Repaint);

		/// <summary>Indicates that no other properties are refreshed if the property value is changed. This field is read-only.</summary>
		// Token: 0x040010A4 RID: 4260
		public static readonly RefreshPropertiesAttribute Default = new RefreshPropertiesAttribute(RefreshProperties.None);

		// Token: 0x040010A5 RID: 4261
		private RefreshProperties refresh;
	}
}
