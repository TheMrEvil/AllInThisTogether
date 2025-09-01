using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000069 RID: 105
public class AIMovement : EntityMovement
{
	// Token: 0x1700004A RID: 74
	// (get) Token: 0x060003EE RID: 1006 RVA: 0x0001FF8B File Offset: 0x0001E18B
	public AIControl Control
	{
		get
		{
			return this.controller as AIControl;
		}
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x060003EF RID: 1007 RVA: 0x0001FF98 File Offset: 0x0001E198
	public virtual bool HasVelocity
	{
		get
		{
			return true;
		}
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x0001FF9B File Offset: 0x0001E19B
	public virtual bool SetTargetPoint(Vector3 pt)
	{
		return false;
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x0001FF9E File Offset: 0x0001E19E
	public virtual bool SetTargetPoint(PotentialNavPoint pt)
	{
		return false;
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x0001FFA1 File Offset: 0x0001E1A1
	public virtual Vector3 GetTargetPoint()
	{
		return Vector3.one.INVALID();
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x0001FFAD File Offset: 0x0001E1AD
	public virtual NavVisionPoint GetCurrentNavPoint()
	{
		return null;
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x0001FFB0 File Offset: 0x0001E1B0
	public virtual float DistanceFromTarget()
	{
		return Vector3.Distance(this.GetPosition(), this.GetTargetPoint());
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x0001FFC3 File Offset: 0x0001E1C3
	public virtual bool CanReachPosition(Vector3 point)
	{
		return false;
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x0001FFC6 File Offset: 0x0001E1C6
	public virtual Vector3 TransformPointToNavigable(Vector3 pt)
	{
		return pt;
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x0001FFC9 File Offset: 0x0001E1C9
	public virtual AIMovement TransformInto(AIMovement movement)
	{
		this.BaseSpeed = movement.BaseSpeed;
		this.FaceTargetWhenIdle = movement.FaceTargetWhenIdle;
		this.SpeedDelta = movement.SpeedDelta;
		return this;
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x0001FFF0 File Offset: 0x0001E1F0
	public virtual void CancelMovement()
	{
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x0001FFF2 File Offset: 0x0001E1F2
	public override void ForceMovement(ApplyMoveEffect moveEffect, EffectProperties props)
	{
		if (this.Control.Level == EnemyLevel.Boss && props.SourceControl != this.controller)
		{
			return;
		}
		base.ForceMovement(moveEffect, props);
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x0002001F File Offset: 0x0001E21F
	public virtual List<PotentialNavPoint> GetAvailablePoints()
	{
		return new List<PotentialNavPoint>();
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00020026 File Offset: 0x0001E226
	public override void UpdateFromNetwork(Vector3 pos, Quaternion rot, Vector3 vel)
	{
		this.wantPosition = pos;
		this.wantVel = vel;
		this.wantRot = rot;
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x0002003D File Offset: 0x0001E23D
	public virtual Vector3 NearestLOSPoint(EntityControl targ, Vector2 distRange)
	{
		return Vector3.one.INVALID();
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00020049 File Offset: 0x0001E249
	public AIMovement()
	{
	}

	// Token: 0x04000399 RID: 921
	public Vector2 SpeedDelta = Vector2.zero;

	// Token: 0x0400039A RID: 922
	[NonSerialized]
	public Vector3 movementOffset;

	// Token: 0x0400039B RID: 923
	public bool FaceTargetWhenIdle;
}
