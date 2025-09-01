using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000119 RID: 281
	public class LookAtController : MonoBehaviour
	{
		// Token: 0x06000C44 RID: 3140 RVA: 0x00052161 File Offset: 0x00050361
		private void Start()
		{
			this.lastPosition = this.ik.solver.IKPosition;
			this.dir = this.ik.solver.IKPosition - this.pivot;
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0005219C File Offset: 0x0005039C
		private void LateUpdate()
		{
			if (this.target != this.lastTarget)
			{
				if (this.lastTarget == null && this.target != null && this.ik.solver.IKPositionWeight <= 0f)
				{
					this.lastPosition = this.target.position;
					this.dir = this.target.position - this.pivot;
					this.ik.solver.IKPosition = this.target.position + this.offset;
				}
				else
				{
					this.lastPosition = this.ik.solver.IKPosition;
					this.dir = this.ik.solver.IKPosition - this.pivot;
				}
				this.switchWeight = 0f;
				this.lastTarget = this.target;
			}
			float num = (this.target != null) ? this.weight : 0f;
			this.ik.solver.IKPositionWeight = Mathf.SmoothDamp(this.ik.solver.IKPositionWeight, num, ref this.weightV, this.weightSmoothTime);
			if (this.ik.solver.IKPositionWeight >= 0.999f && num > this.ik.solver.IKPositionWeight)
			{
				this.ik.solver.IKPositionWeight = 1f;
			}
			if (this.ik.solver.IKPositionWeight <= 0.001f && num < this.ik.solver.IKPositionWeight)
			{
				this.ik.solver.IKPositionWeight = 0f;
			}
			if (this.ik.solver.IKPositionWeight <= 0f)
			{
				return;
			}
			this.switchWeight = Mathf.SmoothDamp(this.switchWeight, 1f, ref this.switchWeightV, this.targetSwitchSmoothTime);
			if (this.switchWeight >= 0.999f)
			{
				this.switchWeight = 1f;
			}
			if (this.target != null)
			{
				this.ik.solver.IKPosition = Vector3.Lerp(this.lastPosition, this.target.position + this.offset, this.switchWeight);
			}
			if (this.smoothTurnTowardsTarget != this.lastSmoothTowardsTarget)
			{
				this.dir = this.ik.solver.IKPosition - this.pivot;
				this.lastSmoothTowardsTarget = this.smoothTurnTowardsTarget;
			}
			if (this.smoothTurnTowardsTarget)
			{
				Vector3 b = this.ik.solver.IKPosition - this.pivot;
				this.dir = Vector3.Slerp(this.dir, b, Time.deltaTime * this.slerpSpeed);
				this.dir = Vector3.RotateTowards(this.dir, b, Time.deltaTime * this.maxRadiansDelta, this.maxMagnitudeDelta);
				this.ik.solver.IKPosition = this.pivot + this.dir;
			}
			this.ApplyMinDistance();
			this.RootRotation();
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x000524C6 File Offset: 0x000506C6
		private Vector3 pivot
		{
			get
			{
				return this.ik.transform.position + this.ik.transform.rotation * this.pivotOffsetFromRoot;
			}
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x000524F8 File Offset: 0x000506F8
		private void ApplyMinDistance()
		{
			Vector3 pivot = this.pivot;
			Vector3 b = this.ik.solver.IKPosition - pivot;
			b = b.normalized * Mathf.Max(b.magnitude, this.minDistance);
			this.ik.solver.IKPosition = pivot + b;
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0005255C File Offset: 0x0005075C
		private void RootRotation()
		{
			float num = Mathf.Lerp(180f, this.maxRootAngle, this.ik.solver.IKPositionWeight);
			if (num < 180f)
			{
				Vector3 vector = Quaternion.Inverse(this.ik.transform.rotation) * (this.ik.solver.IKPosition - this.pivot);
				float num2 = Mathf.Atan2(vector.x, vector.z) * 57.29578f;
				float angle = 0f;
				if (num2 > num)
				{
					angle = num2 - num;
				}
				if (num2 < -num)
				{
					angle = num2 + num;
				}
				this.ik.transform.rotation = Quaternion.AngleAxis(angle, this.ik.transform.up) * this.ik.transform.rotation;
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00052638 File Offset: 0x00050838
		public LookAtController()
		{
		}

		// Token: 0x04000991 RID: 2449
		public LookAtIK ik;

		// Token: 0x04000992 RID: 2450
		[Header("Target Smoothing")]
		[Tooltip("The target to look at. Do not use the Target transform that is assigned to LookAtIK. Set to null if you wish to stop looking.")]
		public Transform target;

		// Token: 0x04000993 RID: 2451
		[Range(0f, 1f)]
		public float weight = 1f;

		// Token: 0x04000994 RID: 2452
		public Vector3 offset;

		// Token: 0x04000995 RID: 2453
		[Tooltip("The time it takes to switch targets.")]
		public float targetSwitchSmoothTime = 0.3f;

		// Token: 0x04000996 RID: 2454
		[Tooltip("The time it takes to blend in/out of LookAtIK weight.")]
		public float weightSmoothTime = 0.3f;

		// Token: 0x04000997 RID: 2455
		[Header("Turning Towards The Target")]
		[Tooltip("Enables smooth turning towards the target according to the parameters under this header.")]
		public bool smoothTurnTowardsTarget = true;

		// Token: 0x04000998 RID: 2456
		[Tooltip("Speed of turning towards the target using Vector3.RotateTowards.")]
		public float maxRadiansDelta = 3f;

		// Token: 0x04000999 RID: 2457
		[Tooltip("Speed of moving towards the target using Vector3.RotateTowards.")]
		public float maxMagnitudeDelta = 3f;

		// Token: 0x0400099A RID: 2458
		[Tooltip("Speed of slerping towards the target.")]
		public float slerpSpeed = 3f;

		// Token: 0x0400099B RID: 2459
		[Tooltip("The position of the pivot that the look at target is rotated around relative to the root of the character.")]
		public Vector3 pivotOffsetFromRoot = Vector3.up;

		// Token: 0x0400099C RID: 2460
		[Tooltip("Minimum distance of looking from the first bone. Keeps the solver from failing if the target is too close.")]
		public float minDistance = 1f;

		// Token: 0x0400099D RID: 2461
		[Header("RootRotation")]
		[Tooltip("Character root will be rotate around the Y axis to keep root forward within this angle from the look direction.")]
		[Range(0f, 180f)]
		public float maxRootAngle = 45f;

		// Token: 0x0400099E RID: 2462
		private Transform lastTarget;

		// Token: 0x0400099F RID: 2463
		private float switchWeight;

		// Token: 0x040009A0 RID: 2464
		private float switchWeightV;

		// Token: 0x040009A1 RID: 2465
		private float weightV;

		// Token: 0x040009A2 RID: 2466
		private Vector3 lastPosition;

		// Token: 0x040009A3 RID: 2467
		private Vector3 dir;

		// Token: 0x040009A4 RID: 2468
		private bool lastSmoothTowardsTarget;
	}
}
