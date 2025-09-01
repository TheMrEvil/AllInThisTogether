using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000038 RID: 56
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class HideNetworkBehaviourFieldsAttribute : Attribute
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00002102 File Offset: 0x00000302
		public HideNetworkBehaviourFieldsAttribute()
		{
		}
	}
}
