using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x02000010 RID: 16
	public class MagicFX5_FreezeRotation : MagicFX5_IScriptInstance
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000038F6 File Offset: 0x00001AF6
		internal override void OnEnableExtended()
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000038F8 File Offset: 0x00001AF8
		internal override void OnDisableExtended()
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000038FC File Offset: 0x00001AFC
		internal override void ManualUpdate()
		{
			Vector3 eulerAngles = base.transform.eulerAngles;
			if (this.FreezeX)
			{
				eulerAngles.x = 0f;
			}
			if (this.FreezeY)
			{
				eulerAngles.y = 0f;
			}
			if (this.FreezeZ)
			{
				eulerAngles.z = 0f;
			}
			base.transform.eulerAngles = eulerAngles;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000395D File Offset: 0x00001B5D
		public MagicFX5_FreezeRotation()
		{
		}

		// Token: 0x04000065 RID: 101
		public bool FreezeX = true;

		// Token: 0x04000066 RID: 102
		public bool FreezeY = true;

		// Token: 0x04000067 RID: 103
		public bool FreezeZ = true;
	}
}
