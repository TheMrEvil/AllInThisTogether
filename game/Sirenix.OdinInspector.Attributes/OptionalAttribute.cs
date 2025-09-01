using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200004D RID: 77
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class OptionalAttribute : Attribute
	{
		// Token: 0x060000EA RID: 234 RVA: 0x00002102 File Offset: 0x00000302
		public OptionalAttribute()
		{
		}
	}
}
