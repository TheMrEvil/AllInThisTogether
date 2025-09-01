using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000308 RID: 776
public class AITest_And : AITestNode
{
	// Token: 0x06001B34 RID: 6964 RVA: 0x000A86C0 File Offset: 0x000A68C0
	public override bool Evaluate(EntityControl entity)
	{
		bool flag = true;
		foreach (Node node in this.Tests)
		{
			AITestNode aitestNode = (AITestNode)node;
			bool flag2 = aitestNode.Evaluate(entity);
			aitestNode.EditorStateDisplay = (flag2 ? NodeState.Success : NodeState.Fail);
			flag = (flag && flag2);
		}
		return flag;
	}

	// Token: 0x06001B35 RID: 6965 RVA: 0x000A872C File Offset: 0x000A692C
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "AND";
		inspectorProps.MinInspectorSize = new Vector2(100f, 0f);
		inspectorProps.ShowInspectorView = false;
		inspectorProps.SortX = true;
		return inspectorProps;
	}

	// Token: 0x06001B36 RID: 6966 RVA: 0x000A8762 File Offset: 0x000A6962
	public AITest_And()
	{
	}

	// Token: 0x04001BBE RID: 7102
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(AITestNode), true, "", PortLocation.Vertical)]
	public List<Node> Tests = new List<Node>();
}
