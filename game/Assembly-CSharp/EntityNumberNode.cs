using System;
using UnityEngine;

// Token: 0x0200037E RID: 894
public class EntityNumberNode : NumberNode
{
	// Token: 0x06001D70 RID: 7536 RVA: 0x000B2FF0 File Offset: 0x000B11F0
	public override float Evaluate(EffectProperties props)
	{
		if (props == null)
		{
			return 0f;
		}
		EntityControl applicationEntity = props.GetApplicationEntity(this.Entity);
		if (applicationEntity == null)
		{
			this.EditorStateDisplay = NodeState.Fail;
			return 0f;
		}
		if (this.NeedsStatusGraph())
		{
			if (this.Status == null)
			{
				return float.NaN;
			}
			EntityStats.NumberStat stat = this.Stat;
			switch (stat)
			{
			case EntityStats.NumberStat.StatusStacks:
			{
				EntityControl.AppliedStatus statusInfoByID = applicationEntity.GetStatusInfoByID(this.Status.ID, -1);
				return (float)((statusInfoByID != null) ? statusInfoByID.Stacks : 0);
			}
			case EntityStats.NumberStat.AugmentStacks:
			case EntityStats.NumberStat.FOVAngle:
				break;
			case EntityStats.NumberStat.StatusNumApplied:
				return (float)applicationEntity.NumStatusesApplied(this.Status.ID);
			case EntityStats.NumberStat.StatusTimeLeft:
			{
				EntityControl.AppliedStatus statusInfoByID2 = applicationEntity.GetStatusInfoByID(this.Status.ID, -1);
				return (statusInfoByID2 != null) ? statusInfoByID2.Duration : 0f;
			}
			default:
				if (stat == EntityStats.NumberStat.StatusLifetime)
				{
					EntityControl.AppliedStatus statusInfoByID3 = applicationEntity.GetStatusInfoByID(this.Status.ID, -1);
					return (statusInfoByID3 != null) ? statusInfoByID3.Lifetime : 0f;
				}
				if (stat == EntityStats.NumberStat.StatusStacksFrom)
				{
					EntityControl entityControl = applicationEntity;
					string id = this.Status.ID;
					EntityControl applicationEntity2 = props.GetApplicationEntity(this.SecondEntity);
					EntityControl.AppliedStatus statusInfoByID4 = entityControl.GetStatusInfoByID(id, (applicationEntity2 != null) ? applicationEntity2.ViewID : -1);
					return (float)((statusInfoByID4 != null) ? statusInfoByID4.Stacks : 0);
				}
				break;
			}
			return 0f;
		}
		else
		{
			PlayerControl playerControl = applicationEntity as PlayerControl;
			if (playerControl != null)
			{
				if (this.Stat == EntityStats.NumberStat.Cooldown)
				{
					return playerControl.actions.GetAbility(this.AbilityT).AbilityCooldown;
				}
				if (this.Stat == EntityStats.NumberStat.CooldownCurrent)
				{
					return playerControl.actions.GetAbility(this.AbilityT).currentCD;
				}
			}
			if (this.Stat == EntityStats.NumberStat.AbilityValue)
			{
				return this.GetAbilityValue(applicationEntity);
			}
			object meta = null;
			if (this.Stat == EntityStats.NumberStat.TimeSince)
			{
				meta = this.TimeStat;
			}
			if (this.Stat == EntityStats.NumberStat.DistanceFrom || this.Stat == EntityStats.NumberStat.FOVAngle)
			{
				EntityControl applicationEntity3 = props.GetApplicationEntity(this.SecondEntity);
				if (applicationEntity3 == null || applicationEntity == applicationEntity3)
				{
					return float.NaN;
				}
				meta = applicationEntity3;
			}
			if (this.Stat == EntityStats.NumberStat.AugmentScalar)
			{
				EffectProperties effectProperties = props;
				if (props.SourceControl != applicationEntity)
				{
					effectProperties = props.Copy(false);
					effectProperties.SourceControl = applicationEntity;
				}
				float num;
				if (this.AugmentT == EntityNumberNode.AugmentScalarTest.EntityPassive)
				{
					num = applicationEntity.AllAugments(true, null).GetScalarValue(applicationEntity, this.EPassive, effectProperties);
				}
				else if (this.AugmentT == EntityNumberNode.AugmentScalarTest.KeywordPassive)
				{
					num = applicationEntity.AllAugments(true, null).GetScalarValue(applicationEntity, new ValueTuple<Keyword, Passive.AbilityValue>(this.Keyword, this.APassive), effectProperties);
				}
				else
				{
					num = applicationEntity.AllAugments(true, null).GetScalarValue(applicationEntity, new ValueTuple<PlayerAbilityType, Passive.AbilityValue>(this.AbilityT, this.APassive), effectProperties);
				}
				if (this.ZeroIndex)
				{
					num -= 1f;
				}
				return num;
			}
			if (this.Stat != EntityStats.NumberStat.AugmentStacks)
			{
				return applicationEntity.GetStatValue(this.Stat, meta);
			}
			if (this.Augment == null)
			{
				return float.NaN;
			}
			return (float)applicationEntity.AllAugments(true, null).GetCount(this.Augment);
		}
	}

	// Token: 0x06001D71 RID: 7537 RVA: 0x000B330C File Offset: 0x000B150C
	private float GetAbilityValue(EntityControl e)
	{
		Ability ability = null;
		if (this.Ability != null)
		{
			ability = e.GetAbility(this.Ability.ID);
		}
		else
		{
			PlayerControl playerControl = e as PlayerControl;
			if (playerControl != null)
			{
				ability = playerControl.actions.GetAbility(this.AbilityT);
			}
		}
		if (ability == null)
		{
			return float.NaN;
		}
		float result;
		switch (this.AbilStat)
		{
		case EntityNumberNode.AbilityStat.CooldownTotal:
			result = ability.AbilityCooldown;
			break;
		case EntityNumberNode.AbilityStat.CooldownCurrent:
			result = ability.currentCD;
			break;
		case EntityNumberNode.AbilityStat.ManaCost:
			result = ability.ManaCost;
			break;
		case EntityNumberNode.AbilityStat.FullManaCost:
			result = e.ModifyManaCost(new EffectProperties(e), ability.ManaCost);
			break;
		default:
			result = float.NaN;
			break;
		}
		return result;
	}

	// Token: 0x06001D72 RID: 7538 RVA: 0x000B33B8 File Offset: 0x000B15B8
	private bool NeedsStatusGraph()
	{
		return this.Stat == EntityStats.NumberStat.StatusNumApplied || this.Stat == EntityStats.NumberStat.StatusStacks || this.Stat == EntityStats.NumberStat.StatusStacksFrom || this.Stat == EntityStats.NumberStat.StatusTimeLeft || this.Stat == EntityStats.NumberStat.StatusLifetime;
	}

	// Token: 0x06001D73 RID: 7539 RVA: 0x000B33F0 File Offset: 0x000B15F0
	private bool NeedsAbilityType()
	{
		return (this.Stat == EntityStats.NumberStat.AugmentScalar && this.AugmentT == EntityNumberNode.AugmentScalarTest.AbilityPassive) || (this.Stat == EntityStats.NumberStat.Cooldown || this.Stat == EntityStats.NumberStat.CooldownCurrent) || (this.Stat == EntityStats.NumberStat.AbilityValue && this.Ability == null);
	}

	// Token: 0x06001D74 RID: 7540 RVA: 0x000B3444 File Offset: 0x000B1644
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Entity Value",
			MinInspectorSize = new Vector2(150f, 0f),
			MaxInspectorSize = new Vector2(150f, 0f),
			AllowMultipleInputs = true
		};
	}

	// Token: 0x06001D75 RID: 7541 RVA: 0x000B3492 File Offset: 0x000B1692
	public EntityNumberNode()
	{
	}

	// Token: 0x04001E1D RID: 7709
	public ApplyOn Entity;

	// Token: 0x04001E1E RID: 7710
	public EntityStats.NumberStat Stat;

	// Token: 0x04001E1F RID: 7711
	public TimeSince TimeStat;

	// Token: 0x04001E20 RID: 7712
	public ApplyOn SecondEntity;

	// Token: 0x04001E21 RID: 7713
	[Space(10f)]
	public EntityNumberNode.AugmentScalarTest AugmentT;

	// Token: 0x04001E22 RID: 7714
	[Space(10f)]
	public Passive.EntityValue EPassive;

	// Token: 0x04001E23 RID: 7715
	[Space(10f)]
	public PlayerAbilityType AbilityT;

	// Token: 0x04001E24 RID: 7716
	public Keyword Keyword;

	// Token: 0x04001E25 RID: 7717
	public Passive.AbilityValue APassive;

	// Token: 0x04001E26 RID: 7718
	public bool ZeroIndex;

	// Token: 0x04001E27 RID: 7719
	public EntityNumberNode.AbilityStat AbilStat;

	// Token: 0x04001E28 RID: 7720
	public StatusTree Status;

	// Token: 0x04001E29 RID: 7721
	public AugmentTree Augment;

	// Token: 0x04001E2A RID: 7722
	public AbilityTree Ability;

	// Token: 0x02000681 RID: 1665
	public enum AugmentScalarTest
	{
		// Token: 0x04002BCD RID: 11213
		EntityPassive,
		// Token: 0x04002BCE RID: 11214
		AbilityPassive,
		// Token: 0x04002BCF RID: 11215
		KeywordPassive
	}

	// Token: 0x02000682 RID: 1666
	public enum AbilityStat
	{
		// Token: 0x04002BD1 RID: 11217
		CooldownTotal,
		// Token: 0x04002BD2 RID: 11218
		CooldownCurrent,
		// Token: 0x04002BD3 RID: 11219
		ManaCost,
		// Token: 0x04002BD4 RID: 11220
		FullManaCost
	}
}
