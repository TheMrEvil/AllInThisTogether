using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000157 RID: 343
	public class VRIKPlatform : MonoBehaviour
	{
		// Token: 0x06000D57 RID: 3415 RVA: 0x0005A1C8 File Offset: 0x000583C8
		private void OnEnable()
		{
			this.lastPosition = base.transform.position;
			this.lastRotation = base.transform.rotation;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0005A1EC File Offset: 0x000583EC
		private void LateUpdate()
		{
			this.ik.solver.AddPlatformMotion(base.transform.position - this.lastPosition, base.transform.rotation * Quaternion.Inverse(this.lastRotation), base.transform.position);
			this.lastRotation = base.transform.rotation;
			this.lastPosition = base.transform.position;
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0005A267 File Offset: 0x00058467
		public VRIKPlatform()
		{
		}

		// Token: 0x04000B15 RID: 2837
		public VRIK ik;

		// Token: 0x04000B16 RID: 2838
		private Vector3 lastPosition;

		// Token: 0x04000B17 RID: 2839
		private Quaternion lastRotation = Quaternion.identity;
	}
}
