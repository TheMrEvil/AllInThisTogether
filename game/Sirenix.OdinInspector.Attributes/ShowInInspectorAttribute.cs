using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000065 RID: 101
	[MeansImplicitUse]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
	[Conditional("UNITY_EDITOR")]
	public class ShowInInspectorAttribute : Attribute
	{
		// Token: 0x0600015C RID: 348 RVA: 0x00002102 File Offset: 0x00000302
		public ShowInInspectorAttribute()
		{
		}
	}
}
