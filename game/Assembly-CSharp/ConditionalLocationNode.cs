using System;
using UnityEngine;

// Token: 0x02000370 RID: 880
public class ConditionalLocationNode : LocationNode
{
	// Token: 0x06001D31 RID: 7473 RVA: 0x000B18BC File Offset: 0x000AFABC
	public override Location GetLocation(EffectProperties props)
	{
		Node node = (this.Filter == null || (this.Filter as LogicNode).Evaluate(props)) ? this.TrueLoc : this.FalseLoc;
		if (!(node == null))
		{
			LocationNode locationNode = node as LocationNode;
			if (locationNode != null)
			{
				return locationNode.GetLocation(props);
			}
		}
		return Location.WorldUp();
	}

	// Token: 0x06001D32 RID: 7474 RVA: 0x000B191C File Offset: 0x000AFB1C
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = new Node.InspectorProps();
		inspectorProps.MinInspectorSize.x = 205f;
		inspectorProps.Title = "Conditional Loc";
		inspectorProps.AllowMultipleInputs = true;
		return inspectorProps;
	}

	// Token: 0x06001D33 RID: 7475 RVA: 0x000B1945 File Offset: 0x000AFB45
	public override bool SholdShowLocation()
	{
		return false;
	}

	// Token: 0x06001D34 RID: 7476 RVA: 0x000B1948 File Offset: 0x000AFB48
	public ConditionalLocationNode()
	{
	}

	// Token: 0x04001DDE RID: 7646
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(LogicNode), true, "If", PortLocation.Vertical)]
	public Node Filter;

	// Token: 0x04001DDF RID: 7647
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "True", PortLocation.Default)]
	public Node TrueLoc;

	// Token: 0x04001DE0 RID: 7648
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "False", PortLocation.Default)]
	public Node FalseLoc;
}
