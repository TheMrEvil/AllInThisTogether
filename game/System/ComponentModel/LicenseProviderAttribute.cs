using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the <see cref="T:System.ComponentModel.LicenseProvider" /> to use with a class. This class cannot be inherited.</summary>
	// Token: 0x020003CA RID: 970
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class LicenseProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseProviderAttribute" /> class without a license provider.</summary>
		// Token: 0x06001F64 RID: 8036 RVA: 0x0006D3C8 File Offset: 0x0006B5C8
		public LicenseProviderAttribute() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseProviderAttribute" /> class with the specified type.</summary>
		/// <param name="typeName">The fully qualified name of the license provider class.</param>
		// Token: 0x06001F65 RID: 8037 RVA: 0x0006D3D1 File Offset: 0x0006B5D1
		public LicenseProviderAttribute(string typeName)
		{
			this._licenseProviderName = typeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseProviderAttribute" /> class with the specified type of license provider.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of the license provider class.</param>
		// Token: 0x06001F66 RID: 8038 RVA: 0x0006D3E0 File Offset: 0x0006B5E0
		public LicenseProviderAttribute(Type type)
		{
			this._licenseProviderType = type;
		}

		/// <summary>Gets the license provider that must be used with the associated class.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of the license provider. The default value is <see langword="null" />.</returns>
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x0006D3EF File Offset: 0x0006B5EF
		public Type LicenseProvider
		{
			get
			{
				if (this._licenseProviderType == null && this._licenseProviderName != null)
				{
					this._licenseProviderType = Type.GetType(this._licenseProviderName);
				}
				return this._licenseProviderType;
			}
		}

		/// <summary>Indicates a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001F68 RID: 8040 RVA: 0x0006D420 File Offset: 0x0006B620
		public override object TypeId
		{
			get
			{
				string text = this._licenseProviderName;
				if (text == null && this._licenseProviderType != null)
				{
					text = this._licenseProviderType.FullName;
				}
				return base.GetType().FullName + text;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <param name="value">Another object to compare to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001F69 RID: 8041 RVA: 0x0006D464 File Offset: 0x0006B664
		public override bool Equals(object value)
		{
			if (value is LicenseProviderAttribute && value != null)
			{
				Type licenseProvider = ((LicenseProviderAttribute)value).LicenseProvider;
				if (licenseProvider == this.LicenseProvider)
				{
					return true;
				}
				if (licenseProvider != null && licenseProvider.Equals(this.LicenseProvider))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.LicenseProviderAttribute" />.</returns>
		// Token: 0x06001F6A RID: 8042 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x0006D4B2 File Offset: 0x0006B6B2
		// Note: this type is marked as 'beforefieldinit'.
		static LicenseProviderAttribute()
		{
		}

		/// <summary>Specifies the default value, which is no provider. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000F53 RID: 3923
		public static readonly LicenseProviderAttribute Default = new LicenseProviderAttribute();

		// Token: 0x04000F54 RID: 3924
		private Type _licenseProviderType;

		// Token: 0x04000F55 RID: 3925
		private string _licenseProviderName;
	}
}
