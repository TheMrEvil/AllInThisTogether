using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000DE RID: 222
	public class IKExecutionOrder : MonoBehaviour
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x0003EBB3 File Offset: 0x0003CDB3
		private bool animatePhysics
		{
			get
			{
				return !(this.animator == null) && this.animator.updateMode == AnimatorUpdateMode.AnimatePhysics;
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0003EBD4 File Offset: 0x0003CDD4
		private void Start()
		{
			for (int i = 0; i < this.IKComponents.Length; i++)
			{
				this.IKComponents[i].enabled = false;
			}
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0003EC02 File Offset: 0x0003CE02
		private void Update()
		{
			if (this.animatePhysics)
			{
				return;
			}
			this.FixTransforms();
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0003EC13 File Offset: 0x0003CE13
		private void FixedUpdate()
		{
			this.fixedFrame = true;
			if (this.animatePhysics)
			{
				this.FixTransforms();
			}
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0003EC2C File Offset: 0x0003CE2C
		private void LateUpdate()
		{
			if (!this.animatePhysics || this.fixedFrame)
			{
				for (int i = 0; i < this.IKComponents.Length; i++)
				{
					this.IKComponents[i].GetIKSolver().Update();
				}
				this.fixedFrame = false;
			}
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0003EC78 File Offset: 0x0003CE78
		private void FixTransforms()
		{
			for (int i = 0; i < this.IKComponents.Length; i++)
			{
				if (this.IKComponents[i].fixTransforms)
				{
					this.IKComponents[i].GetIKSolver().FixTransforms();
				}
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0003ECB9 File Offset: 0x0003CEB9
		public IKExecutionOrder()
		{
		}

		// Token: 0x04000769 RID: 1897
		[Tooltip("The IK components, assign in the order in which you wish to update them.")]
		public IK[] IKComponents;

		// Token: 0x0400076A RID: 1898
		[Tooltip("Optional. Assign it if you are using 'Animate Physics' as the Update Mode.")]
		public Animator animator;

		// Token: 0x0400076B RID: 1899
		private bool fixedFrame;
	}
}
