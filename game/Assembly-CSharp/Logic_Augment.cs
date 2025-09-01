using System;
using UnityEngine;

// Token: 0x02000272 RID: 626
public class Logic_Augment : LogicNode
{
	// Token: 0x060018DF RID: 6367 RVA: 0x0009B330 File Offset: 0x00099530
	public override bool Evaluate(EffectProperties props)
	{
		if (props == null || this.Graph == null)
		{
			this.EditorStateDisplay = NodeState.Fail;
			return false;
		}
		EntityControl applicationEntity = props.GetApplicationEntity(this.Entity);
		bool flag;
		switch (this.Test)
		{
		case Logic_Augment.AugmentTest.EntityHas:
			flag = (applicationEntity != null && applicationEntity.HasAugment(this.Graph.ID, true));
			break;
		case Logic_Augment.AugmentTest.FountainHas:
			flag = InkManager.PurchasedMods.TreeIDs.Contains(this.Graph.ID);
			break;
		case Logic_Augment.AugmentTest.AITeamHas:
			flag = AIManager.GlobalEnemyMods.TreeIDs.Contains(this.Graph.ID);
			break;
		case Logic_Augment.AugmentTest.AITeamHasKeyword:
			flag = AIManager.HasEnemyModTag(this.Tag);
			break;
		case Logic_Augment.AugmentTest.BindingInUse:
			flag = GameplayManager.instance.GenreBindings.TreeIDs.Contains(this.Graph.ID);
			break;
		default:
			flag = false;
			break;
		}
		bool flag2 = flag;
		this.EditorStateDisplay = (flag2 ? NodeState.Success : NodeState.Fail);
		return flag2;
	}

	// Token: 0x060018E0 RID: 6368 RVA: 0x0009B429 File Offset: 0x00099629
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Augment Test";
		inspectorProps.MinInspectorSize = new Vector2(160f, 0f);
		inspectorProps.MaxInspectorSize = new Vector2(160f, 0f);
		return inspectorProps;
	}

	// Token: 0x060018E1 RID: 6369 RVA: 0x0009B466 File Offset: 0x00099666
	public Logic_Augment()
	{
	}

	// Token: 0x040018B6 RID: 6326
	public Logic_Augment.AugmentTest Test;

	// Token: 0x040018B7 RID: 6327
	public ApplyOn Entity;

	// Token: 0x040018B8 RID: 6328
	public AugmentTree Graph;

	// Token: 0x040018B9 RID: 6329
	public EnemyModTag Tag;

	// Token: 0x0200062E RID: 1582
	public enum AugmentTest
	{
		// Token: 0x04002A2B RID: 10795
		EntityHas,
		// Token: 0x04002A2C RID: 10796
		FountainHas,
		// Token: 0x04002A2D RID: 10797
		AITeamHas,
		// Token: 0x04002A2E RID: 10798
		AITeamHasKeyword,
		// Token: 0x04002A2F RID: 10799
		BindingInUse
	}
}
