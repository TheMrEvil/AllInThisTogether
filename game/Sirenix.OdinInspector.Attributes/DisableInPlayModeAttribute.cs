using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000016 RID: 22
	[AttributeUsage(AttributeTargets.All)]
	[DontApplyToListElements]
	[Conditional("UNITY_EDITOR")]
	public class DisableInPlayModeAttribute : Attribute
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002102 File Offset: 0x00000302
		public DisableInPlayModeAttribute()
		{
		}
	}
}
