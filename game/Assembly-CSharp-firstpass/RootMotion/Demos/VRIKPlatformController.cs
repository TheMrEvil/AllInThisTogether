using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000158 RID: 344
	public class VRIKPlatformController : MonoBehaviour
	{
		// Token: 0x06000D5A RID: 3418 RVA: 0x0005A27C File Offset: 0x0005847C
		private void LateUpdate()
		{
			if (this.platform != this.lastPlatform)
			{
				if (this.platform != null)
				{
					if (this.moveToPlatform)
					{
						this.lastPosition = this.ik.transform.position;
						this.lastRotation = this.ik.transform.rotation;
						this.ik.transform.position = this.platform.position;
						this.ik.transform.rotation = this.platform.rotation;
						this.trackingSpace.position = this.platform.position;
						this.trackingSpace.rotation = this.platform.rotation;
						this.ik.solver.AddPlatformMotion(this.platform.position - this.lastPosition, this.platform.rotation * Quaternion.Inverse(this.lastRotation), this.platform.position);
					}
					this.lastPosition = this.platform.position;
					this.lastRotation = this.platform.rotation;
				}
				this.ik.transform.parent = this.platform;
				this.trackingSpace.parent = this.platform;
				this.lastPlatform = this.platform;
			}
			if (this.platform != null)
			{
				this.ik.solver.AddPlatformMotion(this.platform.position - this.lastPosition, this.platform.rotation * Quaternion.Inverse(this.lastRotation), this.platform.position);
				this.lastRotation = this.platform.rotation;
				this.lastPosition = this.platform.position;
			}
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0005A466 File Offset: 0x00058666
		public VRIKPlatformController()
		{
		}

		// Token: 0x04000B18 RID: 2840
		public VRIK ik;

		// Token: 0x04000B19 RID: 2841
		public Transform trackingSpace;

		// Token: 0x04000B1A RID: 2842
		public Transform platform;

		// Token: 0x04000B1B RID: 2843
		public bool moveToPlatform = true;

		// Token: 0x04000B1C RID: 2844
		private Transform lastPlatform;

		// Token: 0x04000B1D RID: 2845
		private Vector3 lastPosition;

		// Token: 0x04000B1E RID: 2846
		private Quaternion lastRotation = Quaternion.identity;
	}
}
