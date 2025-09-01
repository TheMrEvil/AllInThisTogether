using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x0200011E RID: 286
	public class Recoil : OffsetModifier
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00052997 File Offset: 0x00050B97
		public bool isFinished
		{
			get
			{
				return Time.time > this.endTime;
			}
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x000529A6 File Offset: 0x00050BA6
		public void SetHandRotations(Quaternion leftHandRotation, Quaternion rightHandRotation)
		{
			if (this.handedness == Recoil.Handedness.Left)
			{
				this.primaryHandRotation = leftHandRotation;
			}
			else
			{
				this.primaryHandRotation = rightHandRotation;
			}
			this.handRotationsSet = true;
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x000529C8 File Offset: 0x00050BC8
		public void Fire(float magnitude)
		{
			float num = magnitude * UnityEngine.Random.value * this.magnitudeRandom;
			this.magnitudeMlp = magnitude + num;
			this.randomRotation = Quaternion.Euler(this.rotationRandom * UnityEngine.Random.value);
			Recoil.RecoilOffset[] array = this.offsets;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Start();
			}
			if (Time.time < this.endTime)
			{
				this.blendWeight = 0f;
			}
			else
			{
				this.blendWeight = 1f;
			}
			Keyframe[] keys = this.recoilWeight.keys;
			this.length = keys[keys.Length - 1].time;
			this.endTime = Time.time + this.length;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00052A80 File Offset: 0x00050C80
		protected override void OnModifyOffset()
		{
			if (this.aimIK != null)
			{
				this.aimIKAxis = this.aimIK.solver.axis;
			}
			if (Time.time >= this.endTime)
			{
				this.rotationOffset = Quaternion.identity;
				return;
			}
			if (!this.initiated && this.ik != null)
			{
				this.initiated = true;
				IKSolverFullBodyBiped solver = this.ik.solver;
				solver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPostUpdate, new IKSolver.UpdateDelegate(this.AfterFBBIK));
				if (this.aimIK != null)
				{
					IKSolverAim solver2 = this.aimIK.solver;
					solver2.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver2.OnPostUpdate, new IKSolver.UpdateDelegate(this.AfterAimIK));
				}
			}
			this.blendTime = Mathf.Max(this.blendTime, 0f);
			if (this.blendTime > 0f)
			{
				this.blendWeight = Mathf.Min(this.blendWeight + Time.deltaTime * (1f / this.blendTime), 1f);
			}
			else
			{
				this.blendWeight = 1f;
			}
			float b = this.recoilWeight.Evaluate(this.length - (this.endTime - Time.time)) * this.magnitudeMlp;
			this.w = Mathf.Lerp(this.w, b, this.blendWeight);
			Quaternion quaternion = (this.aimIK != null && this.aimIK.solver.transform != null && !this.aimIKSolvedLast) ? Quaternion.LookRotation(this.aimIK.solver.IKPosition - this.aimIK.solver.transform.position, this.ik.references.root.up) : this.ik.references.root.rotation;
			quaternion = this.randomRotation * quaternion;
			Recoil.RecoilOffset[] array = this.offsets;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Apply(this.ik.solver, quaternion, this.w, this.length, this.endTime - Time.time);
			}
			if (!this.handRotationsSet)
			{
				this.primaryHandRotation = this.primaryHand.rotation;
			}
			this.handRotationsSet = false;
			this.rotationOffset = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(this.randomRotation * this.primaryHandRotation * this.handRotationOffset), this.w);
			this.handRotation = this.rotationOffset * this.primaryHandRotation;
			if (this.twoHanded)
			{
				Vector3 point = Quaternion.Inverse(this.primaryHand.rotation) * (this.secondaryHand.position - this.primaryHand.position);
				this.secondaryHandRelativeRotation = Quaternion.Inverse(this.primaryHand.rotation) * this.secondaryHand.rotation;
				Vector3 a = this.primaryHand.position + this.primaryHandEffector.positionOffset + this.handRotation * point;
				this.secondaryHandEffector.positionOffset += a - (this.secondaryHand.position + this.secondaryHandEffector.positionOffset);
			}
			if (this.aimIK != null && this.aimIKSolvedLast)
			{
				this.aimIK.solver.axis = Quaternion.Inverse(this.ik.references.root.rotation) * Quaternion.Inverse(this.rotationOffset) * this.aimIKAxis;
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00052E50 File Offset: 0x00051050
		private void AfterFBBIK()
		{
			if (Time.time >= this.endTime)
			{
				return;
			}
			this.primaryHand.rotation = this.handRotation;
			if (this.twoHanded)
			{
				this.secondaryHand.rotation = this.primaryHand.rotation * this.secondaryHandRelativeRotation;
			}
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00052EA5 File Offset: 0x000510A5
		private void AfterAimIK()
		{
			if (this.aimIKSolvedLast)
			{
				this.aimIK.solver.axis = this.aimIKAxis;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00052EC5 File Offset: 0x000510C5
		private IKEffector primaryHandEffector
		{
			get
			{
				if (this.handedness == Recoil.Handedness.Right)
				{
					return this.ik.solver.rightHandEffector;
				}
				return this.ik.solver.leftHandEffector;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x00052EF0 File Offset: 0x000510F0
		private IKEffector secondaryHandEffector
		{
			get
			{
				if (this.handedness == Recoil.Handedness.Right)
				{
					return this.ik.solver.leftHandEffector;
				}
				return this.ik.solver.rightHandEffector;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00052F1B File Offset: 0x0005111B
		private Transform primaryHand
		{
			get
			{
				return this.primaryHandEffector.bone;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x00052F28 File Offset: 0x00051128
		private Transform secondaryHand
		{
			get
			{
				return this.secondaryHandEffector.bone;
			}
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00052F38 File Offset: 0x00051138
		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (this.ik != null && this.initiated)
			{
				IKSolverFullBodyBiped solver = this.ik.solver;
				solver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(solver.OnPostUpdate, new IKSolver.UpdateDelegate(this.AfterFBBIK));
				if (this.aimIK != null)
				{
					IKSolverAim solver2 = this.aimIK.solver;
					solver2.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(solver2.OnPostUpdate, new IKSolver.UpdateDelegate(this.AfterAimIK));
				}
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00052FC8 File Offset: 0x000511C8
		public Recoil()
		{
		}

		// Token: 0x040009AD RID: 2477
		[Tooltip("Reference to the AimIK component. Optional, only used to getting the aiming direction.")]
		public AimIK aimIK;

		// Token: 0x040009AE RID: 2478
		[Tooltip("Set this true if you are using IKExecutionOrder.cs or a custom script to force AimIK solve after FBBIK.")]
		public bool aimIKSolvedLast;

		// Token: 0x040009AF RID: 2479
		[Tooltip("Which hand is holding the weapon?")]
		public Recoil.Handedness handedness;

		// Token: 0x040009B0 RID: 2480
		[Tooltip("Check for 2-handed weapons.")]
		public bool twoHanded = true;

		// Token: 0x040009B1 RID: 2481
		[Tooltip("Weight curve for the recoil offsets. Recoil procedure is as long as this curve.")]
		public AnimationCurve recoilWeight;

		// Token: 0x040009B2 RID: 2482
		[Tooltip("How much is the magnitude randomized each time Recoil is called?")]
		public float magnitudeRandom = 0.1f;

		// Token: 0x040009B3 RID: 2483
		[Tooltip("How much is the rotation randomized each time Recoil is called?")]
		public Vector3 rotationRandom;

		// Token: 0x040009B4 RID: 2484
		[Tooltip("Rotating the primary hand bone for the recoil (in local space).")]
		public Vector3 handRotationOffset;

		// Token: 0x040009B5 RID: 2485
		[Tooltip("Time of blending in another recoil when doing automatic fire.")]
		public float blendTime;

		// Token: 0x040009B6 RID: 2486
		[Space(10f)]
		[Tooltip("FBBIK effector position offsets for the recoil (in aiming direction space).")]
		public Recoil.RecoilOffset[] offsets;

		// Token: 0x040009B7 RID: 2487
		[HideInInspector]
		public Quaternion rotationOffset = Quaternion.identity;

		// Token: 0x040009B8 RID: 2488
		private float magnitudeMlp = 1f;

		// Token: 0x040009B9 RID: 2489
		private float endTime = -1f;

		// Token: 0x040009BA RID: 2490
		private Quaternion handRotation;

		// Token: 0x040009BB RID: 2491
		private Quaternion secondaryHandRelativeRotation;

		// Token: 0x040009BC RID: 2492
		private Quaternion randomRotation;

		// Token: 0x040009BD RID: 2493
		private float length = 1f;

		// Token: 0x040009BE RID: 2494
		private bool initiated;

		// Token: 0x040009BF RID: 2495
		private float blendWeight;

		// Token: 0x040009C0 RID: 2496
		private float w;

		// Token: 0x040009C1 RID: 2497
		private Quaternion primaryHandRotation = Quaternion.identity;

		// Token: 0x040009C2 RID: 2498
		private bool handRotationsSet;

		// Token: 0x040009C3 RID: 2499
		private Vector3 aimIKAxis;

		// Token: 0x02000227 RID: 551
		[Serializable]
		public class RecoilOffset
		{
			// Token: 0x06001195 RID: 4501 RVA: 0x0006D43B File Offset: 0x0006B63B
			public void Start()
			{
				if (this.additivity <= 0f)
				{
					return;
				}
				this.additiveOffset = Vector3.ClampMagnitude(this.lastOffset * this.additivity, this.maxAdditiveOffsetMag);
			}

			// Token: 0x06001196 RID: 4502 RVA: 0x0006D470 File Offset: 0x0006B670
			public void Apply(IKSolverFullBodyBiped solver, Quaternion rotation, float masterWeight, float length, float timeLeft)
			{
				this.additiveOffset = Vector3.Lerp(Vector3.zero, this.additiveOffset, timeLeft / length);
				this.lastOffset = rotation * (this.offset * masterWeight) + rotation * this.additiveOffset;
				foreach (Recoil.RecoilOffset.EffectorLink effectorLink in this.effectorLinks)
				{
					solver.GetEffector(effectorLink.effector).positionOffset += this.lastOffset * effectorLink.weight;
				}
			}

			// Token: 0x06001197 RID: 4503 RVA: 0x0006D507 File Offset: 0x0006B707
			public RecoilOffset()
			{
			}

			// Token: 0x0400104D RID: 4173
			[Tooltip("Offset vector for the associated effector when doing recoil.")]
			public Vector3 offset;

			// Token: 0x0400104E RID: 4174
			[Tooltip("When firing before the last recoil has faded, how much of the current recoil offset will be maintained?")]
			[Range(0f, 1f)]
			public float additivity = 1f;

			// Token: 0x0400104F RID: 4175
			[Tooltip("Max additive recoil for automatic fire.")]
			public float maxAdditiveOffsetMag = 0.2f;

			// Token: 0x04001050 RID: 4176
			[Tooltip("Linking this recoil offset to FBBIK effectors.")]
			public Recoil.RecoilOffset.EffectorLink[] effectorLinks;

			// Token: 0x04001051 RID: 4177
			private Vector3 additiveOffset;

			// Token: 0x04001052 RID: 4178
			private Vector3 lastOffset;

			// Token: 0x02000251 RID: 593
			[Serializable]
			public class EffectorLink
			{
				// Token: 0x060011E5 RID: 4581 RVA: 0x0006E8D2 File Offset: 0x0006CAD2
				public EffectorLink()
				{
				}

				// Token: 0x0400111E RID: 4382
				[Tooltip("Type of the FBBIK effector to use")]
				public FullBodyBipedEffector effector;

				// Token: 0x0400111F RID: 4383
				[Tooltip("Weight of using this effector")]
				public float weight;
			}
		}

		// Token: 0x02000228 RID: 552
		[Serializable]
		public enum Handedness
		{
			// Token: 0x04001054 RID: 4180
			Right,
			// Token: 0x04001055 RID: 4181
			Left
		}
	}
}
