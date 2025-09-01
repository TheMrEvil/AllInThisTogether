using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property or event should be displayed in a Properties window.</summary>
	// Token: 0x02000364 RID: 868
	[AttributeUsage(AttributeTargets.All)]
	public sealed class BrowsableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BrowsableAttribute" /> class.</summary>
		/// <param name="browsable">
		///   <see langword="true" /> if a property or event can be modified at design time; otherwise, <see langword="false" />. The default is <see langword="true" />.</param>
		// Token: 0x06001CD7 RID: 7383 RVA: 0x00067EB9 File Offset: 0x000660B9
		public BrowsableAttribute(bool browsable)
		{
			this.Browsable = browsable;
		}

		/// <summary>Gets a value indicating whether an object is browsable.</summary>
		/// <returns>
		///   <see langword="true" /> if the object is browsable; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x00067EC8 File Offset: 0x000660C8
		public bool Browsable
		{
			[CompilerGenerated]
			get
			{
				return this.<Browsable>k__BackingField;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">Another object to compare to.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CD9 RID: 7385 RVA: 0x00067ED0 File Offset: 0x000660D0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			BrowsableAttribute browsableAttribute = obj as BrowsableAttribute;
			bool? flag = (browsableAttribute != null) ? new bool?(browsableAttribute.Browsable) : null;
			bool browsable = this.Browsable;
			return flag.GetValueOrDefault() == browsable & flag != null;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001CDA RID: 7386 RVA: 0x00067F1C File Offset: 0x0006611C
		public override int GetHashCode()
		{
			return this.Browsable.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute is the default value for this attribute class; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CDB RID: 7387 RVA: 0x00067F37 File Offset: 0x00066137
		public override bool IsDefaultAttribute()
		{
			return this.Equals(BrowsableAttribute.Default);
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x00067F44 File Offset: 0x00066144
		// Note: this type is marked as 'beforefieldinit'.
		static BrowsableAttribute()
		{
		}

		/// <summary>Specifies that a property or event can be modified at design time. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000E9E RID: 3742
		public static readonly BrowsableAttribute Yes = new BrowsableAttribute(true);

		/// <summary>Specifies that a property or event cannot be modified at design time. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000E9F RID: 3743
		public static readonly BrowsableAttribute No = new BrowsableAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.BrowsableAttribute" />, which is <see cref="F:System.ComponentModel.BrowsableAttribute.Yes" />. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04000EA0 RID: 3744
		public static readonly BrowsableAttribute Default = BrowsableAttribute.Yes;

		// Token: 0x04000EA1 RID: 3745
		[CompilerGenerated]
		private readonly bool <Browsable>k__BackingField;
	}
}
