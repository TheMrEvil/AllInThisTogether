using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x0200011D RID: 285
	public class PenetrationAvoidance : OffsetModifier
	{
		// Token: 0x06000C5C RID: 3164 RVA: 0x00052954 File Offset: 0x00050B54
		protected override void OnModifyOffset()
		{
			PenetrationAvoidance.Avoider[] array = this.avoiders;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Solve(this.ik.solver, this.weight);
			}
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0005298F File Offset: 0x00050B8F
		public PenetrationAvoidance()
		{
		}

		// Token: 0x040009AC RID: 2476
		[Tooltip("Definitions of penetration avoidances.")]
		public PenetrationAvoidance.Avoider[] avoiders;

		// Token: 0x02000226 RID: 550
		[Serializable]
		public class Avoider
		{
			// Token: 0x06001191 RID: 4497 RVA: 0x0006D284 File Offset: 0x0006B484
			public void Solve(IKSolverFullBodyBiped solver, float weight)
			{
				this.offsetTarget = this.GetOffsetTarget(solver);
				float smoothTime = (this.offsetTarget.sqrMagnitude > this.offset.sqrMagnitude) ? this.smoothTimeIn : this.smoothTimeOut;
				this.offset = Vector3.SmoothDamp(this.offset, this.offsetTarget, ref this.offsetV, smoothTime);
				foreach (PenetrationAvoidance.Avoider.EffectorLink effectorLink in this.effectors)
				{
					solver.GetEffector(effectorLink.effector).positionOffset += this.offset * weight * effectorLink.weight;
				}
			}

			// Token: 0x06001192 RID: 4498 RVA: 0x0006D330 File Offset: 0x0006B530
			private Vector3 GetOffsetTarget(IKSolverFullBodyBiped solver)
			{
				Vector3 vector = Vector3.zero;
				foreach (Transform transform in this.raycastFrom)
				{
					vector += this.Raycast(transform.position, this.raycastTo.position + vector);
				}
				return vector;
			}

			// Token: 0x06001193 RID: 4499 RVA: 0x0006D384 File Offset: 0x0006B584
			private Vector3 Raycast(Vector3 from, Vector3 to)
			{
				Vector3 direction = to - from;
				float magnitude = direction.magnitude;
				RaycastHit raycastHit;
				if (this.raycastRadius <= 0f)
				{
					Physics.Raycast(from, direction, out raycastHit, magnitude, this.layers);
				}
				else
				{
					Physics.SphereCast(from, this.raycastRadius, direction, out raycastHit, magnitude, this.layers);
				}
				if (raycastHit.collider == null)
				{
					return Vector3.zero;
				}
				return Vector3.Project(-direction.normalized * (magnitude - raycastHit.distance), raycastHit.normal);
			}

			// Token: 0x06001194 RID: 4500 RVA: 0x0006D41D File Offset: 0x0006B61D
			public Avoider()
			{
			}

			// Token: 0x04001043 RID: 4163
			[Tooltip("Bones to start the raycast from. Multiple raycasts can be used by assigning more than 1 bone.")]
			public Transform[] raycastFrom;

			// Token: 0x04001044 RID: 4164
			[Tooltip("The Transform to raycast towards. Usually the body part that you want to keep from penetrating.")]
			public Transform raycastTo;

			// Token: 0x04001045 RID: 4165
			[Tooltip("If 0, will use simple raycasting, if > 0, will use sphere casting (better, but slower).")]
			[Range(0f, 1f)]
			public float raycastRadius;

			// Token: 0x04001046 RID: 4166
			[Tooltip("Linking this to FBBIK effectors.")]
			public PenetrationAvoidance.Avoider.EffectorLink[] effectors;

			// Token: 0x04001047 RID: 4167
			[Tooltip("The time of smooth interpolation of the offset value to avoid penetration.")]
			public float smoothTimeIn = 0.1f;

			// Token: 0x04001048 RID: 4168
			[Tooltip("The time of smooth interpolation of the offset value blending out of penetration avoidance.")]
			public float smoothTimeOut = 0.3f;

			// Token: 0x04001049 RID: 4169
			[Tooltip("Layers to keep penetrating from.")]
			public LayerMask layers;

			// Token: 0x0400104A RID: 4170
			private Vector3 offset;

			// Token: 0x0400104B RID: 4171
			private Vector3 offsetTarget;

			// Token: 0x0400104C RID: 4172
			private Vector3 offsetV;

			// Token: 0x02000250 RID: 592
			[Serializable]
			public class EffectorLink
			{
				// Token: 0x060011E4 RID: 4580 RVA: 0x0006E8CA File Offset: 0x0006CACA
				public EffectorLink()
				{
				}

				// Token: 0x0400111C RID: 4380
				[Tooltip("Effector to apply the offset to.")]
				public FullBodyBipedEffector effector;

				// Token: 0x0400111D RID: 4381
				[Tooltip("Multiplier of the offset value, can be negative.")]
				public float weight;
			}
		}
	}
}
