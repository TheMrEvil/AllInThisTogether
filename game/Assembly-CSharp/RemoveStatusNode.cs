using System;
using UnityEngine;

// Token: 0x020002D7 RID: 727
public class RemoveStatusNode : EffectNode
{
	// Token: 0x06001A75 RID: 6773 RVA: 0x000A45FC File Offset: 0x000A27FC
	internal override void Apply(EffectProperties properties)
	{
		EntityControl applicationEntity = properties.GetApplicationEntity(this.ApplyTo);
		if (this.Status == null || applicationEntity == null || (applicationEntity.IsDead && !(this.Status.RootNode as StatusRootNode).PersistThroughDeath))
		{
			return;
		}
		if (!this.ShouldApply(properties, applicationEntity))
		{
			return;
		}
		if (!applicationEntity.HasStatusEffectGUID(this.Status.RootNode.guid))
		{
			return;
		}
		int sourceID = -1;
		if (properties.SourceControl != null)
		{
			sourceID = properties.SourceControl.view.ViewID;
		}
		int num = this.Stacks;
		if (this.Value != null)
		{
			NumberNode numberNode = this.Value as NumberNode;
			if (numberNode != null)
			{
				num = (int)numberNode.Evaluate(properties);
				if (num <= 0)
				{
					return;
				}
			}
		}
		if (this.Status.Root.Batched)
		{
			applicationEntity.net.RemoveStatusBatched(this.Status.HashCode, sourceID, num, this.UseFrameDelay, this.RemoveByAll);
		}
		else
		{
			applicationEntity.net.RemoveStatus(this.Status.HashCode, sourceID, num, this.UseFrameDelay, this.RemoveByAll);
		}
		base.Completed();
	}

	// Token: 0x06001A76 RID: 6774 RVA: 0x000A4726 File Offset: 0x000A2926
	internal override bool ShouldApply(EffectProperties props, EntityControl applyTo)
	{
		return (props.IsLocal && !(applyTo is PlayerControl)) || applyTo == PlayerControl.myInstance;
	}

	// Token: 0x06001A77 RID: 6775 RVA: 0x000A474A File Offset: 0x000A294A
	private void NewStatusGraph()
	{
		GraphTree editorTreeRef = base.EditorTreeRef;
		this.Status = (StatusTree.CreateAndOpenTree(((editorTreeRef != null) ? editorTreeRef.name : null) ?? "") as StatusTree);
	}

	// Token: 0x06001A78 RID: 6776 RVA: 0x000A4777 File Offset: 0x000A2977
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Remove Status",
			MinInspectorSize = new Vector2(260f, 0f)
		};
	}

	// Token: 0x06001A79 RID: 6777 RVA: 0x000A479E File Offset: 0x000A299E
	public RemoveStatusNode()
	{
	}

	// Token: 0x04001AE8 RID: 6888
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Stacks", PortLocation.Default)]
	public Node Value;

	// Token: 0x04001AE9 RID: 6889
	public ApplyOn ApplyTo = ApplyOn.Affected;

	// Token: 0x04001AEA RID: 6890
	public StatusTree Status;

	// Token: 0x04001AEB RID: 6891
	public int Stacks;

	// Token: 0x04001AEC RID: 6892
	public bool RemoveByAll;

	// Token: 0x04001AED RID: 6893
	public bool UseFrameDelay;
}
