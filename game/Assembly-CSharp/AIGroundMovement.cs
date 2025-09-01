using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000065 RID: 101
public class AIGroundMovement : AIMovement
{
	// Token: 0x06000374 RID: 884 RVA: 0x0001CF28 File Offset: 0x0001B128
	public override void Setup()
	{
		base.Setup();
		this.agent = base.GetComponent<NavMeshAgent>();
		if (this.agent == null)
		{
			this.agent = base.gameObject.AddComponent<NavMeshAgent>();
		}
		System.Random random = new System.Random(this.controller.ViewID);
		this.movementOffset = new Vector3((float)random.Next(-100, 100) / 100f, 0f, 0f);
		this.delta = (float)random.Next((int)(this.SpeedDelta.x * 1000f), (int)(this.SpeedDelta.y * 1000f)) / 1000f;
		this.agent.speed = base.CurrentSpeed;
		this.agent.updateRotation = false;
		this.agent.stoppingDistance = 0.3f;
		this.agent.acceleration = this.Acceleration;
		this.agent.radius = this.AgentRadius;
		this.agent.height = 4f;
		this.agent.angularSpeed = 200f * this.MovementTurnSpeed;
		this.agent.autoTraverseOffMeshLink = false;
		if (!this.CanJump)
		{
			int num = 1 << NavMesh.GetAreaFromName("Jump");
			this.agent.areaMask &= ~num;
		}
		this.baseMask = this.agent.areaMask;
		EntityHealth health = base.Control.health;
		health.OnDie = (Action<DamageInfo>)Delegate.Combine(health.OnDie, new Action<DamageInfo>(this.OnDie));
		EntityHealth health2 = base.Control.health;
		health2.OnRevive = (Action<int>)Delegate.Combine(health2.OnRevive, new Action<int>(this.OnRevive));
	}

	// Token: 0x06000375 RID: 885 RVA: 0x0001D0EC File Offset: 0x0001B2EC
	public override bool SetTargetPoint(PotentialNavPoint pt)
	{
		Vector3 targetPoint = pt.pos;
		if (pt.visionPoint != null)
		{
			Vector3 onUnitSphere = UnityEngine.Random.onUnitSphere;
			onUnitSphere.y = 0f;
			targetPoint = pt.pos + onUnitSphere * Mathf.Sqrt(pt.ValidRadSqr());
		}
		if (!this.SetTargetPoint(targetPoint))
		{
			return false;
		}
		if (pt.visionPoint != null)
		{
			this.SetOwnedPos(pt.visionPoint);
		}
		return true;
	}

	// Token: 0x06000376 RID: 886 RVA: 0x0001D158 File Offset: 0x0001B358
	public override bool SetTargetPoint(Vector3 pt)
	{
		if (this.agent == null || !this.agent.enabled)
		{
			return false;
		}
		Vector3 vector = AIManager.NearestNavPoint(pt, 5f);
		if (!vector.IsValid())
		{
			return false;
		}
		NavMeshPath path = new NavMeshPath();
		if (!NavMesh.CalculatePath(this.GetPosition(), vector, this.agent.areaMask, path))
		{
			return false;
		}
		this.ReleaseOwnedPt();
		this.agent.SetPath(path);
		return true;
	}

	// Token: 0x06000377 RID: 887 RVA: 0x0001D1CE File Offset: 0x0001B3CE
	public override NavVisionPoint GetCurrentNavPoint()
	{
		return AIVisionGraph.instance.NearestNavPoint(this.GetPosition());
	}

	// Token: 0x06000378 RID: 888 RVA: 0x0001D1E0 File Offset: 0x0001B3E0
	private Vector3 ModifyVectorRandomly(Vector3 v)
	{
		Vector3 b = UnityEngine.Random.insideUnitSphere * 2f;
		this.TransformPointToNavigable(v + b);
		return Vector3.zero;
	}

	// Token: 0x06000379 RID: 889 RVA: 0x0001D210 File Offset: 0x0001B410
	public override Vector3 TransformPointToNavigable(Vector3 pt)
	{
		return AIManager.NearestNavPoint(pt, 10f);
	}

	// Token: 0x0600037A RID: 890 RVA: 0x0001D220 File Offset: 0x0001B420
	public override void Update()
	{
		this.HandlePausing();
		base.Update();
		float num = base.CurrentSpeed;
		num = ((base.Control.currentTarget != null && !base.Control.CanSeeEntity(base.Control.currentTarget)) ? (num * this.NoLOSBoostMult) : num);
		this.agent.speed = (base.AllowedToMove() ? num : 0f);
		this.agent.acceleration = Mathf.Max(this.Acceleration, this.Acceleration * (num / Mathf.Max(1f, this.BaseSpeed)));
		if (this.agent.isOnOffMeshLink && !this.traversingLink && num > 0f)
		{
			base.StartCoroutine(this.TraverseOffMeshLink());
		}
		float num2 = this.timeToMeshLink;
		if (this.debugMovementPath && this.IsMoving())
		{
			this.DebugMovementPath();
		}
		if (this.controller.IsMine)
		{
			this.netSyncPoint = this.GetPosition();
			this.HandleRotation();
		}
		else
		{
			if (!base.Control.IsUsingActiveAbility())
			{
				base.transform.forward = Vector3.Lerp(this.GetForward(), this.wantForward, Time.deltaTime * 6f);
			}
			float num3 = Vector3.Distance(this.netSyncPoint, this.GetPosition());
			if (this.agent != null && this.agent.enabled && this.agent.isOnNavMesh && num3 > 2f)
			{
				this.agent.Move((this.netSyncPoint - this.GetPosition()).normalized * (num3 * Time.deltaTime));
			}
		}
		if (this.IsMoving())
		{
			base.Control.UpdateEventTime(TimeSince.Moved);
		}
	}

	// Token: 0x0600037B RID: 891 RVA: 0x0001D3EC File Offset: 0x0001B5EC
	private void HandlePausing()
	{
		if (PausePanel.IsGamePaused)
		{
			if (!this.didPause)
			{
				this.didPause = true;
				this.pauseVel = this.agent.velocity;
				this.agent.velocity = Vector3.zero;
				return;
			}
		}
		else if (this.didPause)
		{
			this.didPause = false;
			this.agent.velocity = this.pauseVel;
		}
	}

	// Token: 0x0600037C RID: 892 RVA: 0x0001D454 File Offset: 0x0001B654
	private void HandleRotation()
	{
		if (this.controller.IsDead)
		{
			return;
		}
		if (this.AlignToSurface)
		{
			this.AlignToTerrain();
		}
		if (base.Control.IsUsingActiveAbility() || base.Control.HasStatusKeyword(StatusKeyword.Freeze_Display))
		{
			return;
		}
		if (this.FaceTargetWhenIdle && base.Control.currentTarget != null && (!this.IsMoving() || this.MovementTurnSpeed == 0f || this.IsHovering))
		{
			this.IdleRotation();
			return;
		}
		this.MovingRotation();
	}

	// Token: 0x0600037D RID: 893 RVA: 0x0001D4E0 File Offset: 0x0001B6E0
	private void IdleRotation()
	{
		Vector3 forward = this.GetForward();
		Vector3 vector = (base.Control.currentTarget.movement.GetPosition() - this.GetPosition()).normalized;
		vector = Vector3.ProjectOnPlane(vector, Vector3.up);
		float num = (this.MovementTurnSpeed > 0f) ? (this.MovementTurnSpeed * 45f) : 180f;
		vector = Vector3.RotateTowards(forward, vector, num * 0.017453292f * Time.deltaTime, 1f);
		this.SetForward(vector, false);
	}

	// Token: 0x0600037E RID: 894 RVA: 0x0001D56C File Offset: 0x0001B76C
	private void MovingRotation()
	{
		Vector3 forward = this.GetForward();
		Vector3 a = Vector3.ProjectOnPlane(this.GetMoveDir(), Vector3.up);
		if (a.magnitude < 0.05f)
		{
			return;
		}
		float maxRadiansDelta = 0.7853982f * Time.deltaTime * this.MovementTurnSpeed;
		Vector3 vector = Vector3.RotateTowards(forward, a.normalized, maxRadiansDelta, 1f);
		UnityEngine.Debug.DrawRay(base.transform.position + Vector3.up, a * 10f, Color.white);
		UnityEngine.Debug.DrawRay(base.transform.position + Vector3.up, vector * 10f, Color.blue);
		this.SetForward(vector, false);
	}

	// Token: 0x0600037F RID: 895 RVA: 0x0001D624 File Offset: 0x0001B824
	private void AlignToTerrain()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(new Vector3(base.transform.position.x, base.transform.position.y + 2f, base.transform.position.z), -Vector3.up, out raycastHit, 3f, this.AlignMask))
		{
			Quaternion rhs = Quaternion.LookRotation(base.transform.forward, Vector3.up);
			Quaternion b = Quaternion.FromToRotation(Vector3.up, raycastHit.normal) * rhs;
			Quaternion rotation = Quaternion.Slerp(this.previousRotation, b, Time.deltaTime * 4f);
			this.AlignTransform.rotation = rotation;
			this.previousRotation = rotation;
		}
	}

	// Token: 0x06000380 RID: 896 RVA: 0x0001D6E8 File Offset: 0x0001B8E8
	private void DebugMovementPath()
	{
		if (!this.IsMoving())
		{
			return;
		}
		List<Vector3> list = new List<Vector3>();
		list.Add(this.GetPosition());
		foreach (Vector3 item in this.agent.path.corners)
		{
			list.Add(item);
		}
		Vector3 up = Vector3.up;
		for (int j = 1; j < list.Count; j++)
		{
			UnityEngine.Debug.DrawLine(list[j - 1] + up, list[j] + up, Color.green);
		}
	}

	// Token: 0x06000381 RID: 897 RVA: 0x0001D784 File Offset: 0x0001B984
	public override void CancelMovement()
	{
		if (this.agent != null && this.agent.enabled && this.agent.isOnNavMesh)
		{
			this.agent.isStopped = true;
			this.agent.ResetPath();
		}
		this.ReleaseOwnedPt();
	}

	// Token: 0x06000382 RID: 898 RVA: 0x0001D7D6 File Offset: 0x0001B9D6
	public override void DisableMover()
	{
		if (this.agent == null)
		{
			return;
		}
		this.CancelMovement();
		this.agent.enabled = false;
	}

	// Token: 0x06000383 RID: 899 RVA: 0x0001D7FC File Offset: 0x0001B9FC
	public override void EnableMover()
	{
		Vector3 point = AIManager.NearestNavPoint(this.GetPosition(), 10f);
		Vector3 forward = this.GetForward();
		this.agent.enabled = true;
		this.overrideVel = Vector3.zero;
		this.SetPositionImmediate(point, forward, true);
	}

	// Token: 0x06000384 RID: 900 RVA: 0x0001D841 File Offset: 0x0001BA41
	public override bool CanInterruptMovement()
	{
		return !this.traversingLink;
	}

	// Token: 0x06000385 RID: 901 RVA: 0x0001D84C File Offset: 0x0001BA4C
	public override void AddForce(ApplyForceNode node, Vector3 dir, EffectProperties props)
	{
		if (base.Control.Level == EnemyLevel.Boss)
		{
			return;
		}
		if (this.agent != null && this.agent.enabled && !this.traversingLink)
		{
			this.agent.velocity += dir * node.GetForceValue(props);
		}
	}

	// Token: 0x06000386 RID: 902 RVA: 0x0001D8AF File Offset: 0x0001BAAF
	private void CancelLinkTraversal()
	{
		base.StopAllCoroutines();
	}

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x06000387 RID: 903 RVA: 0x0001D8B7 File Offset: 0x0001BAB7
	public override bool IsOnForcedMove
	{
		get
		{
			return this.curMovement != null || this.traversingLink;
		}
	}

	// Token: 0x06000388 RID: 904 RVA: 0x0001D8CF File Offset: 0x0001BACF
	private IEnumerator TraverseOffMeshLink()
	{
		this.traversingLink = true;
		Transform tr = base.transform;
		OffMeshLinkData currentOffMeshLinkData = this.agent.currentOffMeshLinkData;
		this.agent.updatePosition = false;
		Vector3 startPos = tr.position;
		Vector3 endPos = currentOffMeshLinkData.endPos + Vector3.up * this.agent.baseOffset;
		(this.controller.display as AIDisplay).TryJump(startPos);
		yield return new WaitForSeconds(0.2f);
		float magnitude = (endPos - startPos).magnitude;
		float num = 0.66f;
		float t = 0f;
		float tStep = 1f / num;
		float num2 = endPos.y - startPos.y;
		if (num2 == 0f)
		{
			num2 = 0.001f;
		}
		float height = (num2 > 0f) ? 0.3f : 1f;
		while (t < 1f)
		{
			float d = (height + this.AddJumpHeight) * 4f * (t - t * t);
			tr.position = Vector3.Lerp(startPos, endPos, t) + d * Vector3.up;
			this.agent.destination = tr.position;
			t += tStep * GameplayManager.deltaTime;
			yield return null;
		}
		base.transform.position = endPos;
		this.agent.SetAreaCost(NavMesh.GetAreaFromName("Jump"), 9999f);
		this.agent.updatePosition = true;
		this.agent.CompleteOffMeshLink();
		this.agent.Warp(base.transform.position);
		Vector3 destination = this.agent.destination;
		this.agent.ResetPath();
		this.agent.SetDestination(destination);
		yield return true;
		this.traversingLink = false;
		(this.controller.display as AIDisplay).StopCurrentAbilityAnim();
		(this.controller.display as AIDisplay).TryLand(startPos);
		base.Invoke("AllowJumping", 15f);
		yield break;
	}

	// Token: 0x06000389 RID: 905 RVA: 0x0001D8DE File Offset: 0x0001BADE
	private void AllowJumping()
	{
		if (!this.CanJump || !this.agent.enabled)
		{
			return;
		}
		this.agent.SetAreaCost(NavMesh.GetAreaFromName("Jump"), 1.25f);
	}

	// Token: 0x0600038A RID: 906 RVA: 0x0001D910 File Offset: 0x0001BB10
	public override List<PotentialNavPoint> GetAvailablePoints()
	{
		List<PotentialNavPoint> list = new List<PotentialNavPoint>();
		foreach (NavVisionPoint navVisionPoint in AIVisionGraph.instance.points)
		{
			if (AIVisionGraph.CanTarget(navVisionPoint))
			{
				list.Add(new PotentialNavPoint(navVisionPoint));
			}
		}
		return list;
	}

	// Token: 0x0600038B RID: 907 RVA: 0x0001D97C File Offset: 0x0001BB7C
	private void SetOwnedPos(NavVisionPoint pt)
	{
		this.ReleaseOwnedPt();
		AIVisionGraph.AddOwnership(pt);
		this.ownedPt = pt;
	}

	// Token: 0x0600038C RID: 908 RVA: 0x0001D991 File Offset: 0x0001BB91
	private void ReleaseOwnedPt()
	{
		if (this.ownedPt == null)
		{
			return;
		}
		AIVisionGraph.ReleaseOwnership(this.ownedPt);
		this.ownedPt = null;
	}

	// Token: 0x0600038D RID: 909 RVA: 0x0001D9B0 File Offset: 0x0001BBB0
	private Vector3 OldLOS(Vector3 targetPt, Vector2 distRange)
	{
		UnityEngine.Debug.Log("Checking LOS Linear");
		int num = 25;
		for (int i = 0; i < num; i++)
		{
			Vector3 vector = AIManager.NearestNavPoint(Vector3.Lerp(this.GetPosition(), targetPt, (float)i / (float)num), 5f);
			if (vector.IsValid())
			{
				float num2 = Vector3.Distance(targetPt, vector);
				if (num2 < distRange.x)
				{
					return Vector3.zero.INVALID();
				}
				if (num2 <= distRange.y)
				{
					Vector3 inputOverride = vector + Vector3.up * 2.5f;
					if (GameplayManager.PointIsVisible(targetPt, inputOverride, distRange))
					{
						return vector;
					}
				}
			}
		}
		NavMeshHit navMeshHit;
		if (distRange.x == 0f && NavMesh.SamplePosition(targetPt, out navMeshHit, 5f, -1))
		{
			return navMeshHit.position;
		}
		return Vector3.up.INVALID();
	}

	// Token: 0x0600038E RID: 910 RVA: 0x0001DA78 File Offset: 0x0001BC78
	public override bool IsMoving()
	{
		return !(this.controller == null) && !(this.agent == null) && !this.controller.IsDead && (this.traversingLink || this.IsOnForcedMove || (this.agent.enabled && this.agent.isOnNavMesh && !this.agent.isStopped && (this.agent.velocity.magnitude > 0.05f || this.agent.speed < 0.1f)));
	}

	// Token: 0x0600038F RID: 911 RVA: 0x0001DB1C File Offset: 0x0001BD1C
	public override bool CanReachPosition(Vector3 point)
	{
		Vector3 vector = AIManager.NearestNavPoint(point, 5f);
		if (!vector.IsValid())
		{
			return false;
		}
		NavMeshPath path = new NavMeshPath();
		return this.agent.CalculatePath(vector, path);
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06000390 RID: 912 RVA: 0x0001DB52 File Offset: 0x0001BD52
	// (set) Token: 0x06000391 RID: 913 RVA: 0x0001DB5A File Offset: 0x0001BD5A
	public Vector3 overrideVel
	{
		[CompilerGenerated]
		get
		{
			return this.<overrideVel>k__BackingField;
		}
		[CompilerGenerated]
		set
		{
			this.<overrideVel>k__BackingField = value;
		}
	} = Vector3.zero;

	// Token: 0x06000392 RID: 914 RVA: 0x0001DB64 File Offset: 0x0001BD64
	public override Vector3 GetVelocity()
	{
		if (this.curMovement != null)
		{
			return (this.forceTargetPoint - this.forceMoveStart).normalized * base.forceMoveSpeed;
		}
		if (this.overrideVel.sqrMagnitude > 0f)
		{
			return this.overrideVel;
		}
		if (this.agent == null || !this.agent.enabled)
		{
			return Vector3.zero;
		}
		return this.agent.velocity;
	}

	// Token: 0x06000393 RID: 915 RVA: 0x0001DBEC File Offset: 0x0001BDEC
	public Vector3 GetMoveDir()
	{
		if (!this.IsMoving())
		{
			return Vector3.zero;
		}
		return (this.agent.steeringTarget - this.GetPosition()).normalized;
	}

	// Token: 0x06000394 RID: 916 RVA: 0x0001DC25 File Offset: 0x0001BE25
	public override Vector3 GetTargetPoint()
	{
		if (this.IsMoving())
		{
			return this.agent.destination;
		}
		return Vector3.one.INVALID();
	}

	// Token: 0x06000395 RID: 917 RVA: 0x0001DC48 File Offset: 0x0001BE48
	public override AIMovement TransformInto(AIMovement movement)
	{
		base.TransformInto(movement);
		AIGroundMovement aigroundMovement = movement as AIGroundMovement;
		if (aigroundMovement != null)
		{
			this.agent.radius = (this.AgentRadius = aigroundMovement.AgentRadius);
			this.agent.angularSpeed = (this.MovementTurnSpeed = aigroundMovement.MovementTurnSpeed);
			this.agent.acceleration = (this.Acceleration = aigroundMovement.Acceleration);
			this.IsHovering = aigroundMovement.IsHovering;
			this.NoLOSBoostMult = aigroundMovement.NoLOSBoostMult;
			this.AlignToSurface = aigroundMovement.AlignToSurface;
			this.AlignMask = aigroundMovement.AlignMask;
			this.CanJump = aigroundMovement.CanJump;
			this.AddJumpHeight = aigroundMovement.AddJumpHeight;
			return this;
		}
		AIStationaryMovement aistationaryMovement = movement as AIStationaryMovement;
		if (aistationaryMovement != null)
		{
			AIStationaryMovement aistationaryMovement2 = base.gameObject.AddComponent<AIStationaryMovement>();
			aistationaryMovement2.MovementTurnSpeed = aistationaryMovement.MovementTurnSpeed;
			aistationaryMovement2.BaseSpeed = movement.BaseSpeed;
			aistationaryMovement2.FaceTargetWhenIdle = movement.FaceTargetWhenIdle;
			aistationaryMovement2.SpeedDelta = movement.SpeedDelta;
			UnityEngine.Object.Destroy(this.agent);
			UnityEngine.Object.Destroy(this);
			return aistationaryMovement2;
		}
		return this;
	}

	// Token: 0x06000396 RID: 918 RVA: 0x0001DD5C File Offset: 0x0001BF5C
	public override void UpdateFromNetwork(Vector3 pos, Quaternion rot, Vector3 vel)
	{
		this.netSyncPoint = pos;
		if (Vector3.Distance(this.GetPosition(), pos) < 10f || this.agent == null || !this.agent.enabled || this.traversingLink)
		{
			return;
		}
		AIManager.instance.DoTPFX(base.Control, this.GetPosition(), pos);
		Vector3 targetPoint = this.GetTargetPoint();
		this.SetPositionImmediate(pos, base.transform.forward, true);
		if (targetPoint.IsValid())
		{
			this.SetTargetPoint(targetPoint);
		}
	}

	// Token: 0x06000397 RID: 919 RVA: 0x0001DDE8 File Offset: 0x0001BFE8
	public override void SetPositionImmediate(Vector3 point, Vector3 forward, bool clearMomentum = true)
	{
		if (this.agent != null && this.agent.enabled)
		{
			this.CancelMovement();
			this.agent.Warp(point);
			return;
		}
		base.transform.position = point;
		if (!this.controller.IsUsingActiveAbility() && forward.magnitude > 0f)
		{
			base.transform.forward = forward;
		}
	}

	// Token: 0x06000398 RID: 920 RVA: 0x0001DE57 File Offset: 0x0001C057
	private void OnDie(DamageInfo dmg)
	{
		this.DisableMover();
		this.CancelLinkTraversal();
	}

	// Token: 0x06000399 RID: 921 RVA: 0x0001DE65 File Offset: 0x0001C065
	private void OnRevive(int health)
	{
		this.EnableMover();
	}

	// Token: 0x0600039A RID: 922 RVA: 0x0001DE70 File Offset: 0x0001C070
	private void OnDestroy()
	{
		EntityHealth health = base.Control.health;
		health.OnDie = (Action<DamageInfo>)Delegate.Remove(health.OnDie, new Action<DamageInfo>(this.OnDie));
		EntityHealth health2 = base.Control.health;
		health2.OnRevive = (Action<int>)Delegate.Remove(health2.OnRevive, new Action<int>(this.OnRevive));
	}

	// Token: 0x0600039B RID: 923 RVA: 0x0001DED8 File Offset: 0x0001C0D8
	public AIGroundMovement()
	{
	}

	// Token: 0x04000354 RID: 852
	private NavMeshAgent agent;

	// Token: 0x04000355 RID: 853
	public bool IsHovering;

	// Token: 0x04000356 RID: 854
	public float AgentRadius = 1f;

	// Token: 0x04000357 RID: 855
	public float Acceleration = 8f;

	// Token: 0x04000358 RID: 856
	public float MovementTurnSpeed = 1f;

	// Token: 0x04000359 RID: 857
	public float NoLOSBoostMult = 1f;

	// Token: 0x0400035A RID: 858
	public bool AlignToSurface;

	// Token: 0x0400035B RID: 859
	public LayerMask AlignMask;

	// Token: 0x0400035C RID: 860
	public Transform AlignTransform;

	// Token: 0x0400035D RID: 861
	public bool CanJump = true;

	// Token: 0x0400035E RID: 862
	public float AddJumpHeight;

	// Token: 0x0400035F RID: 863
	public bool debugMovementPath;

	// Token: 0x04000360 RID: 864
	[NonSerialized]
	public NavVisionPoint ownedPt;

	// Token: 0x04000361 RID: 865
	private Vector3 netSyncPoint;

	// Token: 0x04000362 RID: 866
	private float timeToMeshLink;

	// Token: 0x04000363 RID: 867
	private int baseMask;

	// Token: 0x04000364 RID: 868
	private bool didPause;

	// Token: 0x04000365 RID: 869
	private Vector3 pauseVel;

	// Token: 0x04000366 RID: 870
	private Quaternion previousRotation;

	// Token: 0x04000367 RID: 871
	private bool traversingLink;

	// Token: 0x04000368 RID: 872
	private float lastWeight;

	// Token: 0x04000369 RID: 873
	[CompilerGenerated]
	private Vector3 <overrideVel>k__BackingField;

	// Token: 0x02000486 RID: 1158
	[CompilerGenerated]
	private sealed class <TraverseOffMeshLink>d__42 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060021C3 RID: 8643 RVA: 0x000C3A5D File Offset: 0x000C1C5D
		[DebuggerHidden]
		public <TraverseOffMeshLink>d__42(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000C3A6C File Offset: 0x000C1C6C
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x000C3A70 File Offset: 0x000C1C70
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			AIGroundMovement aigroundMovement = this;
			switch (num)
			{
			case 0:
			{
				this.<>1__state = -1;
				aigroundMovement.traversingLink = true;
				tr = aigroundMovement.transform;
				OffMeshLinkData currentOffMeshLinkData = aigroundMovement.agent.currentOffMeshLinkData;
				aigroundMovement.agent.updatePosition = false;
				startPos = tr.position;
				endPos = currentOffMeshLinkData.endPos + Vector3.up * aigroundMovement.agent.baseOffset;
				(aigroundMovement.controller.display as AIDisplay).TryJump(startPos);
				this.<>2__current = new WaitForSeconds(0.2f);
				this.<>1__state = 1;
				return true;
			}
			case 1:
			{
				this.<>1__state = -1;
				float magnitude = (endPos - startPos).magnitude;
				float num2 = 0.66f;
				t = 0f;
				tStep = 1f / num2;
				float num3 = endPos.y - startPos.y;
				if (num3 == 0f)
				{
					num3 = 0.001f;
				}
				height = ((num3 > 0f) ? 0.3f : 1f);
				break;
			}
			case 2:
				this.<>1__state = -1;
				break;
			case 3:
				this.<>1__state = -1;
				aigroundMovement.traversingLink = false;
				(aigroundMovement.controller.display as AIDisplay).StopCurrentAbilityAnim();
				(aigroundMovement.controller.display as AIDisplay).TryLand(startPos);
				aigroundMovement.Invoke("AllowJumping", 15f);
				return false;
			default:
				return false;
			}
			if (t >= 1f)
			{
				aigroundMovement.transform.position = endPos;
				aigroundMovement.agent.SetAreaCost(NavMesh.GetAreaFromName("Jump"), 9999f);
				aigroundMovement.agent.updatePosition = true;
				aigroundMovement.agent.CompleteOffMeshLink();
				aigroundMovement.agent.Warp(aigroundMovement.transform.position);
				Vector3 destination = aigroundMovement.agent.destination;
				aigroundMovement.agent.ResetPath();
				aigroundMovement.agent.SetDestination(destination);
				this.<>2__current = true;
				this.<>1__state = 3;
				return true;
			}
			float d = (height + aigroundMovement.AddJumpHeight) * 4f * (t - t * t);
			tr.position = Vector3.Lerp(startPos, endPos, t) + d * Vector3.up;
			aigroundMovement.agent.destination = tr.position;
			t += tStep * GameplayManager.deltaTime;
			this.<>2__current = null;
			this.<>1__state = 2;
			return true;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060021C6 RID: 8646 RVA: 0x000C3D66 File Offset: 0x000C1F66
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x000C3D6E File Offset: 0x000C1F6E
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x000C3D75 File Offset: 0x000C1F75
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040022F9 RID: 8953
		private int <>1__state;

		// Token: 0x040022FA RID: 8954
		private object <>2__current;

		// Token: 0x040022FB RID: 8955
		public AIGroundMovement <>4__this;

		// Token: 0x040022FC RID: 8956
		private Transform <tr>5__2;

		// Token: 0x040022FD RID: 8957
		private Vector3 <startPos>5__3;

		// Token: 0x040022FE RID: 8958
		private Vector3 <endPos>5__4;

		// Token: 0x040022FF RID: 8959
		private float <t>5__5;

		// Token: 0x04002300 RID: 8960
		private float <tStep>5__6;

		// Token: 0x04002301 RID: 8961
		private float <height>5__7;
	}
}
