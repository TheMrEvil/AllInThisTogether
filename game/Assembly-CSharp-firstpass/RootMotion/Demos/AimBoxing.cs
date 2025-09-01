using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000124 RID: 292
	public class AimBoxing : MonoBehaviour
	{
		// Token: 0x06000C8F RID: 3215 RVA: 0x00054E43 File Offset: 0x00053043
		private void LateUpdate()
		{
			this.aimIK.solver.transform.LookAt(this.pin.position);
			this.aimIK.solver.IKPosition = base.transform.position;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00054E80 File Offset: 0x00053080
		public AimBoxing()
		{
		}

		// Token: 0x040009D3 RID: 2515
		public AimIK aimIK;

		// Token: 0x040009D4 RID: 2516
		public Transform pin;
	}
}
