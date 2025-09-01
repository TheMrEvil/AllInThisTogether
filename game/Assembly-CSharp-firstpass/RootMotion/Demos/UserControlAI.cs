using System;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200015F RID: 351
	public class UserControlAI : UserControlThirdPerson
	{
		// Token: 0x06000D95 RID: 3477 RVA: 0x0005C1E6 File Offset: 0x0005A3E6
		protected override void Start()
		{
			base.Start();
			this.navigator.Initiate(base.transform);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0005C200 File Offset: 0x0005A400
		protected override void Update()
		{
			float d = this.walkByDefault ? 0.5f : 1f;
			if (this.navigator.activeTargetSeeking)
			{
				this.navigator.Update(this.moveTarget.position);
				this.state.move = this.navigator.normalizedDeltaPosition * d;
				return;
			}
			Vector3 a = this.moveTarget.position - base.transform.position;
			float magnitude = a.magnitude;
			Vector3 up = base.transform.up;
			Vector3.OrthoNormalize(ref up, ref a);
			float num = (this.state.move != Vector3.zero) ? this.stoppingDistance : (this.stoppingDistance * this.stoppingThreshold);
			this.state.move = ((magnitude > num) ? (a * d) : Vector3.zero);
			this.state.lookPos = this.moveTarget.position;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0005C2FD File Offset: 0x0005A4FD
		private void OnDrawGizmos()
		{
			if (this.navigator.activeTargetSeeking)
			{
				this.navigator.Visualize();
			}
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0005C317 File Offset: 0x0005A517
		public UserControlAI()
		{
		}

		// Token: 0x04000B8A RID: 2954
		public Transform moveTarget;

		// Token: 0x04000B8B RID: 2955
		public float stoppingDistance = 0.5f;

		// Token: 0x04000B8C RID: 2956
		public float stoppingThreshold = 1.5f;

		// Token: 0x04000B8D RID: 2957
		public Navigator navigator;
	}
}
