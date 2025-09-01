using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E8 RID: 744
public class TriggerAbilityActionNode : EffectNode
{
	// Token: 0x06001ABA RID: 6842 RVA: 0x000A63A4 File Offset: 0x000A45A4
	internal override void Apply(EffectProperties properties)
	{
		EffectProperties effectProperties = properties.Copy(false);
		effectProperties.AbilityType = this.Ability;
		if (!(effectProperties.SourceControl == null))
		{
			PlayerControl playerControl = effectProperties.SourceControl as PlayerControl;
			if (playerControl != null)
			{
				Ability ability = playerControl.actions.GetAbility(this.Ability);
				if (ability == null)
				{
					return;
				}
				List<ActionTree> abilityActions = ability.rootNode.Usage.AbilityMetadata.AbilityActions;
				effectProperties.StartLoc = effectProperties.OutLoc.Copy();
				if (this.LocOverride != null)
				{
					effectProperties.StartLoc = (effectProperties.OutLoc = (this.LocOverride as PoseNode).GetPose(effectProperties));
				}
				else if (this.UseAbilityLocation && ability.rootNode.FromLocation != null)
				{
					PoseNode poseNode = ability.rootNode.FromLocation as PoseNode;
					if (poseNode != null)
					{
						effectProperties.StartLoc = (effectProperties.OutLoc = poseNode.GetPose(effectProperties));
					}
				}
				Debug.DrawLine(effectProperties.StartLoc.GetPosition(effectProperties), Vector3.up, Color.white, 1f);
				foreach (ActionTree actionTree in abilityActions)
				{
					actionTree.Root.Apply(effectProperties.Copy(false));
				}
				EffectProperties effectProperties2 = effectProperties.Copy(false);
				effectProperties2.SetExtra(EProp.FromSnippet, 1f);
				playerControl.TriggerSnippets(EventTrigger.AbilityUsed, effectProperties2.Copy(false), 1f);
				playerControl.TriggerSnippets(EventTrigger.AbilityReleased, effectProperties2, 1f);
				return;
			}
		}
	}

	// Token: 0x06001ABB RID: 6843 RVA: 0x000A6540 File Offset: 0x000A4740
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Trigger Ability";
		inspectorProps.MinInspectorSize = new Vector2(160f, 0f);
		inspectorProps.MaxInspectorSize = new Vector2(160f, 0f);
		return inspectorProps;
	}

	// Token: 0x06001ABC RID: 6844 RVA: 0x000A657D File Offset: 0x000A477D
	public TriggerAbilityActionNode()
	{
	}

	// Token: 0x04001B4A RID: 6986
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(PoseNode), false, "Pose Override", PortLocation.Default)]
	public Node LocOverride;

	// Token: 0x04001B4B RID: 6987
	public PlayerAbilityType Ability;

	// Token: 0x04001B4C RID: 6988
	public bool UseAbilityLocation = true;
}
