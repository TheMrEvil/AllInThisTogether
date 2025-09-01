using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200002F RID: 47
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	public class HideInInlineEditorsAttribute : Attribute
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00002102 File Offset: 0x00000302
		public HideInInlineEditorsAttribute()
		{
		}
	}
}
