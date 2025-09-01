using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000036 RID: 54
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	public class HideLabelAttribute : Attribute
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00002102 File Offset: 0x00000302
		public HideLabelAttribute()
		{
		}
	}
}
