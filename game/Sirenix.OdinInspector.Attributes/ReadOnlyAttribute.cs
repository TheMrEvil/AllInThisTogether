using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000057 RID: 87
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class ReadOnlyAttribute : Attribute
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00002102 File Offset: 0x00000302
		public ReadOnlyAttribute()
		{
		}
	}
}
