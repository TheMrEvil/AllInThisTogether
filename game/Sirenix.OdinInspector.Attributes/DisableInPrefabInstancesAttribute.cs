using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000018 RID: 24
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use [DisableIn(PrefabKind.PrefabInstance)] instead.", false)]
	public class DisableInPrefabInstancesAttribute : Attribute
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002102 File Offset: 0x00000302
		public DisableInPrefabInstancesAttribute()
		{
		}
	}
}
