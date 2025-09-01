using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000134 RID: 308
	public class ExplosionDemo : MonoBehaviour
	{
		// Token: 0x06000CD6 RID: 3286 RVA: 0x00057110 File Offset: 0x00055310
		private void Start()
		{
			this.defaultScale = base.transform.localScale;
			this.r = this.character.GetComponent<Rigidbody>();
			this.ik = this.character.GetComponent<FullBodyBipedIK>();
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00057148 File Offset: 0x00055348
		private void Update()
		{
			this.weight = Mathf.Clamp(this.weight - Time.deltaTime * this.weightFalloffSpeed, 0f, 1f);
			if (Input.GetKeyDown(KeyCode.E) && this.character.isGrounded)
			{
				this.ik.solver.IKPositionWeight = 1f;
				this.ik.solver.leftHandEffector.position = this.ik.solver.leftHandEffector.bone.position;
				this.ik.solver.rightHandEffector.position = this.ik.solver.rightHandEffector.bone.position;
				this.ik.solver.leftFootEffector.position = this.ik.solver.leftFootEffector.bone.position;
				this.ik.solver.rightFootEffector.position = this.ik.solver.rightFootEffector.bone.position;
				this.weight = 1f;
				Vector3 vector = this.r.position - base.transform.position;
				vector.y = 0f;
				float d = this.explosionForceByDistance.Evaluate(vector.magnitude);
				this.r.velocity = (vector.normalized + Vector3.up * this.upForce) * d * this.forceMlp;
			}
			if (this.weight < 0.5f && this.character.isGrounded)
			{
				this.weight = Mathf.Clamp(this.weight - Time.deltaTime * 3f, 0f, 1f);
			}
			this.SetEffectorWeights(this.weightFalloff.Evaluate(this.weight));
			base.transform.localScale = this.scale.Evaluate(this.weight) * this.defaultScale;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00057368 File Offset: 0x00055568
		private void SetEffectorWeights(float w)
		{
			this.ik.solver.leftHandEffector.positionWeight = w;
			this.ik.solver.rightHandEffector.positionWeight = w;
			this.ik.solver.leftFootEffector.positionWeight = w;
			this.ik.solver.rightFootEffector.positionWeight = w;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x000573CD File Offset: 0x000555CD
		public ExplosionDemo()
		{
		}

		// Token: 0x04000A55 RID: 2645
		public SimpleLocomotion character;

		// Token: 0x04000A56 RID: 2646
		public float forceMlp = 1f;

		// Token: 0x04000A57 RID: 2647
		public float upForce = 1f;

		// Token: 0x04000A58 RID: 2648
		public float weightFalloffSpeed = 1f;

		// Token: 0x04000A59 RID: 2649
		public AnimationCurve weightFalloff;

		// Token: 0x04000A5A RID: 2650
		public AnimationCurve explosionForceByDistance;

		// Token: 0x04000A5B RID: 2651
		public AnimationCurve scale;

		// Token: 0x04000A5C RID: 2652
		private float weight;

		// Token: 0x04000A5D RID: 2653
		private Vector3 defaultScale = Vector3.one;

		// Token: 0x04000A5E RID: 2654
		private Rigidbody r;

		// Token: 0x04000A5F RID: 2655
		private FullBodyBipedIK ik;
	}
}
