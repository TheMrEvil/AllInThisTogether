using System;
using UnityEngine;

// Token: 0x020001AE RID: 430
public class FountainUIInvestPoint : MonoBehaviour
{
	// Token: 0x060011D2 RID: 4562 RVA: 0x0006EA98 File Offset: 0x0006CC98
	public void UpdateDisplay(int index, bool isInvested)
	{
		this.Divider.SetActive(index > 0);
		this.Filled.SetActive(isInvested);
	}

	// Token: 0x060011D3 RID: 4563 RVA: 0x0006EAB5 File Offset: 0x0006CCB5
	public FountainUIInvestPoint()
	{
	}

	// Token: 0x04001089 RID: 4233
	public GameObject Divider;

	// Token: 0x0400108A RID: 4234
	public GameObject Filled;
}
