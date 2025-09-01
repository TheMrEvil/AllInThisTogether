using System;
using UnityEngine;

// Token: 0x02000312 RID: 786
public class AISubActionNode : AIActionNode
{
	// Token: 0x06001B59 RID: 7001 RVA: 0x000A8FB8 File Offset: 0x000A71B8
	internal override AILogicState Run(AIControl entity)
	{
		if (this.Action == null)
		{
			return AILogicState.Fail;
		}
		EffectProperties effectProperties = new EffectProperties(entity);
		effectProperties.SourceType = ActionSource.Snippet;
		if (this.Loc != null)
		{
			PoseNode poseNode = this.Loc as PoseNode;
			if (poseNode != null)
			{
				global::Pose pose = poseNode.GetPose(effectProperties);
				effectProperties.StartLoc = (effectProperties.OutLoc = pose);
			}
		}
		float val = this.Input;
		if (this.Value != null)
		{
			NumberNode numberNode = this.Value as NumberNode;
			if (numberNode != null)
			{
				val = numberNode.Evaluate(effectProperties);
			}
		}
		effectProperties.SetExtra(EProp.Snip_Input, val);
		effectProperties.Depth++;
		entity.net.ExecuteActionTree(this.Action.RootNode.guid, effectProperties);
		return AILogicState.Success;
	}

	// Token: 0x06001B5A RID: 7002 RVA: 0x000A907F File Offset: 0x000A727F
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Run Action",
			MinInspectorSize = new Vector2(125f, 0f)
		};
	}

	// Token: 0x06001B5B RID: 7003 RVA: 0x000A90A6 File Offset: 0x000A72A6
	private void NewActionGraph()
	{
		GraphTree editorTreeRef = base.EditorTreeRef;
		this.Action = (ActionTree.CreateAndOpenTree(((editorTreeRef != null) ? editorTreeRef.name : null) ?? "") as ActionTree);
	}

	// Token: 0x06001B5C RID: 7004 RVA: 0x000A90D3 File Offset: 0x000A72D3
	public AISubActionNode()
	{
	}

	// Token: 0x04001BD0 RID: 7120
	public ActionTree Action;

	// Token: 0x04001BD1 RID: 7121
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(PoseNode), false, "Pose", PortLocation.Header)]
	public Node Loc;

	// Token: 0x04001BD2 RID: 7122
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Dynamic Input", PortLocation.Default)]
	public Node Value;

	// Token: 0x04001BD3 RID: 7123
	public float Input;
}
