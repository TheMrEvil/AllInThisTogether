using System;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000142 RID: 322
	public class MotionAbsorbCharacter : MonoBehaviour
	{
		// Token: 0x06000D0B RID: 3339 RVA: 0x000589B0 File Offset: 0x00056BB0
		private void Start()
		{
			this.cubeDefaultPosition = this.cube.position;
			this.cubeRigidbody = this.cube.GetComponent<Rigidbody>();
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x000589D4 File Offset: 0x00056BD4
		private void Update()
		{
			this.info = this.animator.GetCurrentAnimatorStateInfo(0);
			this.motionAbsorb.weight = this.motionAbsorbWeight.Evaluate(this.info.normalizedTime - (float)((int)this.info.normalizedTime));
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00058A24 File Offset: 0x00056C24
		private void SwingStart()
		{
			this.cubeRigidbody.MovePosition(this.cubeDefaultPosition + UnityEngine.Random.insideUnitSphere * this.cubeRandomPosition);
			this.cubeRigidbody.MoveRotation(Quaternion.identity);
			this.cubeRigidbody.velocity = Vector3.zero;
			this.cubeRigidbody.angularVelocity = Vector3.zero;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00058A87 File Offset: 0x00056C87
		public MotionAbsorbCharacter()
		{
		}

		// Token: 0x04000AB0 RID: 2736
		public Animator animator;

		// Token: 0x04000AB1 RID: 2737
		public MotionAbsorb motionAbsorb;

		// Token: 0x04000AB2 RID: 2738
		public Transform cube;

		// Token: 0x04000AB3 RID: 2739
		public float cubeRandomPosition = 0.1f;

		// Token: 0x04000AB4 RID: 2740
		public AnimationCurve motionAbsorbWeight;

		// Token: 0x04000AB5 RID: 2741
		private Vector3 cubeDefaultPosition;

		// Token: 0x04000AB6 RID: 2742
		private AnimatorStateInfo info;

		// Token: 0x04000AB7 RID: 2743
		private Rigidbody cubeRigidbody;
	}
}
