using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Fixes the address of a static value type field throughout its lifetime. This class cannot be inherited.</summary>
	// Token: 0x020007F1 RID: 2033
	[AttributeUsage(AttributeTargets.Field)]
	[Serializable]
	public sealed class FixedAddressValueTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.FixedAddressValueTypeAttribute" /> class.</summary>
		// Token: 0x060045FB RID: 17915 RVA: 0x00002050 File Offset: 0x00000250
		public FixedAddressValueTypeAttribute()
		{
		}
	}
}
