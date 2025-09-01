using System;
using UnityEngine;

// Token: 0x02000326 RID: 806
public class AILocationCheck : AITestNode
{
	// Token: 0x06001BAD RID: 7085 RVA: 0x000AA3BC File Offset: 0x000A85BC
	public override bool Evaluate(EntityControl entity)
	{
		if (this.Location == null)
		{
			return false;
		}
		AIControl aicontrol = entity as AIControl;
		EffectProperties props = new EffectProperties(aicontrol);
		Vector3 position = (this.Location as LocationNode).GetLocation(props).GetPosition(props);
		AILocationCheck.CheckType test = this.Test;
		if (test == AILocationCheck.CheckType.InRange)
		{
			float num = Vector3.Distance(aicontrol.Movement.GetPosition(), position);
			return num >= this.ValidRange.x && num <= this.ValidRange.y;
		}
		if (test != AILocationCheck.CheckType.InFOV)
		{
			return false;
		}
		Vector3 forward = aicontrol.Movement.GetForward();
		Vector3 normalized = (aicontrol.currentTarget.movement.GetPosition() - aicontrol.Movement.GetPosition()).normalized;
		float num2 = Mathf.Acos(Vector3.Dot(forward, normalized)) * 57.29578f;
		return num2 <= this.FOV_Max && num2 >= this.FOV_Min;
	}

	// Token: 0x06001BAE RID: 7086 RVA: 0x000AA4AE File Offset: 0x000A86AE
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Location Test";
		inspectorProps.MinInspectorSize = new Vector2(210f, 0f);
		return inspectorProps;
	}

	// Token: 0x06001BAF RID: 7087 RVA: 0x000AA4D6 File Offset: 0x000A86D6
	public AILocationCheck()
	{
	}

	// Token: 0x04001C05 RID: 7173
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Point", PortLocation.Default)]
	public Node Location;

	// Token: 0x04001C06 RID: 7174
	public AILocationCheck.CheckType Test;

	// Token: 0x04001C07 RID: 7175
	public Vector2 ValidRange = new Vector2(0f, 999f);

	// Token: 0x04001C08 RID: 7176
	[Range(0f, 179f)]
	public float FOV_Min;

	// Token: 0x04001C09 RID: 7177
	[Range(1f, 180f)]
	public float FOV_Max = 45f;

	// Token: 0x0200065F RID: 1631
	public enum CheckType
	{
		// Token: 0x04002B32 RID: 11058
		InRange,
		// Token: 0x04002B33 RID: 11059
		InFOV
	}
}
