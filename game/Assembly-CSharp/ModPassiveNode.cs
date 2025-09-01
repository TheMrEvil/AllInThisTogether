using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000337 RID: 823
public class ModPassiveNode : Node
{
	// Token: 0x17000198 RID: 408
	// (get) Token: 0x06001C09 RID: 7177 RVA: 0x000ABCC8 File Offset: 0x000A9EC8
	private bool IsDamageScaling
	{
		get
		{
			if (this.category == ModPassiveNode.Category.Entity)
			{
				return this.entityPassive.Value == Passive.EntityValue.DamageTaken;
			}
			if (this.category == ModPassiveNode.Category.Ability)
			{
				return this.abilityPassive.Value == Passive.AbilityValue.AllDamage || this.abilityPassive.Value == Passive.AbilityValue.Area_Damage;
			}
			return this.category == ModPassiveNode.Category.Keyword && this.keywordPassive.Value == Passive.AbilityValue.AllDamage;
		}
	}

	// Token: 0x06001C0A RID: 7178 RVA: 0x000ABD38 File Offset: 0x000A9F38
	public float TryModifyValue(EntityControl control, EffectProperties props, Passive passive, float startVal, AugmentRootNode root, int stackCount = 1, bool ignoreRequirements = false)
	{
		if (!this.MatchesPassive(passive))
		{
			return 0f;
		}
		if (!ignoreRequirements && !this.RequirementsMet(props))
		{
			return 0f;
		}
		float num = this.ModifyValue(props, startVal, stackCount);
		if (this.IsDamageScaling)
		{
			PlayerControl playerControl = props.SourceControl as PlayerControl;
			if (playerControl != null)
			{
				playerControl.PStats.AugmentDamage(root.GetCauseID(), (int)num);
			}
		}
		return num;
	}

	// Token: 0x06001C0B RID: 7179 RVA: 0x000ABDA0 File Offset: 0x000A9FA0
	private float ModifyValue(EffectProperties props, float startVal, int stackCount)
	{
		float num = this.Value;
		if (this.InValue != null)
		{
			NumberNode numberNode = this.InValue as NumberNode;
			if (numberNode != null)
			{
				num = numberNode.Evaluate(props);
			}
		}
		if (!this.Multiplier)
		{
			return num * (float)stackCount;
		}
		return Mathf.Abs(startVal) * (num * (float)stackCount);
	}

	// Token: 0x06001C0C RID: 7180 RVA: 0x000ABDF4 File Offset: 0x000A9FF4
	public bool RequirementsMet(EntityControl entity)
	{
		if (!(this.Reqs == null))
		{
			LogicNode logicNode = this.Reqs as LogicNode;
			if (logicNode != null)
			{
				return logicNode.Evaluate(entity.SelfProps);
			}
		}
		return true;
	}

	// Token: 0x06001C0D RID: 7181 RVA: 0x000ABE2C File Offset: 0x000AA02C
	public bool RequirementsMet(EffectProperties props)
	{
		if (!(this.Reqs == null))
		{
			LogicNode logicNode = this.Reqs as LogicNode;
			if (logicNode != null)
			{
				return logicNode.Evaluate(props);
			}
		}
		return true;
	}

	// Token: 0x06001C0E RID: 7182 RVA: 0x000ABE60 File Offset: 0x000AA060
	public bool MatchesPassive(Passive p)
	{
		if (p is EntityPassive)
		{
			return this.category == ModPassiveNode.Category.Entity && this.entityPassive.Matches(p);
		}
		if (p is AbilityPassive)
		{
			return this.category == ModPassiveNode.Category.Ability && this.abilityPassive.Matches(p);
		}
		return p is KeywordPassive && this.category == ModPassiveNode.Category.Keyword && this.keywordPassive.Matches(p);
	}

	// Token: 0x06001C0F RID: 7183 RVA: 0x000ABED0 File Offset: 0x000AA0D0
	public Passive GetPassive()
	{
		Passive result;
		switch (this.category)
		{
		case ModPassiveNode.Category.Entity:
			result = this.entityPassive;
			break;
		case ModPassiveNode.Category.Ability:
			result = this.abilityPassive;
			break;
		case ModPassiveNode.Category.Keyword:
			result = this.keywordPassive;
			break;
		default:
			result = this.entityPassive;
			break;
		}
		return result;
	}

	// Token: 0x06001C10 RID: 7184 RVA: 0x000ABF1B File Offset: 0x000AA11B
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Passive",
			MinInspectorSize = new Vector2(150f, 0f)
		};
	}

	// Token: 0x06001C11 RID: 7185 RVA: 0x000ABF44 File Offset: 0x000AA144
	public override Node Clone(Dictionary<string, Node> alreadyCloned = null, bool fullClone = false)
	{
		ModPassiveNode modPassiveNode = base.Clone(alreadyCloned, fullClone) as ModPassiveNode;
		modPassiveNode.entityPassive = this.entityPassive.Copy();
		modPassiveNode.abilityPassive = this.abilityPassive.Copy();
		modPassiveNode.keywordPassive = this.keywordPassive.Copy();
		return modPassiveNode;
	}

	// Token: 0x06001C12 RID: 7186 RVA: 0x000ABF91 File Offset: 0x000AA191
	public override void OnCloned()
	{
		this.entityPassive = this.entityPassive.Copy();
		this.abilityPassive = this.abilityPassive.Copy();
		this.keywordPassive = this.keywordPassive.Copy();
	}

	// Token: 0x06001C13 RID: 7187 RVA: 0x000ABFC6 File Offset: 0x000AA1C6
	public ModPassiveNode()
	{
	}

	// Token: 0x04001C68 RID: 7272
	public ModPassiveNode.Category category;

	// Token: 0x04001C69 RID: 7273
	public EntityPassive entityPassive;

	// Token: 0x04001C6A RID: 7274
	public AbilityPassive abilityPassive;

	// Token: 0x04001C6B RID: 7275
	public KeywordPassive keywordPassive;

	// Token: 0x04001C6C RID: 7276
	[InputPort(typeof(LogicNode), false, "Requirements", PortLocation.Vertical)]
	[HideInInspector]
	[SerializeField]
	public Node Reqs;

	// Token: 0x04001C6D RID: 7277
	[OutputPort(typeof(NumberNode), false, "Value Override", PortLocation.Default)]
	[SerializeField]
	[HideInInspector]
	public Node InValue;

	// Token: 0x04001C6E RID: 7278
	public float Value;

	// Token: 0x04001C6F RID: 7279
	public bool Multiplier;

	// Token: 0x02000665 RID: 1637
	public enum Category
	{
		// Token: 0x04002B4B RID: 11083
		Entity,
		// Token: 0x04002B4C RID: 11084
		Ability,
		// Token: 0x04002B4D RID: 11085
		Keyword
	}

	// Token: 0x02000666 RID: 1638
	public enum PassiveMode
	{
		// Token: 0x04002B4F RID: 11087
		Flat,
		// Token: 0x04002B50 RID: 11088
		ScalingMultiplier
	}
}
