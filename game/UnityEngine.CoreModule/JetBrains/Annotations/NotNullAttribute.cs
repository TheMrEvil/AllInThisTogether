using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000B6 RID: 182
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Delegate | AttributeTargets.GenericParameter)]
	public sealed class NotNullAttribute : Attribute
	{
		// Token: 0x0600033E RID: 830 RVA: 0x00002050 File Offset: 0x00000250
		public NotNullAttribute()
		{
		}
	}
}
