using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000004 RID: 4
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class AssetsOnlyAttribute : Attribute
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002102 File Offset: 0x00000302
		public AssetsOnlyAttribute()
		{
		}
	}
}
