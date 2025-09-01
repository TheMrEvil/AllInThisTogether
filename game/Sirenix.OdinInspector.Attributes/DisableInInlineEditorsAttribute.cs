using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000014 RID: 20
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	public class DisableInInlineEditorsAttribute : Attribute
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002102 File Offset: 0x00000302
		public DisableInInlineEditorsAttribute()
		{
		}
	}
}
