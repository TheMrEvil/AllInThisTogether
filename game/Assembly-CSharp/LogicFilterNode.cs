using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200026A RID: 618
public class LogicFilterNode : Node
{
	// Token: 0x17000181 RID: 385
	// (get) Token: 0x060018C3 RID: 6339 RVA: 0x0009AE44 File Offset: 0x00099044
	internal override bool CanSkipClone
	{
		get
		{
			return true;
		}
	}

	// Token: 0x060018C4 RID: 6340 RVA: 0x0009AE47 File Offset: 0x00099047
	public virtual void Filter(ref List<EffectProperties> propList, EffectProperties props)
	{
	}

	// Token: 0x060018C5 RID: 6341 RVA: 0x0009AE49 File Offset: 0x00099049
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			ShowInputNode = false
		};
	}

	// Token: 0x060018C6 RID: 6342 RVA: 0x0009AE57 File Offset: 0x00099057
	public LogicFilterNode()
	{
	}

	// Token: 0x040018AA RID: 6314
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(Node), true, "", PortLocation.Vertical)]
	public Node Outputs;
}
