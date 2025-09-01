using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200002E RID: 46
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	public class HideInEditorModeAttribute : Attribute
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00002102 File Offset: 0x00000302
		public HideInEditorModeAttribute()
		{
		}
	}
}
