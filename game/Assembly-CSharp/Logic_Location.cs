using System;
using UnityEngine;

// Token: 0x02000283 RID: 643
public class Logic_Location : LogicNode
{
	// Token: 0x060018FF RID: 6399 RVA: 0x0009BF48 File Offset: 0x0009A148
	public override bool Evaluate(EffectProperties props)
	{
		Vector3 vector = Vector3.one.INVALID();
		Vector3 vector2 = vector;
		float num = this.B;
		float num2 = this.C;
		if (this.Loc != null)
		{
			LocationNode locationNode = this.Loc as LocationNode;
			if (locationNode != null)
			{
				vector = locationNode.GetLocation(props).GetPosition(props);
			}
		}
		if (this.ShowSecondLocation() && this.Loc2 != null)
		{
			LocationNode locationNode2 = this.Loc2 as LocationNode;
			if (locationNode2 != null)
			{
				vector2 = locationNode2.GetLocation(props).GetPosition(props);
			}
		}
		if (this.NeedsNumbers() && this.Num1 != null)
		{
			NumberNode numberNode = this.Num1 as NumberNode;
			if (numberNode != null)
			{
				num = numberNode.Evaluate(props);
			}
		}
		if (this.ShowSecondNum() && this.Num2 != null)
		{
			NumberNode numberNode2 = this.Num2 as NumberNode;
			if (numberNode2 != null)
			{
				num2 = numberNode2.Evaluate(props);
			}
		}
		if (!vector.IsValid() || (this.ShowSecondLocation() && !vector2.IsValid()))
		{
			return false;
		}
		bool locationBool = this.GetLocationBool(props, vector, vector2, num, num2);
		this.EditorStateDisplay = (locationBool ? NodeState.Success : NodeState.Fail);
		return locationBool;
	}

	// Token: 0x06001900 RID: 6400 RVA: 0x0009C06C File Offset: 0x0009A26C
	private bool GetLocationBool(EffectProperties props, Vector3 v1, Vector3 v2, float num1, float num2)
	{
		if (float.IsNaN(num1) || float.IsNaN(num2))
		{
			return false;
		}
		switch (this.Test)
		{
		case Logic_Location.LocationTest.IsValid:
			return v1.IsValid();
		case Logic_Location.LocationTest.Distance:
		{
			float v3 = Vector3.Distance(v1, v2);
			return LogicNode.NumericTest(this.NumTest, v3, num1, num2);
		}
		case Logic_Location.LocationTest.InFOV:
		{
			EntityControl applicationEntity = props.GetApplicationEntity(this.Entity);
			if (applicationEntity == null)
			{
				return false;
			}
			Vector3 forward = applicationEntity.movement.GetForward();
			Vector3 normalized = (v1 - applicationEntity.movement.GetPosition()).normalized;
			float num3 = Mathf.Acos(Vector3.Dot(forward, normalized)) * 57.29578f;
			return num3 <= num1 && num3 >= num2;
		}
		case Logic_Location.LocationTest.InLOS:
		{
			Vector3 vector = v2 - v1;
			return !Physics.Raycast(v1, vector.normalized, vector.magnitude, AIVisionGraph.instance.mask);
		}
		default:
			return false;
		}
	}

	// Token: 0x06001901 RID: 6401 RVA: 0x0009C164 File Offset: 0x0009A364
	public bool ShowSecondLocation()
	{
		Logic_Location.LocationTest test = this.Test;
		return test == Logic_Location.LocationTest.Distance || test == Logic_Location.LocationTest.InLOS;
	}

	// Token: 0x06001902 RID: 6402 RVA: 0x0009C190 File Offset: 0x0009A390
	public bool NeedsNumberTest()
	{
		Logic_Location.LocationTest test = this.Test;
		return test == Logic_Location.LocationTest.Distance || (test != Logic_Location.LocationTest.InFOV && false);
	}

	// Token: 0x06001903 RID: 6403 RVA: 0x0009C1BC File Offset: 0x0009A3BC
	public bool NeedsNumbers()
	{
		Logic_Location.LocationTest test = this.Test;
		return test == Logic_Location.LocationTest.Distance || test == Logic_Location.LocationTest.InFOV;
	}

	// Token: 0x06001904 RID: 6404 RVA: 0x0009C1E8 File Offset: 0x0009A3E8
	public bool ShowSecondNum()
	{
		return this.Test == Logic_Location.LocationTest.Distance && this.NumTest == NumberTest.Between;
	}

	// Token: 0x06001905 RID: 6405 RVA: 0x0009C210 File Offset: 0x0009A410
	public bool NeedEntitySelect()
	{
		return this.Test == Logic_Location.LocationTest.InFOV;
	}

	// Token: 0x06001906 RID: 6406 RVA: 0x0009C22D File Offset: 0x0009A42D
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Location Test";
		inspectorProps.MinInspectorSize = new Vector2(150f, 0f);
		inspectorProps.MaxInspectorSize = new Vector2(150f, 0f);
		return inspectorProps;
	}

	// Token: 0x06001907 RID: 6407 RVA: 0x0009C26A File Offset: 0x0009A46A
	public Logic_Location()
	{
	}

	// Token: 0x04001914 RID: 6420
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Location", PortLocation.Default)]
	public Node Loc;

	// Token: 0x04001915 RID: 6421
	[HideInInspector]
	[ShowPort("ShowSecondLocation")]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Location 2", PortLocation.Default)]
	public Node Loc2;

	// Token: 0x04001916 RID: 6422
	[HideInInspector]
	[ShowPort("NeedsNumbers")]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, " Value (B)", PortLocation.Default)]
	public Node Num1;

	// Token: 0x04001917 RID: 6423
	[HideInInspector]
	[ShowPort("ShowSecondNum")]
	[SerializeField]
	[OutputPort(typeof(NumberNode), false, "Value (C)", PortLocation.Default)]
	public Node Num2;

	// Token: 0x04001918 RID: 6424
	public Logic_Location.LocationTest Test;

	// Token: 0x04001919 RID: 6425
	public NumberTest NumTest;

	// Token: 0x0400191A RID: 6426
	public float B;

	// Token: 0x0400191B RID: 6427
	public float C;

	// Token: 0x0400191C RID: 6428
	public ApplyOn Entity;

	// Token: 0x02000635 RID: 1589
	public enum LocationTest
	{
		// Token: 0x04002A66 RID: 10854
		IsValid,
		// Token: 0x04002A67 RID: 10855
		Distance,
		// Token: 0x04002A68 RID: 10856
		InFOV,
		// Token: 0x04002A69 RID: 10857
		InLOS
	}
}
