using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002F6 RID: 758
public class ProjectileNode : Node
{
	// Token: 0x06001AF0 RID: 6896 RVA: 0x000A7664 File Offset: 0x000A5864
	public void AddInteraction(ProjectileInteractionNode node)
	{
		ProjectileInteractionNode projectileInteractionNode = null;
		foreach (Node node2 in this.Interactions)
		{
			ProjectileInteractionNode projectileInteractionNode2 = (ProjectileInteractionNode)node2;
			if (projectileInteractionNode2.InteractsWith == node.InteractsWith)
			{
				projectileInteractionNode = projectileInteractionNode2;
				break;
			}
		}
		if (projectileInteractionNode == null)
		{
			this.Interactions.Add(node);
			return;
		}
		if (projectileInteractionNode.PierceCount == 0)
		{
			projectileInteractionNode.VelocityDampen = node.VelocityDampen;
			projectileInteractionNode.Result = node.Result;
		}
		projectileInteractionNode.PierceCount += node.PierceCount;
		foreach (Node item in node.OnImpact)
		{
			projectileInteractionNode.OnImpact.Add(item);
		}
	}

	// Token: 0x06001AF1 RID: 6897 RVA: 0x000A775C File Offset: 0x000A595C
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Projectile Properties",
			ShowInspectorView = true,
			AllowMultipleInputs = true
		};
	}

	// Token: 0x06001AF2 RID: 6898 RVA: 0x000A777C File Offset: 0x000A597C
	public ProjectileNode()
	{
	}

	// Token: 0x04001B91 RID: 7057
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(ProjectileMoveProps), false, "Movement", PortLocation.Default)]
	public Node MoveProps;

	// Token: 0x04001B92 RID: 7058
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(ProjectileLifetimeProps), false, "Lifetime", PortLocation.Default)]
	public Node LifetimeProps;

	// Token: 0x04001B93 RID: 7059
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(ProjectileInteractionNode), true, "Interactions", PortLocation.Default)]
	public List<Node> Interactions = new List<Node>();

	// Token: 0x04001B94 RID: 7060
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Spawn", PortLocation.Default)]
	public List<Node> OnSpawn = new List<Node>();

	// Token: 0x04001B95 RID: 7061
	public float SnippetChanceMult = 1f;

	// Token: 0x04001B96 RID: 7062
	public bool CanOverride = true;
}
