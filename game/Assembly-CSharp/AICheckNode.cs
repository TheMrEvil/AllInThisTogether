using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002F8 RID: 760
public class AICheckNode : AILogicNode
{
	// Token: 0x06001AF4 RID: 6900 RVA: 0x000A77B4 File Offset: 0x000A59B4
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Check",
			ShowInspectorView = false,
			MinInspectorSize = new Vector2(140f, 0f)
		};
	}

	// Token: 0x06001AF5 RID: 6901 RVA: 0x000A77E4 File Offset: 0x000A59E4
	internal override AILogicState Run(AIControl entity)
	{
		if (!(this.Then is AIInterruptableNode) && this.InProgress())
		{
			AILogicState ailogicState = (this.Then as AINode).DoUpdate(entity);
			this.EditorStateDisplay = (NodeState)(ailogicState + 1);
			return ailogicState;
		}
		bool flag = true;
		if (this.Test != null)
		{
			LogicNode logicNode = this.Test as LogicNode;
			if (logicNode != null)
			{
				flag = logicNode.Evaluate(new EffectProperties(entity));
				this.Test.EditorStateDisplay = (flag ? NodeState.Success : NodeState.Fail);
			}
		}
		if (!flag)
		{
			return AILogicState.Fail;
		}
		if (!(this.Then != null))
		{
			return AILogicState.Success;
		}
		return (this.Then as AINode).DoUpdate(entity);
	}

	// Token: 0x06001AF6 RID: 6902 RVA: 0x000A7887 File Offset: 0x000A5A87
	public override List<AbilityTree> CollectActions(List<AbilityTree> list, List<Node> wasChecked = null)
	{
		if (wasChecked == null)
		{
			wasChecked = new List<Node>();
		}
		if (wasChecked.Contains(this))
		{
			return list;
		}
		wasChecked.Add(this);
		if (this.Then != null)
		{
			(this.Then as AINode).CollectActions(list, wasChecked);
		}
		return list;
	}

	// Token: 0x06001AF7 RID: 6903 RVA: 0x000A78C7 File Offset: 0x000A5AC7
	public static bool RunNumericTest(float dynamicVal, float testAgainst, NumericTest test)
	{
		switch (test)
		{
		case NumericTest.LessThan:
			return dynamicVal < testAgainst;
		case NumericTest.GreaterThan:
			return dynamicVal > testAgainst;
		case NumericTest.Equals:
			return dynamicVal == testAgainst;
		default:
			return false;
		}
	}

	// Token: 0x06001AF8 RID: 6904 RVA: 0x000A78ED File Offset: 0x000A5AED
	public AICheckNode()
	{
	}

	// Token: 0x04001B97 RID: 7063
	[HideInInspector]
	[SerializeField]
	[InputPort(typeof(LogicNode), false, "If", PortLocation.Vertical)]
	public Node Test;

	// Token: 0x04001B98 RID: 7064
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AINode), false, "Then", PortLocation.Default)]
	public Node Then;

	// Token: 0x02000653 RID: 1619
	public enum TestType
	{
		// Token: 0x04002B09 RID: 11017
		All,
		// Token: 0x04002B0A RID: 11018
		Any,
		// Token: 0x04002B0B RID: 11019
		NotAny,
		// Token: 0x04002B0C RID: 11020
		None
	}
}
