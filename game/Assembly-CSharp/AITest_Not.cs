using System;
using UnityEngine;

// Token: 0x02000309 RID: 777
public class AITest_Not : AITestNode
{
	// Token: 0x06001B37 RID: 6967 RVA: 0x000A8778 File Offset: 0x000A6978
	public override bool Evaluate(EntityControl entity)
	{
		if (this.Test == null)
		{
			return false;
		}
		bool flag = (this.Test as AITestNode).Evaluate(entity);
		this.Test.EditorStateDisplay = (flag ? NodeState.Success : NodeState.Fail);
		return !flag;
	}

	// Token: 0x06001B38 RID: 6968 RVA: 0x000A87BD File Offset: 0x000A69BD
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "NOT";
		inspectorProps.MinInspectorSize = new Vector2(100f, 0f);
		inspectorProps.ShowInspectorView = false;
		return inspectorProps;
	}

	// Token: 0x06001B39 RID: 6969 RVA: 0x000A87EC File Offset: 0x000A69EC
	public AITest_Not()
	{
	}

	// Token: 0x04001BBF RID: 7103
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(AITestNode), false, "", PortLocation.Vertical)]
	public Node Test;
}
