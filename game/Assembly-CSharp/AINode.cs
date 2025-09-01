using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020002FF RID: 767
public class AINode : Node
{
	// Token: 0x17000193 RID: 403
	// (get) Token: 0x06001B0B RID: 6923 RVA: 0x000A7B54 File Offset: 0x000A5D54
	public AIRootNode AIRoot
	{
		get
		{
			if (this.RootNode == null)
			{
				return null;
			}
			return this.RootNode as AIRootNode;
		}
	}

	// Token: 0x06001B0C RID: 6924 RVA: 0x000A7B71 File Offset: 0x000A5D71
	internal virtual AILogicState Run(AIControl entity)
	{
		string str = "AINode ";
		Type type = base.GetType();
		Debug.LogError(str + ((type != null) ? type.ToString() : null) + " Run() Not implemented!");
		return AILogicState.Fail;
	}

	// Token: 0x06001B0D RID: 6925 RVA: 0x000A7B9C File Offset: 0x000A5D9C
	public AILogicState DoUpdate(AIControl entity)
	{
		if (!this.started)
		{
			this.CurrentState = AILogicState.Running;
			this.started = true;
		}
		this.CurrentState = this.Run(entity);
		if (this.CurrentState == AILogicState.Fail || this.CurrentState == AILogicState.Success)
		{
			this.started = false;
		}
		this.EditorStateDisplay = (NodeState)(this.CurrentState + 1);
		this.visualizeState = this.EditorStateDisplay;
		this.lastState = this.CurrentState;
		return this.CurrentState;
	}

	// Token: 0x06001B0E RID: 6926 RVA: 0x000A7C10 File Offset: 0x000A5E10
	public override void OnCloned()
	{
		this.started = false;
	}

	// Token: 0x06001B0F RID: 6927 RVA: 0x000A7C1C File Offset: 0x000A5E1C
	public virtual void ClearState(List<Node> wasChecked = null)
	{
		if (wasChecked == null)
		{
			wasChecked = new List<Node>();
		}
		if (wasChecked.Contains(this))
		{
			return;
		}
		wasChecked.Add(this);
		this.started = false;
		foreach (FieldInfo fieldInfo in base.GetType().GetFields())
		{
			if (fieldInfo.FieldType == typeof(Node))
			{
				Node node = fieldInfo.GetValue(this) as Node;
				if (node is AINode)
				{
					(node as AINode).ClearState(wasChecked);
				}
			}
			else if (fieldInfo.FieldType == typeof(List<Node>))
			{
				foreach (Node node2 in (fieldInfo.GetValue(this) as List<Node>))
				{
					if (node2 is AINode)
					{
						(node2 as AINode).ClearState(wasChecked);
					}
				}
			}
		}
	}

	// Token: 0x06001B10 RID: 6928 RVA: 0x000A7D20 File Offset: 0x000A5F20
	public override bool InProgress()
	{
		return this.started && this.lastState == AILogicState.Running;
	}

	// Token: 0x06001B11 RID: 6929 RVA: 0x000A7D35 File Offset: 0x000A5F35
	public virtual List<AbilityTree> CollectActions(List<AbilityTree> actions, List<Node> wasChecked = null)
	{
		return actions;
	}

	// Token: 0x06001B12 RID: 6930 RVA: 0x000A7D38 File Offset: 0x000A5F38
	public AINode()
	{
	}

	// Token: 0x04001BA6 RID: 7078
	[NonSerialized]
	public AILogicState CurrentState;

	// Token: 0x04001BA7 RID: 7079
	[NonSerialized]
	public bool started;

	// Token: 0x04001BA8 RID: 7080
	private NodeState visualizeState;

	// Token: 0x04001BA9 RID: 7081
	private AILogicState lastState;
}
