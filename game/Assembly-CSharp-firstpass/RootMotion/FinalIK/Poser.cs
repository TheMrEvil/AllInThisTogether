using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x02000108 RID: 264
	public abstract class Poser : SolverManager
	{
		// Token: 0x06000BC7 RID: 3015
		public abstract void AutoMapping();

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0004FB18 File Offset: 0x0004DD18
		public void UpdateManual()
		{
			this.UpdatePoser();
		}

		// Token: 0x06000BC9 RID: 3017
		protected abstract void InitiatePoser();

		// Token: 0x06000BCA RID: 3018
		protected abstract void UpdatePoser();

		// Token: 0x06000BCB RID: 3019
		protected abstract void FixPoserTransforms();

		// Token: 0x06000BCC RID: 3020 RVA: 0x0004FB20 File Offset: 0x0004DD20
		protected override void UpdateSolver()
		{
			if (!this.initiated)
			{
				this.InitiateSolver();
			}
			if (!this.initiated)
			{
				return;
			}
			this.UpdatePoser();
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0004FB3F File Offset: 0x0004DD3F
		protected override void InitiateSolver()
		{
			if (this.initiated)
			{
				return;
			}
			this.InitiatePoser();
			this.initiated = true;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0004FB57 File Offset: 0x0004DD57
		protected override void FixTransforms()
		{
			if (!this.initiated)
			{
				return;
			}
			this.FixPoserTransforms();
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0004FB68 File Offset: 0x0004DD68
		protected Poser()
		{
		}

		// Token: 0x04000931 RID: 2353
		public Transform poseRoot;

		// Token: 0x04000932 RID: 2354
		[Range(0f, 1f)]
		public float weight = 1f;

		// Token: 0x04000933 RID: 2355
		[Range(0f, 1f)]
		public float localRotationWeight = 1f;

		// Token: 0x04000934 RID: 2356
		[Range(0f, 1f)]
		public float localPositionWeight;

		// Token: 0x04000935 RID: 2357
		private bool initiated;
	}
}
