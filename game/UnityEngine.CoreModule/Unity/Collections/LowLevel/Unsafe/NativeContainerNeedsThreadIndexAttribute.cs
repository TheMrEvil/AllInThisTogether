using System;
using UnityEngine.Scripting;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x020000A5 RID: 165
	[AttributeUsage(AttributeTargets.Struct)]
	[Obsolete("Use NativeSetThreadIndexAttribute instead")]
	[RequiredByNativeCode]
	public sealed class NativeContainerNeedsThreadIndexAttribute : Attribute
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x00002050 File Offset: 0x00000250
		public NativeContainerNeedsThreadIndexAttribute()
		{
		}
	}
}
