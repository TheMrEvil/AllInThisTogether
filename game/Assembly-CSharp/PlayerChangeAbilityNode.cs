using System;
using UnityEngine;

// Token: 0x020002D3 RID: 723
public class PlayerChangeAbilityNode : EffectNode
{
	// Token: 0x06001A64 RID: 6756 RVA: 0x000A42C8 File Offset: 0x000A24C8
	internal override void Apply(EffectProperties properties)
	{
		PlayerChangeAbilityNode.ActionType action = this.Action;
		if (action == PlayerChangeAbilityNode.ActionType.Change)
		{
			this.ChangeAbility(properties);
			return;
		}
		if (action != PlayerChangeAbilityNode.ActionType.Revert)
		{
			return;
		}
		this.RevertAbility(properties);
	}

	// Token: 0x06001A65 RID: 6757 RVA: 0x000A42F4 File Offset: 0x000A24F4
	private void ChangeAbility(EffectProperties properties)
	{
		if (this.Ability == null)
		{
			return;
		}
		PlayerControl playerControl = properties.SourceControl as PlayerControl;
		if (playerControl == null)
		{
			Debug.LogError("Change Ability node only works for Player Source!");
			return;
		}
		PlayerAbilityType plrAbilityType = this.Ability.Root.PlrAbilityType;
		Ability ability = PlayerControl.myInstance.actions.GetAbility(plrAbilityType);
		if (((ability != null) ? ability.AbilityTree : null) == this.Ability)
		{
			return;
		}
		playerControl.actions.LoadAbility(plrAbilityType, this.Ability.ID, this.Temporary);
		float cooldown = this.Ability.Root.Usage.Cooldown;
		playerControl.actions.SetCooldown(plrAbilityType, this.StartingCD * cooldown, null);
	}

	// Token: 0x06001A66 RID: 6758 RVA: 0x000A43B0 File Offset: 0x000A25B0
	private void RevertAbility(EffectProperties properties)
	{
		PlayerControl playerControl = properties.SourceControl as PlayerControl;
		if (playerControl == null)
		{
			Debug.LogError("Change Ability node only works for Player Source!");
			return;
		}
		playerControl.actions.ResetTempAbility(this.AbilityType);
	}

	// Token: 0x06001A67 RID: 6759 RVA: 0x000A43E8 File Offset: 0x000A25E8
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Swap Ability"
		};
	}

	// Token: 0x06001A68 RID: 6760 RVA: 0x000A43FA File Offset: 0x000A25FA
	public PlayerChangeAbilityNode()
	{
	}

	// Token: 0x04001AD9 RID: 6873
	public PlayerChangeAbilityNode.ActionType Action;

	// Token: 0x04001ADA RID: 6874
	public AbilityTree Ability;

	// Token: 0x04001ADB RID: 6875
	[Range(0f, 1f)]
	public float StartingCD;

	// Token: 0x04001ADC RID: 6876
	public bool Temporary;

	// Token: 0x04001ADD RID: 6877
	public PlayerAbilityType AbilityType;

	// Token: 0x0200064F RID: 1615
	public enum ActionType
	{
		// Token: 0x04002AFB RID: 11003
		Change,
		// Token: 0x04002AFC RID: 11004
		Revert
	}
}
