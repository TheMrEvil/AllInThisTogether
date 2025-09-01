using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000B5 RID: 181
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Delegate | AttributeTargets.GenericParameter)]
	public sealed class CanBeNullAttribute : Attribute
	{
		// Token: 0x0600033D RID: 829 RVA: 0x00002050 File Offset: 0x00000250
		public CanBeNullAttribute()
		{
		}
	}
}
