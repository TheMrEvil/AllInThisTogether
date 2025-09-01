using System;
using UnityEngine.Scripting;

namespace UnityEngine.TestTools
{
	// Token: 0x0200048D RID: 1165
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method)]
	[UsedByNativeCode]
	public class ExcludeFromCoverageAttribute : Attribute
	{
		// Token: 0x06002946 RID: 10566 RVA: 0x00002050 File Offset: 0x00000250
		public ExcludeFromCoverageAttribute()
		{
		}
	}
}
