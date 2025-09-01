using System;
using UnityEngine;

// Token: 0x02000075 RID: 117
public class AIDetatchBone : MonoBehaviour
{
	// Token: 0x0600047E RID: 1150 RVA: 0x0002231A File Offset: 0x0002051A
	public void Detatch()
	{
		if (this.isDetatched)
		{
			return;
		}
		this.isDetatched = true;
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x0002232C File Offset: 0x0002052C
	public AIDetatchBone()
	{
	}

	// Token: 0x040003BC RID: 956
	private bool isDetatched;

	// Token: 0x040003BD RID: 957
	public Transform NewParent;
}
