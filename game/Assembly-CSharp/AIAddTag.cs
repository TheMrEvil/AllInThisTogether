using System;

// Token: 0x0200030C RID: 780
public class AIAddTag : AIActionNode
{
	// Token: 0x06001B44 RID: 6980 RVA: 0x000A8BF8 File Offset: 0x000A6DF8
	internal override AILogicState Run(AIControl entity)
	{
		entity.AddTag(this.Tag);
		return AILogicState.Success;
	}

	// Token: 0x06001B45 RID: 6981 RVA: 0x000A8C07 File Offset: 0x000A6E07
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Add Tag"
		};
	}

	// Token: 0x06001B46 RID: 6982 RVA: 0x000A8C19 File Offset: 0x000A6E19
	public AIAddTag()
	{
	}

	// Token: 0x04001BC1 RID: 7105
	public string Tag;
}
