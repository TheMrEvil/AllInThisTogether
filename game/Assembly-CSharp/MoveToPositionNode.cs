using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000316 RID: 790
public class MoveToPositionNode : AIActionNode
{
	// Token: 0x06001B67 RID: 7015 RVA: 0x000A9256 File Offset: 0x000A7456
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Move to Point",
			SortX = true,
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x06001B68 RID: 7016 RVA: 0x000A9284 File Offset: 0x000A7484
	internal override AILogicState Run(AIControl entity)
	{
		if (this.Location == null)
		{
			return AILogicState.Fail;
		}
		EffectProperties effectProperties = new EffectProperties(entity);
		effectProperties.OverrideSeed(this.curSeed, 0);
		AIMovement movement = entity.Movement;
		PotentialNavPoint potentialNavPoint = this.currentTarget;
		bool isRetest = this.currentTarget != null;
		Vector3 origin = this.GetOrigin(entity, effectProperties);
		if (!origin.IsValid())
		{
			return AILogicState.Fail;
		}
		if (this.UseMapData && this.CancelIfValid && movement.IsMoving() && movement.GetCurrentNavPoint() != null)
		{
			bool flag = true;
			NavVisionPoint currentNavPoint = movement.GetCurrentNavPoint();
			foreach (Node node in this.Filters)
			{
				PositionFilterNode positionFilterNode = (PositionFilterNode)node;
				flag &= positionFilterNode.PointIsValid(entity, new PotentialNavPoint(currentNavPoint), origin, isRetest);
			}
			if (flag)
			{
				movement.CancelMovement();
				this.NewSeed();
				this.currentTarget = null;
				return AILogicState.Success;
			}
		}
		if (this.ForceUpdate || potentialNavPoint == null)
		{
			bool flag2 = false;
			if (potentialNavPoint != null && this.UseMapData)
			{
				flag2 = true;
				foreach (Node node2 in this.Filters)
				{
					PositionFilterNode positionFilterNode2 = (PositionFilterNode)node2;
					flag2 &= positionFilterNode2.PointIsValid(entity, potentialNavPoint, origin, isRetest);
				}
			}
			if (!flag2)
			{
				List<PotentialNavPoint> list = this.UseMapData ? movement.GetAvailablePoints() : new List<PotentialNavPoint>();
				PotentialNavPoint item = new PotentialNavPoint((this.Location as LocationNode).GetLocation(effectProperties).GetPosition(effectProperties), true, true);
				list.Add(item);
				if (this.UseMapData)
				{
					foreach (Node node3 in this.Filters)
					{
						PositionFilterNode positionFilterNode3 = (PositionFilterNode)node3;
						int count = list.Count;
						positionFilterNode3.FilterPoints(ref list, entity, origin, isRetest);
					}
				}
				potentialNavPoint = ((list.Count > 0) ? list[0] : null);
			}
		}
		if (potentialNavPoint == null)
		{
			this.NewSeed();
			return this.Fail();
		}
		if (this.UseMapData && movement.GetCurrentNavPoint() == potentialNavPoint.visionPoint)
		{
			this.currentTarget = null;
			movement.CancelMovement();
			this.NewSeed();
			return AILogicState.Success;
		}
		if (this.UseMapData && potentialNavPoint.IsRaw && (movement.GetPosition() - potentialNavPoint.pos).sqrMagnitude < potentialNavPoint.ValidRadSqr())
		{
			this.currentTarget = null;
			movement.CancelMovement();
			this.NewSeed();
			return AILogicState.Success;
		}
		if ((movement.GetTargetPoint() - potentialNavPoint.pos).sqrMagnitude < potentialNavPoint.ValidRadSqr())
		{
			if (!movement.IsMoving())
			{
				this.currentTarget = null;
				this.NewSeed();
				return AILogicState.Success;
			}
			return AILogicState.Running;
		}
		else
		{
			if (!this.UseMapData && (movement.GetPosition() - potentialNavPoint.pos).sqrMagnitude < 0.1f)
			{
				this.currentTarget = null;
				this.NewSeed();
				return AILogicState.Success;
			}
			if (!movement.SetTargetPoint(potentialNavPoint))
			{
				return this.Fail();
			}
			this.currentTarget = potentialNavPoint;
			if (!this.UseMapData && !movement.IsMoving())
			{
				return AILogicState.Fail;
			}
			return AILogicState.Running;
		}
	}

	// Token: 0x06001B69 RID: 7017 RVA: 0x000A95D4 File Offset: 0x000A77D4
	private void NewSeed()
	{
		this.curSeed = UnityEngine.Random.Range(0, 999999);
	}

	// Token: 0x06001B6A RID: 7018 RVA: 0x000A95E7 File Offset: 0x000A77E7
	private AILogicState Fail()
	{
		this.currentTarget = null;
		return AILogicState.Fail;
	}

	// Token: 0x06001B6B RID: 7019 RVA: 0x000A95F4 File Offset: 0x000A77F4
	private Vector3 GetOrigin(AIControl entity, EffectProperties props)
	{
		Vector3 position = entity.movement.GetPosition();
		if (this.Location != null)
		{
			LocationNode locationNode = this.Location as LocationNode;
			if (locationNode != null)
			{
				return locationNode.GetLocation(props).GetPosition(props);
			}
		}
		return position;
	}

	// Token: 0x06001B6C RID: 7020 RVA: 0x000A9639 File Offset: 0x000A7839
	public bool UsesMapData()
	{
		return this.UseMapData;
	}

	// Token: 0x06001B6D RID: 7021 RVA: 0x000A9641 File Offset: 0x000A7841
	public MoveToPositionNode()
	{
	}

	// Token: 0x04001BD8 RID: 7128
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Reference Point", PortLocation.Default)]
	public Node Location;

	// Token: 0x04001BD9 RID: 7129
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(PositionFilterNode), true, "Filters", PortLocation.Vertical)]
	[ShowPort("UsesMapData")]
	public List<Node> Filters = new List<Node>();

	// Token: 0x04001BDA RID: 7130
	public bool ForceUpdate;

	// Token: 0x04001BDB RID: 7131
	public bool CancelIfValid = true;

	// Token: 0x04001BDC RID: 7132
	public bool UseMapData = true;

	// Token: 0x04001BDD RID: 7133
	private PotentialNavPoint currentTarget;

	// Token: 0x04001BDE RID: 7134
	private int curSeed = 73129;

	// Token: 0x02000657 RID: 1623
	public enum SelectionMode
	{
		// Token: 0x04002B17 RID: 11031
		Nearest,
		// Token: 0x04002B18 RID: 11032
		Random
	}
}
