using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200005E RID: 94
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class SceneObjectsOnlyAttribute : Attribute
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00002102 File Offset: 0x00000302
		public SceneObjectsOnlyAttribute()
		{
		}
	}
}
