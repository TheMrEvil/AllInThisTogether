using System;
using UnityEngine;

// Token: 0x02000221 RID: 545
public class DestroyTimed : MonoBehaviour
{
	// Token: 0x060016C7 RID: 5831 RVA: 0x00090C50 File Offset: 0x0008EE50
	private void OnEnable()
	{
		ActionPool.ReleaseDelayed(base.gameObject, this.destroyTime);
	}

	// Token: 0x060016C8 RID: 5832 RVA: 0x00090C63 File Offset: 0x0008EE63
	private void OnDisable()
	{
		ActionPool.CancelRelease(base.gameObject);
	}

	// Token: 0x060016C9 RID: 5833 RVA: 0x00090C70 File Offset: 0x0008EE70
	public DestroyTimed()
	{
	}

	// Token: 0x040016DF RID: 5855
	public float destroyTime = 2.5f;
}
