using System;
using UnityEngine;

// Token: 0x02000284 RID: 644
public class Logic_Not : LogicNode
{
	// Token: 0x06001908 RID: 6408 RVA: 0x0009C274 File Offset: 0x0009A474
	public override bool Evaluate(EffectProperties props)
	{
		if (!(this.Test == null))
		{
			LogicNode logicNode = this.Test as LogicNode;
			if (logicNode != null)
			{
				bool flag = logicNode.Evaluate(props);
				this.EditorStateDisplay = ((!flag) ? NodeState.Success : NodeState.Fail);
				return !flag;
			}
		}
		return false;
	}

	// Token: 0x06001909 RID: 6409 RVA: 0x0009C2B9 File Offset: 0x0009A4B9
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "NOT";
		inspectorProps.MinInspectorSize = new Vector2(100f, 0f);
		inspectorProps.ShowInspectorView = false;
		return inspectorProps;
	}

	// Token: 0x0600190A RID: 6410 RVA: 0x0009C2E8 File Offset: 0x0009A4E8
	public Logic_Not()
	{
	}

	// Token: 0x0400191D RID: 6429
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(LogicNode), false, "", PortLocation.Vertical)]
	public Node Test;
}
