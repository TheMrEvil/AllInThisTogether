using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000B8 RID: 184
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Delegate)]
	public sealed class ItemCanBeNullAttribute : Attribute
	{
		// Token: 0x06000340 RID: 832 RVA: 0x00002050 File Offset: 0x00000250
		public ItemCanBeNullAttribute()
		{
		}
	}
}
