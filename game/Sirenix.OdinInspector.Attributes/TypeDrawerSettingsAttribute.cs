using System;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000074 RID: 116
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class TypeDrawerSettingsAttribute : Attribute
	{
		// Token: 0x06000181 RID: 385 RVA: 0x00003D5D File Offset: 0x00001F5D
		public TypeDrawerSettingsAttribute()
		{
		}

		// Token: 0x04000149 RID: 329
		public Type BaseType;

		// Token: 0x0400014A RID: 330
		public TypeInclusionFilter Filter = TypeInclusionFilter.IncludeAll;
	}
}
