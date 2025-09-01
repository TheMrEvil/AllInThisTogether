using System;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000162 RID: 354
	public class SlowMo : MonoBehaviour
	{
		// Token: 0x06000D9E RID: 3486 RVA: 0x0005C4C8 File Offset: 0x0005A6C8
		private void Update()
		{
			Time.timeScale = (this.IsSlowMotion() ? this.slowMoTimeScale : 1f);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0005C4E4 File Offset: 0x0005A6E4
		private bool IsSlowMotion()
		{
			if (this.mouse0 && Input.GetMouseButton(0))
			{
				return true;
			}
			if (this.mouse1 && Input.GetMouseButton(1))
			{
				return true;
			}
			for (int i = 0; i < this.keyCodes.Length; i++)
			{
				if (Input.GetKey(this.keyCodes[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0005C53A File Offset: 0x0005A73A
		public SlowMo()
		{
		}

		// Token: 0x04000B93 RID: 2963
		public KeyCode[] keyCodes;

		// Token: 0x04000B94 RID: 2964
		public bool mouse0;

		// Token: 0x04000B95 RID: 2965
		public bool mouse1;

		// Token: 0x04000B96 RID: 2966
		public float slowMoTimeScale = 0.3f;
	}
}
