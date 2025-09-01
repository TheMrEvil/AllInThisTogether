using System;
using UnityEngine;

// Token: 0x020002B5 RID: 693
public class ApplyDamageNode : EffectNode
{
	// Token: 0x1700018C RID: 396
	// (get) Token: 0x060019D9 RID: 6617 RVA: 0x000A0C38 File Offset: 0x0009EE38
	internal override bool CanSkipClone
	{
		get
		{
			return true;
		}
	}

	// Token: 0x060019DA RID: 6618 RVA: 0x000A0C3B File Offset: 0x0009EE3B
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Apply Damage",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x060019DB RID: 6619 RVA: 0x000A0C64 File Offset: 0x0009EE64
	internal override void Apply(EffectProperties properties)
	{
		base.Completed();
		EntityControl applicationEntity = properties.GetApplicationEntity(this.DamageTarget);
		if (applicationEntity == null)
		{
			return;
		}
		EffectProperties effectProperties = properties.Copy(false);
		if (this.StatSourceOverride && this.AbilitySrc != PlayerAbilityType.Any)
		{
			effectProperties.AbilityType = this.AbilitySrc;
		}
		float num = (float)this.Amount;
		float num2 = num;
		if (this.Value != null)
		{
			num2 = (num = (this.Value as NumberNode).Evaluate(effectProperties));
		}
		bool flag = this.CanOneShot(num);
		if (this.CanScale)
		{
			AIControl aicontrol = effectProperties.SourceControl as AIControl;
			if (aicontrol != null && aicontrol.TeamID == 2)
			{
				num2 = AIManager.ModifyBaseDamage(num2, aicontrol);
			}
		}
		effectProperties.Affected = applicationEntity.gameObject;
		if (this.CanScale)
		{
			effectProperties.SetExtra(EProp.Snip_DamageDone, num2);
			num = effectProperties.ModifyAbilityPassives(Passive.AbilityValue.AllDamage, num2);
		}
		num = Mathf.Max(num, 0f);
		effectProperties.SetExtra(EProp.Snip_DamageDone, num);
		if (num != 0f)
		{
			EntityHealth health = applicationEntity.health;
			if (health != null && !health.isDead)
			{
				bool canOneShot = health is PlayerHealth && flag;
				DNumType dnumType = this.DamageOverride;
				if (this.CanCrit && this.TryCrit(effectProperties))
				{
					dnumType = DNumType.Crit;
					if (properties.AbilityType == PlayerAbilityType.Primary)
					{
						num *= effectProperties.ModifyAbilityPassives(Passive.AbilityValue.CritDamageMult, 2f);
					}
					else
					{
						num *= effectProperties.ModifyAbilityPassives(Passive.AbilityValue.CritDamageMult, 2f);
					}
				}
				effectProperties.SetExtra(EProp.Snip_DamageTaken, num);
				float num3 = num;
				num = applicationEntity.ModifyDamageTaken(effectProperties, num);
				if (!this.CanScale && num > num3)
				{
					num = num3;
				}
				num = Mathf.Max(num, 0f);
				DamageInfo damageInfo = new DamageInfo(num, dnumType, applicationEntity.ViewID, this.SnippetChanceMult, effectProperties);
				damageInfo.CanOneShot = canOneShot;
				if (this.StatSourceOverride && this.StatOverride != null)
				{
					damageInfo.CauseName = this.StatOverride.Root.GetCauseName();
					damageInfo.CauseID = this.StatOverride.Root.GetCauseID();
				}
				float num4 = effectProperties.GetFloat("i_TotalDmg");
				if (float.IsNaN(num4))
				{
					num4 = 0f;
				}
				effectProperties.SaveFloat("i_TotalDmg", num4 + num);
				if (this.ShouldApply(effectProperties, applicationEntity))
				{
					health.TryDamage(damageInfo);
					if (effectProperties.SourceControl == PlayerControl.myInstance && effectProperties.AffectedControl != PlayerControl.myInstance && damageInfo.DamageType != DNumType.Invisible)
					{
						HitMarker.Show(dnumType, (int)num, this.FCTextFromCenter ? effectProperties.AffectedControl.display.CenterOfMass.position : effectProperties.GetOutputPoint(), effectProperties.Depth);
					}
				}
				effectProperties.TriggerDamageDone(ApplyOn.Source, damageInfo);
			}
		}
		if (properties.Affected != null)
		{
			HitReaction component = properties.Affected.GetComponent<HitReaction>();
			if (component != null)
			{
				component.ImpactAction();
			}
		}
	}

	// Token: 0x060019DC RID: 6620 RVA: 0x000A0F46 File Offset: 0x0009F146
	private bool CanOneShot(float dmg)
	{
		if (!RaidManager.IsInRaid)
		{
			return dmg >= 125f;
		}
		if (RaidManager.instance.Difficulty == RaidDB.Difficulty.Hard)
		{
			return dmg >= 75f;
		}
		return dmg >= 100f;
	}

	// Token: 0x060019DD RID: 6621 RVA: 0x000A0F80 File Offset: 0x0009F180
	private bool TryCrit(EffectProperties props)
	{
		if (props.SourceControl == null)
		{
			return false;
		}
		float num = props.ModifyAbilityPassives(Passive.AbilityValue.CritChance, 0f);
		return (float)UnityEngine.Random.Range(0, 100) < num * 100f;
	}

	// Token: 0x060019DE RID: 6622 RVA: 0x000A0FBC File Offset: 0x0009F1BC
	internal override bool ShouldApply(EffectProperties props, EntityControl applyTo = null)
	{
		return !(applyTo == null) && applyTo.Initialized && ((props.IsLocal && !(applyTo is PlayerControl)) || applyTo == PlayerControl.myInstance);
	}

	// Token: 0x060019DF RID: 6623 RVA: 0x000A0FF3 File Offset: 0x0009F1F3
	private bool ValidateAmount(int num)
	{
		return num >= 0;
	}

	// Token: 0x060019E0 RID: 6624 RVA: 0x000A0FFC File Offset: 0x0009F1FC
	public ApplyDamageNode()
	{
	}

	// Token: 0x04001A34 RID: 6708
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Scaling Value", PortLocation.Default)]
	public Node Value;

	// Token: 0x04001A35 RID: 6709
	public int Amount = 10;

	// Token: 0x04001A36 RID: 6710
	public ApplyOn DamageTarget = ApplyOn.Affected;

	// Token: 0x04001A37 RID: 6711
	public bool CanCrit = true;

	// Token: 0x04001A38 RID: 6712
	public bool CanScale = true;

	// Token: 0x04001A39 RID: 6713
	public float SnippetChanceMult = 1f;

	// Token: 0x04001A3A RID: 6714
	public DNumType DamageOverride;

	// Token: 0x04001A3B RID: 6715
	public bool FCTextFromCenter;

	// Token: 0x04001A3C RID: 6716
	public bool StatSourceOverride;

	// Token: 0x04001A3D RID: 6717
	public AugmentTree StatOverride;

	// Token: 0x04001A3E RID: 6718
	public PlayerAbilityType AbilitySrc = PlayerAbilityType.Any;
}
