using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000111 RID: 273
	public class Amplifier : OffsetModifier
	{
		// Token: 0x06000C23 RID: 3107 RVA: 0x00051654 File Offset: 0x0004F854
		protected override void OnModifyOffset()
		{
			if (!this.ik.fixTransforms)
			{
				if (!Warning.logged)
				{
					Warning.Log("Amplifier needs the Fix Transforms option of the FBBIK to be set to true. Otherwise it might amplify to infinity, should the animator of the character stop because of culling.", base.transform, false);
				}
				return;
			}
			Amplifier.Body[] array = this.bodies;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Update(this.ik.solver, this.weight, base.deltaTime);
			}
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x000516BB File Offset: 0x0004F8BB
		public Amplifier()
		{
		}

		// Token: 0x0400097B RID: 2427
		[Tooltip("The amplified bodies.")]
		public Amplifier.Body[] bodies;

		// Token: 0x0200021A RID: 538
		[Serializable]
		public class Body
		{
			// Token: 0x0600114C RID: 4428 RVA: 0x0006C2F8 File Offset: 0x0006A4F8
			public void Update(IKSolverFullBodyBiped solver, float w, float deltaTime)
			{
				if (this.transform == null || this.relativeTo == null)
				{
					return;
				}
				Vector3 a = this.relativeTo.InverseTransformDirection(this.transform.position - this.relativeTo.position);
				if (this.firstUpdate)
				{
					this.lastRelativePos = a;
					this.firstUpdate = false;
				}
				Vector3 vector = (a - this.lastRelativePos) / deltaTime;
				this.smoothDelta = ((this.speed <= 0f) ? vector : Vector3.Lerp(this.smoothDelta, vector, deltaTime * this.speed));
				Vector3 v = this.relativeTo.TransformDirection(this.smoothDelta);
				Vector3 a2 = V3Tools.ExtractVertical(v, solver.GetRoot().up, this.verticalWeight) + V3Tools.ExtractHorizontal(v, solver.GetRoot().up, this.horizontalWeight);
				for (int i = 0; i < this.effectorLinks.Length; i++)
				{
					solver.GetEffector(this.effectorLinks[i].effector).positionOffset += a2 * w * this.effectorLinks[i].weight;
				}
				this.lastRelativePos = a;
			}

			// Token: 0x0600114D RID: 4429 RVA: 0x0006C43E File Offset: 0x0006A63E
			private static Vector3 Multiply(Vector3 v1, Vector3 v2)
			{
				v1.x *= v2.x;
				v1.y *= v2.y;
				v1.z *= v2.z;
				return v1;
			}

			// Token: 0x0600114E RID: 4430 RVA: 0x0006C474 File Offset: 0x0006A674
			public Body()
			{
			}

			// Token: 0x04000FFA RID: 4090
			[Tooltip("The Transform that's motion we are reading.")]
			public Transform transform;

			// Token: 0x04000FFB RID: 4091
			[Tooltip("Amplify the 'transform's' position relative to this Transform.")]
			public Transform relativeTo;

			// Token: 0x04000FFC RID: 4092
			[Tooltip("Linking the body to effectors. One Body can be used to offset more than one effector.")]
			public Amplifier.Body.EffectorLink[] effectorLinks;

			// Token: 0x04000FFD RID: 4093
			[Tooltip("Amplification magnitude along the up axis of the character.")]
			public float verticalWeight = 1f;

			// Token: 0x04000FFE RID: 4094
			[Tooltip("Amplification magnitude along the horizontal axes of the character.")]
			public float horizontalWeight = 1f;

			// Token: 0x04000FFF RID: 4095
			[Tooltip("Speed of the amplifier. 0 means instant.")]
			public float speed = 3f;

			// Token: 0x04001000 RID: 4096
			private Vector3 lastRelativePos;

			// Token: 0x04001001 RID: 4097
			private Vector3 smoothDelta;

			// Token: 0x04001002 RID: 4098
			private bool firstUpdate;

			// Token: 0x0200024A RID: 586
			[Serializable]
			public class EffectorLink
			{
				// Token: 0x060011D6 RID: 4566 RVA: 0x0006E723 File Offset: 0x0006C923
				public EffectorLink()
				{
				}

				// Token: 0x04001108 RID: 4360
				[Tooltip("Type of the FBBIK effector to use")]
				public FullBodyBipedEffector effector;

				// Token: 0x04001109 RID: 4361
				[Tooltip("Weight of using this effector")]
				public float weight;
			}
		}
	}
}
