using System;

namespace System.Runtime.Serialization
{
	/// <summary>Specifies that the field is an enumeration member and should be serialized.</summary>
	// Token: 0x020000D2 RID: 210
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class EnumMemberAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.EnumMemberAttribute" /> class.</summary>
		// Token: 0x06000C2D RID: 3117 RVA: 0x000254FF File Offset: 0x000236FF
		public EnumMemberAttribute()
		{
		}

		/// <summary>Gets or sets the value associated with the enumeration member the attribute is applied to.</summary>
		/// <returns>The value associated with the enumeration member.</returns>
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x00032B82 File Offset: 0x00030D82
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x00032B8A File Offset: 0x00030D8A
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
				this.isValueSetExplicitly = true;
			}
		}

		/// <summary>Gets whether the <see cref="P:System.Runtime.Serialization.EnumMemberAttribute.Value" /> has been explicitly set.</summary>
		/// <returns>
		///   <see langword="true" /> if the value has been explicitly set; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00032B9A File Offset: 0x00030D9A
		public bool IsValueSetExplicitly
		{
			get
			{
				return this.isValueSetExplicitly;
			}
		}

		// Token: 0x04000515 RID: 1301
		private string value;

		// Token: 0x04000516 RID: 1302
		private bool isValueSetExplicitly;
	}
}
