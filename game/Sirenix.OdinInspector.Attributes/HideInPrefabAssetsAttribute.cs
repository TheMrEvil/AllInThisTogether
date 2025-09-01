using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000032 RID: 50
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	[Obsolete("Use [HideIn(PrefabKind.PrefabAsset)] instead.", false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class HideInPrefabAssetsAttribute : Attribute
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00002102 File Offset: 0x00000302
		public HideInPrefabAssetsAttribute()
		{
		}
	}
}
