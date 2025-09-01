using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000069 RID: 105
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class SuppressInvalidAttributeErrorAttribute : Attribute
	{
		// Token: 0x06000166 RID: 358 RVA: 0x00002102 File Offset: 0x00000302
		public SuppressInvalidAttributeErrorAttribute()
		{
		}
	}
}
