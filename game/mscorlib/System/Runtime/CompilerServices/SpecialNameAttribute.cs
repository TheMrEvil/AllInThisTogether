using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that a type or member is treated in a special way by the runtime or tools.  This class cannot be inherited.</summary>
	// Token: 0x02000804 RID: 2052
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event)]
	public sealed class SpecialNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.SpecialNameAttribute" /> class.</summary>
		// Token: 0x0600461F RID: 17951 RVA: 0x00002050 File Offset: 0x00000250
		public SpecialNameAttribute()
		{
		}
	}
}
