using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200026F RID: 623
public class LogicNode : Node
{
	// Token: 0x17000182 RID: 386
	// (get) Token: 0x060018D4 RID: 6356 RVA: 0x0009B065 File Offset: 0x00099265
	internal override bool CanSkipClone
	{
		get
		{
			return true;
		}
	}

	// Token: 0x060018D5 RID: 6357 RVA: 0x0009B068 File Offset: 0x00099268
	public virtual bool Evaluate(EffectProperties props)
	{
		return false;
	}

	// Token: 0x060018D6 RID: 6358 RVA: 0x0009B06C File Offset: 0x0009926C
	public static bool NumericTest(NumberTest test, float v1, float v2, float v3 = 0f)
	{
		if (float.IsNaN(v1) || float.IsNaN(v2) || float.IsNaN(v3))
		{
			return false;
		}
		bool result;
		switch (test)
		{
		case NumberTest.Equals:
			result = (Math.Abs(v1 - v2) < Mathf.Epsilon);
			break;
		case NumberTest.NotEquals:
			result = (Math.Abs(v1 - v2) > Mathf.Epsilon);
			break;
		case NumberTest.LessThan:
			result = (v1 < v2);
			break;
		case NumberTest.GreaterThan:
			result = (v1 > v2);
			break;
		case NumberTest.Between:
			result = (v1 >= v2 && v1 <= v3);
			break;
		case NumberTest.LTOrE:
			result = (v1 <= v2);
			break;
		case NumberTest.GTOrE:
			result = (v1 >= v2);
			break;
		case NumberTest.HasFlag:
			result = (((int)v1 & 1 << (int)v2) != 0);
			break;
		default:
			result = false;
			break;
		}
		return result;
	}

	// Token: 0x060018D7 RID: 6359 RVA: 0x0009B125 File Offset: 0x00099325
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			ShowInputNode = false,
			SortX = true
		};
	}

	// Token: 0x060018D8 RID: 6360 RVA: 0x0009B13A File Offset: 0x0009933A
	public LogicNode()
	{
	}

	// Token: 0x040018B1 RID: 6321
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(Node), true, "", PortLocation.Vertical)]
	public List<Node> TestNode = new List<Node>();
}
