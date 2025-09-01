using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200001E RID: 30
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class DontValidateAttribute : Attribute
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00002102 File Offset: 0x00000302
		public DontValidateAttribute()
		{
		}
	}
}
