using System;
using UnityEngine;

// Token: 0x02000311 RID: 785
public class AIStatusNode : AIActionNode
{
	// Token: 0x06001B55 RID: 6997 RVA: 0x000A8EBC File Offset: 0x000A70BC
	internal override AILogicState Run(AIControl entity)
	{
		if (this.Status == null)
		{
			return AILogicState.Fail;
		}
		float num = (float)this.Duration;
		int stacks = this.Stacks;
		if (this.Type == AIStatusNode.Affect.Add)
		{
			entity.net.ApplyStatus(this.Status.HashCode, entity.view.ViewID, (num > 0f) ? num : -2f, stacks, this.FrameDelay, 0);
		}
		else
		{
			entity.net.RemoveStatus(this.Status.HashCode, entity.view.ViewID, stacks, this.FrameDelay, true);
		}
		return AILogicState.Success;
	}

	// Token: 0x06001B56 RID: 6998 RVA: 0x000A8F55 File Offset: 0x000A7155
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Status",
			MinInspectorSize = new Vector2(150f, 0f)
		};
	}

	// Token: 0x06001B57 RID: 6999 RVA: 0x000A8F7C File Offset: 0x000A717C
	private void NewStatusGraph()
	{
		GraphTree editorTreeRef = base.EditorTreeRef;
		this.Status = (StatusTree.CreateAndOpenTree(((editorTreeRef != null) ? editorTreeRef.name : null) ?? "") as StatusTree);
	}

	// Token: 0x06001B58 RID: 7000 RVA: 0x000A8FA9 File Offset: 0x000A71A9
	public AIStatusNode()
	{
	}

	// Token: 0x04001BCB RID: 7115
	public StatusTree Status;

	// Token: 0x04001BCC RID: 7116
	public AIStatusNode.Affect Type;

	// Token: 0x04001BCD RID: 7117
	public int Duration;

	// Token: 0x04001BCE RID: 7118
	public int Stacks = 1;

	// Token: 0x04001BCF RID: 7119
	public bool FrameDelay;

	// Token: 0x02000656 RID: 1622
	public enum Affect
	{
		// Token: 0x04002B14 RID: 11028
		Add,
		// Token: 0x04002B15 RID: 11029
		Remove
	}
}
