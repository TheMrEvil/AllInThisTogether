using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000349 RID: 841
public class StatusOverrideNode : ModOverrideNode
{
	// Token: 0x06001C60 RID: 7264 RVA: 0x000AD748 File Offset: 0x000AB948
	internal override bool CanScope()
	{
		return false;
	}

	// Token: 0x06001C61 RID: 7265 RVA: 0x000AD74C File Offset: 0x000AB94C
	public override void OverrideNodeEffects(EffectProperties props, Node node, ref List<ModOverrideNode> overrides)
	{
		if (!(node is StatusRootNode))
		{
			return;
		}
		if (this.Status == null)
		{
			return;
		}
		if ((node as StatusRootNode).guid != this.Status.RootNode.guid)
		{
			return;
		}
		overrides.Add(this);
	}

	// Token: 0x06001C62 RID: 7266 RVA: 0x000AD79C File Offset: 0x000AB99C
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Status Override",
			MinInspectorSize = new Vector2(300f, 0f)
		};
	}

	// Token: 0x06001C63 RID: 7267 RVA: 0x000AD7C4 File Offset: 0x000AB9C4
	public StatusOverrideNode()
	{
	}

	// Token: 0x04001D33 RID: 7475
	public StatusTree Status;

	// Token: 0x04001D34 RID: 7476
	public bool OverrideStacks;

	// Token: 0x04001D35 RID: 7477
	public bool CanStack;

	// Token: 0x04001D36 RID: 7478
	public int MaxStacks = 1;

	// Token: 0x04001D37 RID: 7479
	public bool AdditiveStacks;

	// Token: 0x04001D38 RID: 7480
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Apply", PortLocation.Default)]
	public List<Node> OnApply = new List<Node>();

	// Token: 0x04001D39 RID: 7481
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Stack Changed", PortLocation.Default)]
	public List<Node> OnStackChanged = new List<Node>();

	// Token: 0x04001D3A RID: 7482
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Tick", PortLocation.Default)]
	public List<Node> OnTick = new List<Node>();

	// Token: 0x04001D3B RID: 7483
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Expire", PortLocation.Default)]
	public List<Node> OnExpire = new List<Node>();

	// Token: 0x04001D3C RID: 7484
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(EffectNode), true, "On Died With", PortLocation.Default)]
	public List<Node> OnDiedWith = new List<Node>();

	// Token: 0x04001D3D RID: 7485
	public List<StatusKeyword> AddKeywords;

	// Token: 0x04001D3E RID: 7486
	public List<StatusKeyword> RemoveKeywords;

	// Token: 0x04001D3F RID: 7487
	public List<StatusRootNode.StatusAugment> AddAugments;
}
