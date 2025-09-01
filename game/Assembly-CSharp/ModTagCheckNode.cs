using System;
using UnityEngine;

// Token: 0x0200034E RID: 846
public class ModTagCheckNode : ModTagNode
{
	// Token: 0x06001C70 RID: 7280 RVA: 0x000AD9D6 File Offset: 0x000ABBD6
	public override bool Validate(EntityControl control)
	{
		return !(control == null) && control.HasModTag(this.Tag);
	}

	// Token: 0x06001C71 RID: 7281 RVA: 0x000AD9EF File Offset: 0x000ABBEF
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Tag";
		inspectorProps.MinInspectorSize = new Vector2(300f, 0f);
		return inspectorProps;
	}

	// Token: 0x06001C72 RID: 7282 RVA: 0x000ADA17 File Offset: 0x000ABC17
	public ModTagCheckNode()
	{
	}

	// Token: 0x04001D44 RID: 7492
	public ModTag Tag;
}
