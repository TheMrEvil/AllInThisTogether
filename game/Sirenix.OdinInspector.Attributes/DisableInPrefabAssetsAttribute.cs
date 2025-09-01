using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000017 RID: 23
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use [DisableIn(PrefabKind.PrefabAsset)] instead.", false)]
	public class DisableInPrefabAssetsAttribute : Attribute
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002102 File Offset: 0x00000302
		public DisableInPrefabAssetsAttribute()
		{
		}
	}
}
