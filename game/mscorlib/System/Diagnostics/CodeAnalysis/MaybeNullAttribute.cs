using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A05 RID: 2565
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	public sealed class MaybeNullAttribute : Attribute
	{
		// Token: 0x06005B5B RID: 23387 RVA: 0x00002050 File Offset: 0x00000250
		public MaybeNullAttribute()
		{
		}
	}
}
