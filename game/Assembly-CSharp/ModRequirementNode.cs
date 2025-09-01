using System;
using UnityEngine;

// Token: 0x0200033C RID: 828
public class ModRequirementNode : Node
{
	// Token: 0x06001C28 RID: 7208 RVA: 0x000AC1F8 File Offset: 0x000AA3F8
	public bool HasRequirement(EffectProperties props)
	{
		if (this.Requirement == EStat._)
		{
			return true;
		}
		if (this.Requirement == EStat.ManaConsumed)
		{
			return this.VerifyMana(props);
		}
		EntityControl applicationEntity = props.GetApplicationEntity(this.Check);
		return this.HasRequirement(applicationEntity, props);
	}

	// Token: 0x06001C29 RID: 7209 RVA: 0x000AC238 File Offset: 0x000AA438
	public bool HasRequirement(EntityControl entity, EffectProperties props = null)
	{
		if (this.Requirement == EStat._)
		{
			return true;
		}
		if (this.NeedsID())
		{
			if (this.NeedsNumber())
			{
				float statIDValue = entity.GetStatIDValue(this.Requirement, this.Tree.RootNode.guid);
				return statIDValue >= this.Min && (statIDValue <= this.Max || this.Max <= this.Min);
			}
			return entity.HasStatID(this.Requirement, this.Tree.RootNode.guid);
		}
		else
		{
			if (this.NeedsNumber())
			{
				float statValue;
				if (this.Requirement.NeedsAbilityInfo())
				{
					statValue = entity.GetStatValue(this.Requirement, this.AbilityScope);
				}
				else
				{
					statValue = entity.GetStatValue(this.Requirement);
				}
				return statValue >= this.Min && (statValue <= this.Max || this.Max <= this.Min);
			}
			if (this.NeedsEntity())
			{
				return props != null && entity.CheckEntity(this.Requirement, props.GetApplicationEntity(this.OtherEntity));
			}
			return entity.GetStatTrigger(this.Requirement);
		}
	}

	// Token: 0x06001C2A RID: 7210 RVA: 0x000AC358 File Offset: 0x000AA558
	private bool NeedsAbilityScope()
	{
		return this.Requirement.NeedsAbilityInfo();
	}

	// Token: 0x06001C2B RID: 7211 RVA: 0x000AC365 File Offset: 0x000AA565
	public bool NeedsNumber()
	{
		return EntityStats.UsesNumber(this.Requirement);
	}

	// Token: 0x06001C2C RID: 7212 RVA: 0x000AC372 File Offset: 0x000AA572
	public bool NeedsID()
	{
		return EntityStats.UsesID(this.Requirement);
	}

	// Token: 0x06001C2D RID: 7213 RVA: 0x000AC37F File Offset: 0x000AA57F
	public bool NeedsEntity()
	{
		return EntityStats.UsesEntity(this.Requirement);
	}

	// Token: 0x06001C2E RID: 7214 RVA: 0x000AC38C File Offset: 0x000AA58C
	private bool VerifyMana(EffectProperties props)
	{
		if (props == null)
		{
			return false;
		}
		if (!props.ManaConsumed.ContainsKey(this.magicColor))
		{
			return this.Min <= 0f;
		}
		int num = props.ManaConsumed[this.magicColor];
		return this.Min <= (float)num && this.Max >= (float)num;
	}

	// Token: 0x06001C2F RID: 7215 RVA: 0x000AC3ED File Offset: 0x000AA5ED
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Requirement",
			MinInspectorSize = new Vector2(200f, 0f)
		};
	}

	// Token: 0x06001C30 RID: 7216 RVA: 0x000AC414 File Offset: 0x000AA614
	public ModRequirementNode()
	{
	}

	// Token: 0x04001C75 RID: 7285
	public ApplyOn Check;

	// Token: 0x04001C76 RID: 7286
	public EStat Requirement;

	// Token: 0x04001C77 RID: 7287
	public PlayerAbilityType AbilityScope = PlayerAbilityType.Any;

	// Token: 0x04001C78 RID: 7288
	public MagicColor magicColor = MagicColor.Neutral;

	// Token: 0x04001C79 RID: 7289
	public float Min;

	// Token: 0x04001C7A RID: 7290
	public float Max;

	// Token: 0x04001C7B RID: 7291
	public GraphTree Tree;

	// Token: 0x04001C7C RID: 7292
	public ApplyOn OtherEntity = ApplyOn.SeekTarget;
}
