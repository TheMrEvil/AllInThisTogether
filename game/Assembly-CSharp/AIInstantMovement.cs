using System;
using UnityEngine;

// Token: 0x02000067 RID: 103
public class AIInstantMovement : AIMovement
{
	// Token: 0x17000044 RID: 68
	// (get) Token: 0x0600039E RID: 926 RVA: 0x0001E021 File Offset: 0x0001C221
	public override bool HasVelocity
	{
		get
		{
			return false;
		}
	}

	// Token: 0x0600039F RID: 927 RVA: 0x0001E024 File Offset: 0x0001C224
	public override void Update()
	{
		if (!base.Control.IsMine && this.doneInitialSet)
		{
			Vector3 vector = base.transform.position;
			float num = Vector3.Distance(vector, this.wantPosition);
			vector = Vector3.MoveTowards(vector, this.wantPosition, this.wantVel.magnitude * 1f * Time.deltaTime);
			vector = Vector3.Lerp(vector, this.wantPosition, Time.deltaTime * ((num < 25f) ? 0.5f : 8f));
			base.transform.position = vector;
		}
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x0001E0B6 File Offset: 0x0001C2B6
	public override void SetPositionImmediate(Vector3 point, Vector3 forward, bool clearMomentum = true)
	{
		this.wantPosition = point;
		this.wantRot = Quaternion.LookRotation(forward, Vector3.up);
		base.transform.SetPositionAndRotation(point, this.wantRot);
		this.doneInitialSet = true;
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x0001E0E9 File Offset: 0x0001C2E9
	public override void UpdateFromNetwork(Vector3 pos, Quaternion rot, Vector3 vel)
	{
		base.UpdateFromNetwork(pos, rot, vel);
		if (Vector3.Distance(this.GetPosition(), pos) < 5f)
		{
			return;
		}
		this.doneInitialSet = true;
		base.transform.SetPositionAndRotation(pos, rot);
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x0001E11C File Offset: 0x0001C31C
	public AIInstantMovement()
	{
	}

	// Token: 0x0400036C RID: 876
	private bool doneInitialSet;
}
