using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000320 RID: 800
public class SelectTargetNode : AIActionNode
{
	// Token: 0x06001B92 RID: 7058 RVA: 0x000A9AC5 File Offset: 0x000A7CC5
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = new Node.InspectorProps();
		inspectorProps.MinInspectorSize.x = 250f;
		inspectorProps.Title = "Select Target";
		inspectorProps.SortX = true;
		return inspectorProps;
	}

	// Token: 0x06001B93 RID: 7059 RVA: 0x000A9AF0 File Offset: 0x000A7CF0
	internal override AILogicState Run(AIControl entity)
	{
		bool flag = entity.currentTarget != null && !entity.currentTarget.IsDead;
		if (flag && this.TargetingMode == SelectTargetNode.TargetOverrideMode.FailIfHasTarget)
		{
			return AILogicState.Fail;
		}
		if (base.AIRoot.CurSequence != null)
		{
			return AILogicState.Fail;
		}
		if (entity.IsUsingActiveAbility())
		{
			return AILogicState.Fail;
		}
		List<EffectProperties> list = new List<EffectProperties>();
		EffectProperties effectProperties = new EffectProperties(entity);
		foreach (EntityControl entityControl in EntityControl.AllEntities)
		{
			if (!entityControl.IsDead && entityControl.Targetable && (!(entityControl == entity) || this.IncludeSelf) && (this.Entities != SelectTargetNode.EntitySelectionType.Enemy || entityControl.TeamID != entity.TeamID) && (this.Entities != SelectTargetNode.EntitySelectionType.Allies || entityControl.TeamID == entity.TeamID) && (this.Entities != SelectTargetNode.EntitySelectionType.Player || entityControl is PlayerControl))
			{
				EffectProperties effectProperties2 = effectProperties.Copy(false);
				effectProperties2.Affected = ((entityControl != null) ? entityControl.gameObject : null);
				effectProperties2.SeekTarget = effectProperties2.Affected;
				list.Add(effectProperties2);
			}
		}
		foreach (Node node in this.Filters)
		{
			((LogicFilterNode)node).Filter(ref list, effectProperties);
		}
		if (list.Count == 0)
		{
			return AILogicState.Fail;
		}
		if (this.TargetingMode == SelectTargetNode.TargetOverrideMode.KeepIfValid && flag)
		{
			using (List<EffectProperties>.Enumerator enumerator3 = list.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					if (enumerator3.Current.AffectedControl == entity.currentTarget)
					{
						return AILogicState.Success;
					}
				}
			}
		}
		if (list[0].AffectedControl == null || list[0].AffectedControl.IsDead)
		{
			return AILogicState.Fail;
		}
		entity.SetTarget(list[0].AffectedControl);
		return AILogicState.Success;
	}

	// Token: 0x06001B94 RID: 7060 RVA: 0x000A9D28 File Offset: 0x000A7F28
	private EntityControl SelectHighestThreat(EntityControl ai, List<EntityControl> options)
	{
		ai.movement.GetPosition();
		float num = 0f;
		EntityControl result = null;
		foreach (EntityControl entityControl in options)
		{
			float num2 = 0f;
			if (num2 > num)
			{
				num = num2;
				result = entityControl;
			}
		}
		return result;
	}

	// Token: 0x06001B95 RID: 7061 RVA: 0x000A9D98 File Offset: 0x000A7F98
	private EntityControl SelectClosest(EntityControl ai, List<EntityControl> options)
	{
		Vector3 position = ai.movement.GetPosition();
		float num = float.MaxValue;
		EntityControl result = null;
		foreach (EntityControl entityControl in options)
		{
			float num2 = Vector3.Distance(position, entityControl.movement.GetPosition());
			if (num2 < num)
			{
				num = num2;
				result = entityControl;
			}
		}
		return result;
	}

	// Token: 0x06001B96 RID: 7062 RVA: 0x000A9E14 File Offset: 0x000A8014
	private EntityControl SelectClosestVisible(EntityControl ai, List<EntityControl> options)
	{
		this.FilterVisibleEnemies(ai, options);
		return this.SelectClosest(ai, options);
	}

	// Token: 0x06001B97 RID: 7063 RVA: 0x000A9E28 File Offset: 0x000A8028
	private EntityControl SelectFarthestVisible(EntityControl ai, List<EntityControl> options)
	{
		this.FilterVisibleEnemies(ai, options);
		Vector3 position = ai.movement.GetPosition();
		float num = 0f;
		EntityControl result = null;
		foreach (EntityControl entityControl in options)
		{
			float num2 = Vector3.Distance(position, entityControl.movement.GetPosition());
			if (num2 > num)
			{
				num = num2;
				result = entityControl;
			}
		}
		return result;
	}

	// Token: 0x06001B98 RID: 7064 RVA: 0x000A9EAC File Offset: 0x000A80AC
	private void FilterVisibleEnemies(EntityControl ai, List<EntityControl> options)
	{
		for (int i = options.Count - 1; i >= 0; i--)
		{
			if (!false)
			{
				options.RemoveAt(i);
			}
		}
	}

	// Token: 0x06001B99 RID: 7065 RVA: 0x000A9ED6 File Offset: 0x000A80D6
	public SelectTargetNode()
	{
	}

	// Token: 0x04001BF1 RID: 7153
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(LogicFilterNode), true, "Filters", PortLocation.Vertical)]
	public List<Node> Filters = new List<Node>();

	// Token: 0x04001BF2 RID: 7154
	public SelectTargetNode.TargetOverrideMode TargetingMode;

	// Token: 0x04001BF3 RID: 7155
	public bool IncludeSelf;

	// Token: 0x04001BF4 RID: 7156
	public SelectTargetNode.EntitySelectionType Entities;

	// Token: 0x0200065A RID: 1626
	public enum TargetSelectionType
	{
		// Token: 0x04002B1E RID: 11038
		Threat,
		// Token: 0x04002B1F RID: 11039
		Closest,
		// Token: 0x04002B20 RID: 11040
		ClosestVisible,
		// Token: 0x04002B21 RID: 11041
		FarthestVisible
	}

	// Token: 0x0200065B RID: 1627
	public enum TargetOverrideMode
	{
		// Token: 0x04002B23 RID: 11043
		FailIfHasTarget,
		// Token: 0x04002B24 RID: 11044
		KeepIfValid,
		// Token: 0x04002B25 RID: 11045
		OverrideTarget
	}

	// Token: 0x0200065C RID: 1628
	public enum EntitySelectionType
	{
		// Token: 0x04002B27 RID: 11047
		All,
		// Token: 0x04002B28 RID: 11048
		Enemy,
		// Token: 0x04002B29 RID: 11049
		Allies,
		// Token: 0x04002B2A RID: 11050
		Player
	}
}
