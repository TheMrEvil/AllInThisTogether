using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000342 RID: 834
public class AugmentAwardOverrideNode : ModOverrideNode
{
	// Token: 0x06001C44 RID: 7236 RVA: 0x000ACCB2 File Offset: 0x000AAEB2
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Augment Reward Options",
			MinInspectorSize = new Vector2(300f, 0f)
		};
	}

	// Token: 0x06001C45 RID: 7237 RVA: 0x000ACCD9 File Offset: 0x000AAED9
	internal override bool CanScope()
	{
		return false;
	}

	// Token: 0x06001C46 RID: 7238 RVA: 0x000ACCDC File Offset: 0x000AAEDC
	public override Node Clone(Dictionary<string, Node> alreadyCloned = null, bool fullClone = false)
	{
		AugmentAwardOverrideNode augmentAwardOverrideNode = base.Clone(alreadyCloned, fullClone) as AugmentAwardOverrideNode;
		augmentAwardOverrideNode.Filter = this.Filter.Copy();
		return augmentAwardOverrideNode;
	}

	// Token: 0x06001C47 RID: 7239 RVA: 0x000ACCFC File Offset: 0x000AAEFC
	public override void OnCloned()
	{
		this.Filter = this.Filter.Copy();
	}

	// Token: 0x06001C48 RID: 7240 RVA: 0x000ACD0F File Offset: 0x000AAF0F
	public AugmentAwardOverrideNode()
	{
	}

	// Token: 0x04001CD4 RID: 7380
	public AugmentFilter Filter;
}
