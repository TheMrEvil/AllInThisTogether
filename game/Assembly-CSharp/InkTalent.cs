using System;
using Photon.Pun;

// Token: 0x02000073 RID: 115
[Serializable]
public class InkTalent
{
	// Token: 0x17000056 RID: 86
	// (get) Token: 0x06000479 RID: 1145 RVA: 0x00022254 File Offset: 0x00020454
	public int Cost
	{
		get
		{
			if (PhotonNetwork.CurrentRoom == null)
			{
				return 1;
			}
			if (!(this.Augment == null))
			{
				return this.Augment.Root.InkCost;
			}
			return 1;
		}
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x0600047A RID: 1146 RVA: 0x0002227F File Offset: 0x0002047F
	public bool CanPurchase
	{
		get
		{
			return this.Row.IsUnlocked;
		}
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x0002228C File Offset: 0x0002048C
	public InkTalent()
	{
		this.ID = Guid.NewGuid().ToString();
	}

	// Token: 0x040003B5 RID: 949
	public AugmentTree Augment;

	// Token: 0x040003B6 RID: 950
	public int CurrentValue;

	// Token: 0x040003B7 RID: 951
	[NonSerialized]
	public InkRow Row;

	// Token: 0x040003B8 RID: 952
	public InkTalent.InkPurchaseState State;

	// Token: 0x040003B9 RID: 953
	public string ID;

	// Token: 0x02000493 RID: 1171
	public enum InkPurchaseState
	{
		// Token: 0x0400233A RID: 9018
		Available,
		// Token: 0x0400233B RID: 9019
		Purchased,
		// Token: 0x0400233C RID: 9020
		Locked,
		// Token: 0x0400233D RID: 9021
		Unavailable
	}
}
