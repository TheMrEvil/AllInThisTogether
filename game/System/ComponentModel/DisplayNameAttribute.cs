using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies the display name for a property, event, or public void method which takes no arguments.</summary>
	// Token: 0x0200036B RID: 875
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event)]
	public class DisplayNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DisplayNameAttribute" /> class.</summary>
		// Token: 0x06001CFE RID: 7422 RVA: 0x000682B2 File Offset: 0x000664B2
		public DisplayNameAttribute() : this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DisplayNameAttribute" /> class using the display name.</summary>
		/// <param name="displayName">The display name.</param>
		// Token: 0x06001CFF RID: 7423 RVA: 0x000682BF File Offset: 0x000664BF
		public DisplayNameAttribute(string displayName)
		{
			this.DisplayNameValue = displayName;
		}

		/// <summary>Gets the display name for a property, event, or public void method that takes no arguments stored in this attribute.</summary>
		/// <returns>The display name.</returns>
		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x000682CE File Offset: 0x000664CE
		public virtual string DisplayName
		{
			get
			{
				return this.DisplayNameValue;
			}
		}

		/// <summary>Gets or sets the display name.</summary>
		/// <returns>The display name.</returns>
		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x000682D6 File Offset: 0x000664D6
		// (set) Token: 0x06001D02 RID: 7426 RVA: 0x000682DE File Offset: 0x000664DE
		protected string DisplayNameValue
		{
			[CompilerGenerated]
			get
			{
				return this.<DisplayNameValue>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DisplayNameValue>k__BackingField = value;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.DisplayNameAttribute" /> instances are equal.</summary>
		/// <param name="obj">The <see cref="T:System.ComponentModel.DisplayNameAttribute" /> to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D03 RID: 7427 RVA: 0x000682E8 File Offset: 0x000664E8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DisplayNameAttribute displayNameAttribute = obj as DisplayNameAttribute;
			return displayNameAttribute != null && displayNameAttribute.DisplayName == this.DisplayName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.DisplayNameAttribute" />.</returns>
		// Token: 0x06001D04 RID: 7428 RVA: 0x00068318 File Offset: 0x00066518
		public override int GetHashCode()
		{
			return this.DisplayName.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is the default value for this attribute class; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D05 RID: 7429 RVA: 0x00068325 File Offset: 0x00066525
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DisplayNameAttribute.Default);
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x00068332 File Offset: 0x00066532
		// Note: this type is marked as 'beforefieldinit'.
		static DisplayNameAttribute()
		{
		}

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DisplayNameAttribute" />. This field is read-only.</summary>
		// Token: 0x04000EB6 RID: 3766
		public static readonly DisplayNameAttribute Default = new DisplayNameAttribute();

		// Token: 0x04000EB7 RID: 3767
		[CompilerGenerated]
		private string <DisplayNameValue>k__BackingField;
	}
}
