using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000101 RID: 257
	[Serializable]
	public class InteractionLookAt
	{
		// Token: 0x06000B49 RID: 2889 RVA: 0x0004D440 File Offset: 0x0004B640
		public void Look(Transform target, float time)
		{
			if (this.ik == null)
			{
				return;
			}
			if (this.ik.solver.IKPositionWeight <= 0f)
			{
				this.ik.solver.IKPosition = this.ik.solver.GetRoot().position + this.ik.solver.GetRoot().forward * 3f;
			}
			this.lookAtTarget = target;
			this.stopLookTime = time;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0004D4CA File Offset: 0x0004B6CA
		public void OnFixTransforms()
		{
			if (this.ik == null)
			{
				return;
			}
			if (this.ik.fixTransforms)
			{
				this.ik.solver.FixTransforms();
			}
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0004D4F8 File Offset: 0x0004B6F8
		public void Update()
		{
			if (this.ik == null)
			{
				return;
			}
			if (this.ik.enabled)
			{
				this.ik.enabled = false;
			}
			if (this.lookAtTarget == null)
			{
				return;
			}
			if (this.isPaused)
			{
				this.stopLookTime += Time.deltaTime;
			}
			float num = (Time.time < this.stopLookTime) ? this.weightSpeed : (-this.weightSpeed);
			this.weight = Mathf.Clamp(this.weight + num * Time.deltaTime, 0f, 1f);
			this.ik.solver.IKPositionWeight = Interp.Float(this.weight, InterpolationMode.InOutQuintic);
			this.ik.solver.IKPosition = Vector3.Lerp(this.ik.solver.IKPosition, this.lookAtTarget.position, this.lerpSpeed * Time.deltaTime);
			if (this.weight <= 0f)
			{
				this.lookAtTarget = null;
			}
			this.firstFBBIKSolve = true;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0004D60C File Offset: 0x0004B80C
		public void SolveSpine()
		{
			if (this.ik == null)
			{
				return;
			}
			if (!this.firstFBBIKSolve)
			{
				return;
			}
			float headWeight = this.ik.solver.headWeight;
			float eyesWeight = this.ik.solver.eyesWeight;
			this.ik.solver.headWeight = 0f;
			this.ik.solver.eyesWeight = 0f;
			this.ik.solver.Update();
			this.ik.solver.headWeight = headWeight;
			this.ik.solver.eyesWeight = eyesWeight;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0004D6B0 File Offset: 0x0004B8B0
		public void SolveHead()
		{
			if (this.ik == null)
			{
				return;
			}
			if (!this.firstFBBIKSolve)
			{
				return;
			}
			float bodyWeight = this.ik.solver.bodyWeight;
			this.ik.solver.bodyWeight = 0f;
			this.ik.solver.Update();
			this.ik.solver.bodyWeight = bodyWeight;
			this.firstFBBIKSolve = false;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0004D723 File Offset: 0x0004B923
		public InteractionLookAt()
		{
		}

		// Token: 0x040008F5 RID: 2293
		[Tooltip("(Optional) reference to the LookAtIK component that will be used to make the character look at the objects that it is interacting with.")]
		public LookAtIK ik;

		// Token: 0x040008F6 RID: 2294
		[Tooltip("Interpolation speed of the LookAtIK target.")]
		public float lerpSpeed = 5f;

		// Token: 0x040008F7 RID: 2295
		[Tooltip("Interpolation speed of the LookAtIK weight.")]
		public float weightSpeed = 1f;

		// Token: 0x040008F8 RID: 2296
		[HideInInspector]
		public bool isPaused;

		// Token: 0x040008F9 RID: 2297
		private Transform lookAtTarget;

		// Token: 0x040008FA RID: 2298
		private float stopLookTime;

		// Token: 0x040008FB RID: 2299
		private float weight;

		// Token: 0x040008FC RID: 2300
		private bool firstFBBIKSolve;
	}
}
