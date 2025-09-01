using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000118 RID: 280
	public class Inertia : OffsetModifier
	{
		// Token: 0x06000C41 RID: 3137 RVA: 0x000520D4 File Offset: 0x000502D4
		public void ResetBodies()
		{
			this.lastTime = Time.time;
			Inertia.Body[] array = this.bodies;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Reset();
			}
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0005210C File Offset: 0x0005030C
		protected override void OnModifyOffset()
		{
			Inertia.Body[] array = this.bodies;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Update(this.ik.solver, this.weight, base.deltaTime);
			}
			base.ApplyLimits(this.limits);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00052159 File Offset: 0x00050359
		public Inertia()
		{
		}

		// Token: 0x0400098F RID: 2447
		[Tooltip("The array of Bodies")]
		public Inertia.Body[] bodies;

		// Token: 0x04000990 RID: 2448
		[Tooltip("The array of OffsetLimits")]
		public OffsetModifier.OffsetLimits[] limits;

		// Token: 0x02000221 RID: 545
		[Serializable]
		public class Body
		{
			// Token: 0x0600117C RID: 4476 RVA: 0x0006CCB6 File Offset: 0x0006AEB6
			public void Reset()
			{
				if (this.transform == null)
				{
					return;
				}
				this.lazyPoint = this.transform.position;
				this.lastPosition = this.transform.position;
				this.direction = Vector3.zero;
			}

			// Token: 0x0600117D RID: 4477 RVA: 0x0006CCF4 File Offset: 0x0006AEF4
			public void Update(IKSolverFullBodyBiped solver, float weight, float deltaTime)
			{
				if (this.transform == null)
				{
					return;
				}
				if (this.firstUpdate)
				{
					this.Reset();
					this.firstUpdate = false;
				}
				this.direction = Vector3.Lerp(this.direction, (this.transform.position - this.lazyPoint) / deltaTime * 0.01f, deltaTime * this.acceleration);
				this.lazyPoint += this.direction * deltaTime * this.speed;
				this.delta = this.transform.position - this.lastPosition;
				this.lazyPoint += this.delta * this.matchVelocity;
				this.lazyPoint.y = this.lazyPoint.y + this.gravity * deltaTime;
				foreach (Inertia.Body.EffectorLink effectorLink in this.effectorLinks)
				{
					solver.GetEffector(effectorLink.effector).positionOffset += (this.lazyPoint - this.transform.position) * effectorLink.weight * weight;
				}
				this.lastPosition = this.transform.position;
			}

			// Token: 0x0600117E RID: 4478 RVA: 0x0006CE4E File Offset: 0x0006B04E
			public Body()
			{
			}

			// Token: 0x04001023 RID: 4131
			[Tooltip("The Transform to follow, can be any bone of the character")]
			public Transform transform;

			// Token: 0x04001024 RID: 4132
			[Tooltip("Linking the body to effectors. One Body can be used to offset more than one effector")]
			public Inertia.Body.EffectorLink[] effectorLinks;

			// Token: 0x04001025 RID: 4133
			[Tooltip("The speed to follow the Transform")]
			public float speed = 10f;

			// Token: 0x04001026 RID: 4134
			[Tooltip("The acceleration, smaller values means lazyer following")]
			public float acceleration = 3f;

			// Token: 0x04001027 RID: 4135
			[Tooltip("Matching target velocity")]
			[Range(0f, 1f)]
			public float matchVelocity;

			// Token: 0x04001028 RID: 4136
			[Tooltip("gravity applied to the Body")]
			public float gravity;

			// Token: 0x04001029 RID: 4137
			private Vector3 delta;

			// Token: 0x0400102A RID: 4138
			private Vector3 lazyPoint;

			// Token: 0x0400102B RID: 4139
			private Vector3 direction;

			// Token: 0x0400102C RID: 4140
			private Vector3 lastPosition;

			// Token: 0x0400102D RID: 4141
			private bool firstUpdate = true;

			// Token: 0x0200024F RID: 591
			[Serializable]
			public class EffectorLink
			{
				// Token: 0x060011E3 RID: 4579 RVA: 0x0006E8C2 File Offset: 0x0006CAC2
				public EffectorLink()
				{
				}

				// Token: 0x0400111A RID: 4378
				[Tooltip("Type of the FBBIK effector to use")]
				public FullBodyBipedEffector effector;

				// Token: 0x0400111B RID: 4379
				[Tooltip("Weight of using this effector")]
				public float weight;
			}
		}
	}
}
