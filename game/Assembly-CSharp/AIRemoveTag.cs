using System;

// Token: 0x0200030F RID: 783
public class AIRemoveTag : AIActionNode
{
	// Token: 0x06001B4F RID: 6991 RVA: 0x000A8DB5 File Offset: 0x000A6FB5
	internal override AILogicState Run(AIControl entity)
	{
		entity.RemoveTag(this.Tag);
		return AILogicState.Success;
	}

	// Token: 0x06001B50 RID: 6992 RVA: 0x000A8DC4 File Offset: 0x000A6FC4
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Remove Tag"
		};
	}

	// Token: 0x06001B51 RID: 6993 RVA: 0x000A8DD6 File Offset: 0x000A6FD6
	public AIRemoveTag()
	{
	}

	// Token: 0x04001BC7 RID: 7111
	public string Tag;
}
