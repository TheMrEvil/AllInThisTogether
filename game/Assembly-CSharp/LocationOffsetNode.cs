using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000372 RID: 882
public class LocationOffsetNode : Node
{
	// Token: 0x06001D3D RID: 7485 RVA: 0x000B1ADA File Offset: 0x000AFCDA
	public virtual LocOffset GetOffset(EffectProperties props)
	{
		if (this.Multiplier.Count == 0)
		{
			return this.Offset;
		}
		LocOffset locOffset = this.Offset.Copy();
		locOffset.Offset = this.GetScaledOffset(props);
		return locOffset;
	}

	// Token: 0x06001D3E RID: 7486 RVA: 0x000B1B08 File Offset: 0x000AFD08
	public void ApplyOffset(ref Vector3 origin, EffectProperties props, Transform t = null)
	{
		this.Offset.ApplyOffset(ref origin, props, t);
	}

	// Token: 0x06001D3F RID: 7487 RVA: 0x000B1B18 File Offset: 0x000AFD18
	private Vector3 GetScaledOffset(EffectProperties props)
	{
		if (this.Multiplier.Count == 1)
		{
			NumberNode numberNode = this.Multiplier[0] as NumberNode;
			if (numberNode != null)
			{
				return this.Offset.Offset * numberNode.Evaluate(props);
			}
		}
		float num = 1f;
		if (this.Multiplier.Count > 0)
		{
			NumberNode numberNode2 = this.Multiplier[0] as NumberNode;
			if (numberNode2 != null)
			{
				num = numberNode2.Evaluate(props);
			}
		}
		float num2 = 1f;
		if (this.Multiplier.Count > 1)
		{
			NumberNode numberNode3 = this.Multiplier[1] as NumberNode;
			if (numberNode3 != null)
			{
				num2 = numberNode3.Evaluate(props);
			}
		}
		float num3 = 1f;
		if (this.Multiplier.Count > 2)
		{
			NumberNode numberNode4 = this.Multiplier[2] as NumberNode;
			if (numberNode4 != null)
			{
				num3 = numberNode4.Evaluate(props);
			}
		}
		if (float.IsNaN(num))
		{
			num = 1f;
		}
		if (float.IsNaN(num2))
		{
			num2 = 1f;
		}
		if (float.IsNaN(num3))
		{
			num3 = 1f;
		}
		float x = this.Offset.Offset.x * num;
		float y = this.Offset.Offset.y * num2;
		float z = this.Offset.Offset.z * num3;
		return new Vector3(x, y, z);
	}

	// Token: 0x06001D40 RID: 7488 RVA: 0x000B1C6B File Offset: 0x000AFE6B
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = new Node.InspectorProps();
		inspectorProps.MinInspectorSize.x = 200f;
		inspectorProps.Title = "Loc Offset";
		inspectorProps.AllowMultipleInputs = true;
		return inspectorProps;
	}

	// Token: 0x06001D41 RID: 7489 RVA: 0x000B1C94 File Offset: 0x000AFE94
	public override Node Clone(Dictionary<string, Node> alreadyCloned = null, bool fullClone = false)
	{
		Node result = base.Clone(alreadyCloned, fullClone) as LocationOffsetNode;
		this.Offset = this.Offset.Copy();
		return result;
	}

	// Token: 0x06001D42 RID: 7490 RVA: 0x000B1CB4 File Offset: 0x000AFEB4
	public override void OnCloned()
	{
		this.Offset = this.Offset.Copy();
	}

	// Token: 0x06001D43 RID: 7491 RVA: 0x000B1CC7 File Offset: 0x000AFEC7
	public virtual bool SholdShowLocation()
	{
		return true;
	}

	// Token: 0x06001D44 RID: 7492 RVA: 0x000B1CCA File Offset: 0x000AFECA
	public LocationOffsetNode()
	{
	}

	// Token: 0x04001DE3 RID: 7651
	[HideInInspector]
	[SerializeField]
	[ShowPort("SholdShowLocation")]
	[OutputPort(typeof(NumberNode), true, "Scalar", PortLocation.Header)]
	public List<Node> Multiplier = new List<Node>();

	// Token: 0x04001DE4 RID: 7652
	public LocOffset Offset;
}
