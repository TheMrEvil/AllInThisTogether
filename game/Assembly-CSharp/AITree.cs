using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200030B RID: 779
[CreateAssetMenu(order = -5)]
public class AITree : GraphTree
{
	// Token: 0x06001B3D RID: 6973 RVA: 0x000A88AD File Offset: 0x000A6AAD
	public override void CreateRootNode()
	{
		this.RootNode = (base.CreateNode(typeof(AIRootNode)) as AIRootNode);
		base.CreateRootNode();
	}

	// Token: 0x06001B3E RID: 6974 RVA: 0x000A88D0 File Offset: 0x000A6AD0
	public void Init()
	{
		foreach (Node node in this.nodes)
		{
			if (!(node is AIRootNode))
			{
				node.RootNode = this.RootNode;
			}
		}
	}

	// Token: 0x06001B3F RID: 6975 RVA: 0x000A8930 File Offset: 0x000A6B30
	public override Dictionary<Type, string> GetContextOptions()
	{
		return new Dictionary<Type, string>
		{
			{
				typeof(AISelectorNode),
				"Logic/Selector"
			},
			{
				typeof(AIRandomNode),
				"Logic/Random Selector"
			},
			{
				typeof(AISequenceNode),
				"Logic/Sequence"
			},
			{
				typeof(AICheckNode),
				"Logic/If -> Then"
			},
			{
				typeof(AIParallelNode),
				"Logic/Do In Parallel"
			},
			{
				typeof(AIInterruptableNode),
				"Logic/Interruptible"
			},
			{
				typeof(AICooldownNode),
				"Logic/Cooldown"
			},
			{
				typeof(AISubtreeNode),
				"Logic/Subtree"
			},
			{
				typeof(AIWaitNode),
				"Actions/Wait"
			},
			{
				typeof(UseAbilityNode),
				"Actions/Use Ability"
			},
			{
				typeof(AICancelAbilityNode),
				"Actions/Release Ability"
			},
			{
				typeof(AISubActionNode),
				"Actions/Run Action"
			},
			{
				typeof(AIRotateNode),
				"Actions/Rotate"
			},
			{
				typeof(AIAddTag),
				"Actions/Add Tag"
			},
			{
				typeof(AIRemoveTag),
				"Actions/Remove Tag"
			},
			{
				typeof(AIStatusNode),
				"Actions/Modify Status"
			},
			{
				typeof(AIChangeBrainNode),
				"Actions/Change Brain"
			},
			{
				typeof(AITransformNode),
				"Actions/Transform"
			},
			{
				typeof(SelectTargetNode),
				"Targeting/Select Target"
			},
			{
				typeof(AIDropTargetNode),
				"Targeting/Drop Target"
			},
			{
				typeof(MoveToPositionNode),
				"Movement/Move to Point"
			},
			{
				typeof(CancelMovementNode),
				"Movement/Stop Moving"
			},
			{
				typeof(PositionFilter_Distance),
				"Movement/Filters/Distance"
			},
			{
				typeof(PositionFilter_TargetVisible),
				"Movement/Filters/Target Visible"
			},
			{
				typeof(PositionFilter_SpecificPoint),
				"Movement/Filters/Specific Point"
			},
			{
				typeof(PositionSort_Distance),
				"Movement/Sort/Distance"
			},
			{
				typeof(PositionSort_Random),
				"Movement/Sort/Random Point"
			},
			{
				typeof(AIEventNode),
				"Event Reaction"
			}
		};
	}

	// Token: 0x06001B40 RID: 6976 RVA: 0x000A8B8E File Offset: 0x000A6D8E
	public override string GetGraphUXML()
	{
		return "Assets/GraphSystem/Styles/ActionTreeEditor.uss";
	}

	// Token: 0x06001B41 RID: 6977 RVA: 0x000A8B95 File Offset: 0x000A6D95
	public override string GetNodeUXML()
	{
		return "Assets/GraphSystem/Styles/NodeViewEditor.uxml";
	}

	// Token: 0x06001B42 RID: 6978 RVA: 0x000A8B9C File Offset: 0x000A6D9C
	public void ResetVisualState()
	{
		foreach (Node node in this.nodes)
		{
			node.EditorStateDisplay = NodeState._;
		}
	}

	// Token: 0x06001B43 RID: 6979 RVA: 0x000A8BF0 File Offset: 0x000A6DF0
	public AITree()
	{
	}
}
