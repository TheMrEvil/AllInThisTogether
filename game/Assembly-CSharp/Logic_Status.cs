using System;
using UnityEngine;

// Token: 0x02000287 RID: 647
public class Logic_Status : LogicNode
{
	// Token: 0x06001912 RID: 6418 RVA: 0x0009C4C4 File Offset: 0x0009A6C4
	public override bool Evaluate(EffectProperties props)
	{
		if (props == null || this.Graph == null)
		{
			this.EditorStateDisplay = NodeState.Fail;
			return false;
		}
		bool flag = false;
		EntityControl applicationEntity = props.GetApplicationEntity(this.Entity);
		EntityControl applicationEntity2 = props.GetApplicationEntity(this.Entity2);
		float v = this.B;
		float v2 = 0f;
		if (applicationEntity == null)
		{
			this.EditorStateDisplay = NodeState.Fail;
			return false;
		}
		if (this.Num1 != null)
		{
			v = (this.Num1 as NumberNode).Evaluate(props);
		}
		if (this.NeedsSecondNumber() && this.Num2 != null)
		{
			v2 = (this.Num2 as NumberNode).Evaluate(props);
		}
		switch (this.Test)
		{
		case Logic_Status.StatusTest.Stacks:
		{
			EntityControl.AppliedStatus statusInfoByID = applicationEntity.GetStatusInfoByID(this.Graph.ID, -1);
			int num = 0;
			if (statusInfoByID != null)
			{
				num = statusInfoByID.Stacks;
			}
			flag = LogicNode.NumericTest(this.NumTest, (float)num, v, v2);
			break;
		}
		case Logic_Status.StatusTest.AppliedOnOthers:
		{
			int num2 = applicationEntity.NumStatusesApplied(this.Graph.ID);
			flag = LogicNode.NumericTest(this.NumTest, (float)num2, v, v2);
			break;
		}
		case Logic_Status.StatusTest.HasStatus:
		{
			EntityControl.AppliedStatus statusInfoByID2 = applicationEntity.GetStatusInfoByID(this.Graph.ID, -1);
			this.EditorStateDisplay = ((statusInfoByID2 != null) ? NodeState.Success : NodeState.Fail);
			return statusInfoByID2 != null;
		}
		case Logic_Status.StatusTest.HasStatusFrom:
			if (applicationEntity2 != null)
			{
				flag = applicationEntity.HasStatusFromEntity(this.Graph.ID, applicationEntity2.ViewID);
			}
			break;
		case Logic_Status.StatusTest.Lifetime:
		{
			EntityControl.AppliedStatus statusInfoByID3 = applicationEntity.GetStatusInfoByID(this.Graph.ID, -1);
			float v3 = 0f;
			if (statusInfoByID3 != null)
			{
				v3 = statusInfoByID3.Lifetime;
			}
			flag = LogicNode.NumericTest(this.NumTest, v3, v, v2);
			break;
		}
		}
		this.EditorStateDisplay = (flag ? NodeState.Success : NodeState.Fail);
		return flag;
	}

	// Token: 0x06001913 RID: 6419 RVA: 0x0009C68C File Offset: 0x0009A88C
	public bool NeedsNumbers()
	{
		return this.Test != Logic_Status.StatusTest.HasStatus && this.Test != Logic_Status.StatusTest.HasStatusFrom;
	}

	// Token: 0x06001914 RID: 6420 RVA: 0x0009C6A5 File Offset: 0x0009A8A5
	public bool NeedsSecondNumber()
	{
		return this.NeedsNumbers() && this.NumTest == NumberTest.Between;
	}

	// Token: 0x06001915 RID: 6421 RVA: 0x0009C6BA File Offset: 0x0009A8BA
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Status Test";
		inspectorProps.MinInspectorSize = new Vector2(150f, 0f);
		inspectorProps.MaxInspectorSize = new Vector2(150f, 0f);
		return inspectorProps;
	}

	// Token: 0x06001916 RID: 6422 RVA: 0x0009C6F7 File Offset: 0x0009A8F7
	public Logic_Status()
	{
	}

	// Token: 0x04001926 RID: 6438
	[HideInInspector]
	[SerializeField]
	[ShowPort("NeedsNumbers")]
	[OutputPort(typeof(NumberNode), false, "Value (B)", PortLocation.Default)]
	public Node Num1;

	// Token: 0x04001927 RID: 6439
	[HideInInspector]
	[ShowPort("NeedsSecondNumber")]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Max (C)", PortLocation.Default)]
	public Node Num2;

	// Token: 0x04001928 RID: 6440
	public ApplyOn Entity;

	// Token: 0x04001929 RID: 6441
	public Logic_Status.StatusTest Test = Logic_Status.StatusTest.HasStatus;

	// Token: 0x0400192A RID: 6442
	public ApplyOn Entity2;

	// Token: 0x0400192B RID: 6443
	public StatusTree Graph;

	// Token: 0x0400192C RID: 6444
	public NumberTest NumTest;

	// Token: 0x0400192D RID: 6445
	public float B;

	// Token: 0x02000636 RID: 1590
	public enum StatusTest
	{
		// Token: 0x04002A6B RID: 10859
		Stacks,
		// Token: 0x04002A6C RID: 10860
		AppliedOnOthers,
		// Token: 0x04002A6D RID: 10861
		HasStatus,
		// Token: 0x04002A6E RID: 10862
		HasStatusFrom,
		// Token: 0x04002A6F RID: 10863
		Lifetime
	}
}
