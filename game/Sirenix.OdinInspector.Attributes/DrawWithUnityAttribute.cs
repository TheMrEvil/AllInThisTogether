using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200001F RID: 31
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	public class DrawWithUnityAttribute : Attribute
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002102 File Offset: 0x00000302
		public DrawWithUnityAttribute()
		{
		}

		// Token: 0x0400004B RID: 75
		public bool PreferImGUI;
	}
}
