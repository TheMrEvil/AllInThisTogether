using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

namespace RootMotion.Demos
{
	// Token: 0x02000163 RID: 355
	[Serializable]
	public class Navigator
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x0005C54D File Offset: 0x0005A74D
		// (set) Token: 0x06000DA2 RID: 3490 RVA: 0x0005C555 File Offset: 0x0005A755
		public Vector3 normalizedDeltaPosition
		{
			[CompilerGenerated]
			get
			{
				return this.<normalizedDeltaPosition>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<normalizedDeltaPosition>k__BackingField = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x0005C55E File Offset: 0x0005A75E
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x0005C566 File Offset: 0x0005A766
		public Navigator.State state
		{
			[CompilerGenerated]
			get
			{
				return this.<state>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<state>k__BackingField = value;
			}
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0005C570 File Offset: 0x0005A770
		public void Initiate(Transform transform)
		{
			this.transform = transform;
			this.path = new NavMeshPath();
			this.initiated = true;
			this.cornerIndex = 0;
			this.corners = new Vector3[0];
			this.state = Navigator.State.Idle;
			this.lastTargetPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0005C5CC File Offset: 0x0005A7CC
		public void Update(Vector3 targetPosition)
		{
			if (!this.initiated)
			{
				Debug.LogError("Trying to update an uninitiated Navigator.");
				return;
			}
			switch (this.state)
			{
			case Navigator.State.Idle:
				if (this.activeTargetSeeking && Time.time > this.nextPathTime)
				{
					this.CalculatePath(targetPosition);
				}
				break;
			case Navigator.State.Seeking:
				this.normalizedDeltaPosition = Vector3.zero;
				if (this.path.status == NavMeshPathStatus.PathComplete)
				{
					this.corners = this.path.corners;
					this.cornerIndex = 0;
					if (this.corners.Length == 0)
					{
						Debug.LogWarning("Zero Corner Path", this.transform);
						this.Stop();
					}
					else
					{
						this.state = Navigator.State.OnPath;
					}
				}
				if (this.path.status == NavMeshPathStatus.PathPartial)
				{
					Debug.LogWarning("Path Partial", this.transform);
				}
				if (this.path.status == NavMeshPathStatus.PathInvalid)
				{
					Debug.LogWarning("Path Invalid", this.transform);
					return;
				}
				break;
			case Navigator.State.OnPath:
				if (this.activeTargetSeeking && Time.time > this.nextPathTime && this.HorDistance(targetPosition, this.lastTargetPosition) > this.recalculateOnPathDistance)
				{
					this.CalculatePath(targetPosition);
					return;
				}
				if (this.cornerIndex < this.corners.Length)
				{
					Vector3 a = this.corners[this.cornerIndex] - this.transform.position;
					a.y = 0f;
					float magnitude = a.magnitude;
					if (magnitude > 0f)
					{
						this.normalizedDeltaPosition = a / a.magnitude;
					}
					else
					{
						this.normalizedDeltaPosition = Vector3.zero;
					}
					if (magnitude < this.cornerRadius)
					{
						this.cornerIndex++;
						if (this.cornerIndex >= this.corners.Length)
						{
							this.Stop();
							return;
						}
					}
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0005C789 File Offset: 0x0005A989
		private void CalculatePath(Vector3 targetPosition)
		{
			if (this.Find(targetPosition))
			{
				this.lastTargetPosition = targetPosition;
				this.state = Navigator.State.Seeking;
			}
			else
			{
				this.Stop();
			}
			this.nextPathTime = Time.time + this.nextPathInterval;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0005C7BC File Offset: 0x0005A9BC
		private bool Find(Vector3 targetPosition)
		{
			if (this.HorDistance(this.transform.position, targetPosition) < this.cornerRadius * 2f)
			{
				return false;
			}
			if (NavMesh.CalculatePath(this.transform.position, targetPosition, -1, this.path))
			{
				return true;
			}
			NavMeshHit navMeshHit = default(NavMeshHit);
			return NavMesh.SamplePosition(targetPosition, out navMeshHit, this.maxSampleDistance, -1) && NavMesh.CalculatePath(this.transform.position, navMeshHit.position, -1, this.path);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0005C843 File Offset: 0x0005AA43
		private void Stop()
		{
			this.state = Navigator.State.Idle;
			this.normalizedDeltaPosition = Vector3.zero;
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0005C857 File Offset: 0x0005AA57
		private float HorDistance(Vector3 p1, Vector3 p2)
		{
			return Vector2.Distance(new Vector2(p1.x, p1.z), new Vector2(p2.x, p2.z));
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0005C880 File Offset: 0x0005AA80
		public void Visualize()
		{
			if (this.state == Navigator.State.Idle)
			{
				Gizmos.color = Color.gray;
			}
			if (this.state == Navigator.State.Seeking)
			{
				Gizmos.color = Color.red;
			}
			if (this.state == Navigator.State.OnPath)
			{
				Gizmos.color = Color.green;
			}
			if (this.corners.Length != 0 && this.state == Navigator.State.OnPath && this.cornerIndex == 0)
			{
				Gizmos.DrawLine(this.transform.position, this.corners[0]);
			}
			for (int i = 0; i < this.corners.Length; i++)
			{
				Gizmos.DrawSphere(this.corners[i], 0.1f);
			}
			if (this.corners.Length > 1)
			{
				for (int j = 0; j < this.corners.Length - 1; j++)
				{
					Gizmos.DrawLine(this.corners[j], this.corners[j + 1]);
				}
			}
			Gizmos.color = Color.white;
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0005C96D File Offset: 0x0005AB6D
		public Navigator()
		{
		}

		// Token: 0x04000B97 RID: 2967
		[Tooltip("Should this Navigator be actively seeking a path.")]
		public bool activeTargetSeeking;

		// Token: 0x04000B98 RID: 2968
		[Tooltip("Increase this value if the character starts running in a circle, not able to reach the corner because of a too large turning radius.")]
		public float cornerRadius = 0.5f;

		// Token: 0x04000B99 RID: 2969
		[Tooltip("Recalculate path if target position has moved by this distance from the position it was at when the path was originally calculated")]
		public float recalculateOnPathDistance = 1f;

		// Token: 0x04000B9A RID: 2970
		[Tooltip("Sample within this distance from sourcePosition.")]
		public float maxSampleDistance = 5f;

		// Token: 0x04000B9B RID: 2971
		[Tooltip("Interval of updating the path")]
		public float nextPathInterval = 3f;

		// Token: 0x04000B9C RID: 2972
		[CompilerGenerated]
		private Vector3 <normalizedDeltaPosition>k__BackingField;

		// Token: 0x04000B9D RID: 2973
		[CompilerGenerated]
		private Navigator.State <state>k__BackingField;

		// Token: 0x04000B9E RID: 2974
		private Transform transform;

		// Token: 0x04000B9F RID: 2975
		private int cornerIndex;

		// Token: 0x04000BA0 RID: 2976
		private Vector3[] corners = new Vector3[0];

		// Token: 0x04000BA1 RID: 2977
		private NavMeshPath path;

		// Token: 0x04000BA2 RID: 2978
		private Vector3 lastTargetPosition;

		// Token: 0x04000BA3 RID: 2979
		private bool initiated;

		// Token: 0x04000BA4 RID: 2980
		private float nextPathTime;

		// Token: 0x0200023E RID: 574
		public enum State
		{
			// Token: 0x040010D5 RID: 4309
			Idle,
			// Token: 0x040010D6 RID: 4310
			Seeking,
			// Token: 0x040010D7 RID: 4311
			OnPath
		}
	}
}
