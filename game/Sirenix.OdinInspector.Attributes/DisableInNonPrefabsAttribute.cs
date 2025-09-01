using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000015 RID: 21
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Conditional("UNITY_EDITOR")]
	[Obsolete("Use [DisableIn(PrefabKind.NonPrefabInstance)] instead.", false)]
	public class DisableInNonPrefabsAttribute : Attribute
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002102 File Offset: 0x00000302
		public DisableInNonPrefabsAttribute()
		{
		}
	}
}
