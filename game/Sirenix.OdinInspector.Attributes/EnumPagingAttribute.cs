using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000024 RID: 36
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class EnumPagingAttribute : Attribute
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00002102 File Offset: 0x00000302
		public EnumPagingAttribute()
		{
		}
	}
}
