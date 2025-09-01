using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000035 RID: 53
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class HideInTablesAttribute : Attribute
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00002102 File Offset: 0x00000302
		public HideInTablesAttribute()
		{
		}
	}
}
