using System;

namespace System
{
	/// <summary>Specifies the usage of another attribute class. This class cannot be inherited.</summary>
	// Token: 0x020000FE RID: 254
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	[Serializable]
	public sealed class AttributeUsageAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AttributeUsageAttribute" /> class with the specified list of <see cref="T:System.AttributeTargets" />, the <see cref="P:System.AttributeUsageAttribute.AllowMultiple" /> value, and the <see cref="P:System.AttributeUsageAttribute.Inherited" /> value.</summary>
		/// <param name="validOn">The set of values combined using a bitwise OR operation to indicate which program elements are valid.</param>
		// Token: 0x06000751 RID: 1873 RVA: 0x0002172B File Offset: 0x0001F92B
		public AttributeUsageAttribute(AttributeTargets validOn)
		{
			this._attributeTarget = validOn;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0002174C File Offset: 0x0001F94C
		internal AttributeUsageAttribute(AttributeTargets validOn, bool allowMultiple, bool inherited)
		{
			this._attributeTarget = validOn;
			this._allowMultiple = allowMultiple;
			this._inherited = inherited;
		}

		/// <summary>Gets a set of values identifying which program elements that the indicated attribute can be applied to.</summary>
		/// <returns>One or several <see cref="T:System.AttributeTargets" /> values. The default is <see langword="All" />.</returns>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0002177B File Offset: 0x0001F97B
		public AttributeTargets ValidOn
		{
			get
			{
				return this._attributeTarget;
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether more than one instance of the indicated attribute can be specified for a single program element.</summary>
		/// <returns>
		///   <see langword="true" /> if more than one instance is allowed to be specified; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00021783 File Offset: 0x0001F983
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x0002178B File Offset: 0x0001F98B
		public bool AllowMultiple
		{
			get
			{
				return this._allowMultiple;
			}
			set
			{
				this._allowMultiple = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines whether the indicated attribute is inherited by derived classes and overriding members.</summary>
		/// <returns>
		///   <see langword="true" /> if the attribute can be inherited by derived classes and overriding members; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00021794 File Offset: 0x0001F994
		// (set) Token: 0x06000757 RID: 1879 RVA: 0x0002179C File Offset: 0x0001F99C
		public bool Inherited
		{
			get
			{
				return this._inherited;
			}
			set
			{
				this._inherited = value;
			}
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000217A5 File Offset: 0x0001F9A5
		// Note: this type is marked as 'beforefieldinit'.
		static AttributeUsageAttribute()
		{
		}

		// Token: 0x04001064 RID: 4196
		private AttributeTargets _attributeTarget = AttributeTargets.All;

		// Token: 0x04001065 RID: 4197
		private bool _allowMultiple;

		// Token: 0x04001066 RID: 4198
		private bool _inherited = true;

		// Token: 0x04001067 RID: 4199
		internal static AttributeUsageAttribute Default = new AttributeUsageAttribute(AttributeTargets.All);
	}
}
