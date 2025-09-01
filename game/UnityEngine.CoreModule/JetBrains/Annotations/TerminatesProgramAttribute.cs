using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000D2 RID: 210
	[Obsolete("Use [ContractAnnotation('=> halt')] instead")]
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class TerminatesProgramAttribute : Attribute
	{
		// Token: 0x06000378 RID: 888 RVA: 0x00002050 File Offset: 0x00000250
		public TerminatesProgramAttribute()
		{
		}
	}
}
