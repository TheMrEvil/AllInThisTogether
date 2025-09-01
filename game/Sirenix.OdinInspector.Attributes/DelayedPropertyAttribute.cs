using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200000D RID: 13
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class DelayedPropertyAttribute : Attribute
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002102 File Offset: 0x00000302
		public DelayedPropertyAttribute()
		{
		}
	}
}
