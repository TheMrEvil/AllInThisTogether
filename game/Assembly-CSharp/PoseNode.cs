using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x0200038B RID: 907
public class PoseNode : Node
{
	// Token: 0x170001A7 RID: 423
	// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x000B4006 File Offset: 0x000B2206
	internal override bool CanSkipClone
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06001DA2 RID: 7586 RVA: 0x000B400C File Offset: 0x000B220C
	public global::Pose GetPose(EffectProperties props)
	{
		if (this.Point == null)
		{
			Debug.LogError("Location not set up for Pose");
			return null;
		}
		Location location = (this.Point as LocationNode).GetLocation(props);
		Location f = (this.LookAt != null) ? (this.LookAt as LocationNode).GetLocation(props) : Location.WorldUp();
		return new global::Pose(location, f);
	}

	// Token: 0x06001DA3 RID: 7587 RVA: 0x000B4074 File Offset: 0x000B2274
	[return: TupleElementNames(new string[]
	{
		"point",
		"lookAt"
	})]
	public ValueTuple<Vector3, Vector3> GetVectors(EffectProperties props)
	{
		if (this.Point == null)
		{
			Debug.LogError("Location not set up for Pose");
			return new ValueTuple<Vector3, Vector3>(Vector3.zero, Vector3.up);
		}
		Vector3 point = (this.Point as LocationNode).GetPoint(props);
		Vector3 vector = Vector3.up;
		if (this.LookAt != null)
		{
			LocationNode locationNode = this.LookAt as LocationNode;
			if (locationNode != null)
			{
				vector = locationNode.GetPoint(props);
				vector = (vector - point).normalized;
			}
		}
		return new ValueTuple<Vector3, Vector3>(point, vector);
	}

	// Token: 0x06001DA4 RID: 7588 RVA: 0x000B40FD File Offset: 0x000B22FD
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			ShowInspectorView = true,
			Title = "Pose",
			AllowMultipleInputs = true
		};
	}

	// Token: 0x06001DA5 RID: 7589 RVA: 0x000B411D File Offset: 0x000B231D
	public PoseNode()
	{
	}

	// Token: 0x04001E52 RID: 7762
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Point", PortLocation.Header)]
	public Node Point;

	// Token: 0x04001E53 RID: 7763
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Look At", PortLocation.Default)]
	public Node LookAt;

	// Token: 0x04001E54 RID: 7764
	public bool IsDynamic;
}
