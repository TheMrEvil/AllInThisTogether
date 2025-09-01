using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000060 RID: 96
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	public class ShowDrawerChainAttribute : Attribute
	{
		// Token: 0x0600014C RID: 332 RVA: 0x00002102 File Offset: 0x00000302
		public ShowDrawerChainAttribute()
		{
		}
	}
}
