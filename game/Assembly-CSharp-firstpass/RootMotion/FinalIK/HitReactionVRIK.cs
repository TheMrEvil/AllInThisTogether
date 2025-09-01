using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000117 RID: 279
	public class HitReactionVRIK : OffsetModifierVRIK
	{
		// Token: 0x06000C3E RID: 3134 RVA: 0x00051FCC File Offset: 0x000501CC
		protected override void OnModifyOffset()
		{
			HitReactionVRIK.PositionOffset[] array = this.positionOffsets;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Apply(this.ik, this.offsetCurves, this.weight);
			}
			HitReactionVRIK.RotationOffset[] array2 = this.rotationOffsets;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].Apply(this.ik, this.offsetCurves, this.weight);
			}
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00052038 File Offset: 0x00050238
		public void Hit(Collider collider, Vector3 force, Vector3 point)
		{
			if (this.ik == null)
			{
				Debug.LogError("No IK assigned in HitReaction");
				return;
			}
			foreach (HitReactionVRIK.PositionOffset positionOffset in this.positionOffsets)
			{
				if (positionOffset.collider == collider)
				{
					positionOffset.Hit(force, this.offsetCurves, point);
				}
			}
			foreach (HitReactionVRIK.RotationOffset rotationOffset in this.rotationOffsets)
			{
				if (rotationOffset.collider == collider)
				{
					rotationOffset.Hit(force, this.offsetCurves, point);
				}
			}
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x000520CB File Offset: 0x000502CB
		public HitReactionVRIK()
		{
		}

		// Token: 0x0400098C RID: 2444
		public AnimationCurve[] offsetCurves;

		// Token: 0x0400098D RID: 2445
		[Tooltip("Hit points for the FBBIK effectors")]
		public HitReactionVRIK.PositionOffset[] positionOffsets;

		// Token: 0x0400098E RID: 2446
		[Tooltip(" Hit points for bones without an effector, such as the head")]
		public HitReactionVRIK.RotationOffset[] rotationOffsets;

		// Token: 0x0200021E RID: 542
		[Serializable]
		public abstract class Offset
		{
			// Token: 0x17000252 RID: 594
			// (get) Token: 0x06001166 RID: 4454 RVA: 0x0006C897 File Offset: 0x0006AA97
			// (set) Token: 0x06001167 RID: 4455 RVA: 0x0006C89F File Offset: 0x0006AA9F
			private protected float crossFader
			{
				[CompilerGenerated]
				protected get
				{
					return this.<crossFader>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<crossFader>k__BackingField = value;
				}
			}

			// Token: 0x17000253 RID: 595
			// (get) Token: 0x06001168 RID: 4456 RVA: 0x0006C8A8 File Offset: 0x0006AAA8
			// (set) Token: 0x06001169 RID: 4457 RVA: 0x0006C8B0 File Offset: 0x0006AAB0
			private protected float timer
			{
				[CompilerGenerated]
				protected get
				{
					return this.<timer>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<timer>k__BackingField = value;
				}
			}

			// Token: 0x17000254 RID: 596
			// (get) Token: 0x0600116A RID: 4458 RVA: 0x0006C8B9 File Offset: 0x0006AAB9
			// (set) Token: 0x0600116B RID: 4459 RVA: 0x0006C8C1 File Offset: 0x0006AAC1
			private protected Vector3 force
			{
				[CompilerGenerated]
				protected get
				{
					return this.<force>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<force>k__BackingField = value;
				}
			}

			// Token: 0x17000255 RID: 597
			// (get) Token: 0x0600116C RID: 4460 RVA: 0x0006C8CA File Offset: 0x0006AACA
			// (set) Token: 0x0600116D RID: 4461 RVA: 0x0006C8D2 File Offset: 0x0006AAD2
			private protected Vector3 point
			{
				[CompilerGenerated]
				protected get
				{
					return this.<point>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<point>k__BackingField = value;
				}
			}

			// Token: 0x0600116E RID: 4462 RVA: 0x0006C8DC File Offset: 0x0006AADC
			public void Hit(Vector3 force, AnimationCurve[] curves, Vector3 point)
			{
				if (this.length == 0f)
				{
					this.length = this.GetLength(curves);
				}
				if (this.length <= 0f)
				{
					Debug.LogError("Hit Point WeightCurve length is zero.");
					return;
				}
				if (this.timer < 1f)
				{
					this.crossFader = 0f;
				}
				this.crossFadeSpeed = ((this.crossFadeTime > 0f) ? (1f / this.crossFadeTime) : 0f);
				this.CrossFadeStart();
				this.timer = 0f;
				this.force = force;
				this.point = point;
			}

			// Token: 0x0600116F RID: 4463 RVA: 0x0006C978 File Offset: 0x0006AB78
			public void Apply(VRIK ik, AnimationCurve[] curves, float weight)
			{
				float num = Time.time - this.lastTime;
				this.lastTime = Time.time;
				if (this.timer >= this.length)
				{
					return;
				}
				this.timer = Mathf.Clamp(this.timer + num, 0f, this.length);
				if (this.crossFadeSpeed > 0f)
				{
					this.crossFader = Mathf.Clamp(this.crossFader + num * this.crossFadeSpeed, 0f, 1f);
				}
				else
				{
					this.crossFader = 1f;
				}
				this.OnApply(ik, curves, weight);
			}

			// Token: 0x06001170 RID: 4464
			protected abstract float GetLength(AnimationCurve[] curves);

			// Token: 0x06001171 RID: 4465
			protected abstract void CrossFadeStart();

			// Token: 0x06001172 RID: 4466
			protected abstract void OnApply(VRIK ik, AnimationCurve[] curves, float weight);

			// Token: 0x06001173 RID: 4467 RVA: 0x0006CA11 File Offset: 0x0006AC11
			protected Offset()
			{
			}

			// Token: 0x04001013 RID: 4115
			[Tooltip("Just for visual clarity, not used at all")]
			public string name;

			// Token: 0x04001014 RID: 4116
			[Tooltip("Linking this hit point to a collider")]
			public Collider collider;

			// Token: 0x04001015 RID: 4117
			[Tooltip("Only used if this hit point gets hit when already processing another hit")]
			[SerializeField]
			private float crossFadeTime = 0.1f;

			// Token: 0x04001016 RID: 4118
			[CompilerGenerated]
			private float <crossFader>k__BackingField;

			// Token: 0x04001017 RID: 4119
			[CompilerGenerated]
			private float <timer>k__BackingField;

			// Token: 0x04001018 RID: 4120
			[CompilerGenerated]
			private Vector3 <force>k__BackingField;

			// Token: 0x04001019 RID: 4121
			[CompilerGenerated]
			private Vector3 <point>k__BackingField;

			// Token: 0x0400101A RID: 4122
			private float length;

			// Token: 0x0400101B RID: 4123
			private float crossFadeSpeed;

			// Token: 0x0400101C RID: 4124
			private float lastTime;
		}

		// Token: 0x0200021F RID: 543
		[Serializable]
		public class PositionOffset : HitReactionVRIK.Offset
		{
			// Token: 0x06001174 RID: 4468 RVA: 0x0006CA24 File Offset: 0x0006AC24
			protected override float GetLength(AnimationCurve[] curves)
			{
				float num = (curves[this.forceDirCurveIndex].keys.Length != 0) ? curves[this.forceDirCurveIndex].keys[curves[this.forceDirCurveIndex].length - 1].time : 0f;
				float min = (curves[this.upDirCurveIndex].keys.Length != 0) ? curves[this.upDirCurveIndex].keys[curves[this.upDirCurveIndex].length - 1].time : 0f;
				return Mathf.Clamp(num, min, num);
			}

			// Token: 0x06001175 RID: 4469 RVA: 0x0006CAB8 File Offset: 0x0006ACB8
			protected override void CrossFadeStart()
			{
				HitReactionVRIK.PositionOffset.PositionOffsetLink[] array = this.offsetLinks;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].CrossFadeStart();
				}
			}

			// Token: 0x06001176 RID: 4470 RVA: 0x0006CAE4 File Offset: 0x0006ACE4
			protected override void OnApply(VRIK ik, AnimationCurve[] curves, float weight)
			{
				Vector3 a = ik.transform.up * base.force.magnitude;
				Vector3 vector = curves[this.forceDirCurveIndex].Evaluate(base.timer) * base.force + curves[this.upDirCurveIndex].Evaluate(base.timer) * a;
				vector *= weight;
				HitReactionVRIK.PositionOffset.PositionOffsetLink[] array = this.offsetLinks;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Apply(ik, vector, base.crossFader);
				}
			}

			// Token: 0x06001177 RID: 4471 RVA: 0x0006CB7F File Offset: 0x0006AD7F
			public PositionOffset()
			{
			}

			// Token: 0x0400101D RID: 4125
			[Tooltip("Offset magnitude in the direction of the hit force")]
			public int forceDirCurveIndex;

			// Token: 0x0400101E RID: 4126
			[Tooltip("Offset magnitude in the direction of character.up")]
			public int upDirCurveIndex = 1;

			// Token: 0x0400101F RID: 4127
			[Tooltip("Linking this offset to the VRIK position offsets")]
			public HitReactionVRIK.PositionOffset.PositionOffsetLink[] offsetLinks;

			// Token: 0x0200024D RID: 589
			[Serializable]
			public class PositionOffsetLink
			{
				// Token: 0x060011DD RID: 4573 RVA: 0x0006E80D File Offset: 0x0006CA0D
				public void Apply(VRIK ik, Vector3 offset, float crossFader)
				{
					this.current = Vector3.Lerp(this.lastValue, offset * this.weight, crossFader);
					ik.solver.AddPositionOffset(this.positionOffset, this.current);
				}

				// Token: 0x060011DE RID: 4574 RVA: 0x0006E844 File Offset: 0x0006CA44
				public void CrossFadeStart()
				{
					this.lastValue = this.current;
				}

				// Token: 0x060011DF RID: 4575 RVA: 0x0006E852 File Offset: 0x0006CA52
				public PositionOffsetLink()
				{
				}

				// Token: 0x04001112 RID: 4370
				[Tooltip("The FBBIK effector type")]
				public IKSolverVR.PositionOffset positionOffset;

				// Token: 0x04001113 RID: 4371
				[Tooltip("The weight of this effector (could also be negative)")]
				public float weight;

				// Token: 0x04001114 RID: 4372
				private Vector3 lastValue;

				// Token: 0x04001115 RID: 4373
				private Vector3 current;
			}
		}

		// Token: 0x02000220 RID: 544
		[Serializable]
		public class RotationOffset : HitReactionVRIK.Offset
		{
			// Token: 0x06001178 RID: 4472 RVA: 0x0006CB8E File Offset: 0x0006AD8E
			protected override float GetLength(AnimationCurve[] curves)
			{
				if (curves[this.curveIndex].keys.Length == 0)
				{
					return 0f;
				}
				return curves[this.curveIndex].keys[curves[this.curveIndex].length - 1].time;
			}

			// Token: 0x06001179 RID: 4473 RVA: 0x0006CBCC File Offset: 0x0006ADCC
			protected override void CrossFadeStart()
			{
				HitReactionVRIK.RotationOffset.RotationOffsetLink[] array = this.offsetLinks;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].CrossFadeStart();
				}
			}

			// Token: 0x0600117A RID: 4474 RVA: 0x0006CBF8 File Offset: 0x0006ADF8
			protected override void OnApply(VRIK ik, AnimationCurve[] curves, float weight)
			{
				if (this.collider == null)
				{
					Debug.LogError("No collider assigned for a HitPointBone in the HitReaction component.");
					return;
				}
				if (this.rigidbody == null)
				{
					this.rigidbody = this.collider.GetComponent<Rigidbody>();
				}
				if (this.rigidbody != null)
				{
					Vector3 axis = Vector3.Cross(base.force, base.point - this.rigidbody.worldCenterOfMass);
					Quaternion offset = Quaternion.AngleAxis(curves[this.curveIndex].Evaluate(base.timer) * weight, axis);
					HitReactionVRIK.RotationOffset.RotationOffsetLink[] array = this.offsetLinks;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].Apply(ik, offset, base.crossFader);
					}
				}
			}

			// Token: 0x0600117B RID: 4475 RVA: 0x0006CCAE File Offset: 0x0006AEAE
			public RotationOffset()
			{
			}

			// Token: 0x04001020 RID: 4128
			[Tooltip("The angle to rotate the bone around it's rigidbody's world center of mass")]
			public int curveIndex;

			// Token: 0x04001021 RID: 4129
			[Tooltip("Linking this hit point to bone(s)")]
			public HitReactionVRIK.RotationOffset.RotationOffsetLink[] offsetLinks;

			// Token: 0x04001022 RID: 4130
			private Rigidbody rigidbody;

			// Token: 0x0200024E RID: 590
			[Serializable]
			public class RotationOffsetLink
			{
				// Token: 0x060011E0 RID: 4576 RVA: 0x0006E85A File Offset: 0x0006CA5A
				public void Apply(VRIK ik, Quaternion offset, float crossFader)
				{
					this.current = Quaternion.Lerp(this.lastValue, Quaternion.Lerp(Quaternion.identity, offset, this.weight), crossFader);
					ik.solver.AddRotationOffset(this.rotationOffset, this.current);
				}

				// Token: 0x060011E1 RID: 4577 RVA: 0x0006E896 File Offset: 0x0006CA96
				public void CrossFadeStart()
				{
					this.lastValue = this.current;
				}

				// Token: 0x060011E2 RID: 4578 RVA: 0x0006E8A4 File Offset: 0x0006CAA4
				public RotationOffsetLink()
				{
				}

				// Token: 0x04001116 RID: 4374
				[Tooltip("Reference to the bone that this hit point rotates")]
				public IKSolverVR.RotationOffset rotationOffset;

				// Token: 0x04001117 RID: 4375
				[Tooltip("Weight of rotating the bone")]
				[Range(0f, 1f)]
				public float weight;

				// Token: 0x04001118 RID: 4376
				private Quaternion lastValue = Quaternion.identity;

				// Token: 0x04001119 RID: 4377
				private Quaternion current = Quaternion.identity;
			}
		}
	}
}
