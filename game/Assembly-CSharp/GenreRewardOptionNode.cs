using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200035F RID: 863
public class GenreRewardOptionNode : Node
{
	// Token: 0x06001CC7 RID: 7367 RVA: 0x000AF592 File Offset: 0x000AD792
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Augment Filter",
			AllowMultipleInputs = true,
			MinInspectorSize = new Vector2(200f, 0f)
		};
	}

	// Token: 0x06001CC8 RID: 7368 RVA: 0x000AF5C0 File Offset: 0x000AD7C0
	public List<AugmentTree> GetModifiers(ModType modType, List<string> exclude = null)
	{
		return this.Filter.GetModifiers(modType, exclude);
	}

	// Token: 0x06001CC9 RID: 7369 RVA: 0x000AF5D0 File Offset: 0x000AD7D0
	public override void OnConnectionsChanged()
	{
		if (this.CalledFrom == null)
		{
			this.Filter.CanUseEnemy = true;
			this.Filter.CanUsePlayer = true;
			this.Filter.CanUseFountain = false;
			return;
		}
		if (this.CalledFrom is GenreWaveNode)
		{
			this.Filter.CanUseEnemy = false;
			this.Filter.CanUsePlayer = false;
			this.Filter.CanUseFountain = false;
			return;
		}
		if (this.CalledFrom is GenreSpawnNode)
		{
			this.Filter.CanUseEnemy = false;
			this.Filter.CanUsePlayer = true;
			this.Filter.CanUseFountain = false;
			return;
		}
		if (this.CalledFrom is GenreFountainNode)
		{
			this.Filter.CanUseEnemy = false;
			this.Filter.CanUsePlayer = false;
			this.Filter.CanUseFountain = true;
			return;
		}
		GenreRewardNode genreRewardNode = this.CalledFrom as GenreRewardNode;
		if (genreRewardNode != null)
		{
			this.Filter.CanUseEnemy = genreRewardNode.EnemyReward.Contains(this);
			this.Filter.CanUsePlayer = genreRewardNode.PlayerReward.Contains(this);
			this.Filter.CanUseFountain = false;
			return;
		}
	}

	// Token: 0x06001CCA RID: 7370 RVA: 0x000AF6F0 File Offset: 0x000AD8F0
	public override Node Clone(Dictionary<string, Node> alreadyCloned = null, bool fullClone = false)
	{
		AugmentAwardOverrideNode augmentAwardOverrideNode = base.Clone(alreadyCloned, fullClone) as AugmentAwardOverrideNode;
		augmentAwardOverrideNode.Filter = this.Filter.Copy();
		return augmentAwardOverrideNode;
	}

	// Token: 0x06001CCB RID: 7371 RVA: 0x000AF710 File Offset: 0x000AD910
	public override void OnCloned()
	{
		this.Filter = this.Filter.Copy();
	}

	// Token: 0x06001CCC RID: 7372 RVA: 0x000AF723 File Offset: 0x000AD923
	public GenreRewardOptionNode()
	{
	}

	// Token: 0x04001D79 RID: 7545
	public AugmentFilter Filter;

	// Token: 0x02000674 RID: 1652
	[Serializable]
	public class AugmentRequest
	{
		// Token: 0x060027C7 RID: 10183 RVA: 0x000D701A File Offset: 0x000D521A
		public AugmentRequest()
		{
		}

		// Token: 0x04002B9C RID: 11164
		public UpgradeLayout.UpgradeSelection Selection;

		// Token: 0x04002B9D RID: 11165
		public bool RestrictRarity;

		// Token: 0x04002B9E RID: 11166
		public Rarity MinRarity;

		// Token: 0x04002B9F RID: 11167
		public Rarity MaxRarity;
	}
}
