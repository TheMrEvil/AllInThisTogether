using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000037 RID: 55
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class HideMonoScriptAttribute : Attribute
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00002102 File Offset: 0x00000302
		public HideMonoScriptAttribute()
		{
		}
	}
}
