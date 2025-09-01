using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000021 RID: 33
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	public class EnableGUIAttribute : Attribute
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00002102 File Offset: 0x00000302
		public EnableGUIAttribute()
		{
		}
	}
}
