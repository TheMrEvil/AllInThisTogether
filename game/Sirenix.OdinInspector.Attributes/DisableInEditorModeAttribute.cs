using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000013 RID: 19
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	public class DisableInEditorModeAttribute : Attribute
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002102 File Offset: 0x00000302
		public DisableInEditorModeAttribute()
		{
		}
	}
}
