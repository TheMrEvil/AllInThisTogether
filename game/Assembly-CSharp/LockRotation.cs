using System;
using UnityEngine;

// Token: 0x02000230 RID: 560
public class LockRotation : MonoBehaviour
{
	// Token: 0x0600173C RID: 5948 RVA: 0x000930A7 File Offset: 0x000912A7
	private void LateUpdate()
	{
		if (this.Worldspace)
		{
			base.transform.rotation = Quaternion.identity;
			return;
		}
		base.transform.localRotation = Quaternion.identity;
	}

	// Token: 0x0600173D RID: 5949 RVA: 0x000930D2 File Offset: 0x000912D2
	public LockRotation()
	{
	}

	// Token: 0x04001703 RID: 5891
	public bool Worldspace;
}
