using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A03 RID: 2563
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	public sealed class AllowNullAttribute : Attribute
	{
		// Token: 0x06005B59 RID: 23385 RVA: 0x00002050 File Offset: 0x00000250
		public AllowNullAttribute()
		{
		}
	}
}
