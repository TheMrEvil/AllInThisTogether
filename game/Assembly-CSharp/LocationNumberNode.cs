using System;
using UnityEngine;

// Token: 0x02000380 RID: 896
public class LocationNumberNode : NumberNode
{
	// Token: 0x06001D79 RID: 7545 RVA: 0x000B34FC File Offset: 0x000B16FC
	public override float Evaluate(EffectProperties props)
	{
		Vector3 vector = Vector3.back.INVALID();
		if (this.Loc != null)
		{
			LocationNode locationNode = this.Loc as LocationNode;
			if (locationNode != null)
			{
				vector = locationNode.GetLocation(props).GetPosition(props);
			}
		}
		Vector3 vector2 = Vector3.back.INVALID();
		if (this.Loc2 != null)
		{
			LocationNode locationNode2 = this.Loc2 as LocationNode;
			if (locationNode2 != null)
			{
				vector2 = locationNode2.GetLocation(props).GetPosition(props);
			}
		}
		if (!vector.IsValid())
		{
			return float.NaN;
		}
		if (this.ShowSecondLocation() && !vector2.IsValid())
		{
			return float.NaN;
		}
		switch (this.Stat)
		{
		case LocationNumberNode.LocationNumStat.Distance:
			return Vector3.Distance(vector, vector2);
		case LocationNumberNode.LocationNumStat.LineOfSight:
			return (float)(this.ArePointsLOS(vector, vector2) ? 1 : 0);
		case LocationNumberNode.LocationNumStat.HeightDifference:
			return vector.y - vector2.y;
		default:
			return float.NaN;
		}
	}

	// Token: 0x06001D7A RID: 7546 RVA: 0x000B35E1 File Offset: 0x000B17E1
	private bool ArePointsLOS(Vector3 p1, Vector3 p2)
	{
		Vector3.Distance(p1, p2);
		return !Physics.Linecast(p1, p2, this.Mask);
	}

	// Token: 0x06001D7B RID: 7547 RVA: 0x000B3600 File Offset: 0x000B1800
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Location Num",
			MinInspectorSize = new Vector2(150f, 0f),
			MaxInspectorSize = new Vector2(150f, 0f),
			AllowMultipleInputs = true
		};
	}

	// Token: 0x06001D7C RID: 7548 RVA: 0x000B3650 File Offset: 0x000B1850
	public bool ShowSecondLocation()
	{
		bool result;
		switch (this.Stat)
		{
		case LocationNumberNode.LocationNumStat.Distance:
			result = true;
			break;
		case LocationNumberNode.LocationNumStat.LineOfSight:
			result = true;
			break;
		case LocationNumberNode.LocationNumStat.HeightDifference:
			result = true;
			break;
		default:
			result = false;
			break;
		}
		return result;
	}

	// Token: 0x06001D7D RID: 7549 RVA: 0x000B3687 File Offset: 0x000B1887
	public LocationNumberNode()
	{
	}

	// Token: 0x04001E2C RID: 7724
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Location", PortLocation.Default)]
	public Node Loc;

	// Token: 0x04001E2D RID: 7725
	[HideInInspector]
	[ShowPort("ShowSecondLocation")]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Location 2", PortLocation.Default)]
	public Node Loc2;

	// Token: 0x04001E2E RID: 7726
	public LocationNumberNode.LocationNumStat Stat;

	// Token: 0x04001E2F RID: 7727
	public LayerMask Mask = 129;

	// Token: 0x02000683 RID: 1667
	public enum LocationNumStat
	{
		// Token: 0x04002BD6 RID: 11222
		Distance,
		// Token: 0x04002BD7 RID: 11223
		LineOfSight,
		// Token: 0x04002BD8 RID: 11224
		HeightDifference
	}
}
