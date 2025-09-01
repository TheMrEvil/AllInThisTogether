using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies a description for a property or event.</summary>
	// Token: 0x02000366 RID: 870
	[AttributeUsage(AttributeTargets.All)]
	public class DescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DescriptionAttribute" /> class with no parameters.</summary>
		// Token: 0x06001CE1 RID: 7393 RVA: 0x00068003 File Offset: 0x00066203
		public DescriptionAttribute() : this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DescriptionAttribute" /> class with a description.</summary>
		/// <param name="description">The description text.</param>
		// Token: 0x06001CE2 RID: 7394 RVA: 0x00068010 File Offset: 0x00066210
		public DescriptionAttribute(string description)
		{
			this.DescriptionValue = description;
		}

		/// <summary>Gets the description stored in this attribute.</summary>
		/// <returns>The description stored in this attribute.</returns>
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001CE3 RID: 7395 RVA: 0x0006801F File Offset: 0x0006621F
		public virtual string Description
		{
			get
			{
				return this.DescriptionValue;
			}
		}

		/// <summary>Gets or sets the string stored as the description.</summary>
		/// <returns>The string stored as the description. The default value is an empty string ("").</returns>
		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x00068027 File Offset: 0x00066227
		// (set) Token: 0x06001CE5 RID: 7397 RVA: 0x0006802F File Offset: 0x0006622F
		protected string DescriptionValue
		{
			[CompilerGenerated]
			get
			{
				return this.<DescriptionValue>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DescriptionValue>k__BackingField = value;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DescriptionAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CE6 RID: 7398 RVA: 0x00068038 File Offset: 0x00066238
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DescriptionAttribute descriptionAttribute = obj as DescriptionAttribute;
			return descriptionAttribute != null && descriptionAttribute.Description == this.Description;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001CE7 RID: 7399 RVA: 0x00068068 File Offset: 0x00066268
		public override int GetHashCode()
		{
			return this.Description.GetHashCode();
		}

		/// <summary>Returns a value indicating whether this is the default <see cref="T:System.ComponentModel.DescriptionAttribute" /> instance.</summary>
		/// <returns>
		///   <see langword="true" />, if this is the default <see cref="T:System.ComponentModel.DescriptionAttribute" /> instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CE8 RID: 7400 RVA: 0x00068075 File Offset: 0x00066275
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DescriptionAttribute.Default);
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x00068082 File Offset: 0x00066282
		// Note: this type is marked as 'beforefieldinit'.
		static DescriptionAttribute()
		{
		}

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DescriptionAttribute" />, which is an empty string (""). This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000EA2 RID: 3746
		public static readonly DescriptionAttribute Default = new DescriptionAttribute();

		// Token: 0x04000EA3 RID: 3747
		[CompilerGenerated]
		private string <DescriptionValue>k__BackingField;
	}
}
