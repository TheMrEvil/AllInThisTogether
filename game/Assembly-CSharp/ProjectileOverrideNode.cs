using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000348 RID: 840
public class ProjectileOverrideNode : ModOverrideNode
{
	// Token: 0x06001C59 RID: 7257 RVA: 0x000AD5F8 File Offset: 0x000AB7F8
	public override void OverrideNodeProperties(EffectProperties props, Node node, object[] values)
	{
		if (!(node is SpawnProjectileEffectNode))
		{
			return;
		}
		if (!base.ScopeMatches(props.AbilityType))
		{
			return;
		}
		if (this.OverrideSpawn)
		{
			IntHolder intHolder = values[0] as IntHolder;
			FloatHolder floatHolder = values[1] as FloatHolder;
			intHolder.value = this.Count;
			floatHolder.value = this.Angle;
		}
	}

	// Token: 0x06001C5A RID: 7258 RVA: 0x000AD64D File Offset: 0x000AB84D
	public override bool ShouldOverride(EffectProperties props, Node node)
	{
		return node is ProjectileNode && props != null && base.ScopeMatches(props.AbilityType) && (props.Keyword == Keyword.None || this.OverrideScope == PlayerAbilityType.Any);
	}

	// Token: 0x06001C5B RID: 7259 RVA: 0x000AD685 File Offset: 0x000AB885
	public override void OverrideNodeEffects(EffectProperties props, Node node, ref List<ModOverrideNode> overrides)
	{
		if (!this.ShouldOverride(props, node))
		{
			return;
		}
		if (node as ProjectileNode == null)
		{
			return;
		}
		overrides.Add(this);
	}

	// Token: 0x06001C5C RID: 7260 RVA: 0x000AD6A9 File Offset: 0x000AB8A9
	private bool HasMovement()
	{
		return this.MoveProps != null;
	}

	// Token: 0x06001C5D RID: 7261 RVA: 0x000AD6B4 File Offset: 0x000AB8B4
	public bool HasDistEvents()
	{
		return this.OnDistance.Count > 0;
	}

	// Token: 0x06001C5E RID: 7262 RVA: 0x000AD6C4 File Offset: 0x000AB8C4
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Projectile Override",
			MinInspectorSize = new Vector2(300f, 0f)
		};
	}

	// Token: 0x06001C5F RID: 7263 RVA: 0x000AD6EC File Offset: 0x000AB8EC
	public ProjectileOverrideNode()
	{
	}

	// Token: 0x04001D29 RID: 7465
	public bool OverrideSpawn;

	// Token: 0x04001D2A RID: 7466
	public int Count = 1;

	// Token: 0x04001D2B RID: 7467
	[Range(0f, 179f)]
	public float Angle;

	// Token: 0x04001D2C RID: 7468
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(ProjectileMoveModuleNode), true, "New Modules", PortLocation.Default)]
	public List<Node> MoveProps = new List<Node>();

	// Token: 0x04001D2D RID: 7469
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(ProjectileLifetimeProps), false, "Lifetime Override", PortLocation.Default)]
	public Node LifetimeProps;

	// Token: 0x04001D2E RID: 7470
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(ProjectileInteractionNode), true, "New Interactions", PortLocation.Default)]
	public List<Node> Interactions = new List<Node>();

	// Token: 0x04001D2F RID: 7471
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Expire", PortLocation.Default)]
	public List<Node> OnExpire = new List<Node>();

	// Token: 0x04001D30 RID: 7472
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Distance Traveled", PortLocation.Default)]
	public List<Node> OnDistance = new List<Node>();

	// Token: 0x04001D31 RID: 7473
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Time Lived", PortLocation.Default)]
	public List<Node> OnLived = new List<Node>();

	// Token: 0x04001D32 RID: 7474
	public float DistanceEventRate = 1f;
}
