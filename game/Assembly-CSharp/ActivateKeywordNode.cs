using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002B1 RID: 689
public class ActivateKeywordNode : EffectNode
{
	// Token: 0x1700018B RID: 395
	// (get) Token: 0x060019C8 RID: 6600 RVA: 0x000A07BB File Offset: 0x0009E9BB
	internal override bool CanSkipClone
	{
		get
		{
			return true;
		}
	}

	// Token: 0x060019C9 RID: 6601 RVA: 0x000A07C0 File Offset: 0x0009E9C0
	internal override void Apply(EffectProperties properties)
	{
		properties.Keyword = this.Keyword;
		if (this.NeedsTarget())
		{
			EntityControl applicationEntity = properties.GetApplicationEntity(this.ApplyTo);
			properties.Affected = ((applicationEntity != null) ? applicationEntity.gameObject : null);
		}
		if (this.Loc != null)
		{
			PoseNode poseNode = this.Loc as PoseNode;
			if (poseNode != null)
			{
				properties.StartLoc = (properties.OutLoc = poseNode.GetPose(properties));
				properties.SourceLocation = null;
			}
		}
		if (this.Value != null)
		{
			NumberNode numberNode = this.Value as NumberNode;
			if (numberNode != null)
			{
				properties.SetExtra(EProp.DynamicInput, numberNode.Evaluate(properties));
			}
		}
		EffectProperties effectProperties = properties;
		List<ModOverrideNode> overrides = effectProperties.SourceControl.AllAugments(true, null).GetOverrides(effectProperties, this);
		if (overrides.Count > 0)
		{
			effectProperties = properties.Copy(false);
			effectProperties.Increment(EProp.Override_Depth, 1);
		}
		EffectProperties effectProperties2 = properties;
		bool flag = properties.SourceControl != null && properties.SourceControl.CanTriggerSnippets(EventTrigger.KeywordTriggered, true, 1f);
		if (flag)
		{
			effectProperties2 = properties.Copy(false);
		}
		GameDB.KeywordData keywordData = GameDB.Keyword(this.Keyword);
		if (keywordData != null && keywordData.Action != null && keywordData.Action.RootNode != null)
		{
			keywordData.Action.Root.Apply(properties);
		}
		foreach (ModOverrideNode modOverrideNode in overrides)
		{
			foreach (Node node in ((KeywordOverrideNode)modOverrideNode).ExtraEffects)
			{
				((EffectNode)node).Apply(effectProperties.Copy(false));
			}
		}
		if (flag)
		{
			EntityControl sourceControl = effectProperties2.SourceControl;
			if (sourceControl == null)
			{
				return;
			}
			sourceControl.TriggerSnippets(EventTrigger.KeywordTriggered, properties, 1f);
		}
	}

	// Token: 0x060019CA RID: 6602 RVA: 0x000A09BC File Offset: 0x0009EBBC
	public void OpenGraph()
	{
	}

	// Token: 0x060019CB RID: 6603 RVA: 0x000A09BE File Offset: 0x0009EBBE
	private bool NeedsTarget()
	{
		return false;
	}

	// Token: 0x060019CC RID: 6604 RVA: 0x000A09C1 File Offset: 0x0009EBC1
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Keyword",
			MinInspectorSize = new Vector2(220f, 0f)
		};
	}

	// Token: 0x060019CD RID: 6605 RVA: 0x000A09E8 File Offset: 0x0009EBE8
	public ActivateKeywordNode()
	{
	}

	// Token: 0x04001A0B RID: 6667
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Value", PortLocation.Header)]
	public Node Value;

	// Token: 0x04001A0C RID: 6668
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(PoseNode), false, "Pose Override", PortLocation.Default)]
	public Node Loc;

	// Token: 0x04001A0D RID: 6669
	public Keyword Keyword;

	// Token: 0x04001A0E RID: 6670
	public ApplyOn ApplyTo = ApplyOn.Affected;
}
