using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000366 RID: 870
public class GenreWorldNode : Node
{
	// Token: 0x06001D05 RID: 7429 RVA: 0x000B0778 File Offset: 0x000AE978
	public void AddAugments(ref List<AugmentTree> augments, ModType modType)
	{
		List<AugmentTree> list = this.WorldAugments;
		if (modType == ModType.Enemy)
		{
			list = this.EnemyAugments;
		}
		else if (modType == ModType.Player)
		{
			list = this.PlayerAugments;
		}
		foreach (AugmentTree item in list)
		{
			augments.Add(item);
		}
	}

	// Token: 0x06001D06 RID: 7430 RVA: 0x000B07E8 File Offset: 0x000AE9E8
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "World Options",
			MinInspectorSize = new Vector2(200f, 0f)
		};
	}

	// Token: 0x06001D07 RID: 7431 RVA: 0x000B080F File Offset: 0x000AEA0F
	public GenreWorldNode()
	{
	}

	// Token: 0x04001DAA RID: 7594
	public List<AugmentTree> WorldAugments = new List<AugmentTree>();

	// Token: 0x04001DAB RID: 7595
	public List<AugmentTree> PlayerAugments = new List<AugmentTree>();

	// Token: 0x04001DAC RID: 7596
	public List<AugmentTree> EnemyAugments = new List<AugmentTree>();
}
