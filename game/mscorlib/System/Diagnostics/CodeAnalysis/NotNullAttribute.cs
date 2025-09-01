using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A06 RID: 2566
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	public sealed class NotNullAttribute : Attribute
	{
		// Token: 0x06005B5C RID: 23388 RVA: 0x00002050 File Offset: 0x00000250
		public NotNullAttribute()
		{
		}
	}
}
