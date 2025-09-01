using System;
using UnityEngine;

// Token: 0x020002E4 RID: 740
public class StopAbilityNode : EffectNode
{
	// Token: 0x06001AAA RID: 6826 RVA: 0x000A5FC7 File Offset: 0x000A41C7
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Stop Ability",
			ShowInspectorView = true,
			MinInspectorSize = new Vector2(200f, 0f)
		};
	}

	// Token: 0x06001AAB RID: 6827 RVA: 0x000A5FF8 File Offset: 0x000A41F8
	internal override void Apply(EffectProperties properties)
	{
		if (this.Ability == null)
		{
			return;
		}
		EntityControl sourceControl = properties.SourceControl;
		if (!this.ShouldApply(properties, sourceControl))
		{
			return;
		}
		PlayerControl playerControl = sourceControl as PlayerControl;
		if (playerControl != null)
		{
			playerControl.actions.ForceCancel(this.Ability.Root);
			return;
		}
		if (sourceControl is AIControl)
		{
			AINetworked ainetworked = sourceControl.net as AINetworked;
			if (ainetworked != null)
			{
				ainetworked.AbilityReleased(this.Ability);
			}
		}
	}

	// Token: 0x06001AAC RID: 6828 RVA: 0x000A606F File Offset: 0x000A426F
	internal override bool ShouldApply(EffectProperties props, EntityControl applyTo)
	{
		return !(applyTo == null) && ((props.IsLocal && !(applyTo is PlayerControl)) || applyTo == PlayerControl.myInstance);
	}

	// Token: 0x06001AAD RID: 6829 RVA: 0x000A609E File Offset: 0x000A429E
	public StopAbilityNode()
	{
	}

	// Token: 0x04001B41 RID: 6977
	public AbilityTree Ability;
}
