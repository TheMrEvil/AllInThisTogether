using System;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200015A RID: 346
	public class CharacterAnimationSimple : CharacterAnimationBase
	{
		// Token: 0x06000D64 RID: 3428 RVA: 0x0005A64A File Offset: 0x0005884A
		protected override void Start()
		{
			base.Start();
			this.animator = base.GetComponentInChildren<Animator>();
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0005A660 File Offset: 0x00058860
		public override Vector3 GetPivotPoint()
		{
			if (this.pivotOffset == 0f)
			{
				return base.transform.position;
			}
			return base.transform.position + base.transform.forward * this.pivotOffset;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0005A6AC File Offset: 0x000588AC
		private void Update()
		{
			float num = this.moveSpeed.Evaluate(this.characterController.animState.moveDirection.z);
			this.animator.SetFloat("Speed", num);
			this.characterController.Move(this.characterController.transform.forward * Time.deltaTime * num, Quaternion.identity);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0005A71B File Offset: 0x0005891B
		public CharacterAnimationSimple()
		{
		}

		// Token: 0x04000B26 RID: 2854
		public CharacterThirdPerson characterController;

		// Token: 0x04000B27 RID: 2855
		public float pivotOffset;

		// Token: 0x04000B28 RID: 2856
		public AnimationCurve moveSpeed;

		// Token: 0x04000B29 RID: 2857
		private Animator animator;
	}
}
