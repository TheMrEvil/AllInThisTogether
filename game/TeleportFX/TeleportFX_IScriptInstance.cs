using System;
using UnityEngine;

namespace TeleportFX
{
	// Token: 0x02000005 RID: 5
	public abstract class TeleportFX_IScriptInstance : MonoBehaviour
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002B2D File Offset: 0x00000D2D
		protected virtual void OnEnable()
		{
			TeleportFX_GlobalUpdate.CreateInstanceIfRequired();
			TeleportFX_GlobalUpdate.ScriptInstances.Add(this);
			this.OnEnableExtended();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002B45 File Offset: 0x00000D45
		protected virtual void OnDisable()
		{
			TeleportFX_GlobalUpdate.ScriptInstances.Remove(this);
			this.OnDisableExtended();
		}

		// Token: 0x06000021 RID: 33
		internal abstract void OnEnableExtended();

		// Token: 0x06000022 RID: 34
		internal abstract void OnDisableExtended();

		// Token: 0x06000023 RID: 35
		internal abstract void ManualUpdate();

		// Token: 0x06000024 RID: 36 RVA: 0x00002B59 File Offset: 0x00000D59
		protected TeleportFX_IScriptInstance()
		{
		}
	}
}
