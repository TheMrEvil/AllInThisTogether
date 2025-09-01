using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000064 RID: 100
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	public class ShowInInlineEditorsAttribute : Attribute
	{
		// Token: 0x0600015B RID: 347 RVA: 0x00002102 File Offset: 0x00000302
		public ShowInInlineEditorsAttribute()
		{
		}
	}
}
