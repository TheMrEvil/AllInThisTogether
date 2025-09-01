using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020002B3 RID: 691
public class AddAugmentEffectNode : EffectNode
{
	// Token: 0x060019CE RID: 6606 RVA: 0x000A09F8 File Offset: 0x0009EBF8
	internal override void Apply(EffectProperties properties)
	{
		EntityControl applicationEntity = properties.GetApplicationEntity(this.ApplyTo);
		if (applicationEntity == null)
		{
			return;
		}
		if (!this.ShouldApply(properties, applicationEntity))
		{
			return;
		}
		switch (this.AugEvent)
		{
		case AddAugmentEffectNode.Event.Add:
			applicationEntity.AddAugment(this.Augment, 1);
			if (applicationEntity == PlayerControl.myInstance)
			{
				GameHUD.instance.GotRewardAugment(this.Augment);
			}
			break;
		case AddAugmentEffectNode.Event.Remove:
		{
			EntityControl entityControl = applicationEntity;
			AugmentTree augment = this.Augment;
			if (entityControl.HasAugment((augment != null) ? augment.ID : null, true))
			{
				applicationEntity.RemoveAugment(this.Augment, 1);
			}
			break;
		}
		case AddAugmentEffectNode.Event.RemoveRandom:
			this.RemoveRandomAugment(applicationEntity);
			break;
		case AddAugmentEffectNode.Event.PageChoice:
			AugmentsPanel.AwardUpgradeChoice(this.Filter);
			break;
		}
		base.Completed();
	}

	// Token: 0x060019CF RID: 6607 RVA: 0x000A0AB8 File Offset: 0x0009ECB8
	private void RemoveRandomAugment(EntityControl target)
	{
		if (target.Augment.TreeIDs.Count <= 0)
		{
			return;
		}
		List<string> list = target.Augment.TreeIDs.ToList<string>();
		AugmentTree augment = GraphDB.GetAugment(list[UnityEngine.Random.Range(0, list.Count)]);
		target.RemoveAugment(augment, 1);
	}

	// Token: 0x060019D0 RID: 6608 RVA: 0x000A0B0A File Offset: 0x0009ED0A
	internal override bool ShouldApply(EffectProperties props, EntityControl applyTo)
	{
		return (props.IsLocal && !(applyTo is PlayerControl)) || applyTo == PlayerControl.myInstance;
	}

	// Token: 0x060019D1 RID: 6609 RVA: 0x000A0B2E File Offset: 0x0009ED2E
	private bool NeedsAugment()
	{
		return this.AugEvent == AddAugmentEffectNode.Event.Add || this.AugEvent == AddAugmentEffectNode.Event.Remove;
	}

	// Token: 0x060019D2 RID: 6610 RVA: 0x000A0B43 File Offset: 0x0009ED43
	private void NewAugmentGraph()
	{
		GraphTree editorTreeRef = base.EditorTreeRef;
		this.Augment = (AugmentTree.CreateAndOpenTree(((editorTreeRef != null) ? editorTreeRef.name : null) ?? "") as AugmentTree);
	}

	// Token: 0x060019D3 RID: 6611 RVA: 0x000A0B70 File Offset: 0x0009ED70
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Augment",
			MinInspectorSize = new Vector2(220f, 0f)
		};
	}

	// Token: 0x060019D4 RID: 6612 RVA: 0x000A0B97 File Offset: 0x0009ED97
	public AddAugmentEffectNode()
	{
	}

	// Token: 0x04001A2E RID: 6702
	public AddAugmentEffectNode.Event AugEvent;

	// Token: 0x04001A2F RID: 6703
	public ApplyOn ApplyTo = ApplyOn.Affected;

	// Token: 0x04001A30 RID: 6704
	public AugmentTree Augment;

	// Token: 0x04001A31 RID: 6705
	public AugmentFilter Filter;

	// Token: 0x04001A32 RID: 6706
	public bool AllowDuplicates;

	// Token: 0x02000645 RID: 1605
	public enum Event
	{
		// Token: 0x04002AC2 RID: 10946
		Add,
		// Token: 0x04002AC3 RID: 10947
		Remove,
		// Token: 0x04002AC4 RID: 10948
		RemoveRandom,
		// Token: 0x04002AC5 RID: 10949
		PageChoice
	}
}
