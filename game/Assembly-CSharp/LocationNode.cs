using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000371 RID: 881
public class LocationNode : Node
{
	// Token: 0x170001A2 RID: 418
	// (get) Token: 0x06001D35 RID: 7477 RVA: 0x000B1950 File Offset: 0x000AFB50
	internal override bool CanSkipClone
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06001D36 RID: 7478 RVA: 0x000B1954 File Offset: 0x000AFB54
	public virtual Location GetLocation(EffectProperties props)
	{
		Location location = this.Loc.Copy();
		if (location.LocType == LocationType.Maintain_WorldPt)
		{
			location = Location.AtWorldPoint(props.GetOrigin());
		}
		foreach (Node node in this.LocOffsets)
		{
			LocationOffsetNode locationOffsetNode = (LocationOffsetNode)node;
			location.Offsets.Add(locationOffsetNode.GetOffset(props));
		}
		return location;
	}

	// Token: 0x06001D37 RID: 7479 RVA: 0x000B19DC File Offset: 0x000AFBDC
	public Vector3 GetPoint(EffectProperties props)
	{
		if (this.Loc.LocType == LocationType.Maintain_WorldPt)
		{
			return props.GetOrigin();
		}
		Vector3 position = this.Loc.GetPosition(props);
		Transform transform = this.Loc.GetTransform(props);
		foreach (Node node in this.LocOffsets)
		{
			((LocationOffsetNode)node).ApplyOffset(ref position, props, transform);
		}
		return position;
	}

	// Token: 0x06001D38 RID: 7480 RVA: 0x000B1A68 File Offset: 0x000AFC68
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = new Node.InspectorProps();
		inspectorProps.MinInspectorSize.x = 200f;
		inspectorProps.Title = "Location";
		inspectorProps.AllowMultipleInputs = true;
		return inspectorProps;
	}

	// Token: 0x06001D39 RID: 7481 RVA: 0x000B1A91 File Offset: 0x000AFC91
	public override Node Clone(Dictionary<string, Node> alreadyCloned = null, bool fullClone = false)
	{
		LocationNode locationNode = base.Clone(alreadyCloned, fullClone) as LocationNode;
		locationNode.Loc = this.Loc.Copy();
		return locationNode;
	}

	// Token: 0x06001D3A RID: 7482 RVA: 0x000B1AB1 File Offset: 0x000AFCB1
	public override void OnCloned()
	{
		this.Loc = this.Loc.Copy();
	}

	// Token: 0x06001D3B RID: 7483 RVA: 0x000B1AC4 File Offset: 0x000AFCC4
	public virtual bool SholdShowLocation()
	{
		return true;
	}

	// Token: 0x06001D3C RID: 7484 RVA: 0x000B1AC7 File Offset: 0x000AFCC7
	public LocationNode()
	{
	}

	// Token: 0x04001DE1 RID: 7649
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationOffsetNode), true, "Loc Offsets", PortLocation.Default)]
	[ShowPort("SholdShowLocation")]
	public List<Node> LocOffsets = new List<Node>();

	// Token: 0x04001DE2 RID: 7650
	public Location Loc;
}
