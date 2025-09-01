using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that a native enumeration is not qualified by the enumeration type name. This class cannot be inherited.</summary>
	// Token: 0x02000848 RID: 2120
	[AttributeUsage(AttributeTargets.Enum)]
	[Serializable]
	public sealed class ScopelessEnumAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.ScopelessEnumAttribute" /> class.</summary>
		// Token: 0x060046C7 RID: 18119 RVA: 0x00002050 File Offset: 0x00000250
		public ScopelessEnumAttribute()
		{
		}
	}
}
