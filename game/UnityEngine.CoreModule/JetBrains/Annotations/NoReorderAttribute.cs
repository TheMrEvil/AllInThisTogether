using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000D6 RID: 214
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	public sealed class NoReorderAttribute : Attribute
	{
		// Token: 0x0600037C RID: 892 RVA: 0x00002050 File Offset: 0x00000250
		public NoReorderAttribute()
		{
		}
	}
}
