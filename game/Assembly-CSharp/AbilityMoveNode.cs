using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x0200029A RID: 666
public class AbilityMoveNode : AbilityNode
{
	// Token: 0x06001958 RID: 6488 RVA: 0x0009DEF4 File Offset: 0x0009C0F4
	internal override AbilityState Run(EffectProperties props)
	{
		if (this.Speed <= 0f)
		{
			return AbilityState.Finished;
		}
		EntityControl sourceControl = props.SourceControl;
		AbilityState result = AbilityState.Finished;
		AIStationaryMovement aistationaryMovement = sourceControl.movement as AIStationaryMovement;
		if (aistationaryMovement != null && !this.startedMove)
		{
			this.startedMove = true;
			this.finishedMove = true;
			Location location = (this.Loc != null) ? (this.Loc as LocationNode).GetLocation(props) : new Location();
			aistationaryMovement.SetPositionImmediate(location.GetPosition(props), aistationaryMovement.GetForward(), true);
		}
		if (!sourceControl.movement.CanInterruptMovement())
		{
			return result;
		}
		if (!this.startedMove)
		{
			this.startedMove = true;
			this.canceledMove = false;
			this.forceMoveTime = 0f;
			this.forceMoveDist = 0f;
			this.forceMoveStart = sourceControl.movement.GetPosition();
			Location loc = (this.Loc != null) ? (this.Loc as LocationNode).GetLocation(props) : new Location();
			this.forceTargetPoint = AbilityMoveNode.GetTargetPoint(sourceControl, props, loc, this.UseNavMesh, this.IgnoreObstacles);
			result = AbilityState.Running;
			Vector3 b = this.forceTargetPoint;
			b.y = this.forceMoveStart.y;
			if (this.forceTargetPoint.IsValid())
			{
				this.forceMoveLength = Vector3.Distance(this.forceMoveStart, b);
			}
			this.DisableNav(sourceControl);
		}
		else
		{
			if (this.canceledMove)
			{
				using (List<Node>.Enumerator enumerator = this.OnCanceled.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((AbilityNode)enumerator.Current).DoUpdate(props) == AbilityState.Running)
						{
							result = AbilityState.Running;
						}
					}
					return result;
				}
			}
			if (!this.finishedMove)
			{
				if (!this.forceTargetPoint.IsValid())
				{
					this.finishedMove = true;
					this.EnableNav(sourceControl);
					return AbilityState.Running;
				}
				float num = this.forceMoveDist / Mathf.Max(this.forceMoveLength, 0.1f);
				Vector3 position = sourceControl.movement.GetPosition();
				position.y = this.forceTargetPoint.y;
				Vector3 vector = Vector3.MoveTowards(position, this.forceTargetPoint, this.Speed * this.speedCurve.Evaluate(this.forceMoveTime) * GameplayManager.deltaTime);
				Vector3 vector2 = vector - position;
				vector2.y = 0f;
				this.forceMoveDist += vector2.magnitude;
				this.forceMoveTime += GameplayManager.deltaTime;
				float num2 = (num < 1f) ? this.HeightCurve.Evaluate(num) : 0f;
				float y = Vector3.Lerp(this.forceMoveStart, this.forceTargetPoint, num).y + num2;
				vector.y = y;
				Vector3 vector3 = vector - sourceControl.movement.GetPosition();
				Vector3 vector4 = sourceControl.movement.GetForward();
				if (this.RotateEntity)
				{
					Vector3 target = (sourceControl.movement is AIFlyingMovement) ? vector3.normalized : Vector3.ProjectOnPlane(vector3.normalized, Vector3.up);
					vector4 = Vector3.RotateTowards(vector4, target, Time.deltaTime * this.RotateSpeed * 0.017453292f, 0f);
				}
				AIGroundMovement aigroundMovement = sourceControl.movement as AIGroundMovement;
				if (aigroundMovement != null)
				{
					aigroundMovement.overrideVel = (this.forceTargetPoint - this.forceMoveStart).normalized * (this.Speed * this.speedCurve.Evaluate(this.forceMoveTime));
				}
				sourceControl.movement.SetPositionImmediate(vector, this.RotateEntity ? vector4 : Vector3.zero, true);
				if (num >= 0.995f || Vector3.Distance(vector, this.forceTargetPoint) < 0.05f || (num > 0.9f && vector2.magnitude < 0.01f))
				{
					this.finishedMove = true;
					this.EnableNav(sourceControl);
				}
				result = AbilityState.Running;
			}
			else
			{
				using (List<Node>.Enumerator enumerator = this.OnLand.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((AbilityNode)enumerator.Current).DoUpdate(props) == AbilityState.Running)
						{
							result = AbilityState.Running;
						}
					}
				}
				if (!this.didOverrideRun)
				{
					foreach (ModOverrideNode modOverrideNode in props.SourceControl.AllAugments(true, null).GetOverrides(props, this))
					{
						foreach (Node node in ((MovementOverrideNode)modOverrideNode).OnLand)
						{
							((EffectNode)node).Apply(props.Copy(false));
						}
					}
					this.didOverrideRun = true;
				}
			}
		}
		return result;
	}

	// Token: 0x06001959 RID: 6489 RVA: 0x0009E3DC File Offset: 0x0009C5DC
	private void DisableNav(EntityControl mover)
	{
		mover.movement.DisableMover();
	}

	// Token: 0x0600195A RID: 6490 RVA: 0x0009E3E9 File Offset: 0x0009C5E9
	private void EnableNav(EntityControl mover)
	{
		mover.movement.EnableMover();
	}

	// Token: 0x0600195B RID: 6491 RVA: 0x0009E3F6 File Offset: 0x0009C5F6
	internal override void OnCancel(EffectProperties props)
	{
		if (this.canceledMove)
		{
			return;
		}
		this.canceledMove = true;
		this.EnableNav(props.SourceControl);
		base.OnCancel(props);
	}

	// Token: 0x0600195C RID: 6492 RVA: 0x0009E41B File Offset: 0x0009C61B
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Move",
			AllowMultipleInputs = true,
			MinInspectorSize = new Vector2(250f, 0f)
		};
	}

	// Token: 0x0600195D RID: 6493 RVA: 0x0009E44C File Offset: 0x0009C64C
	public static Vector3 GetTargetPoint(EntityControl mover, EffectProperties props, Location loc, bool useNavMesh, bool ignoreObsticles)
	{
		if (loc.NeedsTarget() && mover.currentTarget == null)
		{
			return Vector3.one.INVALID();
		}
		Vector3 position = mover.movement.GetPosition();
		Vector3 position2 = loc.GetPosition(props);
		Debug.DrawLine(mover.movement.GetPosition(), position2, Color.blue, 1f);
		if (!(mover.movement is AIFlyingMovement))
		{
			if (mover.movement is AIGroundMovement)
			{
				return AbilityMoveNode.GetNavPosition(position, position2, ignoreObsticles);
			}
			if (mover.movement is PlayerMovement)
			{
				if (useNavMesh)
				{
					return AbilityMoveNode.GetNavPosition(position, position2, ignoreObsticles);
				}
				if (ignoreObsticles)
				{
					return position2;
				}
				return AbilityMoveNode.GetStraightlineValid(mover.display.CenterOfMass.position, position2, Mathf.Abs(mover.display.CenterOfMass.position.y - position.y));
			}
		}
		return Vector3.one.INVALID();
	}

	// Token: 0x0600195E RID: 6494 RVA: 0x0009E530 File Offset: 0x0009C730
	public static Vector3 GetNavPosition(Vector3 startPt, Vector3 wantPt, bool ignoreObsticles)
	{
		Vector3 vector = AIManager.NearestNavPoint(wantPt, -1f);
		if (!vector.IsValid())
		{
			return Vector3.one.INVALID();
		}
		wantPt = vector;
		if (ignoreObsticles)
		{
			return vector;
		}
		NavMeshHit navMeshHit;
		if (NavMesh.Raycast(startPt, wantPt, out navMeshHit, -1))
		{
			return navMeshHit.position;
		}
		return wantPt;
	}

	// Token: 0x0600195F RID: 6495 RVA: 0x0009E57C File Offset: 0x0009C77C
	public static Vector3 GetStraightlineValid(Vector3 startPt, Vector3 wantPt, float verticalOffset)
	{
		Vector3 vector = wantPt - startPt;
		Vector3 a = startPt - vector.normalized * 0.2f;
		Ray ray = new Ray(a - vector.normalized * 0.3f, vector.normalized);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, vector.magnitude + 0.3f, PlayerNavManager.instance.MovementCastMask))
		{
			wantPt = raycastHit.point - vector.normalized * 0.3f;
		}
		else if (Physics.SphereCast(ray, 1f, out raycastHit, vector.magnitude + 0.3f, PlayerNavManager.instance.MovementCastMask))
		{
			wantPt = raycastHit.point - vector.normalized * 1f;
		}
		if (Scene_Settings.instance != null && Scene_Settings.instance.SceneTerrain != null)
		{
			float num = Scene_Settings.instance.SceneTerrain.SampleHeight(wantPt);
			float num2 = Scene_Settings.instance.SceneTerrain.transform.position.y + num;
			if (wantPt.y < num2)
			{
				wantPt.y = num2 + 0.01f;
			}
		}
		if (Physics.Raycast(wantPt, Vector3.down, out raycastHit, verticalOffset, PlayerNavManager.instance.MovementCastMask))
		{
			verticalOffset = raycastHit.distance;
		}
		wantPt += Vector3.down * verticalOffset;
		return wantPt;
	}

	// Token: 0x06001960 RID: 6496 RVA: 0x0009E704 File Offset: 0x0009C904
	public AbilityMoveNode()
	{
	}

	// Token: 0x0400197E RID: 6526
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(LocationNode), false, "Location", PortLocation.Header)]
	public Node Loc;

	// Token: 0x0400197F RID: 6527
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AbilityNode), true, "On Land", PortLocation.Default)]
	public List<Node> OnLand = new List<Node>();

	// Token: 0x04001980 RID: 6528
	[HideInInspector]
	[SerializeField]
	[OutputPort(typeof(AbilityNode), true, "On Cancel", PortLocation.Default)]
	public List<Node> OnCanceled = new List<Node>();

	// Token: 0x04001981 RID: 6529
	public bool IgnoreObstacles;

	// Token: 0x04001982 RID: 6530
	public bool UseNavMesh = true;

	// Token: 0x04001983 RID: 6531
	public float Speed = 5f;

	// Token: 0x04001984 RID: 6532
	public AnimationCurve speedCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x04001985 RID: 6533
	public bool RotateEntity;

	// Token: 0x04001986 RID: 6534
	[Range(1f, 360f)]
	public float RotateSpeed;

	// Token: 0x04001987 RID: 6535
	public AnimationCurve HeightCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 0f)
	});

	// Token: 0x04001988 RID: 6536
	private bool startedMove;

	// Token: 0x04001989 RID: 6537
	private bool finishedMove;

	// Token: 0x0400198A RID: 6538
	private bool canceledMove;

	// Token: 0x0400198B RID: 6539
	private bool didOverrideRun;

	// Token: 0x0400198C RID: 6540
	private Vector3 forceMoveStart;

	// Token: 0x0400198D RID: 6541
	private float forceMoveLength;

	// Token: 0x0400198E RID: 6542
	private float forceMoveDist;

	// Token: 0x0400198F RID: 6543
	private float forceMoveTime;

	// Token: 0x04001990 RID: 6544
	private Vector3 forceTargetPoint;

	// Token: 0x0200063D RID: 1597
	[Serializable]
	public class TargetPoint
	{
		// Token: 0x060027A2 RID: 10146 RVA: 0x000D6958 File Offset: 0x000D4B58
		public Vector3 CalculatePoint(EntityControl Mover)
		{
			if (this.NeedsTarget() && (!(Mover is AIControl) || (Mover as AIControl).currentTarget == null))
			{
				return Vector3.one.INVALID();
			}
			Vector3 normalized = new Vector3(this.Horizontal, 0f, this.Forward).normalized;
			Vector3 position = Mover.movement.GetPosition();
			Vector3 vector = Vector3.one.INVALID();
			switch (this.Direction)
			{
			case AbilityMoveNode.TargetPoint.DirectionRef.SelfForward:
				vector = Mover.movement.GetForward();
				break;
			case AbilityMoveNode.TargetPoint.DirectionRef.SelfVelocity:
				vector = Vector3.ProjectOnPlane(Mover.movement.GetVelocity().normalized, Vector3.up);
				if (vector.magnitude == 0f)
				{
					vector = Mover.movement.GetForward();
				}
				break;
			case AbilityMoveNode.TargetPoint.DirectionRef.ToTarget:
				vector = ((Mover as AIControl).currentTarget.movement.GetPosition() - position).normalized;
				break;
			case AbilityMoveNode.TargetPoint.DirectionRef.TargetForward:
				vector = (Mover as AIControl).currentTarget.movement.GetForward();
				break;
			case AbilityMoveNode.TargetPoint.DirectionRef.TargetVelocity:
				vector = Vector3.ProjectOnPlane((Mover as AIControl).currentTarget.movement.GetVelocity().normalized, Vector3.up);
				if (vector.magnitude == 0f)
				{
					vector = (Mover as AIControl).currentTarget.movement.GetForward();
				}
				break;
			}
			if (!vector.IsValid())
			{
				return Vector3.one.INVALID();
			}
			Debug.DrawLine(Mover.display.CenterOfMass.position, Mover.display.CenterOfMass.position + vector, Color.green, 2f);
			if (Mover.movement is AIFlyingMovement)
			{
				return Vector3.one.INVALID();
			}
			Vector3 normalized2 = MathHelper.GetRelativeDir(normalized, vector, Vector3.up).normalized;
			Vector3 vector2 = AIManager.NearestNavPoint(position + normalized2 * this.Distance, this.Distance);
			if (!vector2.IsValid())
			{
				return Vector3.one.INVALID();
			}
			Vector3 vector3 = vector2;
			if (this.IgnoreObstacles)
			{
				return vector2;
			}
			NavMeshHit navMeshHit;
			if (NavMesh.Raycast(position, vector3, out navMeshHit, -1))
			{
				return navMeshHit.position;
			}
			return vector3;
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000D6BA1 File Offset: 0x000D4DA1
		public bool NeedsTarget()
		{
			return this.Direction >= AbilityMoveNode.TargetPoint.DirectionRef.ToTarget;
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x000D6BAF File Offset: 0x000D4DAF
		public TargetPoint()
		{
		}

		// Token: 0x04002A98 RID: 10904
		public AbilityMoveNode.TargetPoint.DirectionRef Direction;

		// Token: 0x04002A99 RID: 10905
		[Range(-1f, 1f)]
		public float Horizontal;

		// Token: 0x04002A9A RID: 10906
		[Range(-1f, 1f)]
		public float Forward = 1f;

		// Token: 0x04002A9B RID: 10907
		public float Distance = 2f;

		// Token: 0x04002A9C RID: 10908
		public bool IgnoreObstacles;

		// Token: 0x020006C4 RID: 1732
		public enum DirectionRef
		{
			// Token: 0x04002CE7 RID: 11495
			SelfForward,
			// Token: 0x04002CE8 RID: 11496
			SelfVelocity,
			// Token: 0x04002CE9 RID: 11497
			ToTarget,
			// Token: 0x04002CEA RID: 11498
			TargetForward,
			// Token: 0x04002CEB RID: 11499
			TargetVelocity
		}
	}
}
