using System;
using UnityEngine;

// Token: 0x02000387 RID: 903
public class ScaleValueNode : NumberNode
{
	// Token: 0x06001D92 RID: 7570 RVA: 0x000B3AFC File Offset: 0x000B1CFC
	public override float Evaluate(EffectProperties props)
	{
		if (this.Mode == ScaleValueNode.ScaleMode.Flat)
		{
			return this.Value;
		}
		if (this.Mode == ScaleValueNode.ScaleMode.EffectProp)
		{
			return props.GetExtra(this.EffectProp, 0f);
		}
		if (this.Mode != ScaleValueNode.ScaleMode.EntityStat)
		{
			return this.Value;
		}
		EntityControl applicationEntity = props.GetApplicationEntity(this.StatCheck);
		if (applicationEntity == null)
		{
			Debug.LogError("Failed to get " + this.StatCheck.ToString() + " for Number evaluation of " + this.Stat.ToString());
			return 0f;
		}
		return applicationEntity.GetStatValue(this.Stat);
	}

	// Token: 0x06001D93 RID: 7571 RVA: 0x000B3BA1 File Offset: 0x000B1DA1
	private bool NeedsAbilityInfo()
	{
		return this.Stat.NeedsAbilityInfo();
	}

	// Token: 0x06001D94 RID: 7572 RVA: 0x000B3BAE File Offset: 0x000B1DAE
	public bool NeedsID()
	{
		return this.Mode == ScaleValueNode.ScaleMode.EntityStat && EntityStats.UsesID(this.Stat);
	}

	// Token: 0x06001D95 RID: 7573 RVA: 0x000B3BC6 File Offset: 0x000B1DC6
	private bool CanUseProps()
	{
		return !(this.CalledFrom is ModPassiveNode) && !(this.CalledFrom is ModSnippetNode) && this.Mode == ScaleValueNode.ScaleMode.EntityStat;
	}

	// Token: 0x06001D96 RID: 7574 RVA: 0x000B3BEF File Offset: 0x000B1DEF
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Value",
			MinInspectorSize = new Vector2(250f, 0f),
			AllowMultipleInputs = true
		};
	}

	// Token: 0x06001D97 RID: 7575 RVA: 0x000B3C1D File Offset: 0x000B1E1D
	public ScaleValueNode()
	{
	}

	// Token: 0x04001E3A RID: 7738
	public ScaleValueNode.ScaleMode Mode;

	// Token: 0x04001E3B RID: 7739
	public float Value;

	// Token: 0x04001E3C RID: 7740
	public EProp EffectProp;

	// Token: 0x04001E3D RID: 7741
	public EStat Stat;

	// Token: 0x04001E3E RID: 7742
	public GraphTree Tree;

	// Token: 0x04001E3F RID: 7743
	public PlayerAbilityType AbilityScope = PlayerAbilityType.Any;

	// Token: 0x04001E40 RID: 7744
	public ApplyOn StatCheck = ApplyOn.Affected;

	// Token: 0x02000685 RID: 1669
	public enum ScaleMode
	{
		// Token: 0x04002BE4 RID: 11236
		Flat,
		// Token: 0x04002BE5 RID: 11237
		EffectProp,
		// Token: 0x04002BE6 RID: 11238
		EntityStat
	}
}
