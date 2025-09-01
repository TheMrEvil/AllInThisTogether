using System;
using UnityEngine;

// Token: 0x02000274 RID: 628
public class Logic_EffectProps : LogicNode
{
	// Token: 0x060018E2 RID: 6370 RVA: 0x0009B470 File Offset: 0x00099670
	public override bool Evaluate(EffectProperties props)
	{
		if (props == null)
		{
			this.EditorStateDisplay = NodeState.Fail;
			return false;
		}
		bool result;
		switch (this.Test)
		{
		case Logic_EffectProps.PropDataType.ActionSource:
			result = (props.SourceType == this.Source);
			break;
		case Logic_EffectProps.PropDataType.AbilityType:
			result = (props.AbilityType == this.AbilityType);
			break;
		case Logic_EffectProps.PropDataType.Carrier:
			result = (props.EffectSource == this.Carrier);
			break;
		case Logic_EffectProps.PropDataType.Keyword:
			result = (props.Keyword == this.Key);
			break;
		case Logic_EffectProps.PropDataType.ManaUsed:
			result = props.ManaConsumed.ContainsKey(this.manaMagicColor);
			break;
		case Logic_EffectProps.PropDataType.IsLocal:
			result = props.IsLocal;
			break;
		case Logic_EffectProps.PropDataType.IsWorld:
			result = props.IsWorld;
			break;
		case Logic_EffectProps.PropDataType.HasEffectAugment:
		{
			Augments effectAugments = props.EffectAugments;
			result = (effectAugments != null && effectAugments.TreeIDs.Contains(this.effectAugment.ID));
			break;
		}
		case Logic_EffectProps.PropDataType.HasData:
			result = props.HasEffectProp(this.Prop);
			break;
		case Logic_EffectProps.PropDataType.IDString:
			result = (props.InputID == this.UID);
			break;
		default:
			result = false;
			break;
		}
		return result;
	}

	// Token: 0x060018E3 RID: 6371 RVA: 0x0009B581 File Offset: 0x00099781
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Effect Test";
		inspectorProps.MinInspectorSize = new Vector2(160f, 0f);
		inspectorProps.MaxInspectorSize = new Vector2(160f, 0f);
		return inspectorProps;
	}

	// Token: 0x060018E4 RID: 6372 RVA: 0x0009B5BE File Offset: 0x000997BE
	public Logic_EffectProps()
	{
	}

	// Token: 0x040018BE RID: 6334
	public Logic_EffectProps.PropDataType Test;

	// Token: 0x040018BF RID: 6335
	public ActionSource Source;

	// Token: 0x040018C0 RID: 6336
	public PlayerAbilityType AbilityType;

	// Token: 0x040018C1 RID: 6337
	public EffectSource Carrier;

	// Token: 0x040018C2 RID: 6338
	public Keyword Key;

	// Token: 0x040018C3 RID: 6339
	public MagicColor manaMagicColor;

	// Token: 0x040018C4 RID: 6340
	public AugmentTree effectAugment;

	// Token: 0x040018C5 RID: 6341
	public EProp Prop;

	// Token: 0x040018C6 RID: 6342
	public string UID;

	// Token: 0x0200062F RID: 1583
	public enum PropDataType
	{
		// Token: 0x04002A31 RID: 10801
		ActionSource,
		// Token: 0x04002A32 RID: 10802
		AbilityType,
		// Token: 0x04002A33 RID: 10803
		Carrier,
		// Token: 0x04002A34 RID: 10804
		Keyword,
		// Token: 0x04002A35 RID: 10805
		ManaUsed,
		// Token: 0x04002A36 RID: 10806
		IsLocal,
		// Token: 0x04002A37 RID: 10807
		IsWorld,
		// Token: 0x04002A38 RID: 10808
		HasEffectAugment,
		// Token: 0x04002A39 RID: 10809
		HasData,
		// Token: 0x04002A3A RID: 10810
		IDString
	}
}
