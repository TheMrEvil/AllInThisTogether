using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000034 RID: 52
	[Obsolete("Use [HideIn(PrefabKind.PrefabAsset | PrefabKind.PrefabInstance)] instead.", false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	public class HideInPrefabsAttribute : Attribute
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00002102 File Offset: 0x00000302
		public HideInPrefabsAttribute()
		{
		}
	}
}
