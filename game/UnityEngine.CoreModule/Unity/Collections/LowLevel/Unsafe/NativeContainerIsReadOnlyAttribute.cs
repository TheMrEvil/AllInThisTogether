using System;
using UnityEngine.Scripting;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200009F RID: 159
	[AttributeUsage(AttributeTargets.Struct)]
	[RequiredByNativeCode]
	public sealed class NativeContainerIsReadOnlyAttribute : Attribute
	{
		// Token: 0x060002DB RID: 731 RVA: 0x00002050 File Offset: 0x00000250
		public NativeContainerIsReadOnlyAttribute()
		{
		}
	}
}
