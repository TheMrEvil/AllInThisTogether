using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000066 RID: 102
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class ShowOdinSerializedPropertiesInInspectorAttribute : Attribute
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00002102 File Offset: 0x00000302
		public ShowOdinSerializedPropertiesInInspectorAttribute()
		{
		}
	}
}
