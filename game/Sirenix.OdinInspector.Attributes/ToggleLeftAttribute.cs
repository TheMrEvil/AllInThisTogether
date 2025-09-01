using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000073 RID: 115
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class ToggleLeftAttribute : Attribute
	{
		// Token: 0x06000180 RID: 384 RVA: 0x00002102 File Offset: 0x00000302
		public ToggleLeftAttribute()
		{
		}
	}
}
