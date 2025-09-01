using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies whether the property this attribute is bound to is read-only or read/write. This class cannot be inherited</summary>
	// Token: 0x02000377 RID: 887
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ReadOnlyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ReadOnlyAttribute" /> class.</summary>
		/// <param name="isReadOnly">
		///   <see langword="true" /> to show that the property this attribute is bound to is read-only; <see langword="false" /> to show that the property is read/write.</param>
		// Token: 0x06001D39 RID: 7481 RVA: 0x000686D3 File Offset: 0x000668D3
		public ReadOnlyAttribute(bool isReadOnly)
		{
			this.IsReadOnly = isReadOnly;
		}

		/// <summary>Gets a value indicating whether the property this attribute is bound to is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the property this attribute is bound to is read-only; <see langword="false" /> if the property is read/write.</returns>
		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x000686E2 File Offset: 0x000668E2
		public bool IsReadOnly
		{
			[CompilerGenerated]
			get
			{
				return this.<IsReadOnly>k__BackingField;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <param name="value">Another object to compare to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D3B RID: 7483 RVA: 0x000686EC File Offset: 0x000668EC
		public override bool Equals(object value)
		{
			if (this == value)
			{
				return true;
			}
			ReadOnlyAttribute readOnlyAttribute = value as ReadOnlyAttribute;
			bool? flag = (readOnlyAttribute != null) ? new bool?(readOnlyAttribute.IsReadOnly) : null;
			bool isReadOnly = this.IsReadOnly;
			return flag.GetValueOrDefault() == isReadOnly & flag != null;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ReadOnlyAttribute" />.</returns>
		// Token: 0x06001D3C RID: 7484 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is the default value for this attribute class; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D3D RID: 7485 RVA: 0x00068738 File Offset: 0x00066938
		public override bool IsDefaultAttribute()
		{
			return this.IsReadOnly == ReadOnlyAttribute.Default.IsReadOnly;
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x0006874C File Offset: 0x0006694C
		// Note: this type is marked as 'beforefieldinit'.
		static ReadOnlyAttribute()
		{
		}

		/// <summary>Specifies that the property this attribute is bound to is read-only and cannot be modified in the server explorer. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000ECA RID: 3786
		public static readonly ReadOnlyAttribute Yes = new ReadOnlyAttribute(true);

		/// <summary>Specifies that the property this attribute is bound to is read/write and can be modified. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000ECB RID: 3787
		public static readonly ReadOnlyAttribute No = new ReadOnlyAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.ReadOnlyAttribute" />, which is <see cref="F:System.ComponentModel.ReadOnlyAttribute.No" /> (that is, the property this attribute is bound to is read/write). This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000ECC RID: 3788
		public static readonly ReadOnlyAttribute Default = ReadOnlyAttribute.No;

		// Token: 0x04000ECD RID: 3789
		[CompilerGenerated]
		private readonly bool <IsReadOnly>k__BackingField;
	}
}
