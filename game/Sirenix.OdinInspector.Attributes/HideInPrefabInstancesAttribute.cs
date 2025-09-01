using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000033 RID: 51
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use [HideIn(PrefabKind.PrefabInstance)] instead.", false)]
	public class HideInPrefabInstancesAttribute : Attribute
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00002102 File Offset: 0x00000302
		public HideInPrefabInstancesAttribute()
		{
		}
	}
}
