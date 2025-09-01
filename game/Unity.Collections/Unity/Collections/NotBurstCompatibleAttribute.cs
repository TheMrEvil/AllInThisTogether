using System;

namespace Unity.Collections
{
	// Token: 0x0200003C RID: 60
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
	public class NotBurstCompatibleAttribute : Attribute
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00002050 File Offset: 0x00000250
		public NotBurstCompatibleAttribute()
		{
		}
	}
}
