using System;
using UnityEngine;

// Token: 0x0200034B RID: 843
public class EnemyModCheckNode : ModTagNode
{
	// Token: 0x06001C67 RID: 7271 RVA: 0x000AD847 File Offset: 0x000ABA47
	public override bool Validate(EntityControl control)
	{
		return AIManager.HasEnemyModTag(this.Tag);
	}

	// Token: 0x06001C68 RID: 7272 RVA: 0x000AD854 File Offset: 0x000ABA54
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Keyword";
		inspectorProps.MinInspectorSize = new Vector2(100f, 0f);
		return inspectorProps;
	}

	// Token: 0x06001C69 RID: 7273 RVA: 0x000AD87C File Offset: 0x000ABA7C
	public EnemyModCheckNode()
	{
	}

	// Token: 0x04001D41 RID: 7489
	public EnemyModTag Tag;
}
