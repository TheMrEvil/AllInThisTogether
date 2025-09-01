using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000116 RID: 278
	public class HitReaction : OffsetModifier
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x00051DE4 File Offset: 0x0004FFE4
		public bool inProgress
		{
			get
			{
				HitReaction.HitPointEffector[] array = this.effectorHitPoints;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].inProgress)
					{
						return true;
					}
				}
				HitReaction.HitPointBone[] array2 = this.boneHitPoints;
				for (int i = 0; i < array2.Length; i++)
				{
					if (array2[i].inProgress)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00051E34 File Offset: 0x00050034
		protected override void OnModifyOffset()
		{
			HitReaction.HitPointEffector[] array = this.effectorHitPoints;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Apply(this.ik.solver, this.weight);
			}
			HitReaction.HitPointBone[] array2 = this.boneHitPoints;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].Apply(this.ik.solver, this.weight);
			}
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00051EA0 File Offset: 0x000500A0
		public void Hit(float force, Vector3 point)
		{
			Collider collider = null;
			float num = 1000f;
			Vector3 vector = Vector3.forward;
			foreach (HitReaction.HitPointEffector hitPointEffector in this.effectorHitPoints)
			{
				if (!(hitPointEffector.collider == null))
				{
					vector = hitPointEffector.collider.transform.position - point;
					if (vector.sqrMagnitude < num)
					{
						num = vector.sqrMagnitude;
						collider = hitPointEffector.collider;
					}
				}
			}
			if (collider != null)
			{
				this.Hit(collider, vector.normalized * force, point);
			}
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00051F3C File Offset: 0x0005013C
		public void Hit(Collider collider, Vector3 force, Vector3 point)
		{
			if (this.ik == null)
			{
				Debug.LogError("No IK assigned in HitReaction");
				return;
			}
			foreach (HitReaction.HitPointEffector hitPointEffector in this.effectorHitPoints)
			{
				if (hitPointEffector.collider == collider)
				{
					hitPointEffector.Hit(force, point);
				}
			}
			foreach (HitReaction.HitPointBone hitPointBone in this.boneHitPoints)
			{
				if (hitPointBone.collider == collider)
				{
					hitPointBone.Hit(force, point);
				}
			}
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00051FC3 File Offset: 0x000501C3
		public HitReaction()
		{
		}

		// Token: 0x0400098A RID: 2442
		[Tooltip("Hit points for the FBBIK effectors")]
		public HitReaction.HitPointEffector[] effectorHitPoints;

		// Token: 0x0400098B RID: 2443
		[Tooltip(" Hit points for bones without an effector, such as the head")]
		public HitReaction.HitPointBone[] boneHitPoints;

		// Token: 0x0200021B RID: 539
		[Serializable]
		public abstract class HitPoint
		{
			// Token: 0x1700024D RID: 589
			// (get) Token: 0x0600114F RID: 4431 RVA: 0x0006C49D File Offset: 0x0006A69D
			public bool inProgress
			{
				get
				{
					return this.timer < this.length;
				}
			}

			// Token: 0x1700024E RID: 590
			// (get) Token: 0x06001150 RID: 4432 RVA: 0x0006C4AD File Offset: 0x0006A6AD
			// (set) Token: 0x06001151 RID: 4433 RVA: 0x0006C4B5 File Offset: 0x0006A6B5
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

			// Token: 0x1700024F RID: 591
			// (get) Token: 0x06001152 RID: 4434 RVA: 0x0006C4BE File Offset: 0x0006A6BE
			// (set) Token: 0x06001153 RID: 4435 RVA: 0x0006C4C6 File Offset: 0x0006A6C6
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

			// Token: 0x17000250 RID: 592
			// (get) Token: 0x06001154 RID: 4436 RVA: 0x0006C4CF File Offset: 0x0006A6CF
			// (set) Token: 0x06001155 RID: 4437 RVA: 0x0006C4D7 File Offset: 0x0006A6D7
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

			// Token: 0x17000251 RID: 593
			// (get) Token: 0x06001156 RID: 4438 RVA: 0x0006C4E0 File Offset: 0x0006A6E0
			// (set) Token: 0x06001157 RID: 4439 RVA: 0x0006C4E8 File Offset: 0x0006A6E8
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

			// Token: 0x06001158 RID: 4440 RVA: 0x0006C4F4 File Offset: 0x0006A6F4
			public void Hit(Vector3 force, Vector3 point)
			{
				if (this.length == 0f)
				{
					this.length = this.GetLength();
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

			// Token: 0x06001159 RID: 4441 RVA: 0x0006C590 File Offset: 0x0006A790
			public void Apply(IKSolverFullBodyBiped solver, float weight)
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
				this.OnApply(solver, weight);
			}

			// Token: 0x0600115A RID: 4442
			protected abstract float GetLength();

			// Token: 0x0600115B RID: 4443
			protected abstract void CrossFadeStart();

			// Token: 0x0600115C RID: 4444
			protected abstract void OnApply(IKSolverFullBodyBiped solver, float weight);

			// Token: 0x0600115D RID: 4445 RVA: 0x0006C628 File Offset: 0x0006A828
			protected HitPoint()
			{
			}

			// Token: 0x04001003 RID: 4099
			[Tooltip("Just for visual clarity, not used at all")]
			public string name;

			// Token: 0x04001004 RID: 4100
			[Tooltip("Linking this hit point to a collider")]
			public Collider collider;

			// Token: 0x04001005 RID: 4101
			[Tooltip("Only used if this hit point gets hit when already processing another hit")]
			[SerializeField]
			private float crossFadeTime = 0.1f;

			// Token: 0x04001006 RID: 4102
			[CompilerGenerated]
			private float <crossFader>k__BackingField;

			// Token: 0x04001007 RID: 4103
			[CompilerGenerated]
			private float <timer>k__BackingField;

			// Token: 0x04001008 RID: 4104
			[CompilerGenerated]
			private Vector3 <force>k__BackingField;

			// Token: 0x04001009 RID: 4105
			[CompilerGenerated]
			private Vector3 <point>k__BackingField;

			// Token: 0x0400100A RID: 4106
			private float length;

			// Token: 0x0400100B RID: 4107
			private float crossFadeSpeed;

			// Token: 0x0400100C RID: 4108
			private float lastTime;
		}

		// Token: 0x0200021C RID: 540
		[Serializable]
		public class HitPointEffector : HitReaction.HitPoint
		{
			// Token: 0x0600115E RID: 4446 RVA: 0x0006C63C File Offset: 0x0006A83C
			protected override float GetLength()
			{
				float num = (this.offsetInForceDirection.keys.Length != 0) ? this.offsetInForceDirection.keys[this.offsetInForceDirection.length - 1].time : 0f;
				float min = (this.offsetInUpDirection.keys.Length != 0) ? this.offsetInUpDirection.keys[this.offsetInUpDirection.length - 1].time : 0f;
				return Mathf.Clamp(num, min, num);
			}

			// Token: 0x0600115F RID: 4447 RVA: 0x0006C6C4 File Offset: 0x0006A8C4
			protected override void CrossFadeStart()
			{
				HitReaction.HitPointEffector.EffectorLink[] array = this.effectorLinks;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].CrossFadeStart();
				}
			}

			// Token: 0x06001160 RID: 4448 RVA: 0x0006C6F0 File Offset: 0x0006A8F0
			protected override void OnApply(IKSolverFullBodyBiped solver, float weight)
			{
				Vector3 a = solver.GetRoot().up * base.force.magnitude;
				Vector3 vector = this.offsetInForceDirection.Evaluate(base.timer) * base.force + this.offsetInUpDirection.Evaluate(base.timer) * a;
				vector *= weight;
				HitReaction.HitPointEffector.EffectorLink[] array = this.effectorLinks;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Apply(solver, vector, base.crossFader);
				}
			}

			// Token: 0x06001161 RID: 4449 RVA: 0x0006C787 File Offset: 0x0006A987
			public HitPointEffector()
			{
			}

			// Token: 0x0400100D RID: 4109
			[Tooltip("Offset magnitude in the direction of the hit force")]
			public AnimationCurve offsetInForceDirection;

			// Token: 0x0400100E RID: 4110
			[Tooltip("Offset magnitude in the direction of character.up")]
			public AnimationCurve offsetInUpDirection;

			// Token: 0x0400100F RID: 4111
			[Tooltip("Linking this offset to the FBBIK effectors")]
			public HitReaction.HitPointEffector.EffectorLink[] effectorLinks;

			// Token: 0x0200024B RID: 587
			[Serializable]
			public class EffectorLink
			{
				// Token: 0x060011D7 RID: 4567 RVA: 0x0006E72C File Offset: 0x0006C92C
				public void Apply(IKSolverFullBodyBiped solver, Vector3 offset, float crossFader)
				{
					this.current = Vector3.Lerp(this.lastValue, offset * this.weight, crossFader);
					solver.GetEffector(this.effector).positionOffset += this.current;
				}

				// Token: 0x060011D8 RID: 4568 RVA: 0x0006E779 File Offset: 0x0006C979
				public void CrossFadeStart()
				{
					this.lastValue = this.current;
				}

				// Token: 0x060011D9 RID: 4569 RVA: 0x0006E787 File Offset: 0x0006C987
				public EffectorLink()
				{
				}

				// Token: 0x0400110A RID: 4362
				[Tooltip("The FBBIK effector type")]
				public FullBodyBipedEffector effector;

				// Token: 0x0400110B RID: 4363
				[Tooltip("The weight of this effector (could also be negative)")]
				public float weight;

				// Token: 0x0400110C RID: 4364
				private Vector3 lastValue;

				// Token: 0x0400110D RID: 4365
				private Vector3 current;
			}
		}

		// Token: 0x0200021D RID: 541
		[Serializable]
		public class HitPointBone : HitReaction.HitPoint
		{
			// Token: 0x06001162 RID: 4450 RVA: 0x0006C78F File Offset: 0x0006A98F
			protected override float GetLength()
			{
				if (this.aroundCenterOfMass.keys.Length == 0)
				{
					return 0f;
				}
				return this.aroundCenterOfMass.keys[this.aroundCenterOfMass.length - 1].time;
			}

			// Token: 0x06001163 RID: 4451 RVA: 0x0006C7C8 File Offset: 0x0006A9C8
			protected override void CrossFadeStart()
			{
				HitReaction.HitPointBone.BoneLink[] array = this.boneLinks;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].CrossFadeStart();
				}
			}

			// Token: 0x06001164 RID: 4452 RVA: 0x0006C7F4 File Offset: 0x0006A9F4
			protected override void OnApply(IKSolverFullBodyBiped solver, float weight)
			{
				if (this.rigidbody == null)
				{
					this.rigidbody = this.collider.GetComponent<Rigidbody>();
				}
				if (this.rigidbody != null)
				{
					Vector3 axis = Vector3.Cross(base.force, base.point - this.rigidbody.worldCenterOfMass);
					Quaternion offset = Quaternion.AngleAxis(this.aroundCenterOfMass.Evaluate(base.timer) * weight, axis);
					HitReaction.HitPointBone.BoneLink[] array = this.boneLinks;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].Apply(solver, offset, base.crossFader);
					}
				}
			}

			// Token: 0x06001165 RID: 4453 RVA: 0x0006C88F File Offset: 0x0006AA8F
			public HitPointBone()
			{
			}

			// Token: 0x04001010 RID: 4112
			[Tooltip("The angle to rotate the bone around it's rigidbody's world center of mass")]
			public AnimationCurve aroundCenterOfMass;

			// Token: 0x04001011 RID: 4113
			[Tooltip("Linking this hit point to bone(s)")]
			public HitReaction.HitPointBone.BoneLink[] boneLinks;

			// Token: 0x04001012 RID: 4114
			private Rigidbody rigidbody;

			// Token: 0x0200024C RID: 588
			[Serializable]
			public class BoneLink
			{
				// Token: 0x060011DA RID: 4570 RVA: 0x0006E790 File Offset: 0x0006C990
				public void Apply(IKSolverFullBodyBiped solver, Quaternion offset, float crossFader)
				{
					this.current = Quaternion.Lerp(this.lastValue, Quaternion.Lerp(Quaternion.identity, offset, this.weight), crossFader);
					this.bone.rotation = this.current * this.bone.rotation;
				}

				// Token: 0x060011DB RID: 4571 RVA: 0x0006E7E1 File Offset: 0x0006C9E1
				public void CrossFadeStart()
				{
					this.lastValue = this.current;
				}

				// Token: 0x060011DC RID: 4572 RVA: 0x0006E7EF File Offset: 0x0006C9EF
				public BoneLink()
				{
				}

				// Token: 0x0400110E RID: 4366
				[Tooltip("Reference to the bone that this hit point rotates")]
				public Transform bone;

				// Token: 0x0400110F RID: 4367
				[Tooltip("Weight of rotating the bone")]
				[Range(0f, 1f)]
				public float weight;

				// Token: 0x04001110 RID: 4368
				private Quaternion lastValue = Quaternion.identity;

				// Token: 0x04001111 RID: 4369
				private Quaternion current = Quaternion.identity;
			}
		}
	}
}
