using System;
using UnityEngine;

// Token: 0x0200032E RID: 814
public class AITargetCheck : AITestNode
{
	// Token: 0x06001BC3 RID: 7107 RVA: 0x000AA8D4 File Offset: 0x000A8AD4
	public override bool Evaluate(EntityControl entity)
	{
		AIControl aicontrol = entity as AIControl;
		if (this.Test == AITargetCheck.CheckType.EverHadTarget)
		{
			return aicontrol.HasTag("HadTarget");
		}
		if (aicontrol.currentTarget == null || aicontrol.currentTarget.IsDead || this.AllowDead)
		{
			return false;
		}
		switch (this.Test)
		{
		case AITargetCheck.CheckType.Exists:
			return true;
		case AITargetCheck.CheckType.InLineOfSight:
			return aicontrol.CanSeeEntity(aicontrol.currentTarget);
		case AITargetCheck.CheckType.InRange:
		{
			float num = Vector3.Distance(aicontrol.Movement.GetPosition(), aicontrol.currentTarget.movement.GetPosition());
			return num >= this.ValidRange.x && num <= this.ValidRange.y;
		}
		case AITargetCheck.CheckType.EverHadTarget:
			return aicontrol.HasTag("HadTarget");
		case AITargetCheck.CheckType.InFOV:
		{
			Vector3 forward = aicontrol.Movement.GetForward();
			Vector3 normalized = (aicontrol.currentTarget.movement.GetPosition() - aicontrol.Movement.GetPosition()).normalized;
			float num2 = Mathf.Acos(Vector3.Dot(forward, normalized)) * 57.29578f;
			return num2 <= this.FOV_Max && num2 >= this.FOV_Min;
		}
		case AITargetCheck.CheckType.IsAlly:
			return aicontrol.TeamID == aicontrol.currentTarget.TeamID;
		default:
			return false;
		}
	}

	// Token: 0x06001BC4 RID: 7108 RVA: 0x000AAA20 File Offset: 0x000A8C20
	public override Node.InspectorProps GetInspectorProps()
	{
		Node.InspectorProps inspectorProps = base.GetInspectorProps();
		inspectorProps.Title = "Target Test";
		inspectorProps.MinInspectorSize = new Vector2(210f, 0f);
		return inspectorProps;
	}

	// Token: 0x06001BC5 RID: 7109 RVA: 0x000AAA48 File Offset: 0x000A8C48
	public AITargetCheck()
	{
	}

	// Token: 0x04001C25 RID: 7205
	public AITargetCheck.CheckType Test;

	// Token: 0x04001C26 RID: 7206
	public bool AllowDead;

	// Token: 0x04001C27 RID: 7207
	public Vector2 ValidRange = new Vector2(0f, 999f);

	// Token: 0x04001C28 RID: 7208
	[Range(0f, 179f)]
	public float FOV_Min;

	// Token: 0x04001C29 RID: 7209
	[Range(1f, 180f)]
	public float FOV_Max = 45f;

	// Token: 0x02000662 RID: 1634
	public enum CheckType
	{
		// Token: 0x04002B3C RID: 11068
		Exists,
		// Token: 0x04002B3D RID: 11069
		InLineOfSight,
		// Token: 0x04002B3E RID: 11070
		InRange,
		// Token: 0x04002B3F RID: 11071
		EverHadTarget,
		// Token: 0x04002B40 RID: 11072
		InFOV,
		// Token: 0x04002B41 RID: 11073
		IsAlly
	}
}
