using System;
using UnityEngine;

// Token: 0x02000270 RID: 624
public class Logic_Ability : LogicNode
{
	// Token: 0x060018D9 RID: 6361 RVA: 0x0009B150 File Offset: 0x00099350
	public override bool Evaluate(EffectProperties props)
	{
		if (props == null || this.Graph == null)
		{
			this.EditorStateDisplay = NodeState.Fail;
			return false;
		}
		EntityControl applicationEntity = props.GetApplicationEntity(this.Entity);
		if (applicationEntity == null)
		{
			this.EditorStateDisplay = NodeState.Fail;
			return false;
		}
		Ability ability = applicationEntity.GetAbility(this.Graph.ID);
		if (ability == null)
		{
			this.EditorStateDisplay = NodeState.Fail;
			return false;
		}
		bool flag;
		switch (this.Test)
		{
		case Logic_Ability.AbilityTest.CanUse:
			flag = (!applicationEntity.IsUsingAbility(this.Graph, false) && ability.currentCD <= 0f);
			break;
		case Logic_Ability.AbilityTest.IsCasting:
			flag = applicationEntity.IsUsingAbility(this.Graph, false);
			break;
		case Logic_Ability.AbilityTest.HasAbility:
			flag = applicationEntity.HasAbility(this.Graph.ID);
			break;
		default:
			flag = false;
			break;
		}
		bool flag2 = flag;
		this.EditorStateDisplay = (flag2 ? NodeState.Success : NodeState.Fail);
		return flag2;
	}

	// Token: 0x060018DA RID: 6362 RVA: 0x0009B22E File Offset: 0x0009942E
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Ability Test";
		inspectorProps.MinInspectorSize = new Vector2(150f, 0f);
		inspectorProps.MaxInspectorSize = new Vector2(150f, 0f);
		return inspectorProps;
	}

	// Token: 0x060018DB RID: 6363 RVA: 0x0009B26B File Offset: 0x0009946B
	public Logic_Ability()
	{
	}

	// Token: 0x040018B2 RID: 6322
	public ApplyOn Entity;

	// Token: 0x040018B3 RID: 6323
	public Logic_Ability.AbilityTest Test;

	// Token: 0x040018B4 RID: 6324
	public AbilityTree Graph;

	// Token: 0x0200062D RID: 1581
	public enum AbilityTest
	{
		// Token: 0x04002A27 RID: 10791
		CanUse,
		// Token: 0x04002A28 RID: 10792
		IsCasting,
		// Token: 0x04002A29 RID: 10793
		HasAbility
	}
}
