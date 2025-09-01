using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A04 RID: 2564
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	public sealed class DisallowNullAttribute : Attribute
	{
		// Token: 0x06005B5A RID: 23386 RVA: 0x00002050 File Offset: 0x00000250
		public DisallowNullAttribute()
		{
		}
	}
}
