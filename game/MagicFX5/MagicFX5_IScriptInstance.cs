using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x02000012 RID: 18
	public abstract class MagicFX5_IScriptInstance : MonoBehaviour
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00003E82 File Offset: 0x00002082
		protected virtual void OnEnable()
		{
			MagicFX5_GlobalUpdate.CreateInstanceIfRequired();
			MagicFX5_GlobalUpdate.ScriptInstances.Add(this);
			this.OnEnableExtended();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003E9A File Offset: 0x0000209A
		protected virtual void OnDisable()
		{
			MagicFX5_GlobalUpdate.ScriptInstances.Remove(this);
			this.OnDisableExtended();
		}

		// Token: 0x0600005B RID: 91
		internal abstract void OnEnableExtended();

		// Token: 0x0600005C RID: 92
		internal abstract void OnDisableExtended();

		// Token: 0x0600005D RID: 93
		internal abstract void ManualUpdate();

		// Token: 0x0600005E RID: 94 RVA: 0x00003EAE File Offset: 0x000020AE
		protected MagicFX5_IScriptInstance()
		{
		}
	}
}
