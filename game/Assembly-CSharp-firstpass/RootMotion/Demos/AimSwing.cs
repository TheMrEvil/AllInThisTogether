using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000125 RID: 293
	public class AimSwing : MonoBehaviour
	{
		// Token: 0x06000C91 RID: 3217 RVA: 0x00054E88 File Offset: 0x00053088
		private void LateUpdate()
		{
			this.ik.solver.axis = this.ik.solver.transform.InverseTransformVector(this.ik.transform.rotation * this.animatedSwingDirection);
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00054ED5 File Offset: 0x000530D5
		public AimSwing()
		{
		}

		// Token: 0x040009D5 RID: 2517
		public AimIK ik;

		// Token: 0x040009D6 RID: 2518
		[Tooltip("The direction of the animated weapon swing in character space. Tweak this value to adjust the aiming.")]
		public Vector3 animatedSwingDirection = Vector3.forward;
	}
}
