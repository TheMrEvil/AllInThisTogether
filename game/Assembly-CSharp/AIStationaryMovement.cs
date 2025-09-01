using System;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class AIStationaryMovement : AIMovement
{
	// Token: 0x06000427 RID: 1063 RVA: 0x00020AF8 File Offset: 0x0001ECF8
	public override void Update()
	{
		base.Update();
		if (this.controller.IsMine && this.FaceTargetWhenIdle && base.Control.currentTarget != null && !this.controller.IsDead && !base.Control.IsUsingActiveAbility())
		{
			Vector3 forward = this.GetForward();
			Vector3 vector = (base.Control.currentTarget.movement.GetPosition() - this.GetPosition()).normalized;
			vector = Vector3.ProjectOnPlane(vector, Vector3.up);
			vector = Vector3.RotateTowards(forward, vector, 3.1415927f * Time.deltaTime * this.MovementTurnSpeed, 1f);
			this.SetForward(vector, false);
		}
		if (!this.controller.IsMine && !base.Control.IsUsingActiveAbility())
		{
			base.transform.forward = Vector3.Lerp(this.GetForward(), this.wantForward, Time.deltaTime * 6f);
		}
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x00020BF5 File Offset: 0x0001EDF5
	public override void SetForward(Vector3 newForward, bool fromAbility)
	{
		base.SetForward(newForward, fromAbility);
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x00020C00 File Offset: 0x0001EE00
	public override AIMovement TransformInto(AIMovement movement)
	{
		base.TransformInto(movement);
		AIGroundMovement aigroundMovement = movement as AIGroundMovement;
		if (aigroundMovement != null)
		{
			AIGroundMovement aigroundMovement2 = base.gameObject.AddComponent<AIGroundMovement>();
			aigroundMovement2.AgentRadius = aigroundMovement.AgentRadius;
			aigroundMovement2.Acceleration = aigroundMovement.Acceleration;
			aigroundMovement2.MovementTurnSpeed = aigroundMovement.MovementTurnSpeed;
			aigroundMovement2.IsHovering = aigroundMovement.IsHovering;
			aigroundMovement2.NoLOSBoostMult = aigroundMovement.NoLOSBoostMult;
			aigroundMovement2.AlignToSurface = aigroundMovement.AlignToSurface;
			aigroundMovement2.AlignMask = aigroundMovement.AlignMask;
			aigroundMovement2.CanJump = aigroundMovement.CanJump;
			aigroundMovement2.AddJumpHeight = aigroundMovement.AddJumpHeight;
			aigroundMovement2.BaseSpeed = movement.BaseSpeed;
			aigroundMovement2.FaceTargetWhenIdle = movement.FaceTargetWhenIdle;
			aigroundMovement2.SpeedDelta = movement.SpeedDelta;
			aigroundMovement2.Setup();
			UnityEngine.Object.Destroy(this);
			return this;
		}
		AIStationaryMovement aistationaryMovement = movement as AIStationaryMovement;
		if (aistationaryMovement != null)
		{
			this.MovementTurnSpeed = aistationaryMovement.MovementTurnSpeed;
			return this;
		}
		return this;
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x00020CE4 File Offset: 0x0001EEE4
	public override void UpdateFromNetwork(Vector3 pos, Quaternion rot, Vector3 vel)
	{
		if (Vector3.Distance(this.GetPosition(), pos) < 2f)
		{
			return;
		}
		if (base.Control.Level != EnemyLevel.Boss)
		{
			AIManager.instance.DoTPFX(base.Control, this.GetPosition(), pos);
		}
		Vector3 targetPoint = this.GetTargetPoint();
		this.SetPositionImmediate(pos, base.transform.forward, true);
		if (targetPoint.IsValid())
		{
			this.SetTargetPoint(targetPoint);
		}
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x00020D55 File Offset: 0x0001EF55
	public override void SetPositionImmediate(Vector3 point, Vector3 forward, bool clearMomentum = true)
	{
		base.transform.position = point;
		if (!this.controller.IsUsingActiveAbility())
		{
			base.transform.forward = forward;
		}
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x00020D7C File Offset: 0x0001EF7C
	public AIStationaryMovement()
	{
	}

	// Token: 0x040003A5 RID: 933
	public float MovementTurnSpeed = 1f;
}
