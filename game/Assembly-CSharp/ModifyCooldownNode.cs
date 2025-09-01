using System;
using UnityEngine;

// Token: 0x020002CC RID: 716
public class ModifyCooldownNode : EffectNode
{
	// Token: 0x06001A45 RID: 6725 RVA: 0x000A3430 File Offset: 0x000A1630
	internal override void Apply(EffectProperties properties)
	{
		if (properties.AffectedControl == null && this.ApplyTo == ApplyOn.Affected)
		{
			return;
		}
		if (properties.SourceControl == null && this.ApplyTo == ApplyOn.Source)
		{
			return;
		}
		EntityControl applicationEntity = properties.GetApplicationEntity(this.ApplyTo);
		if (!this.ShouldApply(properties, applicationEntity))
		{
			return;
		}
		float num = this.CooldownDelta;
		if (this.ValueNode != null)
		{
			num = (this.ValueNode as NumberNode).Evaluate(properties);
		}
		EffectProperties effectProperties = properties.Copy(false);
		effectProperties.AbilityType = this.AbilityType;
		if (num != 0f)
		{
			if (this.AbilityType != PlayerAbilityType.Any)
			{
				PlayerControl playerControl = applicationEntity as PlayerControl;
				if (playerControl != null)
				{
					if (this.ResetCooldown)
					{
						playerControl.actions.ResetCooldown(this.AbilityType, this.UseFrameDelay);
						goto IL_1A3;
					}
					if (this.SetAbsolute)
					{
						playerControl.actions.SetCooldown(this.AbilityType, num, effectProperties);
						goto IL_1A3;
					}
					playerControl.actions.ModifyCooldown(this.AbilityType, num);
					goto IL_1A3;
				}
			}
			if (this.AbilityType == PlayerAbilityType.None && this.Ability != null)
			{
				AbilityRootNode root = this.Ability.Root;
				if (this.ResetCooldown)
				{
					applicationEntity.ResetCooldown(root);
				}
				if (this.SetAbsolute)
				{
					applicationEntity.SetCooldown(root, num, effectProperties);
				}
				else
				{
					applicationEntity.ModifyCooldown(root, num);
				}
			}
			else if (this.RootNode is AbilityRootNode)
			{
				if (this.ResetCooldown)
				{
					applicationEntity.ResetCooldown(this.RootNode as AbilityRootNode);
				}
				if (this.SetAbsolute)
				{
					applicationEntity.SetCooldown(this.RootNode as AbilityRootNode, num, effectProperties);
				}
				else
				{
					applicationEntity.ModifyCooldown(this.RootNode as AbilityRootNode, num);
				}
			}
		}
		IL_1A3:
		base.Completed();
	}

	// Token: 0x06001A46 RID: 6726 RVA: 0x000A35E6 File Offset: 0x000A17E6
	internal override bool ShouldApply(EffectProperties props, EntityControl applyTo)
	{
		return applyTo == PlayerControl.myInstance || !(applyTo is PlayerControl);
	}

	// Token: 0x06001A47 RID: 6727 RVA: 0x000A3602 File Offset: 0x000A1802
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Modify Cooldown",
			MinInspectorSize = new Vector2(260f, 0f)
		};
	}

	// Token: 0x06001A48 RID: 6728 RVA: 0x000A3629 File Offset: 0x000A1829
	public ModifyCooldownNode()
	{
	}

	// Token: 0x04001AAD RID: 6829
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Value", PortLocation.Default)]
	public Node ValueNode;

	// Token: 0x04001AAE RID: 6830
	public ApplyOn ApplyTo;

	// Token: 0x04001AAF RID: 6831
	public PlayerAbilityType AbilityType = PlayerAbilityType.Any;

	// Token: 0x04001AB0 RID: 6832
	public AbilityTree Ability;

	// Token: 0x04001AB1 RID: 6833
	public bool ResetCooldown;

	// Token: 0x04001AB2 RID: 6834
	public bool UseFrameDelay;

	// Token: 0x04001AB3 RID: 6835
	public bool SetAbsolute;

	// Token: 0x04001AB4 RID: 6836
	public float CooldownDelta = -1f;
}
