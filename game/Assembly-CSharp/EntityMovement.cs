using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200007E RID: 126
public class EntityMovement : MonoBehaviour
{
	// Token: 0x17000075 RID: 117
	// (get) Token: 0x06000558 RID: 1368 RVA: 0x00027304 File Offset: 0x00025504
	public float CurrentSpeed
	{
		get
		{
			if (this._currentSpeed != -1f)
			{
				return this._currentSpeed;
			}
			float num = this.BaseSpeed + this.delta;
			AIControl aicontrol = this.controller as AIControl;
			if (aicontrol != null && aicontrol.TeamID == 2)
			{
				num = AIManager.ModifyBaseSpeed(num, aicontrol);
			}
			num = this.controller.GetPassiveMod(Passive.EntityValue.Speed, num);
			foreach (AbilityMoveMod abilityMoveMod in this.MoveModifiers)
			{
				num *= abilityMoveMod.SpeedMult * abilityMoveMod.SpeedCurve.Evaluate(Time.realtimeSinceStartup - abilityMoveMod.appliedAt);
			}
			num = Mathf.Max(num, this.BaseSpeed * 0.2f);
			this._currentSpeed = num;
			return num;
		}
	}

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x06000559 RID: 1369 RVA: 0x000273E0 File Offset: 0x000255E0
	public float SpeedMult
	{
		get
		{
			return this.CurrentSpeed / (this.BaseSpeed + this.delta);
		}
	}

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x0600055A RID: 1370 RVA: 0x000273F6 File Offset: 0x000255F6
	public bool isLocalControl
	{
		get
		{
			return this.controller.net.view.IsMine;
		}
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x0002740D File Offset: 0x0002560D
	public virtual void Awake()
	{
		this.controller = base.GetComponent<EntityControl>();
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x0002741B File Offset: 0x0002561B
	public virtual void Setup()
	{
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x0002741D File Offset: 0x0002561D
	public virtual void Update()
	{
		this.RemoveOldMods();
		this.UpdateForcedMovement();
		this._currentSpeed = -1f;
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x00027436 File Offset: 0x00025636
	public virtual void DisableMover()
	{
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x00027438 File Offset: 0x00025638
	public virtual void EnableMover()
	{
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x0002743A File Offset: 0x0002563A
	public virtual bool IsMoverEnabled()
	{
		return true;
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x0002743D File Offset: 0x0002563D
	public virtual bool CanInterruptMovement()
	{
		return true;
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x06000562 RID: 1378 RVA: 0x00027440 File Offset: 0x00025640
	internal float forceMoveSpeed
	{
		get
		{
			return this.curMovement.Speed * this.curMovement.speedCurve.Evaluate(this.forceMoveU) / Mathf.Max(this.forceMoveLength, 0.01f);
		}
	}

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x06000563 RID: 1379 RVA: 0x00027475 File Offset: 0x00025675
	public virtual bool IsOnForcedMove
	{
		get
		{
			return this.curMovement != null;
		}
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x00027484 File Offset: 0x00025684
	public virtual void ForceMovement(ApplyMoveEffect moveEffect, EffectProperties props)
	{
		if (!this.CanInterruptMovement() && props.SourceControl != this.controller)
		{
			return;
		}
		this.curMovement = moveEffect;
		this.forceMoveProps = props;
		this.forceMoveU = 0f;
		this.forceMoveStart = this.GetPosition();
		LocationNode locationNode = moveEffect.Loc as LocationNode;
		Location loc = ((locationNode != null) ? locationNode.GetLocation(props) : null) ?? new Location();
		this.forceTargetPoint = AbilityMoveNode.GetTargetPoint(this.controller, props, loc, this.curMovement.UseNavMesh, this.curMovement.IgnoreObstacles);
		Vector3 b = this.forceTargetPoint;
		b.y = this.forceMoveStart.y;
		this.forceMoveLength = (this.forceTargetPoint.IsValid() ? Vector3.Distance(this.forceMoveStart, b) : 0f);
		if (!this.forceTargetPoint.IsValid() || this.forceMoveLength < 0.1f)
		{
			return;
		}
		this.DisableMover();
		Debug.DrawLine(this.forceMoveStart, this.forceTargetPoint, Color.blue, 3f);
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x00027598 File Offset: 0x00025798
	private void UpdateForcedMovement()
	{
		if (this.curMovement == null)
		{
			return;
		}
		if (!this.forceTargetPoint.IsValid() || this.forceMoveLength < 0.1f)
		{
			foreach (Node node in this.curMovement.OnLand)
			{
				((EffectNode)node).Invoke(this.forceMoveProps.Copy(false));
			}
			this.EnableMover();
			this.curMovement = null;
			return;
		}
		this.forceMoveU += this.curMovement.Speed * this.curMovement.speedCurve.Evaluate(this.forceMoveU) * GameplayManager.deltaTime / Mathf.Max(this.forceMoveLength, 0.01f);
		float num = Mathf.Clamp01(this.forceMoveU);
		Vector3 point = Vector3.Lerp(this.forceMoveStart, this.forceTargetPoint, num);
		float num2 = this.curMovement.HeightCurve.Evaluate(num);
		point.y = Mathf.Lerp(this.forceMoveStart.y, this.forceTargetPoint.y, num) + num2;
		this.SetPositionImmediate(point, this.GetForward(), true);
		if (num >= 1f)
		{
			foreach (Node node2 in this.curMovement.OnLand)
			{
				((EffectNode)node2).Invoke(this.forceMoveProps.Copy(false));
			}
			this.EnableMover();
			this.curMovement = null;
		}
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x0002774C File Offset: 0x0002594C
	public void AddModifier(AbilityMoveMod mod)
	{
		foreach (AbilityMoveMod abilityMoveMod in this.MoveModifiers)
		{
			if (mod.guid == abilityMoveMod.guid)
			{
				mod.appliedAt = Time.realtimeSinceStartup;
				return;
			}
		}
		this.MoveModifiers.Add(mod);
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x000277C4 File Offset: 0x000259C4
	public void RemoveMod(AbilityMoveMod mod)
	{
		for (int i = this.MoveModifiers.Count - 1; i >= 0; i--)
		{
			if (this.MoveModifiers[i].guid == mod.guid)
			{
				this.MoveModifiers.RemoveAt(i);
			}
		}
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x00027814 File Offset: 0x00025A14
	private void RemoveOldMods()
	{
		for (int i = this.MoveModifiers.Count - 1; i >= 0; i--)
		{
			if (this.MoveModifiers[i].Duration > 0f && Time.realtimeSinceStartup - this.MoveModifiers[i].appliedAt >= this.MoveModifiers[i].Duration)
			{
				this.MoveModifiers.RemoveAt(i);
			}
		}
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x00027887 File Offset: 0x00025A87
	public virtual bool IsMoving()
	{
		return false;
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x0002788A File Offset: 0x00025A8A
	public virtual Vector3 GetPosition()
	{
		return base.transform.position;
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x00027897 File Offset: 0x00025A97
	public virtual Vector3 GetForward()
	{
		return base.transform.forward;
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x000278A4 File Offset: 0x00025AA4
	public virtual Quaternion GetRotation()
	{
		return base.transform.rotation;
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x000278B1 File Offset: 0x00025AB1
	public virtual Vector3 GetVelocity()
	{
		return Vector3.forward * 0.0001f;
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x000278C4 File Offset: 0x00025AC4
	public bool AllowedToMove()
	{
		if (PausePanel.IsGamePaused)
		{
			return false;
		}
		using (List<EntityControl.AppliedStatus>.Enumerator enumerator = this.controller.Statuses.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Keywords.Contains(StatusKeyword.Prevent_Movement))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x00027934 File Offset: 0x00025B34
	public float GetAngleFrom(Vector3 pos)
	{
		Vector3 from = Vector3.ProjectOnPlane((pos - this.GetPosition()).normalized, Vector3.up);
		Vector3 to = Vector3.ProjectOnPlane(this.GetForward(), Vector3.up);
		return Vector3.Angle(from, to);
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x00027978 File Offset: 0x00025B78
	public virtual void SetForward(Vector3 newForward, bool fromAbility)
	{
		if (!fromAbility && (!this.controller.IsMine || this.controller.IsUsingActiveAbility()))
		{
			return;
		}
		base.transform.LookAt(base.transform.position + Vector3.ProjectOnPlane(newForward, Vector3.up));
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x000279C9 File Offset: 0x00025BC9
	public virtual void AddForce(ApplyForceNode node, Vector3 dir, EffectProperties props)
	{
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x000279CB File Offset: 0x00025BCB
	public virtual void UpdateFromNetwork(Vector3 pos, Quaternion rot, Vector3 vel)
	{
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x000279CD File Offset: 0x00025BCD
	public virtual void SetPositionImmediate(Vector3 point, Vector3 forward, bool clearMomentum = true)
	{
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x000279CF File Offset: 0x00025BCF
	public EntityMovement()
	{
	}

	// Token: 0x04000435 RID: 1077
	[NonSerialized]
	public EntityControl controller;

	// Token: 0x04000436 RID: 1078
	[SerializeField]
	public float BaseSpeed = 3f;

	// Token: 0x04000437 RID: 1079
	internal float delta;

	// Token: 0x04000438 RID: 1080
	private float _currentSpeed;

	// Token: 0x04000439 RID: 1081
	internal Vector3 wantVel;

	// Token: 0x0400043A RID: 1082
	internal Vector3 wantPosition;

	// Token: 0x0400043B RID: 1083
	internal Quaternion wantRot;

	// Token: 0x0400043C RID: 1084
	internal Vector3 wantForward;

	// Token: 0x0400043D RID: 1085
	private List<AbilityMoveMod> MoveModifiers = new List<AbilityMoveMod>();

	// Token: 0x0400043E RID: 1086
	internal ApplyMoveEffect curMovement;

	// Token: 0x0400043F RID: 1087
	private EffectProperties forceMoveProps;

	// Token: 0x04000440 RID: 1088
	internal Vector3 forceMoveStart;

	// Token: 0x04000441 RID: 1089
	private float forceMoveLength;

	// Token: 0x04000442 RID: 1090
	internal Vector3 forceTargetPoint;

	// Token: 0x04000443 RID: 1091
	private float forceMoveU;
}
