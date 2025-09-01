using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class AtlasTurret : MonoBehaviour
{
	// Token: 0x06000027 RID: 39 RVA: 0x000043DC File Offset: 0x000025DC
	public void DeactivateLooks()
	{
		foreach (LookAtTarget lookAtTarget in this.LookAts)
		{
			lookAtTarget.Deactivate();
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x0000442C File Offset: 0x0000262C
	public void ActivateLooks()
	{
		foreach (LookAtTarget lookAtTarget in this.LookAts)
		{
			lookAtTarget.Activate();
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x0000447C File Offset: 0x0000267C
	public AtlasTurret()
	{
	}

	// Token: 0x04000014 RID: 20
	public List<LookAtTarget> LookAts;
}
