using System;
using UnityEngine;

// Token: 0x02000324 RID: 804
public class AIDistanceFrom : AITestNode
{
	// Token: 0x06001BA5 RID: 7077 RVA: 0x000AA1D0 File Offset: 0x000A83D0
	public override bool Evaluate(EntityControl entity)
	{
		EffectProperties effectProperties = new EffectProperties(entity);
		effectProperties.Affected = entity.gameObject;
		Vector3 position = (this.Loc as LocationNode).GetLocation(effectProperties).GetPosition(effectProperties);
		Vector3 position2 = (this.Loc2 as LocationNode).GetLocation(effectProperties).GetPosition(effectProperties);
		float val = Vector3.Distance(position, position2);
		return this.NumericTest(val);
	}

	// Token: 0x06001BA6 RID: 7078 RVA: 0x000AA230 File Offset: 0x000A8430
	public override bool Evaluate(EffectProperties props, ApplyOn toCheck)
	{
		Vector3 a = props.GetOrigin();
		Vector3 b = props.GetOutputPoint();
		if (this.Loc != null)
		{
			a = (this.Loc as LocationNode).GetLocation(props).GetPosition(props);
		}
		if (this.Loc2 != null)
		{
			b = (this.Loc2 as LocationNode).GetLocation(props).GetPosition(props);
		}
		float val = Vector3.Distance(a, b);
		return this.NumericTest(val);
	}

	// Token: 0x06001BA7 RID: 7079 RVA: 0x000AA2A8 File Offset: 0x000A84A8
	private bool NumericTest(float val)
	{
		bool result;
		switch (this.Test)
		{
		case NumberTest.Equals:
			result = ((int)val == (int)this.Value);
			break;
		case NumberTest.NotEquals:
			result = ((int)val != (int)this.Value);
			break;
		case NumberTest.LessThan:
			result = (val < this.Value);
			break;
		case NumberTest.GreaterThan:
			result = (val > this.Value);
			break;
		case NumberTest.Between:
			result = (val >= this.Between.x && val <= this.Between.y);
			break;
		default:
			result = false;
			break;
		}
		return result;
	}

	// Token: 0x06001BA8 RID: 7080 RVA: 0x000AA337 File Offset: 0x000A8537
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Distance",
			ShowInputNode = false
		};
	}

	// Token: 0x06001BA9 RID: 7081 RVA: 0x000AA350 File Offset: 0x000A8550
	public AIDistanceFrom()
	{
	}

	// Token: 0x04001BFD RID: 7165
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Location 1", PortLocation.Default)]
	public Node Loc;

	// Token: 0x04001BFE RID: 7166
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Location 2", PortLocation.Default)]
	public Node Loc2;

	// Token: 0x04001BFF RID: 7167
	public NumberTest Test;

	// Token: 0x04001C00 RID: 7168
	public float Value;

	// Token: 0x04001C01 RID: 7169
	public Vector2 Between;
}
