using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000030 RID: 48
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use [HideIn(PrefabKind.NonPrefabInstance)] instead.", false)]
	public class HideInNonPrefabsAttribute : Attribute
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00002102 File Offset: 0x00000302
		public HideInNonPrefabsAttribute()
		{
		}
	}
}
