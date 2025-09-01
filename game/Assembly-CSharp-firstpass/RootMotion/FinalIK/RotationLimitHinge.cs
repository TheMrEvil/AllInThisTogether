using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x0200010C RID: 268
	[HelpURL("http://www.root-motion.com/finalikdox/html/page14.html")]
	[AddComponentMenu("Scripts/RootMotion.FinalIK/Rotation Limits/Rotation Limit Hinge")]
	public class RotationLimitHinge : RotationLimit
	{
		// Token: 0x06000BFA RID: 3066 RVA: 0x000504DD File Offset: 0x0004E6DD
		[ContextMenu("User Manual")]
		private void OpenUserManual()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/page14.html");
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x000504E9 File Offset: 0x0004E6E9
		[ContextMenu("Scrpt Reference")]
		private void OpenScriptReference()
		{
			Application.OpenURL("http://www.root-motion.com/finalikdox/html/class_root_motion_1_1_final_i_k_1_1_rotation_limit_hinge.html");
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x000504F5 File Offset: 0x0004E6F5
		[ContextMenu("Support Group")]
		private void SupportGroup()
		{
			Application.OpenURL("https://groups.google.com/forum/#!forum/final-ik");
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00050501 File Offset: 0x0004E701
		[ContextMenu("Asset Store Thread")]
		private void ASThread()
		{
			Application.OpenURL("http://forum.unity3d.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/");
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0005050D File Offset: 0x0004E70D
		protected override Quaternion LimitRotation(Quaternion rotation)
		{
			return this.LimitHinge(rotation);
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00050518 File Offset: 0x0004E718
		private Quaternion LimitHinge(Quaternion rotation)
		{
			if (this.min == 0f && this.max == 0f && this.useLimits)
			{
				return Quaternion.AngleAxis(0f, this.axis);
			}
			Quaternion quaternion = RotationLimit.Limit1DOF(rotation, this.axis);
			if (!this.useLimits)
			{
				return quaternion;
			}
			Vector3 vector = Quaternion.Inverse(Quaternion.AngleAxis(this.lastAngle, this.axis) * Quaternion.LookRotation(base.secondaryAxis, this.axis)) * quaternion * base.secondaryAxis;
			float num = Mathf.Atan2(vector.x, vector.z) * 57.29578f;
			this.lastAngle = Mathf.Clamp(this.lastAngle + num, this.min, this.max);
			return Quaternion.AngleAxis(this.lastAngle, this.axis);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x000505F5 File Offset: 0x0004E7F5
		public RotationLimitHinge()
		{
		}

		// Token: 0x0400094E RID: 2382
		public bool useLimits = true;

		// Token: 0x0400094F RID: 2383
		public float min = -45f;

		// Token: 0x04000950 RID: 2384
		public float max = 90f;

		// Token: 0x04000951 RID: 2385
		[HideInInspector]
		public float zeroAxisDisplayOffset;

		// Token: 0x04000952 RID: 2386
		private float lastAngle;
	}
}
