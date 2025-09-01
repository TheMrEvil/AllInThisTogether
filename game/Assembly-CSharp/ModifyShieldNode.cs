using System;
using UnityEngine;

// Token: 0x020002D0 RID: 720
public class ModifyShieldNode : EffectNode
{
	// Token: 0x06001A58 RID: 6744 RVA: 0x000A3F6F File Offset: 0x000A216F
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Modify Shield",
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x06001A59 RID: 6745 RVA: 0x000A3F98 File Offset: 0x000A2198
	internal override void Apply(EffectProperties properties)
	{
		EntityControl applicationEntity = properties.GetApplicationEntity(this.ApplyTarget);
		if (applicationEntity == null)
		{
			return;
		}
		if (!this.ShouldApply(properties, applicationEntity))
		{
			return;
		}
		EntityHealth health = applicationEntity.health;
		if (health == null || health.isDead)
		{
			return;
		}
		if (this.Effect == ModifyShieldNode.ShieldEffect.Break)
		{
			health.BreakShield(properties);
			return;
		}
		if (this.Effect == ModifyShieldNode.ShieldEffect.Fill)
		{
			health.FillShield();
			return;
		}
		if (this.Effect == ModifyShieldNode.ShieldEffect.ResetCooldown)
		{
			health.ResetShieldCD(false);
			return;
		}
		int num = this.Amount;
		if (this.Value != null)
		{
			num = (int)((NumberNode)this.Value).Evaluate(properties);
		}
		int num2 = (int)properties.ModifyAbilityPassives(Passive.AbilityValue.ShieldGained, (float)num);
		num2 = Mathf.Max(num2, 0);
		if (num2 == 0)
		{
			return;
		}
		health.ModifyShield((this.Effect == ModifyShieldNode.ShieldEffect.Drain) ? (-num2) : num2, properties);
		base.Completed();
	}

	// Token: 0x06001A5A RID: 6746 RVA: 0x000A4070 File Offset: 0x000A2270
	internal override bool ShouldApply(EffectProperties props, EntityControl applyTo = null)
	{
		return (props.IsLocal && !(applyTo is PlayerControl)) || applyTo == PlayerControl.myInstance;
	}

	// Token: 0x06001A5B RID: 6747 RVA: 0x000A4094 File Offset: 0x000A2294
	private bool ValidateAmount(int num)
	{
		return num >= 0;
	}

	// Token: 0x06001A5C RID: 6748 RVA: 0x000A409D File Offset: 0x000A229D
	public ModifyShieldNode()
	{
	}

	// Token: 0x04001ACC RID: 6860
	public ModifyShieldNode.ShieldEffect Effect;

	// Token: 0x04001ACD RID: 6861
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Scaling Value", PortLocation.Default)]
	public Node Value;

	// Token: 0x04001ACE RID: 6862
	public int Amount = 10;

	// Token: 0x04001ACF RID: 6863
	public ApplyOn ApplyTarget = ApplyOn.Affected;

	// Token: 0x0200064E RID: 1614
	public enum ShieldEffect
	{
		// Token: 0x04002AF5 RID: 10997
		Gain,
		// Token: 0x04002AF6 RID: 10998
		Break,
		// Token: 0x04002AF7 RID: 10999
		Drain,
		// Token: 0x04002AF8 RID: 11000
		Fill,
		// Token: 0x04002AF9 RID: 11001
		ResetCooldown
	}
}
