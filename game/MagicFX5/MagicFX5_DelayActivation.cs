using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x0200000E RID: 14
	public class MagicFX5_DelayActivation : MonoBehaviour
	{
		// Token: 0x0600003C RID: 60 RVA: 0x000036CC File Offset: 0x000018CC
		private void OnEnable()
		{
			this.GameObject.SetActive(false);
			base.Invoke("DelayActivate", this.Delay);
			if (this.LifeTime > 0f)
			{
				base.Invoke("DelayDeactivate", this.Delay + this.LifeTime);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000371B File Offset: 0x0000191B
		private void OnDisable()
		{
			this.GameObject.SetActive(false);
			base.CancelInvoke("DelayActivate");
			base.CancelInvoke("DelayDeactivate");
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000373F File Offset: 0x0000193F
		private void DelayActivate()
		{
			this.GameObject.SetActive(true);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000374D File Offset: 0x0000194D
		private void DelayDeactivate()
		{
			this.GameObject.SetActive(false);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000375B File Offset: 0x0000195B
		public MagicFX5_DelayActivation()
		{
		}

		// Token: 0x0400005A RID: 90
		public GameObject GameObject;

		// Token: 0x0400005B RID: 91
		public float Delay = 1f;

		// Token: 0x0400005C RID: 92
		public float LifeTime = -1f;
	}
}
