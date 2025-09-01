using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000031 RID: 49
	[AttributeUsage(AttributeTargets.All)]
	[DontApplyToListElements]
	[Conditional("UNITY_EDITOR")]
	public class HideInPlayModeAttribute : Attribute
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00002102 File Offset: 0x00000302
		public HideInPlayModeAttribute()
		{
		}
	}
}
