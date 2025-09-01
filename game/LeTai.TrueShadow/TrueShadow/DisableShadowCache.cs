using System;
using LeTai.TrueShadow.PluginInterfaces;
using UnityEngine;

namespace LeTai.TrueShadow
{
	// Token: 0x02000007 RID: 7
	[ExecuteAlways]
	[RequireComponent(typeof(TrueShadow))]
	public class DisableShadowCache : MonoBehaviour, ITrueShadowCustomHashProvider
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000028D6 File Offset: 0x00000AD6
		private void OnEnable()
		{
			this.shadow = base.GetComponent<TrueShadow>();
			this.shadow.CustomHash = this.shadow.GetInstanceID();
			this.shadow.SetTextureDirty();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002905 File Offset: 0x00000B05
		private void OnDisable()
		{
			this.shadow.CustomHash = 0;
			this.shadow.SetTextureDirty();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000291E File Offset: 0x00000B1E
		public DisableShadowCache()
		{
		}

		// Token: 0x04000017 RID: 23
		private TrueShadow shadow;
	}
}
