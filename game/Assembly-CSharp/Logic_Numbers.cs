using System;
using UnityEngine;

// Token: 0x02000285 RID: 645
public class Logic_Numbers : LogicNode
{
	// Token: 0x0600190B RID: 6411 RVA: 0x0009C2F0 File Offset: 0x0009A4F0
	public override bool Evaluate(EffectProperties props)
	{
		if (props == null)
		{
			this.EditorStateDisplay = NodeState.Fail;
			return false;
		}
		float v = this.A;
		float v2 = this.B;
		float v3 = this.C;
		if (this.Num1 != null)
		{
			NumberNode numberNode = this.Num1 as NumberNode;
			if (numberNode != null)
			{
				v = numberNode.Evaluate(props);
			}
		}
		if (this.Num2 != null)
		{
			NumberNode numberNode2 = this.Num2 as NumberNode;
			if (numberNode2 != null)
			{
				v2 = numberNode2.Evaluate(props);
			}
		}
		if (this.NeedsThirdNumber() && this.Num3 != null)
		{
			NumberNode numberNode3 = this.Num3 as NumberNode;
			if (numberNode3 != null)
			{
				v3 = numberNode3.Evaluate(props);
			}
		}
		bool flag = LogicNode.NumericTest(this.NumTest, v, v2, v3);
		this.EditorStateDisplay = (flag ? NodeState.Success : NodeState.Fail);
		return flag;
	}

	// Token: 0x0600190C RID: 6412 RVA: 0x0009C3BD File Offset: 0x0009A5BD
	public bool NeedsThirdNumber()
	{
		return this.NumTest == NumberTest.Between;
	}

	// Token: 0x0600190D RID: 6413 RVA: 0x0009C3C8 File Offset: 0x0009A5C8
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Num Test";
		inspectorProps.MinInspectorSize = new Vector2(150f, 0f);
		inspectorProps.MaxInspectorSize = new Vector2(150f, 0f);
		return inspectorProps;
	}

	// Token: 0x0600190E RID: 6414 RVA: 0x0009C405 File Offset: 0x0009A605
	public Logic_Numbers()
	{
	}

	// Token: 0x0400191E RID: 6430
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "A", PortLocation.Default)]
	public Node Num1;

	// Token: 0x0400191F RID: 6431
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "B", PortLocation.Default)]
	public Node Num2;

	// Token: 0x04001920 RID: 6432
	[HideInInspector]
	[ShowPort("NeedsThirdNumber")]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "C", PortLocation.Default)]
	public Node Num3;

	// Token: 0x04001921 RID: 6433
	public NumberTest NumTest;

	// Token: 0x04001922 RID: 6434
	public float A;

	// Token: 0x04001923 RID: 6435
	public float B;

	// Token: 0x04001924 RID: 6436
	public float C;
}
