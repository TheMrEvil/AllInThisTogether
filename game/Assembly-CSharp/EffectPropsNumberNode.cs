using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200037C RID: 892
public class EffectPropsNumberNode : NumberNode
{
	// Token: 0x06001D6A RID: 7530 RVA: 0x000B2D44 File Offset: 0x000B0F44
	public override float Evaluate(EffectProperties props)
	{
		if (props == null)
		{
			return 0f;
		}
		if (this.Stat == EProp.Lifetime)
		{
			return props.Lifetime;
		}
		if (this.Stat == EProp.TotalDamage)
		{
			float @float = props.GetFloat("i_TotalDmg");
			if (float.IsNaN(@float))
			{
				return 0f;
			}
			return @float;
		}
		else
		{
			if (this.Stat == EProp.AugmentScalar)
			{
				return props.GetScalarValue(new ValueTuple<PlayerAbilityType, Passive.AbilityValue>(this.AbilityT, this.APassive));
			}
			if (this.Stat == EProp.ManaUsed)
			{
				int num = 0;
				foreach (KeyValuePair<MagicColor, int> keyValuePair in props.ManaConsumed)
				{
					num += keyValuePair.Value;
				}
				return (float)num;
			}
			return props.GetExtra(this.Stat, 0f);
		}
	}

	// Token: 0x06001D6B RID: 7531 RVA: 0x000B2E28 File Offset: 0x000B1028
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Effect Props Value",
			MinInspectorSize = new Vector2(200f, 0f),
			MaxInspectorSize = new Vector2(200f, 0f),
			AllowMultipleInputs = true
		};
	}

	// Token: 0x06001D6C RID: 7532 RVA: 0x000B2E76 File Offset: 0x000B1076
	public EffectPropsNumberNode()
	{
	}

	// Token: 0x04001E16 RID: 7702
	public EProp Stat;

	// Token: 0x04001E17 RID: 7703
	public PlayerAbilityType AbilityT;

	// Token: 0x04001E18 RID: 7704
	public Passive.AbilityValue APassive;
}
