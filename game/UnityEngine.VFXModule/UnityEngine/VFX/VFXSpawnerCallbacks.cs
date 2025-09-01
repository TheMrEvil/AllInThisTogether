using System;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x02000012 RID: 18
	[RequiredByNativeCode]
	[Serializable]
	public abstract class VFXSpawnerCallbacks : ScriptableObject
	{
		// Token: 0x0600007B RID: 123
		public abstract void OnPlay(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent);

		// Token: 0x0600007C RID: 124
		public abstract void OnUpdate(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent);

		// Token: 0x0600007D RID: 125
		public abstract void OnStop(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent);

		// Token: 0x0600007E RID: 126 RVA: 0x000027DB File Offset: 0x000009DB
		protected VFXSpawnerCallbacks()
		{
		}
	}
}
