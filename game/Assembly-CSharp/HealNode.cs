using System;
using UnityEngine;

// Token: 0x020002CA RID: 714
public class HealNode : EffectNode
{
	// Token: 0x17000191 RID: 401
	// (get) Token: 0x06001A3C RID: 6716 RVA: 0x000A31BB File Offset: 0x000A13BB
	internal override bool CanSkipClone
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06001A3D RID: 6717 RVA: 0x000A31BE File Offset: 0x000A13BE
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Heal",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x06001A3E RID: 6718 RVA: 0x000A31E8 File Offset: 0x000A13E8
	internal override void Apply(EffectProperties properties)
	{
		base.Completed();
		EntityControl applicationEntity = properties.GetApplicationEntity(this.HealTarget);
		if (applicationEntity == null)
		{
			return;
		}
		float num = (float)this.Amount;
		if (this.Value != null)
		{
			num = (this.Value as NumberNode).Evaluate(properties);
		}
		num = properties.ModifyAbilityPassives(Passive.AbilityValue.HealingDone, num);
		if (num != 0f)
		{
			EntityHealth entityHealth = (applicationEntity != null) ? applicationEntity.health : null;
			if (entityHealth != null && !entityHealth.isDead && num > 0f)
			{
				num = properties.ModifyEntityPassive(Passive.EntityValue.HealingReceived, num);
				num = Mathf.Max(0f, num);
				bool flag = this.ShouldApply(properties, applicationEntity);
				DamageInfo damageInfo = new DamageInfo(num, DNumType.Default, applicationEntity.view.ViewID, 1f, properties);
				if (flag)
				{
					entityHealth.Heal(damageInfo);
				}
				EntityControl sourceControl = properties.SourceControl;
				if (sourceControl != null)
				{
					sourceControl.HealingDone(damageInfo);
				}
				if (properties.SourceControl == PlayerControl.myInstance && applicationEntity != PlayerControl.myInstance)
				{
					CombatTextController.ShowWorldHeal((int)num, applicationEntity.display.CenterOfMass.position);
				}
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

	// Token: 0x06001A3F RID: 6719 RVA: 0x000A3330 File Offset: 0x000A1530
	internal override bool ShouldApply(EffectProperties props, EntityControl applyTo)
	{
		return (props.IsLocal && !(applyTo is PlayerControl)) || applyTo == PlayerControl.myInstance;
	}

	// Token: 0x06001A40 RID: 6720 RVA: 0x000A3354 File Offset: 0x000A1554
	public HealNode()
	{
	}

	// Token: 0x04001AA9 RID: 6825
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Scaling Value", PortLocation.Default)]
	public Node Value;

	// Token: 0x04001AAA RID: 6826
	public int Amount;

	// Token: 0x04001AAB RID: 6827
	public ApplyOn HealTarget = ApplyOn.Affected;
}
