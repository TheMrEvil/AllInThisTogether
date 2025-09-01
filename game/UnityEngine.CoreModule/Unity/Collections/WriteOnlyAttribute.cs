using System;
using UnityEngine.Scripting;

namespace Unity.Collections
{
	// Token: 0x02000087 RID: 135
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	[RequiredByNativeCode]
	public sealed class WriteOnlyAttribute : Attribute
	{
		// Token: 0x06000250 RID: 592 RVA: 0x00002050 File Offset: 0x00000250
		public WriteOnlyAttribute()
		{
		}
	}
}
