using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property should be localized. This class cannot be inherited.</summary>
	// Token: 0x02000375 RID: 885
	[AttributeUsage(AttributeTargets.All)]
	public sealed class LocalizableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LocalizableAttribute" /> class.</summary>
		/// <param name="isLocalizable">
		///   <see langword="true" /> if a property should be localized; otherwise, <see langword="false" />.</param>
		// Token: 0x06001D2D RID: 7469 RVA: 0x000685A2 File Offset: 0x000667A2
		public LocalizableAttribute(bool isLocalizable)
		{
			this.IsLocalizable = isLocalizable;
		}

		/// <summary>Gets a value indicating whether a property should be localized.</summary>
		/// <returns>
		///   <see langword="true" /> if a property should be localized; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001D2E RID: 7470 RVA: 0x000685B1 File Offset: 0x000667B1
		public bool IsLocalizable
		{
			[CompilerGenerated]
			get
			{
				return this.<IsLocalizable>k__BackingField;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.LocalizableAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D2F RID: 7471 RVA: 0x000685BC File Offset: 0x000667BC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			LocalizableAttribute localizableAttribute = obj as LocalizableAttribute;
			bool? flag = (localizableAttribute != null) ? new bool?(localizableAttribute.IsLocalizable) : null;
			bool isLocalizable = this.IsLocalizable;
			return flag.GetValueOrDefault() == isLocalizable & flag != null;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.LocalizableAttribute" />.</returns>
		// Token: 0x06001D30 RID: 7472 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is the default value for this attribute class; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D31 RID: 7473 RVA: 0x00068608 File Offset: 0x00066808
		public override bool IsDefaultAttribute()
		{
			return this.IsLocalizable == LocalizableAttribute.Default.IsLocalizable;
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x0006861C File Offset: 0x0006681C
		// Note: this type is marked as 'beforefieldinit'.
		static LocalizableAttribute()
		{
		}

		// Token: 0x04000EC2 RID: 3778
		[CompilerGenerated]
		private readonly bool <IsLocalizable>k__BackingField;

		/// <summary>Specifies that a property should be localized. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000EC3 RID: 3779
		public static readonly LocalizableAttribute Yes = new LocalizableAttribute(true);

		/// <summary>Specifies that a property should not be localized. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000EC4 RID: 3780
		public static readonly LocalizableAttribute No = new LocalizableAttribute(false);

		/// <summary>Specifies the default value, which is <see cref="F:System.ComponentModel.LocalizableAttribute.No" />. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000EC5 RID: 3781
		public static readonly LocalizableAttribute Default = LocalizableAttribute.No;
	}
}
