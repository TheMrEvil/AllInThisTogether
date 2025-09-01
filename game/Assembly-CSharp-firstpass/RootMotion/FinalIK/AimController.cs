using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x0200010F RID: 271
	public class AimController : MonoBehaviour
	{
		// Token: 0x06000C19 RID: 3097 RVA: 0x00050E9C File Offset: 0x0004F09C
		private void Start()
		{
			this.lastPosition = this.ik.solver.IKPosition;
			this.dir = this.ik.solver.IKPosition - this.pivot;
			this.ik.solver.target = null;
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00050EF4 File Offset: 0x0004F0F4
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
				Vector3 vector = this.ik.solver.IKPosition - this.pivot;
				if (this.slerpSpeed > 0f)
				{
					this.dir = Vector3.Slerp(this.dir, vector, Time.deltaTime * this.slerpSpeed);
				}
				if (this.maxRadiansDelta > 0f || this.maxMagnitudeDelta > 0f)
				{
					this.dir = Vector3.RotateTowards(this.dir, vector, Time.deltaTime * this.maxRadiansDelta, this.maxMagnitudeDelta);
				}
				if (this.smoothDampTime > 0f)
				{
					float yaw = V3Tools.GetYaw(this.dir);
					float yaw2 = V3Tools.GetYaw(vector);
					float y = Mathf.SmoothDampAngle(yaw, yaw2, ref this.yawV, this.smoothDampTime);
					float pitch = V3Tools.GetPitch(this.dir);
					float pitch2 = V3Tools.GetPitch(vector);
					float x = Mathf.SmoothDampAngle(pitch, pitch2, ref this.pitchV, this.smoothDampTime);
					float d = Mathf.SmoothDamp(this.dir.magnitude, vector.magnitude, ref this.dirMagV, this.smoothDampTime);
					this.dir = Quaternion.Euler(x, y, 0f) * Vector3.forward * d;
				}
				this.ik.solver.IKPosition = this.pivot + this.dir;
			}
			this.ApplyMinDistance();
			this.RootRotation();
			if (this.useAnimatedAimDirection)
			{
				this.ik.solver.axis = this.ik.solver.transform.InverseTransformVector(this.ik.transform.rotation * this.animatedAimDirection);
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x00051336 File Offset: 0x0004F536
		private Vector3 pivot
		{
			get
			{
				return this.ik.transform.position + this.ik.transform.rotation * this.pivotOffsetFromRoot;
			}
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00051368 File Offset: 0x0004F568
		private void ApplyMinDistance()
		{
			Vector3 pivot = this.pivot;
			Vector3 b = this.ik.solver.IKPosition - pivot;
			b = b.normalized * Mathf.Max(b.magnitude, this.minDistance);
			this.ik.solver.IKPosition = pivot + b;
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x000513CC File Offset: 0x0004F5CC
		private void RootRotation()
		{
			float num = Mathf.Lerp(180f, this.maxRootAngle * this.turnToTargetMlp, this.ik.solver.IKPositionWeight);
			if (num < 180f)
			{
				Vector3 vector = Quaternion.Inverse(this.ik.transform.rotation) * (this.ik.solver.IKPosition - this.pivot);
				float num2 = Mathf.Atan2(vector.x, vector.z) * 57.29578f;
				float angle = 0f;
				if (num2 > num)
				{
					angle = num2 - num;
					if (!this.turningToTarget && this.turnToTarget)
					{
						base.StartCoroutine(this.TurnToTarget());
					}
				}
				if (num2 < -num)
				{
					angle = num2 + num;
					if (!this.turningToTarget && this.turnToTarget)
					{
						base.StartCoroutine(this.TurnToTarget());
					}
				}
				this.ik.transform.rotation = Quaternion.AngleAxis(angle, this.ik.transform.up) * this.ik.transform.rotation;
			}
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x000514E6 File Offset: 0x0004F6E6
		private IEnumerator TurnToTarget()
		{
			this.turningToTarget = true;
			while (this.turnToTargetMlp > 0f)
			{
				this.turnToTargetMlp = Mathf.SmoothDamp(this.turnToTargetMlp, 0f, ref this.turnToTargetMlpV, this.turnToTargetTime);
				if (this.turnToTargetMlp < 0.01f)
				{
					this.turnToTargetMlp = 0f;
				}
				yield return null;
			}
			this.turnToTargetMlp = 1f;
			this.turningToTarget = false;
			yield break;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x000514F8 File Offset: 0x0004F6F8
		public AimController()
		{
		}

		// Token: 0x0400095A RID: 2394
		[Tooltip("Reference to the AimIK component.")]
		public AimIK ik;

		// Token: 0x0400095B RID: 2395
		[Tooltip("Master weight of the IK solver.")]
		[Range(0f, 1f)]
		public float weight = 1f;

		// Token: 0x0400095C RID: 2396
		[Header("Target Smoothing")]
		[Tooltip("The target to aim at. Do not use the Target transform that is assigned to AimIK. Set to null if you wish to stop aiming.")]
		public Transform target;

		// Token: 0x0400095D RID: 2397
		[Tooltip("The time it takes to switch targets.")]
		public float targetSwitchSmoothTime = 0.3f;

		// Token: 0x0400095E RID: 2398
		[Tooltip("The time it takes to blend in/out of AimIK weight.")]
		public float weightSmoothTime = 0.3f;

		// Token: 0x0400095F RID: 2399
		[Header("Turning Towards The Target")]
		[Tooltip("Enables smooth turning towards the target according to the parameters under this header.")]
		public bool smoothTurnTowardsTarget = true;

		// Token: 0x04000960 RID: 2400
		[Tooltip("Speed of turning towards the target using Vector3.RotateTowards.")]
		public float maxRadiansDelta = 3f;

		// Token: 0x04000961 RID: 2401
		[Tooltip("Speed of moving towards the target using Vector3.RotateTowards.")]
		public float maxMagnitudeDelta = 3f;

		// Token: 0x04000962 RID: 2402
		[Tooltip("Speed of slerping towards the target.")]
		public float slerpSpeed = 3f;

		// Token: 0x04000963 RID: 2403
		[Tooltip("Smoothing time for turning towards the yaw and pitch of the target using Mathf.SmoothDampAngle. Value of 0 means smooth damping is disabled.")]
		public float smoothDampTime;

		// Token: 0x04000964 RID: 2404
		[Tooltip("The position of the pivot that the aim target is rotated around relative to the root of the character.")]
		public Vector3 pivotOffsetFromRoot = Vector3.up;

		// Token: 0x04000965 RID: 2405
		[Tooltip("Minimum distance of aiming from the first bone. Keeps the solver from failing if the target is too close.")]
		public float minDistance = 1f;

		// Token: 0x04000966 RID: 2406
		[Tooltip("Offset applied to the target in world space. Convenient for scripting aiming inaccuracy.")]
		public Vector3 offset;

		// Token: 0x04000967 RID: 2407
		[Header("RootRotation")]
		[Tooltip("Character root will be rotate around the Y axis to keep root forward within this angle from the aiming direction.")]
		[Range(0f, 180f)]
		public float maxRootAngle = 45f;

		// Token: 0x04000968 RID: 2408
		[Tooltip("If enabled, aligns the root forward to target direction after 'Max Root Angle' has been exceeded.")]
		public bool turnToTarget;

		// Token: 0x04000969 RID: 2409
		[Tooltip("The time of turning towards the target direction if 'Max Root Angle has been exceeded and 'Turn To Target' is enabled.")]
		public float turnToTargetTime = 0.2f;

		// Token: 0x0400096A RID: 2410
		[Header("Mode")]
		[Tooltip("If true, AimIK will consider whatever the current direction of the weapon to be the forward aiming direction and work additively on top of that. This enables you to use recoil and reloading animations seamlessly with AimIK. Adjust the Vector3 value below if the weapon is not aiming perfectly forward in the aiming animation clip.")]
		public bool useAnimatedAimDirection;

		// Token: 0x0400096B RID: 2411
		[Tooltip("The direction of the animated weapon aiming in character space. Tweak this value to adjust the aiming. 'Use Animated Aim Direction' must be enabled for this property to work.")]
		public Vector3 animatedAimDirection = Vector3.forward;

		// Token: 0x0400096C RID: 2412
		private Transform lastTarget;

		// Token: 0x0400096D RID: 2413
		private float switchWeight;

		// Token: 0x0400096E RID: 2414
		private float switchWeightV;

		// Token: 0x0400096F RID: 2415
		private float weightV;

		// Token: 0x04000970 RID: 2416
		private Vector3 lastPosition;

		// Token: 0x04000971 RID: 2417
		private Vector3 dir;

		// Token: 0x04000972 RID: 2418
		private bool lastSmoothTowardsTarget;

		// Token: 0x04000973 RID: 2419
		private bool turningToTarget;

		// Token: 0x04000974 RID: 2420
		private float turnToTargetMlp = 1f;

		// Token: 0x04000975 RID: 2421
		private float turnToTargetMlpV;

		// Token: 0x04000976 RID: 2422
		private float yawV;

		// Token: 0x04000977 RID: 2423
		private float pitchV;

		// Token: 0x04000978 RID: 2424
		private float dirMagV;

		// Token: 0x02000218 RID: 536
		[CompilerGenerated]
		private sealed class <TurnToTarget>d__37 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06001143 RID: 4419 RVA: 0x0006C10F File Offset: 0x0006A30F
			[DebuggerHidden]
			public <TurnToTarget>d__37(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06001144 RID: 4420 RVA: 0x0006C11E File Offset: 0x0006A31E
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06001145 RID: 4421 RVA: 0x0006C120 File Offset: 0x0006A320
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				AimController aimController = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
				}
				else
				{
					this.<>1__state = -1;
					aimController.turningToTarget = true;
				}
				if (aimController.turnToTargetMlp <= 0f)
				{
					aimController.turnToTargetMlp = 1f;
					aimController.turningToTarget = false;
					return false;
				}
				aimController.turnToTargetMlp = Mathf.SmoothDamp(aimController.turnToTargetMlp, 0f, ref aimController.turnToTargetMlpV, aimController.turnToTargetTime);
				if (aimController.turnToTargetMlp < 0.01f)
				{
					aimController.turnToTargetMlp = 0f;
				}
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x1700024B RID: 587
			// (get) Token: 0x06001146 RID: 4422 RVA: 0x0006C1C5 File Offset: 0x0006A3C5
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001147 RID: 4423 RVA: 0x0006C1CD File Offset: 0x0006A3CD
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700024C RID: 588
			// (get) Token: 0x06001148 RID: 4424 RVA: 0x0006C1D4 File Offset: 0x0006A3D4
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000FF1 RID: 4081
			private int <>1__state;

			// Token: 0x04000FF2 RID: 4082
			private object <>2__current;

			// Token: 0x04000FF3 RID: 4083
			public AimController <>4__this;
		}
	}
}
